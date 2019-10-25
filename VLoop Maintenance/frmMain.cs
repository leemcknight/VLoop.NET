using System;
using System.Windows.Forms;
using Afni.Applications.VLoop.VLoopBusinessObjects;
using Afni.Applications.VLoop.VLoopDataObjects;
using System.Collections;

namespace Afni.Applications.VLoopMaintenance
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class frmMain : Form
	{
		private System.Windows.Forms.StatusBar _sbMain;
		private System.Windows.Forms.StatusBarPanel _pnlItems;
		private System.Windows.Forms.ToolBar _tbMain;
		private System.Windows.Forms.MainMenu _mnuMain;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.ToolBarButton _tbbtnNew;
		private System.Windows.Forms.ImageList _imglstMain;
		private System.Windows.Forms.ToolBarButton _tbbtnEdit;
		private System.Windows.Forms.ToolBarButton _tbbtnDelete;
		private System.Windows.Forms.ToolBarButton _tbbtnSave;
		private System.Windows.Forms.ToolBarButton _tbbtnHelp;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.ContextMenu _ctxNew;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem21;
		private System.Windows.Forms.TreeView _tvwMaint;
		private System.Windows.Forms.Splitter splitter1;
		private System.ComponentModel.IContainer components;
		private ArrayList _campaigns;
		private System.Windows.Forms.ToolBarButton _tbbtnUp;
		private System.Windows.Forms.ToolBarButton _sep1;
		private System.Windows.Forms.MenuItem menuItem22;
		private System.Windows.Forms.MenuItem menuItem23;
		private System.Windows.Forms.MenuItem menuItem24;
		private System.Windows.Forms.MenuItem menuItem25;
		private System.Windows.Forms.ToolBarButton _sep2;
		private System.Windows.Forms.ListView _lvwItems;
		private System.Windows.Forms.ToolBarButton _tbbtnView;
		private System.Windows.Forms.ContextMenu _ctxView;
		private System.Windows.Forms.MenuItem _mnuList;
		private System.Windows.Forms.MenuItem _mnuDetails;
		private System.Windows.Forms.MenuItem _mnuLargeIcons;
		private System.Windows.Forms.MenuItem _mnuSmallIcons;
		private System.Windows.Forms.StatusBarPanel _pnlView;
		private System.Windows.Forms.ContextMenu _ctxtvw;
		private System.Windows.Forms.MenuItem _ctx_tvw_New;
		private System.Windows.Forms.MenuItem _ctx_tvw_Edit;
		private System.Windows.Forms.MenuItem _ctxtvw_Delete;
		private System.Windows.Forms.MenuItem _ctxtvw_Help;
		private ArrayList _products;

		public static void Main()
		{
			Application.Run(new frmMain());
		}
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this._sbMain = new System.Windows.Forms.StatusBar();
			this._pnlItems = new System.Windows.Forms.StatusBarPanel();
			this._pnlView = new System.Windows.Forms.StatusBarPanel();
			this._tbMain = new System.Windows.Forms.ToolBar();
			this._tbbtnNew = new System.Windows.Forms.ToolBarButton();
			this._ctxNew = new System.Windows.Forms.ContextMenu();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.menuItem19 = new System.Windows.Forms.MenuItem();
			this.menuItem20 = new System.Windows.Forms.MenuItem();
			this.menuItem21 = new System.Windows.Forms.MenuItem();
			this._sep1 = new System.Windows.Forms.ToolBarButton();
			this._tbbtnSave = new System.Windows.Forms.ToolBarButton();
			this._tbbtnEdit = new System.Windows.Forms.ToolBarButton();
			this._tbbtnDelete = new System.Windows.Forms.ToolBarButton();
			this._sep2 = new System.Windows.Forms.ToolBarButton();
			this._tbbtnUp = new System.Windows.Forms.ToolBarButton();
			this._tbbtnView = new System.Windows.Forms.ToolBarButton();
			this._ctxView = new System.Windows.Forms.ContextMenu();
			this._mnuLargeIcons = new System.Windows.Forms.MenuItem();
			this._mnuSmallIcons = new System.Windows.Forms.MenuItem();
			this._mnuList = new System.Windows.Forms.MenuItem();
			this._mnuDetails = new System.Windows.Forms.MenuItem();
			this._tbbtnHelp = new System.Windows.Forms.ToolBarButton();
			this._imglstMain = new System.Windows.Forms.ImageList(this.components);
			this._mnuMain = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem22 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem23 = new System.Windows.Forms.MenuItem();
			this.menuItem24 = new System.Windows.Forms.MenuItem();
			this.menuItem25 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this._tvwMaint = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this._lvwItems = new System.Windows.Forms.ListView();
			this._ctxtvw = new System.Windows.Forms.ContextMenu();
			this._ctx_tvw_New = new System.Windows.Forms.MenuItem();
			this._ctx_tvw_Edit = new System.Windows.Forms.MenuItem();
			this._ctxtvw_Delete = new System.Windows.Forms.MenuItem();
			this._ctxtvw_Help = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this._pnlItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._pnlView)).BeginInit();
			this.SuspendLayout();
			// 
			// _sbMain
			// 
			this._sbMain.Location = new System.Drawing.Point(0, 449);
			this._sbMain.Name = "_sbMain";
			this._sbMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																					   this._pnlItems,
																					   this._pnlView});
			this._sbMain.ShowPanels = true;
			this._sbMain.Size = new System.Drawing.Size(616, 24);
			this._sbMain.TabIndex = 1;
			// 
			// _pnlItems
			// 
			this._pnlItems.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this._pnlItems.Text = "0 Items";
			this._pnlItems.Width = 507;
			// 
			// _pnlView
			// 
			this._pnlView.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this._pnlView.Icon = ((System.Drawing.Icon)(resources.GetObject("_pnlView.Icon")));
			this._pnlView.Text = "Campaigns";
			this._pnlView.Width = 93;
			// 
			// _tbMain
			// 
			this._tbMain.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this._tbMain.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this._tbbtnNew,
																					   this._sep1,
																					   this._tbbtnSave,
																					   this._tbbtnEdit,
																					   this._tbbtnDelete,
																					   this._sep2,
																					   this._tbbtnUp,
																					   this._tbbtnView,
																					   this._tbbtnHelp});
			this._tbMain.DropDownArrows = true;
			this._tbMain.ImageList = this._imglstMain;
			this._tbMain.Name = "_tbMain";
			this._tbMain.ShowToolTips = true;
			this._tbMain.Size = new System.Drawing.Size(616, 25);
			this._tbMain.TabIndex = 2;
			this._tbMain.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			// 
			// _tbbtnNew
			// 
			this._tbbtnNew.DropDownMenu = this._ctxNew;
			this._tbbtnNew.ImageIndex = 0;
			this._tbbtnNew.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this._tbbtnNew.Text = "New";
			// 
			// _ctxNew
			// 
			this._ctxNew.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.menuItem15,
																					this.menuItem16,
																					this.menuItem17,
																					this.menuItem18,
																					this.menuItem19,
																					this.menuItem20,
																					this.menuItem21});
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 0;
			this.menuItem15.Text = "Campaign";
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 1;
			this.menuItem16.Text = "Product";
			// 
			// menuItem17
			// 
			this.menuItem17.Index = 2;
			this.menuItem17.Text = "Disposition";
			// 
			// menuItem18
			// 
			this.menuItem18.Index = 3;
			this.menuItem18.Text = "Campaign Question";
			// 
			// menuItem19
			// 
			this.menuItem19.Index = 4;
			this.menuItem19.Text = "Customer Question";
			// 
			// menuItem20
			// 
			this.menuItem20.Index = 5;
			this.menuItem20.Text = "Call Result";
			// 
			// menuItem21
			// 
			this.menuItem21.Index = 6;
			this.menuItem21.Text = "Transfer Type";
			// 
			// _sep1
			// 
			this._sep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// _tbbtnSave
			// 
			this._tbbtnSave.ImageIndex = 1;
			// 
			// _tbbtnEdit
			// 
			this._tbbtnEdit.ImageIndex = 10;
			// 
			// _tbbtnDelete
			// 
			this._tbbtnDelete.ImageIndex = 16;
			// 
			// _sep2
			// 
			this._sep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// _tbbtnUp
			// 
			this._tbbtnUp.ImageIndex = 11;
			// 
			// _tbbtnView
			// 
			this._tbbtnView.DropDownMenu = this._ctxView;
			this._tbbtnView.ImageIndex = 25;
			this._tbbtnView.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			// 
			// _ctxView
			// 
			this._ctxView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this._mnuLargeIcons,
																					 this._mnuSmallIcons,
																					 this._mnuList,
																					 this._mnuDetails});
			this._ctxView.Popup += new System.EventHandler(this._ctxView_Popup);
			// 
			// _mnuLargeIcons
			// 
			this._mnuLargeIcons.Index = 0;
			this._mnuLargeIcons.Text = "Large Icons";
			this._mnuLargeIcons.Click += new System.EventHandler(this._mnuLargeIcons_Click);
			// 
			// _mnuSmallIcons
			// 
			this._mnuSmallIcons.Index = 1;
			this._mnuSmallIcons.Text = "Small Icons";
			this._mnuSmallIcons.Click += new System.EventHandler(this._mnuSmallIcons_Click);
			// 
			// _mnuList
			// 
			this._mnuList.Index = 2;
			this._mnuList.Text = "List";
			this._mnuList.Click += new System.EventHandler(this._mnuList_Click);
			// 
			// _mnuDetails
			// 
			this._mnuDetails.Index = 3;
			this._mnuDetails.Text = "Details";
			this._mnuDetails.Click += new System.EventHandler(this._mnuDetails_Click);
			// 
			// _tbbtnHelp
			// 
			this._tbbtnHelp.ImageIndex = 20;
			// 
			// _imglstMain
			// 
			this._imglstMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this._imglstMain.ImageSize = new System.Drawing.Size(16, 16);
			this._imglstMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imglstMain.ImageStream")));
			this._imglstMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// _mnuMain
			// 
			this._mnuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItem1,
																					 this.menuItem12,
																					 this.menuItem13,
																					 this.menuItem14});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem11,
																					  this.menuItem22});
			this.menuItem1.Text = "&File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem3,
																					  this.menuItem4,
																					  this.menuItem5,
																					  this.menuItem6,
																					  this.menuItem7,
																					  this.menuItem8,
																					  this.menuItem9,
																					  this.menuItem10});
			this.menuItem2.Text = "&New";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Text = "Campaign";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.Text = "Product";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 2;
			this.menuItem5.Text = "Campaign Question";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 3;
			this.menuItem6.Text = "Customer Question";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 4;
			this.menuItem7.Text = "Disposition";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 5;
			this.menuItem8.Text = "Call Result";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 6;
			this.menuItem9.Text = "Transfer Type";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 7;
			this.menuItem10.Text = "Order Queue";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 1;
			this.menuItem11.Text = "&Save";
			// 
			// menuItem22
			// 
			this.menuItem22.Index = 2;
			this.menuItem22.Text = "&Exit";
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 1;
			this.menuItem12.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem23,
																					   this.menuItem24,
																					   this.menuItem25});
			this.menuItem12.Text = "&Edit";
			// 
			// menuItem23
			// 
			this.menuItem23.Index = 0;
			this.menuItem23.Text = "Edit";
			this.menuItem23.Click += new System.EventHandler(this.menuItem23_Click);
			// 
			// menuItem24
			// 
			this.menuItem24.Index = 1;
			this.menuItem24.Text = "Delete";
			// 
			// menuItem25
			// 
			this.menuItem25.Index = 2;
			this.menuItem25.Text = "Undo";
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 2;
			this.menuItem13.Text = "&View";
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 3;
			this.menuItem14.Text = "&Help";
			// 
			// _tvwMaint
			// 
			this._tvwMaint.Dock = System.Windows.Forms.DockStyle.Left;
			this._tvwMaint.ImageList = this._imglstMain;
			this._tvwMaint.Location = new System.Drawing.Point(0, 25);
			this._tvwMaint.Name = "_tvwMaint";
			this._tvwMaint.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																				  new System.Windows.Forms.TreeNode("Campaigns", 19, 24),
																				  new System.Windows.Forms.TreeNode("Products", 19, 24),
																				  new System.Windows.Forms.TreeNode("Dispositions", 19, 24),
																				  new System.Windows.Forms.TreeNode("Questions", 19, 24)});
			this._tvwMaint.Size = new System.Drawing.Size(176, 424);
			this._tvwMaint.TabIndex = 3;
			this._tvwMaint.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._tvwMaint_AfterSelect);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(176, 25);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 424);
			this.splitter1.TabIndex = 4;
			this.splitter1.TabStop = false;
			// 
			// _lvwItems
			// 
			this._lvwItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this._lvwItems.HideSelection = false;
			this._lvwItems.LargeImageList = this._imglstMain;
			this._lvwItems.Location = new System.Drawing.Point(179, 25);
			this._lvwItems.Name = "_lvwItems";
			this._lvwItems.Size = new System.Drawing.Size(437, 424);
			this._lvwItems.SmallImageList = this._imglstMain;
			this._lvwItems.StateImageList = this._imglstMain;
			this._lvwItems.TabIndex = 5;
			// 
			// _ctxtvw
			// 
			this._ctxtvw.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this._ctx_tvw_New,
																					this._ctx_tvw_Edit,
																					this._ctxtvw_Delete,
																					this._ctxtvw_Help});
			// 
			// _ctx_tvw_New
			// 
			this._ctx_tvw_New.Index = 0;
			this._ctx_tvw_New.Text = "New";
			// 
			// _ctx_tvw_Edit
			// 
			this._ctx_tvw_Edit.Index = 1;
			this._ctx_tvw_Edit.Text = "Edit";
			// 
			// _ctxtvw_Delete
			// 
			this._ctxtvw_Delete.Index = 2;
			this._ctxtvw_Delete.Text = "Delete";
			// 
			// _ctxtvw_Help
			// 
			this._ctxtvw_Help.Index = 3;
			this._ctxtvw_Help.Text = "Help";
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(616, 473);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._lvwItems,
																		  this.splitter1,
																		  this._tvwMaint,
																		  this._tbMain,
																		  this._sbMain});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this._mnuMain;
			this.Name = "frmMain";
			this.Text = "VLoop Maintenance";
			((System.ComponentModel.ISupportInitialize)(this._pnlItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._pnlView)).EndInit();
			this.ResumeLayout(false);

		}
	
		public frmMain()
		{
			InitializeComponent();
			PopulateTreeView();
		}

		private void PopulateTreeView()
		{
			CampaignBSO bso = new CampaignBSO();
			TreeNode node;
			_campaigns = bso.GetAll();
			foreach(Campaign campaign in _campaigns)
			{
				node = _tvwMaint.Nodes[0].Nodes.Add(campaign.CampaignName);
				node.Tag = campaign;
			}

			
		}

		private void _tvwMaint_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			switch(e.Node.Index)
			{
				case 0:
					AddCampaignsToLVW();
					break;
				case 1:
					AddProductsToLVW();
					break;
			}
		}

		private void AddCampaignsToLVW()
		{
			ListViewItem item;
			_lvwItems.Clear();
			_lvwItems.Columns.Clear();
			_lvwItems.Columns.Add("Campaign Name", 100, HorizontalAlignment.Left);
			_lvwItems.Columns.Add("Active", 100,HorizontalAlignment.Left);
			foreach(Campaign campaign in _campaigns)
			{
				item = _lvwItems.Items.Add(campaign.CampaignName);
				item.ImageIndex = 0;
			}

			_pnlItems.Text = _campaigns.Count.ToString() + " items";
		}

		private void AddProductsToLVW()
		{
			ListViewItem item;
			_lvwItems.Clear();
			_lvwItems.Columns.Clear();
			_lvwItems.Columns.Add("Product Name", 100, HorizontalAlignment.Left);
			_lvwItems.Columns.Add("Product Code", 100,HorizontalAlignment.Left);
			
			/*
			foreach(Campaign campaign in _campaigns)
			{
				item = _lvwItems.Items.Add(campaign.CampaignName);
				item.ImageIndex = 0;
			}
			*/
			_pnlItems.Text = "0" + " items";
		}

		private void _mnuDetails_Click(object sender, System.EventArgs e)
		{
			_lvwItems.View = View.Details;
			_mnuDetails.RadioCheck = true;
		}

		private void _mnuSmallIcons_Click(object sender, System.EventArgs e)
		{
			_lvwItems.View = View.SmallIcon;
			_mnuSmallIcons.RadioCheck = true;
		}

		private void _mnuLargeIcons_Click(object sender, System.EventArgs e)
		{
			_lvwItems.View = View.LargeIcon;
			_mnuLargeIcons.RadioCheck = true;
		}

		private void _mnuList_Click(object sender, System.EventArgs e)
		{
			_lvwItems.View = View.List;
			_mnuList.RadioCheck = true;
		}

		private void menuItem23_Click(object sender, System.EventArgs e)
		{
			frmCampProperties frm = new frmCampProperties();
			frm.ShowDialog(this);
		}

		private void _ctxView_Popup(object sender, System.EventArgs e)
		{
		
		}
	}
}
