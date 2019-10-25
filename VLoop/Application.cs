using System;
using System.Collections;
using Afni.FormData;
using Afni.Applications.VLoop.Viewing;
using Afni.Applications.VLoop.Commands;
using Afni.Applications.VLoop.States;
using System.Windows.Forms;
using Afni.Controls;
using Microsoft.Win32;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopBusinessObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;
using Afni.DataUtility;
using System.Drawing;
using System.IO;
using System.Reflection;
using Afni.ControlPlacer.Questions;

namespace Afni.Applications.VLoop
{
	public class Startup
	{
		[STAThread]
		public static void Main()
		{
			Afni.Applications.VLoop.Application app 
				= new Afni.Applications.VLoop.Application();
			app.Start();
		}
	}

	/// <summary>
	/// Represents the main Application object for VLoop.  The 
	/// application object is responsible for maintaining the 
	/// views, the helper objects, and the error handling.
	/// </summary>
	public class Application
	{
		private Hashtable _commands;
		private Hashtable _views;
		private Hashtable _states;
		private Stack _forward_viewstack;
		private Stack _back_viewstack;
		private IForm _activeform;
		private frmMain _parent_form;
		private ApplicationStates _state;
		private CallStatus _call;
		private ArrayList _errors;
		private System.Exception _last_error;
		private Viewing.View _view;
		private MenuManager _menu_manager;
		private ApplicationState _app_state;
		private Campaign _campaign;
		private DisplayTheme _theme;
		private VLoopUser _user;
		private QueueManager _queue_mgr;
		private WorkModes _work_mode;
		private ArrayList _explorers;
		private ArrayList _prodTypes;
		private IViewExplorer _activeExplorer;

		public event EventHandler CampaignSwitched;
		public event EventHandler ThemeChanged;
		public event EventHandler WorkModeChanged;

		public Application()
		{

		}

	
		/// <summary>
		/// Starts VLoop.  
		/// </summary>
		public void Start()
		{
			SplashScreen splash = new SplashScreen();
			splash.Show();
			splash.Invalidate();
			splash.Update();
			_work_mode = WorkModes.None;
			_errors = new ArrayList();
			_forward_viewstack = new Stack();
			_back_viewstack = new Stack();
			_parent_form = new frmMain();
			_parent_form.AppObject = this;

			//manager objects
			_menu_manager = new MenuManager(this);
			_menu_manager.CreateMenu();
			_call = new CallStatus(this);
			
			CreateExplorers();
			CreateViews();
			CreateCommands();
			CreateStates();
			ApplyRegistrySettings();
			
			_parent_form.Show();
			splash.Hide();
			((VLoopCommand)_commands[ActionTypes.Login]).Execute();
			System.Windows.Forms.Application.Run(_parent_form);
		}

		public MenuManager MenuManager
		{
			get { return _menu_manager; }
		}

		/// <summary>
		/// Gets the Queue Manager object
		/// </summary>
		public QueueManager QueueManager
		{
			get { return _queue_mgr; }
		}


		public ArrayList Explorers
		{
			get { return _explorers; }
		}

		/// <summary>
		/// Returns an arraylist of product types in VLoop.  Since this
		/// data is static, used often, and contains relatively small amounts
		/// of data, it is cached in the application object to cut down on 
		/// trips to the database.
		/// </summary>
		public ArrayList ProductTypes
		{
			get 
			{
				if(_prodTypes == null)
				{
					IManager mgr = new ProdTypeBSO();
					_prodTypes = (ArrayList)mgr.GetAll();
				}
				return _prodTypes;
			}
		}

		/// <summary>
		/// Gets or sets the workmode for the application.  Changing the workmode
		/// fires a WorkModeChanged event.
		/// </summary>
		public WorkModes WorkMode
		{
			get { return _work_mode; }
			set
			{
				if(_work_mode != value)
				{
					_work_mode = value;
					switch(_work_mode)
					{
						case WorkModes.CallQueue:
							_queue_mgr = new CallQueueManager(this);
							((VLoopCommand)_commands[ActionTypes.Queue]).Execute();
							break;
						case WorkModes.Dialer:
							_queue_mgr = null;
							break;
						case WorkModes.Manual:
							((VLoopCommand)_commands[ActionTypes.Find]).Execute();
							_queue_mgr = null;
							break;
						case WorkModes.OrderQueue:
							_queue_mgr = new OrderQueueManager(this);
							break;
					}

					if(WorkModeChanged != null)
						WorkModeChanged(this,null);
				}
			}
		}

		/// <summary>
		/// Gets or sets the current campaign data object.  When changed, this fires 
		/// the CampaignSwitched event.
		/// </summary>
		public Campaign CurrentCampaign
		{
			get { return _campaign; }
			set 
			{ 
				if(_campaign != value)
				{
					_campaign = value; 
					RefreshViews();
					if(CampaignSwitched != null)
					{
						CampaignSwitched(this,null);
						((VLoopCommand)_commands[ActionTypes.WorkMode]).Execute();
					}
				}
			}
		}

		/// <summary>
		/// Creates the IViewExplorer objects
		/// and adds them to the _explorers collection
		/// </summary>
		private void CreateExplorers()
		{
			_explorers = new ArrayList();
			_activeExplorer = new XPBarViewExplorer(this,_parent_form.ExplorerSplitter);
			_explorers.Add(new FolderViewExplorer(this,_parent_form.ExplorerSplitter) );
			_explorers.Add(_activeExplorer);
			_activeExplorer.Show();
		}

		/// <summary>
		/// Creates the non campaign-driven actions
		/// </summary>
		private void CreateCommands()
		{
			_commands = new Hashtable();
			_commands.Add(ActionTypes.Login, new LoginCmd(this));
			_commands.Add(ActionTypes.New, new AddNewItemCmd(this));
			_commands.Add(ActionTypes.Save, new SaveCmd(this));
			_commands.Add(ActionTypes.Exit, new ExitAppCmd(this));
			_commands.Add(ActionTypes.Edit, new EditItemCmd(this));
			_commands.Add(ActionTypes.Undo, new UndoCmd(this));
			_commands.Add(ActionTypes.Delete, new DeleteItemCmd(this));
			_commands.Add(ActionTypes.Scripts, new ToggleScriptsCmd(this));
			_commands.Add(ActionTypes.Find, new SearchCmd(this));
			_commands.Add(ActionTypes.Options, new OptionsCmd(this));
			_commands.Add(ActionTypes.CampaignSwitch, new ChangeCampaignCmd(this));
			_commands.Add(ActionTypes.End,new EndCallCmd(this));
			_commands.Add(ActionTypes.Next, new NextCallCmd(this));
			_commands.Add(ActionTypes.Queue, new QueueSetupCmd(this));
			_commands.Add(ActionTypes.WorkMode, new WorkModeCmd(this));
			_commands.Add(ActionTypes.DynHelp, new DynamicHelpCmd(this));
			_commands.Add(ActionTypes.Help, new HelpCmd(this));
		}

		/// <summary>
		/// Refreshes the campaign driven views.
		/// </summary>
		private void RefreshViews()
		{
			ApplicationSetting setting;
			Hashtable settings;

			settings = _campaign.AppSettings;

			//clear out the campaign-driven views
			SafeViewRemove(ViewTypes.Account);
			SafeViewRemove(ViewTypes.AddWTN);
			SafeViewRemove(ViewTypes.CallingCard);
			SafeViewRemove(ViewTypes.NewPlanFinal);
			SafeViewRemove(ViewTypes.NewPlanSelect);
			SafeViewRemove(ViewTypes.NewPlanStep1);
			SafeViewRemove(ViewTypes.TollFree);


			//add back in the any campaign driven views
			setting = (ApplicationSetting)settings[ApplicationSettings.ShowAccount];

			if((setting != null) && (setting.Value.ToUpper() == "YES"))
			{
				_views.Add(ViewTypes.Account, new AccountView(this));
				_views.Add(ViewTypes.NewPlanStep1, new NewPlanWizard1View(this));
				_views.Add(ViewTypes.TollFree, new TollFreeView(this));
				_views.Add(ViewTypes.CallingCard, new CallingCardView(this));
				_views.Add(ViewTypes.NewPlanSelect, new SelectPlanView(this));
				_views.Add(ViewTypes.NewPlanFinal, new NewPlanConfView(this));
				_views.Add(ViewTypes.AddWTN, new AddWTNView(this));
			}

			setting = (ApplicationSetting)settings[ApplicationSettings.ShowOrders];
			if((setting != null) && (setting.Value.ToUpper() == "YES"))
			{
				_views.Add(ViewTypes.Orders, new OrderView(this));
			}
																														 
		}

		private void SafeViewRemove(ViewTypes type)
		{
			if(_views.Contains(type))
				_views.Remove(type);
		}

		/// <summary>
		/// Creates the basic views that are non-campaign driven.
		/// </summary>
		private void CreateViews()
		{
			_views = new Hashtable();	

			_views.Add(ViewTypes.Home, new HomeView(this));
			_views.Add(ViewTypes.CallQueueSetup, new CallQueueView(this));
			_views.Add(ViewTypes.Customer, new CustomerView(this));
			_views.Add(ViewTypes.Campaign, new CampaignView(this));
			_views.Add(ViewTypes.Disposition, new DispositionView(this));
			_views.Add(ViewTypes.JobAids, new JobAidsView(this));
			_views.Add(ViewTypes.CallHistory, new CallHistoryView(this));
			_views.Add(ViewTypes.WorkMode, new WorkModeView(this));
			_views.Add(ViewTypes.Search, new SearchView(this));
		}

		private void CreateStates()
		{
			_states = new Hashtable();
			_states.Add(ApplicationStates.CallActive, new CallActiveState(this));
			_states.Add(ApplicationStates.Disposition, new DispositionState(this));
			_states.Add(ApplicationStates.Idle, new IdleState(this));
			_states.Add(ApplicationStates.LogIn, new LoggingInState(this));
			_states.Add(ApplicationStates.CallSetup, new CallSetupState(this));
		}

		/// <summary>
		/// Reads the VLoop registry settings from the registry and applies
		/// them.
		/// </summary>
		private void ApplyRegistrySettings()
		{
			string theme;
			RegistryKey key;
			
			key = Registry.LocalMachine.CreateSubKey("software\\Afni\\VLoop");
			theme = key.GetValue("Theme").ToString();
			key.Close();
			ApplyTheme(ThemeManager.ThemeByName(theme,this));
		}

		public void SetBusy()
		{
			_parent_form.VLStatusBar.Panels[0].Text = "VLoop working.  Please wait...";
			_parent_form.Cursor = Cursors.WaitCursor;
		}

		public void SetBusy(string message)
		{
			_parent_form.VLStatusBar.Panels[0].Text = message;
			_parent_form.Cursor = Cursors.WaitCursor;
		}

		public void SetIdle()
		{
			_parent_form.VLStatusBar.Panels[0].Text = "Ready...";
			_parent_form.Cursor = Cursors.Default;
		}

		/// <summary>
		/// Applies the theme to the Application.  This call will fire the 
		/// ThemeChanged event.
		/// </summary>
		/// <param name="theme"></param>
		public void ApplyTheme(DisplayTheme theme)
		{
			//title bar
			_parent_form.VLoopTitleBar.LeftColor = theme.TitleGradientLeftColor;
			_parent_form.VLoopTitleBar.RightColor = theme.TitleGradientRightColor;
			
			_theme = theme;

			if(ThemeChanged != null)
				ThemeChanged(this,null);
		}

		/// <summary>
		/// Switches the application state to the state provided.
		/// </summary>
		/// <param name="StateType"></param>
		public void SwitchAppState(ApplicationStates StateType)
		{
			try
			{
				ApplicationState state;
				state = (ApplicationState)_states[StateType];
				if(state != _app_state)
				{
					state.SetActiveState();
					_app_state = state;
				}
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("There was an error switching application states.",
								"VLoop Error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				_errors.Add(ex);
				_last_error = ex;
			}
		}

		public DisplayTheme Theme
		{
			get { return _theme; }
		}

		public ApplicationState CurrentState
		{
			get { return _app_state; }
		}

		public Hashtable Commands
		{
			get { return _commands; }
		}

		public Hashtable Views
		{
			get { return _views; }
		}

		public CallStatus Call
		{
			get { return _call; }
		}

		public System.Exception LastError
		{
			get { return _last_error; }
		}

		public bool IsOnXP
		{
			get 
			{
				Version ver;

				ver = Environment.OSVersion.Version;

				if (ver.Major >= 5 && ver.Minor >= 0)
					return true;
				else if(ver.Major >= 6)
					return true;
				else
					return false;
			}
		}

		/// <summary>
		/// returns the active form
		/// </summary>
		public IForm ActiveForm
		{
			get { return _activeform; }
			set { _activeform = value; }
		}

		/// <summary>
		/// Gets a reference to the current parent form.
		/// </summary>
		public Afni.Applications.VLoop.frmMain ParentForm
		{
			get { return _parent_form; }
		}

		/// <summary>
		/// Closes VLoop
		/// </summary>
		public void Close()
		{
			RegistryKey key;
			//update the registry
			key = Registry.LocalMachine.CreateSubKey("software\\Afni\\VLoop");
			key.SetValue("Theme",_theme.ID);
			key.Close();
			System.Windows.Forms.Application.Exit();
		}
		
		/// <summary>
		/// Gets the VLoop user object
		/// for the user currently signed
		/// in
		/// </summary>
		public VLoopUser User
		{
			get {
				if(_user == null)
					_user = new VLoopUser();
				return _user; 
			}
		}

		/// <summary>
		/// Loads the view contained in the viewitem and 
		/// brings up the record associated with it.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool LoadViewItem(ViewItem item)
		{
			bool load_ok = true;
			try 
			{
				if(_view != null && _view != item.View && item.View != null)
				{ 
					 /* need to swap views.  Push current view
					 * onto the back stack, and clear the 
					 * forward stack.*/
					_view.Hide();
					_back_viewstack.Push(_view);
					_forward_viewstack.Clear();
					_parent_form.BackButton.Enabled = true;
					_parent_form.ForwardButton.Enabled = false;
				}

				if(item.View != null)
				{
					_view = item.View;
					_activeform = _view.ViewedForm;
					_view.Show(item);
				}
			}
			catch(System.Exception ex)
			{
				_last_error = ex;
				_errors.Add(ex);
				load_ok = false;
			}

			return load_ok;
		}

		/// <summary>
		/// Clears the forward and back
		/// view stacks, which will remove any
		/// view history.  Useful after dispositions.
		/// </summary>
		public void ClearViewStacks()
		{
			_forward_viewstack.Clear();
			_back_viewstack.Clear();
			_parent_form.BackButton.Enabled = false;
			_parent_form.ForwardButton.Enabled = false;
		}

		/// <summary>
		/// Loads the view with the default item
		/// onto the screen.
		/// </summary>
		/// <param name="view"></param>
		/// <returns></returns>
		public bool LoadView(Viewing.View view)
		{
			if((_view != null) && (_view != view))
			{
				_view.Hide();
				_back_viewstack.Push(_view);
				_parent_form.BackButton.Enabled = true;
				_parent_form.ForwardButton.Enabled = false;
			}

			_view = view;
			_activeform = _view.ViewedForm;
			_view.Show();
			return true;
		}

		public bool LoadView(ViewTypes Type)
		{
			Viewing.View view;

			view = (Viewing.View)_views[Type];
			return LoadView(view);
		}

		/// <summary>
		/// Moves to the next view on the 
		/// forward stack.
		/// </summary>
		/// <returns></returns>
		public bool MoveNextView()
		{
			bool next_ok = true;
			
			try
			{
				if(_forward_viewstack.Peek() != null)
				{
					/* hide current view and push it onto the back stack */
					_view.Hide();
					_back_viewstack.Push(_view);
					
					/* pop the next view off the forward stack and load it */
					_view = (Viewing.View)_forward_viewstack.Pop();
					_view.Show();

					/* if there's nothing left in the forward stack, 
					 * disable the "next" button.*/
					if(_forward_viewstack.Count == 0)
						_parent_form.ForwardButton.Enabled = false;
					else
						_parent_form.ForwardButton.Enabled = true;
					_parent_form.BackButton.Enabled =true;
					_activeform = _view.ViewedForm;
				}
				else
					next_ok = false;
			}
			catch(System.Exception ex)
			{
				_last_error = ex;
				_errors.Add(ex);
				next_ok = false;
			}

			return next_ok;
		}

		/// <summary>
		/// Moves to the previous view
		/// on the back viewstack.
		/// </summary>
		/// <returns></returns>
		public bool MovePrevView()
		{
			bool prev_ok = true;

			try
			{
				if(_back_viewstack.Peek() != null)
				{
					/* hide current view and push it onto the 
					 * forward view stack */
					_view.Hide();

					if(_view.AllowsForwardNavigation)
					{
						_forward_viewstack.Push(_view);
						_parent_form.ForwardButton.Enabled = true;
					}
					else
						_parent_form.ForwardButton.Enabled = false;

					/*pop previous view off the back stack and
					 * show it */
					_view = (Viewing.View)_back_viewstack.Pop();
					_view.Show();

					/*may need to switch app states... */
					//if(_app_state == ApplicationState.Disposition)
					//	SwitchAppState(ApplicationStates.CallActive);
					

					/*finally, update the navigation buttons */
					if(_back_viewstack.Count == 0)
						_parent_form.BackButton.Enabled = false;
					else
						_parent_form.BackButton.Enabled = true;
					_activeform = _view.ViewedForm;
				}
				else
					prev_ok = false;

			}

			catch(System.Exception ex)
			{
				_last_error = ex;
				_errors.Add(ex);
				prev_ok = false;
			}

			return prev_ok;
		}

		public void LoadExplorer(IViewExplorer viewExplorer)
		{
			if(_activeExplorer != null)
				_activeExplorer.Hide();

			_activeExplorer = viewExplorer;
			viewExplorer.Show();
		}
	}


	public enum VLoopEntities
	{
		Customer,
		Account,
		Plan,
		WTN,
		Disposition,
	}

	public enum WorkModes
	{
		CallQueue,
		OrderQueue,
		Manual,
		Dialer,
		None
	}	

	public enum StepAfterDisposition
	{
		NextCall,
		NoCall,
		ExitApp
	}

	public class VLoopUser
	{
		private string _name;
		private string _ssn;
		private string _acd;

		public string Name
		{
			get { return _name; }
			set {_name = value; }
		}

		public string SSN
		{
			get { return _ssn; }
			set { _ssn = value; }
		}

		public string ACD
		{
			get { return _acd; }
			set { _acd = value; }
		}
	}

	public class VLoopDynControlTypes
	{
		public static QuestionTypes FromControlTypeID(long ControlTypeID)
		{
			QuestionTypes type = QuestionTypes.Text;
			switch(ControlTypeID)
			{
				case 1:
					type = QuestionTypes.Text;
					break;
				case 2:
					type = QuestionTypes.MultiLineText;
					break;
				case 3:
					type = QuestionTypes.Combo;
					break;
				case 4:
					type = QuestionTypes.MultiSelect;
					break;
				case 5:
					type = QuestionTypes.Radio;
					break;
				case 6:
					type = QuestionTypes.CheckBox;
					break;
				case 7:
					type = QuestionTypes.DateMask;
					break;
				case 8:
					type = QuestionTypes.PhoneMask;
					break;
				case 9:
					type = QuestionTypes.Text;
					break;
				case 10:
					type = QuestionTypes.Text;
					break;
			}

			return type;
		}
	}
}
