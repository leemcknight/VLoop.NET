using System;
using Afni.Applications.VLoop;
using System.Windows.Forms;
using Afni.Controls;
using System.Collections;
using Afni.Applications.VLoop.Viewing;
using System.Drawing;

namespace Afni.Applications.VLoop
{
	public interface IViewExplorer
	{
		void AddView(Viewing.View viewBase, Viewing.View parentView );
		void Show();
		void Hide();
		string Name { get; set; }
	}

	public class XPBarViewExplorer : IViewExplorer
	{
		private Afni.Applications.VLoop.Application _app;
		private Afni.Controls.TaskBoxGroup _tbg;
		private TaskBox _viewsBox;
		private TaskBox _detailsBox;
		private TaskBox _tasksBox;
		private string _name = "Task Bar";
		private Splitter _splitter;
		private AfniLink  _homeLink;

		public XPBarViewExplorer(Afni.Applications.VLoop.Application app, Splitter explorerSplitter)
		{
			_app = app;
			_splitter = explorerSplitter;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			//app
			_app.ThemeChanged += new EventHandler(this.app_ThemeChanged);

			_tbg = new TaskBoxGroup();
		
			//"Tasks" task box
			_tasksBox = new TaskBox();
			_tasksBox.Text = "Tasks";
			_tbg.TaskBoxes.Add(_tasksBox);

			//"Other Views" task box
			_viewsBox = new TaskBox();
			_viewsBox.Text = "Other Views";
			_tbg.TaskBoxes.Add(_viewsBox);

			//"Details" task box
			_detailsBox = new TaskBox();
			_detailsBox.Text = "Details";
			_tbg.TaskBoxes.Add(_detailsBox);

			_tbg.Parent = _app.ParentForm.ExplorerPanel;
			_tbg.Dock = DockStyle.Fill;

			//Menu item
			_app.MenuManager.AddMenuItem(this,
										VLoopMenus.View,
										null,
										_name,
										Shortcut.None,
										null);


			_homeLink = new AfniLink();
			_homeLink.Icon = VLoopIcons.VLoopHome;
			_homeLink.Text = "VLoop Home";
			_homeLink.Click += new EventHandler(this.homeLink_Clicked);
			_viewsBox.Tasks.Add(_homeLink);

		}

		#region IViewExplorer implementation
		void IViewExplorer.AddView(Viewing.View viewBase, Viewing.View parentView)
		{
			AfniLink viewLink = new AfniLink();

			viewLink.Tag = viewBase;
			viewLink.Icon = viewBase.Icon;
			viewLink.Text = viewBase.ViewName;
			viewLink.Click += new EventHandler(this.viewLink_Clicked);
			_viewsBox.Tasks.Add(viewLink);
			
		}

		string IViewExplorer.Name
		{
			get { return _name; }
			set { _name = value; }
		}

		void IViewExplorer.Show()
		{
			_splitter.Enabled = false;
			_splitter.Visible = false;
			_app.ParentForm.ExplorerPanel.Width = 210;
			_tbg.Show();
		}

		/// <summary>
		/// Hides the explorer from the app
		/// </summary>
		void IViewExplorer.Hide()
		{
			_tbg.Hide();
		}
		#endregion

		#region events
		/// <summary>
		/// Event handler for a "view" link being clicked on.
		/// In this case, we just rip out the View object from
		/// the tag property of the Afni Link and load it.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void viewLink_Clicked(object sender, EventArgs e)
		{
			AfniLink viewLink = (AfniLink)sender;
			Viewing.View view = (Viewing.View)viewLink.Tag;
			_tasksBox.Tasks.Clear();
			foreach(AfniLink task in view.Tasks)
			{
				_tasksBox.Tasks.Add(task);
			}
			_app.LoadView(view);
		}

		/// <summary>
		/// Event handler for the "home" link being clicked on.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void homeLink_Clicked(object sender, EventArgs e)
		{
			_app.LoadView(ViewTypes.Home);
		}

		private void app_ThemeChanged(object sender, EventArgs e)
		{
			_tbg.TopColor = _app.Theme.NavBarTopGradient;
			_tbg.BottomColor = _app.Theme.NavBarBottomGradient;
			foreach(TaskBox tb in _tbg.TaskBoxes)
			{
				tb.HeaderRightGradient = _app.Theme.TaskBoxHeaderRightGradient;
				tb.HeaderLeftGradient = _app.Theme.TaskBoxHeaderLeftGradient;
				tb.BackColor = _app.Theme.TaskBoxInnerColor;
				tb.BorderColor = _app.Theme.TaskBoxBorderColor;
				tb.ChevronDown = _app.Theme.TaskBoxChevronDown;
				tb.ChevronDownHover = _app.Theme.TaskBoxChevronDownHover;
				tb.ChevronUp = _app.Theme.TaskBoxChevronUp;
				tb.ChevronUpHover = _app.Theme.TaskBoxChevronUpHover;
				tb.LinkColor = _app.Theme.TaskNormalColor;
				tb.HeaderActiveTextColor = _app.Theme.TaskBoxHeaderActiveFontColor;
				tb.HeaderTextColor = _app.Theme.TaskBoxHeaderFontColor;
				tb.ActiveLinkColor = _app.Theme.TaskActiveColor;
			}
		}
		#endregion
	}

	public class FolderViewExplorer : IViewExplorer
	{
		private TreeView _tvw;
		private ImageList _tvw_imglist;
		private TreeNode _root;
		private Afni.Applications.VLoop.Application _app;
		private string _name = "Folders";
		private Splitter _splitter;

		public FolderViewExplorer(Afni.Applications.VLoop.Application app, Splitter explorerSplitter)
		{
			_app = app;
			_splitter = explorerSplitter;
			InitializeComponent();	
		}

		private void InitializeComponent()
		{
			_tvw = new TreeView();
			_tvw_imglist = new ImageList();
			_tvw_imglist.Images.Add(VLoopIcons.VLoopHome);
			_tvw_imglist.Images.Add(VLoopIcons.OpenFolder);
			_tvw_imglist.ImageSize = new System.Drawing.Size(16,16);
			_tvw.ImageList = _tvw_imglist;
			_root = new TreeNode("Home");
			_root.ImageIndex = 0;
			_tvw.Nodes.Add(_root);
			_tvw.AfterSelect += new TreeViewEventHandler(this.tvw_clicked);
			_tvw.Parent = _app.ParentForm.ExplorerPanel;
			_tvw.Dock = DockStyle.Fill;
			_tvw.Hide();

			_app.MenuManager.AddMenuItem(this,
										VLoopMenus.View,
										null,
										_name,
										Shortcut.None,
										null);

		}

		#region IViewExplorer implementation
		void IViewExplorer.AddView(Viewing.View viewBase, Viewing.View parentView)
		{
			TreeNode parent = null;
			TreeNode newChild = null;

			newChild = new TreeNode();
			newChild.Text = viewBase.ViewName;
			newChild.Tag = viewBase;
			
			_tvw_imglist.Images.Add(viewBase.Icon);
			newChild.ImageIndex = _tvw_imglist.Images.Count - 1;
			newChild.SelectedImageIndex = _tvw_imglist.Images.Count - 1;
	
			if( parentView != null )
			{
				foreach(TreeNode childNode in _tvw.Nodes)
				{
					if(childNode.Tag == parentView)
						parent = childNode;
				}
			}
			else
				parent = _root;


			parent.Nodes.Add(newChild);	
		}

		void IViewExplorer.Show()
		{
			_splitter.Visible = true;
			_splitter.Enabled =true;
			_tvw.Show();
		}

		void IViewExplorer.Hide()
		{
			_tvw.Hide();
		}

		string IViewExplorer.Name
		{
			get { return _name; }
			set { _name = value; }
		}
		#endregion

		#region events
		private void tvw_clicked(object sender, TreeViewEventArgs te)
		{
			Viewing.View newView;
			ViewItem newItem;
			ArrayList viewItems;
			TreeNode itemNode;
			object nodeTag;

			nodeTag = _tvw.SelectedNode.Tag;
			
			if (_tvw.SelectedNode == _root)
			{
				_app.LoadView(ViewTypes.Home);
				return;
			}


			if (nodeTag == null)
				return;
		
			if ( nodeTag.GetType() == typeof( ViewItem ) )
			{
				//selected a view Item
				newItem = ( ViewItem )nodeTag;
				_app.LoadViewItem(newItem);
				viewItems = newItem.ChildItems;
			}
			else
			{
				//selected a view
				newView = (Viewing.View)nodeTag;
				_app.LoadView(newView);
				viewItems = newView.ViewItems;
			}

			if(viewItems != null)
			{
				//need to populate the child nodes
				_tvw.SelectedNode.Nodes.Clear();
				foreach(ViewItem item in viewItems)
				{
					itemNode = new TreeNode();
					_tvw.ImageList.Images.Add(item.Icon);
					itemNode.ImageIndex = _tvw.ImageList.Images.Count - 1;
					itemNode.SelectedImageIndex = _tvw.ImageList.Images.Count - 1;
					itemNode.Text = item.DataObject.ToString();
					itemNode.Tag = item;
					_tvw.SelectedNode.Nodes.Add(itemNode);
				}
			}
			
		}
		#endregion
	}
}
