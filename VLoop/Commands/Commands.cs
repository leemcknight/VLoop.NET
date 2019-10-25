/******************************************************************************
 * 
 * Afni.Applications.VLoop.Commands
 * 
 * Common actions are wrapped up and exposed as objects.  Each action object
 * controls the menu item associated with it, the toolbar item associated with
 * it, and the event handlers for both.  By having a single object for each
 * action, we can minimize code duplication by having all actions go through
 * these objects (i.e. we don't need 2 seperate functions to call for the 
 * tool bar and menuitem clicks.).  This also makes it easier to swap different
 * menu controls and toolbar controls into and out of the application in the
 * future if we need to.  Also, entire actions can be enabled/disabled in the
 * app with a single line of code (e.g. if a certain group of people shouldn't 
 * be able to delete items, it's a simple matter of not creating the "delete" 
 * action, and all toolbars, menuitems, etc.. associated with that action will
 * be gone.)  And finally, it makes it easy to execute the same action in different
 * parts of the application.
 * 
 * ***************************************************************************/
using System;
using Afni.Applications.VLoop;
using Afni.Applications.VLoop.Viewing;
using Afni.Applications.VLoop.States;
using Afni.FormData;
using System.Windows.Forms;
using System.Drawing;

namespace Afni.Applications.VLoop.Commands
{
	#region Command
	public abstract class VLoopCommand
	{
		protected Afni.Applications.VLoop.Application _app;
		protected string _name;
		protected Viewing.View _view;
		protected Icon _icon;
		protected ToolBarButton _tb_item;
		protected MenuItem _menu_cmd;
		protected bool _enabled;

		public Afni.Applications.VLoop.Application AppObject
		{
			get { return _app; }
			set 
			{
				_app = value; 
				_view.AppObject = _app;
			}		
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public virtual bool Execute()
		{
			return true;	
		}

		public Viewing.View View
		{
			get { return _view; }
		}

		public bool Enabled
		{
			get { return _enabled; }
			set 
			{
				
				_enabled = value;
				if(_tb_item != null)
					_tb_item.Enabled = _enabled;
				if(_menu_cmd != null)
					_menu_cmd.Enabled = _enabled;
			}
		}

	}
	#endregion

	#region Search Action
	public class SearchCmd : VLoopCommand
	{
		public SearchCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_view = (Viewing.View)_app.Views[ViewTypes.Search];
			Afni.Applications.VLoop.SearchCtl ctl =
				new Afni.Applications.VLoop.SearchCtl(_app);
		
			_icon = VLoopIcons.Search;
			_tb_item = new ToolBarButton();
			_tb_item.Tag = this;
			_tb_item.Enabled = true;
			_tb_item.ImageIndex = (int)ActionIcons.Find;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Text = "Se&arch";
			_tb_item.ToolTipText = "Search for customers";
			_tb_item.Visible = true;

			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.Tools,
													_tb_item,
													"Se&arch",
													Shortcut.CtrlF,
													_icon);
		}

		public override bool Execute()
		{
			_app.LoadView(_view);
			return true;
		}
	}
	#endregion

	#region Login Action
	public class LoginCmd : VLoopCommand
	{
		private frmLogin _form;
		public LoginCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_form = new frmLogin(_app);

			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.System,
													null,
													"Lo&gin as a different user",
													Shortcut.CtrlShiftG,
													null);
		}

		public override bool Execute()
		{
			try
			{
				_app.SwitchAppState(ApplicationStates.LogIn);
				_form.ShowDialog(_app.ParentForm);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
	#endregion

	#region Change Campaign Action
	public class ChangeCampaignCmd : VLoopCommand
	{
		public ChangeCampaignCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		
			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.System,
													null,
													"C&hange Campaign",
													Shortcut.CtrlShiftC,
													null);
		}

		protected void InitializeComponent()
		{
			_view = (Viewing.View)_app.Views[ViewTypes.Campaign];
		}
	
		public override bool Execute()
		{
			_app.LoadView(_view);
			_view.ViewedForm.Refresh();
			return true;
		} 
	}
	#endregion

	#region Queue Setup Action
	public class QueueSetupCmd : VLoopCommand
	{
		public QueueSetupCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.System,
													null,
													"&Modify Call Queue",
													Shortcut.None,
													null);
		}

		public override bool Execute()
		{
			_app.SwitchAppState(ApplicationStates.CallSetup);
			_app.LoadView(ViewTypes.CallQueueSetup);
			return true;
		}
	}
	#endregion

	#region WorkModeSetup Action
	public class WorkModeCmd : VLoopCommand
	{
		public WorkModeCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.System,
													null,
													"Change &Work Mode",
													Shortcut.None,
													null);
		}

		public override bool Execute()
		{
			_app.SwitchAppState(ApplicationStates.CallSetup);
			_app.LoadView(ViewTypes.WorkMode);
			return true;
		}
	}
	#endregion

	#region End Call Action
	public class EndCallCmd : VLoopCommand
	{
		public EndCallCmd()
		{
		}

		public EndCallCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
			_tb_item.Tag = this;
			_tb_item.Enabled = true;
			_tb_item.ImageIndex = (int)ActionIcons.End;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Text = "E&nd Call";
			_tb_item.ToolTipText = "End the current call";
			_tb_item.Visible = true;
			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.Call,
													_tb_item,
													"E&nd Call",
													Shortcut.None,
													_icon);
		}

		protected void InitializeComponent()
		{
			_tb_item = new ToolBarButton();
			_view = (Viewing.View)_app.Views[ViewTypes.Disposition];
			_icon = VLoopIcons.EndCall;
		}
	
		public override bool Execute()
		{
			_app.SwitchAppState(ApplicationStates.Disposition);
			_app.LoadView(_view);
			_view.ViewedForm.Refresh();
			return true;
		} 
	}
	#endregion

	#region Next Call Action
	public class NextCallCmd : VLoopCommand
	{
		public NextCallCmd(Afni.Applications.VLoop.Application app) 
		{
			_app = app;
			InitializeComponent();
			_tb_item = new ToolBarButton();
			_tb_item.Tag = this;
			_tb_item.Enabled = true;
			_tb_item.ImageIndex = (int)ActionIcons.Next;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Text = "Nex&t Call";
			_tb_item.ToolTipText = "Move to the next call in the queue";
			_tb_item.Visible = true;
			
			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.Call,
													_tb_item,
													"Nex&t Call",
													Shortcut.None,
													_icon);
		}

		protected void InitializeComponent()
		{
			_tb_item = new ToolBarButton();
			_view = (Viewing.View)_app.Views[ViewTypes.Disposition];
			_icon = VLoopIcons.Next;
		}
	
		public bool Execute()
		{
			_app.SwitchAppState(ApplicationStates.Disposition);
			_app.LoadView(_view);
			_view.ViewedForm.Refresh();
			return true;
		} 

	}
	#endregion

	#region Add new item Action
	public class AddNewItemCmd : VLoopCommand
	{
		public AddNewItemCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_icon = VLoopIcons.NewItem;
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.ImageIndex = (int)ActionIcons.New;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Tag = this;
			_tb_item.Text = null;
			_tb_item.ToolTipText = null;
			_tb_item.Visible = true;

			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.File,
													_tb_item,
													"&New",
													Shortcut.CtrlN,
													_icon);
		}

		public override bool Execute()
		{
			/* the view is already loaded at this point
			 * so just call AddNew() on the interface */
			((IAddable)_app.ActiveForm).AddNew();
			return true;
		}
	}
	#endregion

	#region Edit item Action
	public class EditItemCmd : VLoopCommand
	{
		public EditItemCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_icon = VLoopIcons.Edit;
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.Tag = this;
			_tb_item.ImageIndex = (int)ActionIcons.Edit;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.ToolTipText = "Edit Item";
			_tb_item.Visible = true;

			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.Edit,
													_tb_item,
													"E&dit",
													Shortcut.None,
													_icon);
		}

		public override bool Execute()
		{
			/* view already loaded; just need to edit... */
			((IEditable)_app.ActiveForm).Edit();
			return true;
		}
	}
	#endregion

	#region Undo Action
	public class UndoCmd : VLoopCommand
	{
		public UndoCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_icon = VLoopIcons.Undo;
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.Tag = this;
			_tb_item.ImageIndex = (int)ActionIcons.Undo;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.ToolTipText = "Undo last action";
			_tb_item.Visible = true;
		
			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.Edit,
													_tb_item,
													"&Undo",
													Shortcut.CtrlZ,
													_icon);
		}

		public override bool Execute()
		{
			return ((ISaveable)_app.ActiveForm).Undo();
		}
	}
	#endregion

	#region Delete item Action
	public class DeleteItemCmd : VLoopCommand
	{
		public DeleteItemCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_icon = VLoopIcons.Delete;
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.Tag = this;
			_tb_item.ImageIndex = (int)ActionIcons.Delete;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Text = null;
			_tb_item.ToolTipText = "Delete the current item";
			_tb_item.Visible = true;
			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.Edit,
													_tb_item,
													"&Delete",
													Shortcut.Del,
													_icon);
		}

		public override bool Execute()
		{
			return ((IDeletable)_app.ActiveForm).Delete();
		}
	}
	#endregion

	#region Help Action
	public class HelpCmd : VLoopCommand
	{
		public HelpCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_icon = VLoopIcons.Help;
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.ImageIndex = (int)ActionIcons.Help;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Text = null;
			_tb_item.Tag = this;
			_tb_item.ToolTipText = "VLoop Help";
			_tb_item.Visible = true;

			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.Help,
													_tb_item,
													"VLoop &Help",
													Shortcut.CtrlShiftF1,
													_icon);
		}
		
		public override bool Execute()
		{
			return true;
		}
	}
	#endregion

	#region Dynamic Help Action
	public class DynamicHelpCmd : VLoopCommand
	{
		public DynamicHelpCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.Help,
													null,
													"D&ynamic Help",
													Shortcut.F1,
													null);
		}

		public override bool Execute()
		{
			IForm form;
			bool exec_ok;
			form = _app.ActiveForm as IForm;

			if(form != null)
				exec_ok = form.ShowHelp();
			else
				exec_ok = false;
			return exec_ok;
		}
	}
	#endregion

	#region Exit App Action
	public class ExitAppCmd : VLoopCommand
	{
		public ExitAppCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.File,
													null,
													"E&xit",
													Shortcut.None,
													null);
		}

		public override bool Execute()
		{
			DialogResult dr;
			dr = MessageBox.Show("Are you sure you want to exit VLoop?",
							"VLoop message",
							MessageBoxButtons.YesNo, 
							MessageBoxIcon.Question);
			if(dr == DialogResult.Yes)
			{
				_app.Close();
				return true;
			}
			else
				return false;
			
		}
	}
	#endregion

	#region Save Action
	public class SaveCmd : VLoopCommand
	{
		public SaveCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_icon = VLoopIcons.Save; 
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.Tag = this;
			_tb_item.ImageIndex = (int)ActionIcons.Save;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Text = null;
			_tb_item.ToolTipText = "Save the current item";
			_tb_item.Visible = true;
			
			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.File,
													_tb_item,
													"&Save",
													Shortcut.CtrlS,
													_icon);
		}

		public override bool Execute()
		{
			return ((ISaveable)_app.ActiveForm).Save();
		}
	}
	#endregion

	#region Toggle Scripts Action
	public class ToggleScriptsCmd : VLoopCommand
	{
		public ToggleScriptsCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.Tag =this;
			_tb_item.ImageIndex = (int)ActionIcons.Scripts;
			_tb_item.Style = ToolBarButtonStyle.ToggleButton;
			_tb_item.Text = "Sc&ripts";
			_tb_item.ToolTipText = "Show the sripts window";
			_tb_item.Visible = true;
		
			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.View,
													_tb_item,
													"Sc&ripts",
													Shortcut.CtrlShiftS,
													null);
		}

		public override bool Execute()
		{
			_menu_cmd.Checked = !_menu_cmd.Checked;
			_app.ParentForm.ToggleScripts();
			return true;
		}

	}
	#endregion

	#region Options Action
	public class OptionsCmd : VLoopCommand
	{
		public OptionsCmd(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = _app.MenuManager.AddMenuItem(this,
													VLoopMenus.Tools,
													null,
													"&Options...",
													Shortcut.None,
													null);
		}

		public override bool Execute()
		{
			frmOpt opt = new frmOpt(_app);
			opt.ShowDialog(_app.ParentForm);
			return true;
		}

		private void MenuClicked(object sender, System.EventArgs e)
		{
			Execute();
		}
	}
	#endregion

	public class ActionHelper
	{
		public static void DisableAction(ActionTypes ActionType, Afni.Applications.VLoop.Application App)
		{
			VLoopCommand cmd;
			cmd = (VLoopCommand)App.Commands[ActionType];
			cmd.Enabled = false;
		}

		public static void EnableAction(ActionTypes ActionType, Afni.Applications.VLoop.Application App)
		{
			VLoopCommand cmd;
			cmd = (VLoopCommand)App.Commands[ActionType];
			cmd.Enabled = true;
		}
	}

	#region Action Icons
	public enum ActionIcons
	{
		New = 0,
		Save = 1,
		Edit = 2,
		Undo = 3,
		Redo = 4,
		Cut = 5,
		Copy = 6,
		Paste = 7,
		Delete = 8,
		Scripts = 9,
		End = 10,
		Next = 11,
		Find = 12,
		DynHelp = 13,
		Help = 14,
		PrevView = 15,
		NextView = 16,
		Queue = 17
	}
	#endregion

	public enum ActionTypes
	{
		New,
		Save,
		Edit,
		Undo,
		Cut,
		Copy,
		Paste,
		Delete,
		Scripts,
		CampaignSwitch,
		End,
		Next,
		Find,
		Options,
		DynHelp,
		Help,
		PrevView,
		NextView,
		Queue,
		WorkMode,
		Login,
		Exit
	}
}

