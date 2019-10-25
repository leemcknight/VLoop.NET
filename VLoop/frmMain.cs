using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Afni.Applications.VLoop;
using Afni.Applications.VLoop.Viewing;
using Afni.Applications.VLoop.Commands;
using Afni.Controls;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.StatusBar sbMain;
		private System.Windows.Forms.ImageList imglstToolBar;
		private System.Windows.Forms.StatusBarPanel StatusPanel;
		private System.Windows.Forms.StatusBarPanel NamePanel;
		private System.Windows.Forms.StatusBarPanel BTNPanel;
		private System.Windows.Forms.StatusBarPanel CompanyPanel;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MainMenu mnuVLoop;
		private System.Windows.Forms.ToolBar tbVLoop;
		private System.Windows.Forms.ToolBarButton btnBack;
		private System.Windows.Forms.ToolBarButton btnForward;
		private System.Windows.Forms.StatusBarPanel DispositionPanel;
		private System.Windows.Forms.StatusBarPanel CallTimePanel;
		private System.Windows.Forms.Panel _explorerPanel;
		private System.Windows.Forms.Splitter splitter1;
		private Afni.Applications.VLoop.ScriptNavigator scriptNavigator2;
		private Afni.Controls.AfniTitleBar afniTitleBar1;
		private System.Windows.Forms.Panel UserPanel;
		private System.Windows.Forms.Splitter splitterScripts;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private Afni.Applications.VLoop.Application _app;

		public frmMain()
		{
			InitializeComponent();
			this.Text = "VLoop";
			scriptNavigator2.Visible = false;
			splitterScripts.Enabled = false;
			splitterScripts.Visible = false;
			btnBack.Enabled = false;
			btnForward.Enabled = false;
		}

		public void ToggleScripts()
		{
			if(scriptNavigator2.Visible)
			{
				scriptNavigator2.Visible = false;
				splitterScripts.Enabled = false;
				splitterScripts.Visible = false;
			}
			else
			{
				splitterScripts.Enabled = true;
				splitterScripts.Visible = true;
				scriptNavigator2.Visible = true;
			}
		}

		public ToolBarButton BackButton
		{
			get { return this.btnBack; }
		}

		public ToolBarButton ForwardButton
		{
			get { return this.btnForward; }
		}

		public Afni.Applications.VLoop.Application AppObject
		{
			get { return _app; }
			set 
			{
				if(_app == null)
					_app = value; 
			}
		}

		public StatusBar VLStatusBar
		{
			get { return sbMain; }
		}

		public System.Windows.Forms.Panel ParentPanel
		{
			get { return this.UserPanel; }
		}

		public System.Windows.Forms.Panel ExplorerPanel
		{
			get { return this._explorerPanel; }
		}

		public MainMenu VLoopMainMenu
		{
			get { return mnuVLoop; }
		}

		public ToolBar VLoopToolBar
		{
			get { return tbVLoop;}
		}

		public System.Windows.Forms.ImageList ActionImageList
		{
			get { return this.imglstToolBar; }
		}

		public Afni.Controls.AfniTitleBar VLoopTitleBar
		{
			get { return this.afniTitleBar1; }
		}

		public Splitter ExplorerSplitter
		{
			get { return splitter1; }
		}

	
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.sbMain = new System.Windows.Forms.StatusBar();
			this.StatusPanel = new System.Windows.Forms.StatusBarPanel();
			this.NamePanel = new System.Windows.Forms.StatusBarPanel();
			this.BTNPanel = new System.Windows.Forms.StatusBarPanel();
			this.CompanyPanel = new System.Windows.Forms.StatusBarPanel();
			this.DispositionPanel = new System.Windows.Forms.StatusBarPanel();
			this.CallTimePanel = new System.Windows.Forms.StatusBarPanel();
			this.imglstToolBar = new System.Windows.Forms.ImageList(this.components);
			this.mnuVLoop = new System.Windows.Forms.MainMenu();
			this.tbVLoop = new System.Windows.Forms.ToolBar();
			this.btnBack = new System.Windows.Forms.ToolBarButton();
			this.btnForward = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this._explorerPanel = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.scriptNavigator2 = new Afni.Applications.VLoop.ScriptNavigator();
			this.splitterScripts = new System.Windows.Forms.Splitter();
			this.afniTitleBar1 = new Afni.Controls.AfniTitleBar();
			this.UserPanel = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.StatusPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NamePanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BTNPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CompanyPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DispositionPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CallTimePanel)).BeginInit();
			this.SuspendLayout();
			// 
			// sbMain
			// 
			this.sbMain.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.sbMain.Location = new System.Drawing.Point(0, 430);
			this.sbMain.Name = "sbMain";
			this.sbMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																					  this.StatusPanel,
																					  this.NamePanel,
																					  this.BTNPanel,
																					  this.CompanyPanel,
																					  this.DispositionPanel,
																					  this.CallTimePanel});
			this.sbMain.ShowPanels = true;
			this.sbMain.Size = new System.Drawing.Size(712, 24);
			this.sbMain.TabIndex = 3;
			this.sbMain.Text = "Ready...";
			// 
			// StatusPanel
			// 
			this.StatusPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.StatusPanel.Text = "Ready...";
			this.StatusPanel.Width = 466;
			// 
			// NamePanel
			// 
			this.NamePanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.NamePanel.Width = 10;
			// 
			// BTNPanel
			// 
			this.BTNPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.BTNPanel.Width = 10;
			// 
			// CompanyPanel
			// 
			this.CompanyPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.CompanyPanel.Width = 10;
			// 
			// imglstToolBar
			// 
			this.imglstToolBar.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imglstToolBar.ImageSize = new System.Drawing.Size(16, 16);
			this.imglstToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstToolBar.ImageStream")));
			this.imglstToolBar.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tbVLoop
			// 
			this.tbVLoop.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tbVLoop.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.btnBack,
																					   this.btnForward,
																					   this.toolBarButton1});
			this.tbVLoop.DropDownArrows = true;
			this.tbVLoop.ImageList = this.imglstToolBar;
			this.tbVLoop.Name = "tbVLoop";
			this.tbVLoop.ShowToolTips = true;
			this.tbVLoop.Size = new System.Drawing.Size(712, 25);
			this.tbVLoop.TabIndex = 93;
			this.tbVLoop.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.tbVLoop.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbVLoop_ButtonClick);
			// 
			// btnBack
			// 
			this.btnBack.ImageIndex = 15;
			this.btnBack.Text = "&Back";
			// 
			// btnForward
			// 
			this.btnForward.ImageIndex = 16;
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// _explorerPanel
			// 
			this._explorerPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this._explorerPanel.Location = new System.Drawing.Point(0, 25);
			this._explorerPanel.Name = "_explorerPanel";
			this._explorerPanel.Size = new System.Drawing.Size(128, 405);
			this._explorerPanel.TabIndex = 86;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(128, 25);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(5, 405);
			this.splitter1.TabIndex = 87;
			this.splitter1.TabStop = false;
			// 
			// scriptNavigator2
			// 
			this.scriptNavigator2.Dock = System.Windows.Forms.DockStyle.Top;
			this.scriptNavigator2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.scriptNavigator2.Location = new System.Drawing.Point(133, 25);
			this.scriptNavigator2.Name = "scriptNavigator2";
			this.scriptNavigator2.Size = new System.Drawing.Size(579, 144);
			this.scriptNavigator2.TabIndex = 88;
			// 
			// splitterScripts
			// 
			this.splitterScripts.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitterScripts.Location = new System.Drawing.Point(133, 169);
			this.splitterScripts.Name = "splitterScripts";
			this.splitterScripts.Size = new System.Drawing.Size(579, 5);
			this.splitterScripts.TabIndex = 90;
			this.splitterScripts.TabStop = false;
			// 
			// afniTitleBar1
			// 
			this.afniTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.afniTitleBar1.LeftColor = System.Drawing.Color.FromArgb(((System.Byte)(40)), ((System.Byte)(91)), ((System.Byte)(197)));
			this.afniTitleBar1.Location = new System.Drawing.Point(133, 174);
			this.afniTitleBar1.Name = "afniTitleBar1";
			this.afniTitleBar1.RightColor = System.Drawing.Color.FromArgb(((System.Byte)(99)), ((System.Byte)(117)), ((System.Byte)(214)));
			this.afniTitleBar1.Size = new System.Drawing.Size(579, 30);
			this.afniTitleBar1.TabIndex = 91;
			this.afniTitleBar1.Title = null;
			this.afniTitleBar1.TitleFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.afniTitleBar1.TitleIcon = null;
			// 
			// UserPanel
			// 
			this.UserPanel.BackColor = System.Drawing.SystemColors.Control;
			this.UserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.UserPanel.Location = new System.Drawing.Point(133, 204);
			this.UserPanel.Name = "UserPanel";
			this.UserPanel.Size = new System.Drawing.Size(579, 226);
			this.UserPanel.TabIndex = 92;
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(712, 454);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.UserPanel,
																		  this.afniTitleBar1,
																		  this.splitterScripts,
																		  this.scriptNavigator2,
																		  this.splitter1,
																		  this._explorerPanel,
																		  this.sbMain,
																		  this.tbVLoop});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mnuVLoop;
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "frmMain";
			this.Text = "VLoop";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
			((System.ComponentModel.ISupportInitialize)(this.StatusPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NamePanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BTNPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CompanyPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DispositionPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CallTimePanel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void tbVLoop_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			try
			{
				if (e.Button == this.btnBack)
				{
					_app.MovePrevView();
				}
				else if (e.Button == this.btnForward)
				{
					_app.MoveNextView();
				}
				else
				{
					VLoopCommand cmd = (VLoopCommand)e.Button.Tag;
					cmd.Execute();
				}
			}
			catch
			{
				MessageBox.Show("Unable to perfom requested action",
								"VLoop error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
			}
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			_app.Close();
			
		}

		
	}
}
