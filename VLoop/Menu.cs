using System;
using System.Collections;
using Afni.FormData;
using Afni.Applications.VLoop.Viewing;
using Afni.Applications.VLoop.Commands;
using System.Windows.Forms;
using System.Drawing;
using Afni.Controls;

namespace Afni.Applications.VLoop
{
	public class MenuManager
	{
		private Afni.Applications.VLoop.Application _app;
		private MainMenu _menu;
		private MenuItem _file;
		private MenuItem _edit;
		private MenuItem _view;
		private MenuItem _call;
		private MenuItem _tools;
		private MenuItem _system;
		private MenuItem _help;

		public MenuManager(Afni.Applications.VLoop.Application app)
		{
			_app = app;
		}

		public MenuItem AddMenuItem(IViewExplorer explorer,
									VLoopMenus MenuType,
									ToolBarButton TBButton,
									string MenuText,
									Shortcut MenuShortcut,
									Icon menuIcon)
		{
			Menu menu = GetMenu(MenuType);

			AfniMenuItem item = new AfniMenuItem();


			item.Text			= MenuText;
			item.Shortcut		= MenuShortcut;
			item.Click			+= new System.EventHandler(this.OnMenuClick);
			item.Icon			= menuIcon;
			item.Key			= explorer;

			menu.MenuItems.Add(item);
			if(TBButton != null)
				_app.ParentForm.VLoopToolBar.Buttons.Add(TBButton);

			return item;
		}

		public MenuItem AddMenuItem(VLoopCommand MenuAction,		
									VLoopMenus MenuType,
									ToolBarButton TBButton,
									string MenuText,
									Shortcut MenuShortcut,
									Icon MenuIcon)
		{
			Menu menu = GetMenu(MenuType);

			AfniMenuItem item = new AfniMenuItem();


			item.Text			= MenuText;
			item.Shortcut		= MenuShortcut;
			item.Click			+= new System.EventHandler(this.OnMenuClick);
			item.Icon			= MenuIcon;
			item.Key			= MenuAction;

			menu.MenuItems.Add(item);
			if(TBButton != null)
				_app.ParentForm.VLoopToolBar.Buttons.Add(TBButton);

			return item;
		}

		private void OnMenuClick(object sender, System.EventArgs e)
		{
			try 
			{
				//the key is either an action or an IViewExplorer.
				object key = ((AfniMenuItem)sender).Key;


				VLoopCommand cmd = key as VLoopCommand;
				if(cmd != null)
					cmd.Execute( );
				else
				{
					IViewExplorer explorer = key as IViewExplorer;
					if( explorer != null )
						_app.LoadExplorer( explorer );
				} 
			}
			catch
			{
				MessageBox.Show("Unable to perform action.",
								"VLoop Error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
			}
		}

		public void CreateMenu()
		{
			_menu = _app.ParentForm.VLoopMainMenu;
			_file = new MenuItem("&File");
			_edit = new MenuItem("&Edit");
			_view = new MenuItem("&View");
			_call = new MenuItem("&Call");
			_system = new MenuItem("S&ystem");
			_tools = new MenuItem("&Tools");
			_help = new MenuItem("&Help");

			_menu.MenuItems.AddRange(new MenuItem[] {
														_file,
														_edit,
														_view,
														_call,
														_tools,
														_system,
														_help});	
	
		}
		
	
		private MenuItem GetMenu(VLoopMenus menutype)
		{
			MenuItem cmd = null; 
			switch(menutype)
			{
				case VLoopMenus.File:
				{
					cmd = _file;
					break;
				}
				case VLoopMenus.Edit:
				{
					cmd = _edit; 
					break;
				}
				case VLoopMenus.View:
				{
					cmd = _view;
					break;
				}
				case VLoopMenus.Call:
				{
					cmd = _call; 
					break;
				}
				case VLoopMenus.Tools:
				{
					cmd = _tools;
					break;
				}
				case VLoopMenus.System:
				{
					cmd = _system;
					break;
				}
				case VLoopMenus.Help:
				{
					cmd = _help;
					break;
				}
			
			}

			return cmd;
		}

	}

	public enum VLoopMenus
	{
		File,
		Edit,
		View,
		Call,
		Tools,
		System,
		Help
	}
}
