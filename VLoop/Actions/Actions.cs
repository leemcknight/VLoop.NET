/******************************************************************************
 * 
 * Afni.Applications.VLoop.Actions
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

namespace Afni.Applications.VLoop.Actions
{
	#region Action
	public abstract class Action
	{
		protected Afni.Applications.VLoop.Application _app;
		protected string _name;
		protected Viewing.View _view;
		protected ActionIcons _icon;
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

		public ActionIcons ActionIcon
		{
			get { return _icon; }
			set { _icon = value; }
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
	public class SearchAction : Action
	{
		public SearchAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_view = new Viewing.View(_app);
			_view.ViewName = "Customer Search";
			_view.ViewType = ViewTypes.Search;
			Afni.Applications.VLoop.SearchCtl ctl =
				new Afni.Applications.VLoop.SearchCtl();
			_view.ViewedForm = (IForm)ctl;
			_menu_cmd = new MenuItem();
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.Find;
			_menu_cmd.Text = "&Find";
			_menu_cmd.Click += new System.EventHandler(this.MenuClick);
			_menu_cmd.Shortcut = Shortcut.CtrlF;
			_tb_item = new ToolBarButton();
			_tb_item.Tag = this;
			_tb_item.Enabled = true;
			_tb_item.ImageIndex = (int)ActionIcons.Find;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Text = "&Find";
			_tb_item.ToolTipText = "Search for customers";
			_tb_item.Visible = true;
			_app.MenuManager.AddMenuItem(_menu_cmd, VLoopMenus.Tools,_tb_item);
		}

		public override bool Execute()
		{
			_app.LoadView(_view);
			return true;
		}

		private void MenuClick(object sender, System.EventArgs e)
		{
			Execute();
		}
	}
	#endregion

	#region Login Action
	public class LoginAction : Action
	{
		private frmLogin _form;
		public LoginAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_form = new frmLogin();
			_menu_cmd = new MenuItem();
			_menu_cmd.Text = "Lo&gin as a different user...";
			_menu_cmd.Shortcut = Shortcut.CtrlShiftG;
			_menu_cmd.Click += new System.EventHandler(this.MenuClick);
			_app.MenuManager.AddMenuItem(_menu_cmd, VLoopMenus.System,_tb_item);
		}

		public override bool Execute()
		{
			_app.SwitchAppState(ApplicationStates.LogIn);
			_form.ShowDialog(_app.ParentForm);
			return ((Action)_app.Actions[ActionTypes.Queue]).Execute();
		}

		private void MenuClick(object sender, System.EventArgs e)
		{
			Execute();
		}
	}
	#endregion

	#region Change Campaign Action
	public class ChangeCampaignAction : Action
	{
		public ChangeCampaignAction()
		{
		}

		public ChangeCampaignAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.End;
			_menu_cmd.Text = "C&hange Campaign";
			_menu_cmd.Shortcut = Shortcut.CtrlShiftC;
			_menu_cmd.Click += new System.EventHandler(this.MenuClick);
			_app.MenuManager.AddMenuItem(_menu_cmd, VLoopMenus.System,_tb_item);
		}

		protected void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			_view = (Viewing.View)_app.Views[ViewTypes.Campaign];
		}
	
		public override bool Execute()
		{
			_app.LoadView(_view);
			_view.ViewedForm.Refresh();
			return true;
		} 

		private void MenuClick(object sender, System.EventArgs e)
		{
			Execute();
		}
	}
	#endregion

	#region Queue Setup Action
	public class QueueSetupAction : Action
	{
		public QueueSetupAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.Queue;
			_menu_cmd.Text = "&Modify Call Queue...";
			_menu_cmd.Click += new System.EventHandler(this.MenuClick);
			_app.MenuManager.AddMenuItem(_menu_cmd, VLoopMenus.System,_tb_item);
		}

		public override bool Execute()
		{
			_app.SwitchAppState(ApplicationStates.Queue);
			_app.LoadView(ViewTypes.Queue);
			return true;
		}

		private void MenuClick(object sender, System.EventArgs e)
		{
			Execute();
		}
	}
	#endregion

	#region End Call Action
	public class EndCallAction : Action
	{
		public EndCallAction()
		{
		}

		public EndCallAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.End;
			_menu_cmd.Text = "E&nd Call...";
			_menu_cmd.Click += new System.EventHandler(this.MenuClick);
			_tb_item.Tag = this;
			_tb_item.Enabled = true;
			_tb_item.ImageIndex = (int)ActionIcons.End;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Text = "E&nd Call...";
			_tb_item.ToolTipText = "End the current call";
			_tb_item.Visible = true;
			_app.MenuManager.AddMenuItem(_menu_cmd, VLoopMenus.Call,_tb_item);
		}

		protected void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			_tb_item = new ToolBarButton();
			_view = (Viewing.View)_app.Views[ViewTypes.Disposition];
		}
	
		public override bool Execute()
		{
			_app.SwitchAppState(ApplicationStates.Disposition);
			_app.LoadView(_view);
			_view.ViewedForm.Refresh();
			return true;
		} 

		private void MenuClick(object sender, System.EventArgs e)
		{
			Execute();
		}
	}
	#endregion

	#region Next Call Action
	public class NextCallAction : EndCallAction
	{
		public NextCallAction(Afni.Applications.VLoop.Application app) 
		{
			_app = app;
			base.InitializeComponent();
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.Next;
			_menu_cmd.Text = "Nex&t Call...";
			_menu_cmd.Click += new System.EventHandler(this.MenuClick);
			_tb_item = new ToolBarButton();
			_tb_item.Tag = this;
			_tb_item.Enabled = true;
			_tb_item.ImageIndex = (int)ActionIcons.Next;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Text = "Nex&t Call...";
			_tb_item.ToolTipText = "Move to the next call in the queue";
			_tb_item.Visible = true;
			_app.MenuManager.AddMenuItem(_menu_cmd, VLoopMenus.Call,_tb_item);
		}

		private void MenuClick(object sender, System.EventArgs e)
		{
			Execute();
		}
	}
	#endregion

	#region Add new item Action
	public class AddNewItemAction : Action
	{
		public AddNewItemAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.New;
			_menu_cmd.Text = "&New";
			_menu_cmd.Shortcut = Shortcut.CtrlN;
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.ImageIndex = (int)ActionIcons.New;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Tag = null;
			_tb_item.Text = null;
			_tb_item.ToolTipText = null;
			_tb_item.Visible = true;
			_app.MenuManager.AddMenuItem(_menu_cmd,VLoopMenus.File,_tb_item);
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
	public class EditItemAction : Action
	{
		public EditItemAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.Edit;
			_menu_cmd.Text = "E&dit";
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.Tag = this;
			_tb_item.ImageIndex = (int)ActionIcons.Edit;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.ToolTipText = "Edit Item";
			_tb_item.Visible = true;
			_app.MenuManager.AddMenuItem(_menu_cmd,VLoopMenus.Edit,_tb_item);
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
	public class UndoAction : Action
	{
		public UndoAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.Undo;
			_menu_cmd.Text = "&Undo";
			_menu_cmd.Shortcut = Shortcut.CtrlZ;
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.Tag = this;
			_tb_item.ImageIndex = (int)ActionIcons.Undo;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.ToolTipText = "Undo last action";
			_tb_item.Visible = true;
			_app.MenuManager.AddMenuItem(_menu_cmd,VLoopMenus.Edit,_tb_item);
		}

		public override bool Execute()
		{
			return true;
		}
	}
	#endregion

	#region Redo Action
	public class RedoAction : Action
	{
		public RedoAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.Redo;
			_menu_cmd.Text = "&Redo";
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.ImageIndex = (int)ActionIcons.Redo;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Tag = this;
			_tb_item.Text = null;
			_tb_item.ToolTipText = "Redo last action";
			_tb_item.Visible = true;
			_app.MenuManager.AddMenuItem(_menu_cmd,VLoopMenus.Edit,_tb_item);
		}

		public override bool Execute()
		{
			return true;
		}
	}
	#endregion

	#region Delete item Action
	public class DeleteItemAction : Action
	{
		public DeleteItemAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.Delete;
			_menu_cmd.Text = "&Delete";
			_menu_cmd.Click += new System.EventHandler(this.MenuClick);
			_menu_cmd.Shortcut = Shortcut.Del;
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.Tag = this;
			_tb_item.ImageIndex = (int)ActionIcons.Delete;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Text = null;
			_tb_item.ToolTipText = "Delete the current item";
			_tb_item.Visible = true;
			_app.MenuManager.AddMenuItem(_menu_cmd,VLoopMenus.Edit,_tb_item);
		}

		public override bool Execute()
		{
			/* View already loaded... */
			return ((IDeletable)_app.ActiveForm).Delete();
		}

		private void MenuClick(object sender, System.EventArgs e)
		{
			DialogResult res;
			res = MessageBox.Show("Are you sure you want to delete this item?",
							"VLoop message",
							MessageBoxButtons.YesNo,
							MessageBoxIcon.Warning);

			if(res == DialogResult.Yes)
				Execute();
		}
	}
	#endregion

	#region Help Action
	public class HelpAction : Action
	{
		public HelpAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.Help;
			_menu_cmd.Text = "VLoop &Help...";
			_menu_cmd.Shortcut = Shortcut.CtrlShiftF1;
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			//_tb_item.Click += new System.EventHandler(this.MenuClick);
			_tb_item.ImageIndex = (int)ActionIcons.Help;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Text = null;
			_tb_item.Tag = this;
			_tb_item.ToolTipText = "VLoop Help";
			_tb_item.Visible = true;
			_app.MenuManager.AddMenuItem(_menu_cmd,VLoopMenus.Help,_tb_item);
		}
		
		public override bool Execute()
		{
			return true;
		}

		private void MenuClick(object sender, System.EventArgs e)
		{
			Execute();
		}

	}
	#endregion

	#region Dynamic Help Action
	public class DynamicHelpAction : Action
	{
		public DynamicHelpAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.DynHelp;
			_menu_cmd.Text = "D&ynamic Help...";
			_menu_cmd.Shortcut = Shortcut.F1;
			
			_app.MenuManager.AddMenuItem(_menu_cmd,VLoopMenus.Help,_tb_item);
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

		private void MenuClick(object sender, System.EventArgs e)
		{
			Execute();
		}
	}
	#endregion

	#region Exit App Action
	public class ExitAppAction : Action
	{
		public ExitAppAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			_menu_cmd.Text= "E&xit";
			_menu_cmd.Click += new System.EventHandler(this.MenuClick);
			_app.MenuManager.AddMenuItem(_menu_cmd,VLoopMenus.File,_tb_item);
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

		private void MenuClick(object sender, System.EventArgs e)
		{
			Execute();
		}
	}
	#endregion

	#region Save Action
	public class SaveAction : Action
	{
		public SaveAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			//_menu_cmd.ImageList = _app.ParentForm.ActionImageList;
			//_menu_cmd.ImageIndex = (int)ActionIcons.Save;
			_menu_cmd.Text= "&Save";
			_menu_cmd.Shortcut = Shortcut.CtrlS;
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.Tag = this;
			_tb_item.ImageIndex = (int)ActionIcons.Save;
			_tb_item.Style = ToolBarButtonStyle.PushButton;
			_tb_item.Text = null;
			_tb_item.ToolTipText = "Save the current item";
			_tb_item.Visible = true;
			_app.MenuManager.AddMenuItem(_menu_cmd,VLoopMenus.File,_tb_item);
		}

		public override bool Execute()
		{
			return ((ISaveable)_app.ActiveForm).Save();
		}

		private void MenuClick(object sender, System.EventArgs e)
		{
			Execute();
		}
	}
	#endregion

	#region Toggle Treeview Action
	public class ToggleTreeviewAction : Action
	{
		public ToggleTreeviewAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			_menu_cmd.Text = "Fol&der List";
			_menu_cmd.Shortcut = Shortcut.CtrlShiftT;
			_menu_cmd.Click += new System.EventHandler(this.MenuClicked);
			_app.MenuManager.AddMenuItem(_menu_cmd, VLoopMenus.View,_tb_item);
		}

		public override bool Execute()
		{
			_menu_cmd.Checked = !_menu_cmd.Checked;
			_app.ParentForm.ToggleTreeView();
			return true;
		}

		private void MenuClicked(object sender, System.EventArgs e)
		{
			Execute();
		}
	}
	#endregion

	#region Toggle Scripts Action
	public class ToggleScriptsAction : Action
	{
		public ToggleScriptsAction(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			_menu_cmd = new MenuItem();
			_menu_cmd.Text = "Sc&ripts";
			_menu_cmd.Click += new System.EventHandler(this.MenuClicked);
			_menu_cmd.Shortcut = Shortcut.CtrlShiftS;
			_tb_item = new ToolBarButton();
			_tb_item.Enabled = true;
			_tb_item.Tag =this;
			_tb_item.ImageIndex = (int)ActionIcons.Scripts;
			_tb_item.Style = ToolBarButtonStyle.ToggleButton;
			_tb_item.Text = "Sc&ripts";
			_tb_item.ToolTipText = "Show the sripts window";
			_tb_item.Visible = true;
			_app.MenuManager.AddMenuItem(_menu_cmd, VLoopMenus.View,_tb_item);
		}

		public override bool Execute()
		{
			_menu_cmd.Checked = !_menu_cmd.Checked;
			_app.ParentForm.ToggleScripts();
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
			Action action;
			action = (Action)App.Actions[ActionType];
			action.Enabled = false;
		}

		public static void EnableAction(ActionTypes ActionType, Afni.Applications.VLoop.Application App)
		{
			Action action;
			action = (Action)App.Actions[ActionType];
			action.Enabled = true;
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
		Redo,
		Cut,
		Copy,
		Paste,
		Delete,
		Scripts,
		CampaignSwitch,
		End,
		Next,
		Find,
		DynHelp,
		Help,
		PrevView,
		NextView,
		Queue,
		Treeview,
		Login,
		Exit
	}
}

