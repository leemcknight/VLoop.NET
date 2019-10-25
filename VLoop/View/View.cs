/******************************************************************************
 * 
 * Afni.Applications.VLoop.Viewing
 * 
 * A "View" is a wrapper around an object that implements the IForm interface.
 * By wrapping an IForm object, we can extend it to behave the way VLoop object
 * are viewed on the screen (i.e. icons, names, tasks, etc...)  The view can be 
 * responsible for updating external GUI elements (toolbars, menus, etc..), while
 * the IForm implemented object can deal with populating itself and displaying
 * the data it is responsible for.  
 * 
 * The view objects are also responsible for updating the rest of the application
 * to use and accept user input based on the actions and interfaces that the
 * viewed object supports.  For example, if the viewed object supports items 
 * being added to it, we might enable the "New" button.  It makes sense to make
 * this logic external to the form.
 * 
 * View are also responsible for keeping track of external tasks that are related
 * in some way to the view.  For example, a customer view might have a task 
 * associated with it for searching for a different customer.  This task is not
 * connected with a customer itself, but it can be realted to the view.
 * 
 * ***************************************************************************/
using System;
using Afni.FormData;
using Afni.Applications.VLoop.Commands;
using Afni.Controls;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;

namespace Afni.Applications.VLoop.Viewing
{

	#region View Base Class
	public class View
	{
		protected IForm _view_form;
		protected ViewTypes _view_type;
		protected Afni.Applications.VLoop.Application _app;
		protected Icon _icon;
		protected bool _nav_to = true;
		protected ArrayList _items;

		/* tasks associated with the view */
		protected ArrayList _tasks;

		/* details associated with the view */
		protected ArrayList _details;
		
		public View()
		{
	
		}

		public View(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}

		/// <summary>
		/// Gets an arraylist of "Tasks" associated
		/// with the view.
		/// </summary>
		public ArrayList Tasks
		{
			get { return _tasks; }
		}

		/// <summary>
		/// Shows the view
		/// </summary>
		/// <returns></returns>
		public virtual bool Show()
		{
			
			_app.SetBusy();
			UserControl ctl;
			ctl = _view_form as UserControl;
			Graphics g = Graphics.FromHwnd(_app.ParentForm.Handle);

			if (ctl == null)
				return false;

			ctl.Parent = _app.ParentForm.ParentPanel;
			ctl.Dock = DockStyle.Fill;
			ctl.Anchor = AnchorStyles.Top | AnchorStyles.Left;

			/* update title bar */
			_app.ParentForm.VLoopTitleBar.Title = _view_form.Name;
			_app.ParentForm.VLoopTitleBar.TitleIcon = _icon;

			UpdateActions();
	
			ctl.Show();
			_view_form.Refresh();
			_app.SetIdle();
			return true;
		}

		protected void OnThemeChange(object sender, EventArgs e)
		{
			ISkinnable sform;
			DisplayTheme theme = _app.Theme;
			bool apply_ok = true;
			try
			{
				sform = _view_form as ISkinnable;
				if(sform != null)
					apply_ok = sform.ApplyTheme(theme);
				else
					apply_ok = false;
			}
			catch
			{
				MessageBox.Show("VLoop was unable to apply the " + 
								theme.Name + " theme to the " + 
								_view_form.Name + " form.",
								"VLoop Error"
								,MessageBoxButtons.OK,
								MessageBoxIcon.Error);
			}

		}

		protected void OnNewCall(object sender, EventArgs e)
		{
			_app.SetBusy("Loading " + _view_form.Name + " information...");
			_view_form.Refresh();
			_app.SetIdle();
		}

		/// <summary>
		/// Gets or sets a reference to the main
		/// vloop application
		/// </summary>
		public Afni.Applications.VLoop.Application AppObject
		{
			get { return _app; }
			set { _app = value; }
		}

		/// <summary>
		/// Shows the view
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public virtual bool Show(ViewItem item)
		{
			this.Show();
			return true;
		}

		/// <summary>
		/// Gets or sets the icon assocaiated with
		/// the view
		/// </summary>
		public Icon Icon
		{
			get { return _icon; }
			set { _icon = value; }
		}

		/// <summary>
		/// Hides the view from the main application
		/// </summary>
		/// <returns></returns>
		public bool Hide()
		{
			UserControl ctl;
			bool hide_ok = true;
			try
			{
				ctl = _view_form as UserControl;

				ctl.Hide();
			}
			catch
			{
				hide_ok =false;
			}

			return hide_ok;
		}

		/// <summary>
		/// Gets or sets the type of the view
		/// </summary>
		public ViewTypes ViewType
		{
			get { return _view_type; }
			set { _view_type = value; }
		}

		/// <summary>
		/// Gets or sets the name of the view
		/// </summary>
		public string ViewName
		{
			get { return _view_form.Name; }
			set { _view_form.Name = value; }
		}

		/// <summary>
		/// Gets or sets a reference to any
		/// IForm implemtation that is connected
		/// to the view.
		/// </summary>
		public IForm ViewedForm
		{
			get { return _view_form; }
			set { _view_form = value; 	}
		}

		/// <summary>
		/// Gets or sets a value indicating
		/// whether or not the view can be navigated
		/// to with the "forward" arrow
		/// </summary>
		public bool AllowsForwardNavigation
		{
			get { return _nav_to; }
			set { _nav_to = value; }
		}

		public ArrayList ViewItems
		{
			get { return _items; }
		}

		protected void RegisterSaveEvents()
		{
			ISaveable save_form = _view_form as ISaveable;
			if(save_form != null)
			{
				save_form.Dirtied += new EventHandler(this.OnViewedFormDirtied);
				save_form.SaveSucceeded += new EventHandler(this.OnViewedFormSaveOK);
				save_form.SaveFailed += new EventHandler(this.OnViewedFormSaveFail);
				save_form.Undone += new EventHandler(this.OnViewedFormUndone);
			}
		}

		public virtual ArrayList FillViewItemChildren(ViewItem item)
		{
			return null;
		}

		private void OnViewedFormDirtied(object sender, System.EventArgs e)
		{
			UpdateActions();
		}

		private void OnViewedFormSaveOK(object sender, System.EventArgs e)
		{
			UpdateActions();
		}

		private void OnViewedFormSaveFail(object sender, System.EventArgs e)
		{
			UpdateActions();
		}

		private void OnViewedFormUndone(object sender, System.EventArgs e)
		{
			UpdateActions();
		}

		private void UpdateActions()
		{
			switch(_view_form.FormState)
			{
				case FormStates.AddInProgress:
					((VLoopCommand)_app.Commands[ActionTypes.Delete]).Enabled = false;
					((VLoopCommand)_app.Commands[ActionTypes.Edit]).Enabled = false;
					((VLoopCommand)_app.Commands[ActionTypes.Save]).Enabled = true;
					((VLoopCommand)_app.Commands[ActionTypes.New]).Enabled = false;
					((VLoopCommand)_app.Commands[ActionTypes.Undo]).Enabled = true;
					break;
				case FormStates.EditInProgress:
					((VLoopCommand)_app.Commands[ActionTypes.Delete]).Enabled = false;
					((VLoopCommand)_app.Commands[ActionTypes.Edit]).Enabled = false;
					((VLoopCommand)_app.Commands[ActionTypes.Save]).Enabled = true;
					((VLoopCommand)_app.Commands[ActionTypes.New]).Enabled = false;
					((VLoopCommand)_app.Commands[ActionTypes.Undo]).Enabled = true;
					break;
				case FormStates.EditPendingChange:
					((VLoopCommand)_app.Commands[ActionTypes.Delete]).Enabled = false;
					((VLoopCommand)_app.Commands[ActionTypes.Edit]).Enabled = false;
					((VLoopCommand)_app.Commands[ActionTypes.Save]).Enabled = false;
					((VLoopCommand)_app.Commands[ActionTypes.New]).Enabled = false;
					((VLoopCommand)_app.Commands[ActionTypes.Undo]).Enabled = false;
					break;
				case FormStates.Idle:
					if((_view_form as IDeletable) != null)
						((VLoopCommand)_app.Commands[ActionTypes.Delete]).Enabled = true;
					else
						((VLoopCommand)_app.Commands[ActionTypes.Delete]).Enabled = false;

					if((_view_form as IEditable) != null)
						((VLoopCommand)_app.Commands[ActionTypes.Edit]).Enabled = true;
					else
						((VLoopCommand)_app.Commands[ActionTypes.Edit]).Enabled = false;

					((VLoopCommand)_app.Commands[ActionTypes.Save]).Enabled = false;

					if((_view_form as IAddable) != null)
						((VLoopCommand)_app.Commands[ActionTypes.New]).Enabled = true;
					else
						((VLoopCommand)_app.Commands[ActionTypes.New]).Enabled = false;

					((VLoopCommand)_app.Commands[ActionTypes.Undo]).Enabled = false;
					break;
			}
		}
	}
	#endregion

	#region Call Queue View 
	public class CallQueueView : View
	{
		public CallQueueView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new QueueCtl(_app);
			_icon = VLoopIcons.VLoopHome;
			AddTasks();
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}

		private void AddTasks()
		{
			AfniLink new_task = new AfniLink();

			new_task.Text = "Change Campaign";
			new_task.Tag = ActionTypes.CampaignSwitch;
			_tasks.Add(new_task);
			new_task.LinkClicked += 
				new EventHandler(this.OnTaskClick);
			
			new_task = new AfniLink();
			new_task.Tag = ActionTypes.DynHelp;
			new_task.Text = "Queue Setup Help";
			_tasks.Add(new_task);
			new_task.LinkClicked += 
				new EventHandler(this.OnTaskClick);
		}

		private void OnTaskClick(object sender, EventArgs e)
		{
			VLoopCommand cmd;
			ActionTypes key;

			try
			{
				key = (ActionTypes)((AfniLink)sender).Tag;
				cmd = (VLoopCommand)_app.Commands[key];
				cmd.Execute();
			}
			catch
			{
				MessageBox.Show("Unable to perform requested action.",
						   "VLoop Error",
							MessageBoxButtons.OK,
							MessageBoxIcon.Error);
			}
		}
	}
	#endregion

	#region Customer Seach View
	public class SearchView : View
	{
		public SearchView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new SearchCtl(_app);
			_icon = VLoopIcons.Search;
			AddTasks();
			
			foreach(IViewExplorer exp in _app.Explorers)
				exp.AddView(this, null);

			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}

		private void AddTasks()
		{
			AfniLink new_task = new AfniLink();

			new_task.Text = "Help on Searching";
			new_task.Tag = ActionTypes.DynHelp;
			new_task.Icon = VLoopIcons.Help;
			_tasks.Add(new_task);
		}

		public override bool Show()
		{
			base.Show();
			_view_form.Refresh();
			return true;
		}
	}
	#endregion

	#region Campaign View
	public class CampaignView : View
	{

		public CampaignView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new CampaignCtl(_app);
			_icon = VLoopIcons.VLoopHome;
			AddTasks();
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}

		private void AddTasks()
		{
			AfniLink new_task = new AfniLink();

			new_task.Text = "Campaign Selection Help";
			new_task.Tag = ActionTypes.DynHelp;
			_tasks.Add(new_task);
		}

		public override bool Show()
		{
			base.Show();
			_view_form.Refresh();
			return true;
		}
	}
	#endregion

	#region WorkMode Selection View
	public class WorkModeView : View
	{
		public WorkModeView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new ctlWorkMode(_app);
			_icon = VLoopIcons.Customer;
			AddTasks();
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}

		private void AddTasks()
		{
			AfniLink new_task = new AfniLink();

			new_task.Text = "Change Campaign";
			new_task.Tag = ActionTypes.CampaignSwitch;
			_tasks.Add(new_task);
			new_task.LinkClicked += 
				new EventHandler(this.OnTaskClick);
			
			new_task = new AfniLink();
			new_task.Tag = ActionTypes.DynHelp;
			new_task.Text = "Work Mode Selection Help";
			new_task.Icon = VLoopIcons.Help;
			_tasks.Add(new_task);
			new_task.LinkClicked += 
				new EventHandler(this.OnTaskClick);
		}

		private void OnTaskClick(object sender, EventArgs e)
		{
			VLoopCommand cmd;
			ActionTypes key;

			try
			{
				key = (ActionTypes)((AfniLink)sender).Tag;
				cmd = (VLoopCommand)_app.Commands[key];
				cmd.Execute();
			}
			catch
			{
				MessageBox.Show("Unable to perform requested action.",
					"VLoop Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		public override bool Show()
		{
			base.Show();
			_view_form.Refresh();
			return true;
		}
	}
	#endregion

	#region Home View
	public class HomeView : View
	{
		public HomeView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new ctlHome(_app);
			_icon = VLoopIcons.VLoopHome;
		}
	}
	#endregion

	#region Customer View
	public class CustomerView : View
	{
		public CustomerView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new CustomerCtl(_app);
			
			_icon = VLoopIcons.Customer;

			AddTasks();
			
			foreach(IViewExplorer exp in _app.Explorers)
				exp.AddView(this,null);

			RegisterSaveEvents();
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
			_app.Call.NewCallStarted += new EventHandler(this.OnNewCall);
		}

		private void AddTasks()
		{
			AfniLink new_task = new AfniLink();

			new_task.Text = "Find another customer";
			new_task.Tag = ActionTypes.Find;
			new_task.LinkClicked += 
				new EventHandler(this.OnViewTaskClicked);
			new_task.Icon = VLoopIcons.Search;
			_tasks.Add(new_task);

			new_task = new AfniLink();
			new_task.Text = "Customer Help";
			new_task.Tag = ActionTypes.DynHelp;
			new_task.Icon = VLoopIcons.Help;
			new_task.LinkClicked += 
				new EventHandler(this.OnViewTaskClicked);
			_tasks.Add(new_task);
		}

		private void OnViewTaskClicked(object sender, EventArgs e)
		{
			ActionTypes key = (ActionTypes)((AfniLink)sender).Tag;
			VLoopCommand cmd = (VLoopCommand)_app.Commands[key];
			cmd.Execute();
		}
	}
	#endregion

	#region Account View
	public class AccountView : View
	{

		public AccountView(Afni.Applications.VLoop.Application AppObj)
		{
			AccountCtl ctl;
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new AccountCtl(_app);
			ctl = (AccountCtl)_view_form;
			ctl.WTNAdd += new System.EventHandler(this.OnAddWTN);
			ctl.PlanAdd += new System.EventHandler(this.OnAddNewPlan);
			_icon = VLoopIcons.Account;

			foreach(IViewExplorer exp in _app.Explorers)
				exp.AddView(this,null);

			AddTasks();
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}

		private void AddTasks()
		{
			/*Add WTN task */
			AfniLink new_task = new AfniLink();
			new_task.Text = "Add a WTN";
			new_task.Icon = VLoopIcons.NewItem;
			new_task.LinkClicked += 
				new EventHandler(this.OnAddWTN);
			_tasks.Add(new_task);

			/* add new plan task */
			new_task =  new AfniLink();
			new_task.Text = "Add a new plan";
			new_task.Icon = VLoopIcons.NewItem;
			new_task.LinkClicked += 
				new EventHandler(this.OnAddNewPlan);
			_tasks.Add(new_task);

			/* help task */
			new_task = new AfniLink();
			new_task.Text = "Account Help";
			new_task.Icon = VLoopIcons.Help;
			new_task.LinkClicked += 
				new EventHandler(this.OnAccountHelp);
			_tasks.Add(new_task);
		}

		private void OnAddWTN(object sender, EventArgs e)
		{
			Viewing.View wtnview;
			wtnview = (Viewing.View)_app.Views[ViewTypes.AddWTN];
			_app.LoadView(wtnview);
		}

		private void OnAddNewPlan(object sender, EventArgs e)
		{
			Viewing.View planview;
			planview = (Viewing.View)_app.Views[ViewTypes.NewPlanStep1];
			_app.LoadView(planview);
		}

		private void OnAccountHelp(object sender, EventArgs e)
		{
			_view_form.ShowHelp();
		}

		public override bool Show()
		{
			ViewItem item;
			ArrayList wtns;
			bool showOK = base.Show();

			if(_items == null)
			{
				//need to wrap the wtns in viewItem objects
				_items = new ArrayList();
				wtns = _app.Call.CurrentCustCampaign.Customer.Account.WTNs;
				foreach(WTN wtn in wtns)
				{
					item = new ViewItem();
					item.DataObject = wtn;
					item.Icon = VLoopIcons.WTN;
					item.View = (Viewing.View)_app.Views[ViewTypes.AddWTN];
					_items.Add(item);
				}
			}

			return showOK;
		}

		public override bool Show(ViewItem item)
		{
			return base.Show();
		}

		public override ArrayList FillViewItemChildren(ViewItem item)
		{
			ArrayList children = new ArrayList();
			ViewItem newItem;
			VLoopDataObject vdo = item.DataObject;
			
			if(vdo == null)
				return children;

			if (vdo.GetType() ==  typeof(WTN))
			{
				foreach(CurrentPlan plan in ((WTN)vdo).CurrentPlans)
				{
					newItem = new ViewItem();
					newItem.Icon = VLoopIcons.Account;
					newItem.DataObject = plan;
					newItem.View = this;
					children.Add(newItem);
				}
			}

			return children;
		}
	}
	#endregion

	#region Order View
	public class OrderView : View
	{
		public OrderView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new ctlOrders(_app);
			_icon = VLoopIcons.Next;

			foreach(IViewExplorer exp in _app.Explorers)
				exp.AddView(this,null);

			AddTasks();
		}

		private void AddTasks()
		{
			/*Add Order task */
			AfniLink new_task = new AfniLink();
			new_task.Text = "Create New Order";
			new_task.Icon = VLoopIcons.NewItem;
			new_task.LinkClicked += 
				new EventHandler(this.OnNewOrder);
			_tasks.Add(new_task);

			/* help task */
			new_task = new AfniLink();
			new_task.Text = "Order/Sales Help";
			new_task.Icon = VLoopIcons.Help;
			new_task.LinkClicked += 
				new EventHandler(this.OnOrderHelp);
			_tasks.Add(new_task);
		}

		private void OnNewOrder(object sender, EventArgs e)
		{

		}

		private void OnOrderHelp(object sender, EventArgs e)
		{
		}
	}
	#endregion

	#region New Plan Wizard Step 1 View
	public class NewPlanWizard1View : View
	{
		public NewPlanWizard1View(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new NewPlanWiz1Ctl(_app);
			_icon = VLoopIcons.Account;
			_nav_to = false;
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}
	}
	#endregion

	#region New Plan Confirmation Screen View
	public class NewPlanConfView : View
	{
		public NewPlanConfView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new NewPlanWizFinalCtl(_app);
			_icon = VLoopIcons.Account;
			_nav_to = false;
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}
	}
	#endregion

	#region Toll Free Plan View
	public class TollFreeView : View
	{
		public TollFreeView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new TollFreeDetailsCtl();
			_icon = VLoopIcons.Account;
			_nav_to = false;
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}
	}
	#endregion

	#region Calling Card View
	public class CallingCardView : View
	{
		public CallingCardView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new TollFreeDetailsCtl();
			_icon = VLoopIcons.Account;
			_nav_to = false;
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}
	}
	#endregion

	#region Plan Selection View
	public class SelectPlanView : View
	{
		public SelectPlanView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new NewPlanWizPlanTypeCtl(_app);
			_icon = VLoopIcons.Account;
			_nav_to = false;
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}
	}
	#endregion

	#region Add WTN View
	public class AddWTNView : View
	{
		public AddWTNView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new WTNControl(_app);
			_icon = VLoopIcons.WTN;
			RegisterSaveEvents();
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}

		public override bool Show(ViewItem item)
		{
			try
			{
				this.Show();
				((WTNControl)_view_form).CurrentWTN = (WTN)item.DataObject;
			}
			catch
			{
				return false;
			}
			return true;
		}

		public override ArrayList FillViewItemChildren(ViewItem item)
		{
			ArrayList children = new ArrayList();
			ViewItem newItem;
			VLoopDataObject vdo = item.DataObject;
			
			if(vdo == null)
				return children;

			if (vdo.GetType() == typeof(WTN))
			{
				foreach(CurrentPlan plan in ((WTN)vdo).CurrentPlans)
				{
					newItem = new ViewItem();
					newItem.Icon = VLoopIcons.Account;
					newItem.DataObject = plan;
					newItem.View = (Viewing.View)_app.Views[ViewTypes.Account];
					children.Add(newItem);
				}
			}

			return children;
		}
	}
	#endregion

	#region Call History View
	public class CallHistoryView : View
	{
		public CallHistoryView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new CallHistoryCtl();
			_icon = VLoopIcons.CallHistory;

			foreach(IViewExplorer exp in _app.Explorers)
				exp.AddView(this,null);

			AddTasks();

			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}

		private void AddTasks()
		{
			AfniLink new_task = new AfniLink();
			new_task.Text = "Call History Help";
			new_task.LinkClicked += 
				new EventHandler(this.OnCallHistoryHelp);
			
			new_task.Icon = VLoopIcons.Help;
			_tasks.Add(new_task);
		}

		private void OnCallHistoryHelp(object sender, EventArgs e)
		{
			_view_form.ShowHelp();
		}
	}
	#endregion

	#region Call Disposition View
	public class DispositionView : View
	{
		public DispositionView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new DispositionCtl(_app);
			_icon = VLoopIcons.Account;
			_nav_to = false;
			AddTasks();
			RegisterSaveEvents();
			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}

		
		private void AddTasks()
		{
			AfniLink new_task = new AfniLink();

			new_task.Text = "Return to Account";
			new_task.LinkClicked += 
				new EventHandler(this.OnReturnToAccount);

			_tasks.Add(new_task);
		}

		private void OnReturnToAccount(object sender, EventArgs e)
		{
			_app.LoadView((Viewing.View)_app.Views[ViewTypes.Customer]);
		}

	}
	#endregion

	#region Job Aids View
	public class JobAidsView : View
	{
		public JobAidsView(Afni.Applications.VLoop.Application AppObj)
		{
			_app = AppObj;
			_tasks = new ArrayList();
			_view_form = new JobAidsCtl();
			_icon = VLoopIcons.Globe;

			foreach(IViewExplorer exp in _app.Explorers)
				exp.AddView(this,null);

			_app.ThemeChanged += new EventHandler(this.OnThemeChange);
		}
	}
	#endregion

	
	internal class ViewHelper
	{
		internal static Afni.Controls.AfniLink CreateTask(string TaskName, TaskBox Group)
		{
			AfniLink task = new AfniLink();
			task.Text = TaskName;
			Group.Tasks.Add(task);
			return task;
		}
	}

	public class ViewItem
	{
		private VLoopDataObjects.VLoopDataObject _obj;
		private View _view;
		private ArrayList _childItems;
		private Icon _icon;

		public VLoopDataObjects.VLoopDataObject DataObject
		{
			get { return _obj; }
			set { _obj = value; }
		}

		public View View
		{
			get { return _view; }
			set { _view = value; }
		}

		public Icon Icon
		{
			get { return _icon; }
			set { _icon = value; }
		}

		public ArrayList ChildItems
		{
			get { 

				if(_childItems == null)
					_childItems = _view.FillViewItemChildren(this);

				return _childItems; 
			}
		}
	}

	public enum ViewTypes
	{
		Home,
		Customer,
		Campaign,
		Account,
		NewPlanStep1,
		TollFree,
		CallingCard,
		NewPlanSelect,
		NewPlanFinal,
		AddWTN,
		Search,
		CallHistory,
		Disposition,
		JobAids,
		CallQueueSetup,
		Orders,
		OrderQueueSetup,
		WorkMode,
		Help
	}

	public enum ViewIcons
	{
		ClosedFolder = 0,
		OpenFolder = 1,
		WTN = 2,
		Customer = 3,
		Home = 4,
		Account = 5,
		CallHistory = 6,
		JobAids = 7,
		Disposition = 8,
		Queue = 9,
		Campaign = 10,
		Search = 11
	}

	public enum ViewAction
	{
		Add,
		Edit,
		None
	}
}
