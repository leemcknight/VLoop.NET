using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;
using Afni.DataUtility;
using Afni.Applications.VLoop.Viewing;
using Afni.Applications.VLoop.Commands;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopBusinessObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;
using Afni.Formatting;
using Afni.Controls;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for AccountCtl.
	/// </summary>
	public class AccountCtl : UserControl, IForm, IAddable, IEditable, IDeletable, ISaveable, ISkinnable
	{
		private System.Windows.Forms.Label lblCAN;
		private System.Windows.Forms.TextBox txtCAN;
		private Afni.FormData.FormStates _form_state = FormStates.Idle;
		private System.Windows.Forms.Label lblBillDate;
		private System.Windows.Forms.TextBox txtBillDate;
		private System.Windows.Forms.CheckBox chkInFranchise;
		private System.Windows.Forms.Label lblInServiceDate;
		private System.Windows.Forms.TextBox txtInServiceDate;
		private System.Windows.Forms.Label lblBTN;
		private System.Windows.Forms.ContextMenu mnuAccount;
		private System.Windows.Forms.MenuItem EditItem;
		private System.Windows.Forms.MenuItem DeleteItem;
		private System.Windows.Forms.MenuItem HelpItem;
		private Afni.Applications.VLoop.Application _app;
		private System.Windows.Forms.ImageList imglstTVW;
		private System.Windows.Forms.TreeView tvwAccount;
		private Afni.Controls.AfniFlatCombo afcboBTN;
		private System.ComponentModel.IContainer components;
		private Afni.Applications.VLoop.DisplayTheme _theme;
		private Afni.Applications.VLoop.VLoopSmartDTO.Account _acct;
		private Afni.Applications.VLoop.NewPlanManager _planWiz;

		private const int TVWICON_PLAN = 0;
		private const int TVWICON_DETAIL =1;
		private const int TVWICON_HOME = 2;
		private const int TVWICON_WTN = 3;
		private const int TVWICON_SELFOLDER = 4;
		private System.Windows.Forms.MenuItem menuItem_New;
		private System.Windows.Forms.MenuItem menuItem1;
		private const int TVWICON_FOLDER  = 5;
	
		public event EventHandler WTNAdd;
		public event EventHandler PlanAdd;
		
		public AccountCtl(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			_planWiz = new NewPlanManager(_app);

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			// add the prod types to the menu
			AddProdTypesToMenu();
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

		private void AddProdTypesToMenu()
		{
			IManager bso = new ProdTypeBSO();
			ArrayList types = (ArrayList)bso.GetAll();
			AfniMenuItem menuItem;

			foreach(ProductType type in types)
			{
				menuItem = new AfniMenuItem();
				menuItem.Text = type.ProductTypeDescription;
				menuItem.Key = type;
				menuItem.Click += new EventHandler(OnNewItemClick);
				menuItem_New.MenuItems.Add(menuItem);
			}
		}

		private void RebuildTVW()
		{
			TreeNode root;
			TreeNode wtn_node;

			tvwAccount.Nodes.Clear();
			root = tvwAccount.Nodes.Add("WTNs");
			root.ImageIndex = TVWICON_HOME;
			root.SelectedImageIndex = TVWICON_HOME;

			foreach(WTN wtn in _acct.WTNs)
			{
				wtn_node = root.Nodes.Add(AfFormat.ToMaskedPhoneNumber(wtn.WorkingTelephoneNumber));
				wtn_node.ImageIndex = TVWICON_WTN;
				wtn_node.SelectedImageIndex = TVWICON_WTN;
				wtn_node.Tag = wtn;
				AddPlansToTVW(wtn_node);
			}
		}

		private void AddPlansToTVW(TreeNode wtn_node)
		{
			TreeNode plan_node;
			TreeNode type_node;
			ProductType prodType;
			Product prod;
			WTN wtn;

			wtn = (WTN)wtn_node.Tag;
			foreach(CurrentPlan plan in wtn.CurrentPlans)
			{
				prod = ProductFromID(plan.ProductID);
				type_node = TreeNodeFromKey(prod);

				if(type_node == null)
				{
					prodType = ProdTypeFromID(prod.ProdTypeID);
					type_node = wtn_node.Nodes.Add(prodType.ProductTypeDescription);
					type_node.Tag = prodType;
					type_node.ImageIndex = TVWICON_FOLDER;
					type_node.SelectedImageIndex = TVWICON_SELFOLDER;
				}
				plan_node = type_node.Nodes.Add(plan.Description);
				plan_node.ImageIndex = TVWICON_PLAN;
				plan_node.Tag = plan;
				AddDetailsToTVW(plan_node);
			}
		}

		private void AddDetailsToTVW(TreeNode plan_node)
		{
			CurrentPlan plan;
			TreeNode detail_node;
			string node_text;
			plan = (CurrentPlan)plan_node.Tag;

			foreach(CurrentPlanDetail detail in plan.PlanDetails)
			{
				node_text = detail.QuestionText + ": ";
				if(detail.LookupID > 0)
					node_text += detail.LookupText;
				else
					node_text += detail.AnswerText;

				detail_node = plan_node.Nodes.Add(node_text);	
				detail_node.ImageIndex = TVWICON_DETAIL;
				detail_node.SelectedImageIndex = TVWICON_DETAIL;
				detail_node.Tag = detail;
			}
		}

		private TreeNode TreeNodeFromKey(object key)
		{
			TreeNode found_node = null;
			ProductType prodType = ProdTypeFromID(((Product)key).ProdTypeID);

			foreach(TreeNode node in tvwAccount.Nodes)
			{
				if(node.Tag == prodType)
				{
					found_node = node;
					break;
				}
			}

			return found_node;
		}

		private ProductType ProdTypeFromID(long prodTypeID)
		{
			ProductType prodType = null;
			foreach(ProductType type in _app.ProductTypes)
			{
				if(type.ProductTypeID == prodTypeID)
				{
					prodType = type;
					break;
				}
			}

			return prodType;
		}

		private Product ProductFromID(long prod_id)
		{
			Product ret = null;
			foreach(Product prod in _app.CurrentCampaign.Products)
			{
				if(prod.ProductID == prod_id)
				{
					ret = prod;
					break;
				}
			}

			return ret;
		}

		#region IForm implementation
		bool IForm.Refresh()
		{
			_form_state = FormStates.Busy;
			_acct = _app.Call.CurrentCustCampaign.Customer.Account;
			txtBillDate.Text = _acct.BillDate.ToString();
			txtInServiceDate.Text = _acct.InServiceDate.ToString();
			txtCAN.Text = _acct.AccountNumber;
			chkInFranchise.Checked = _acct.InFranchise;
			RebuildTVW();
			afcboBTN.DataSource = _acct.WTNs;
			_form_state = FormStates.Idle;
			return true;
		}

		string IForm.Name
		{
			get { return "Account"; }
			set {}
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		FormStates IForm.FormState
		{
			get { return _form_state; }
		}

		#endregion

		#region IAddable implementation
		bool IAddable.AddNew()
		{
			_planWiz.Start();
			return true;
		}

		string IAddable.AddButtonText
		{
			get { return "Add Plan"; }
			set {}
		}
		#endregion

		#region IDeletable implementation
		bool IDeletable.Delete()
		{
			if(DeleteSucceeded != null)
				DeleteSucceeded(this,null);
			return true;
		}

		string IDeletable.DeleteButtonText
		{
			get { return "Delete Plan"; }
			set {}
		}

		public event EventHandler DeleteFailed;
		public event EventHandler DeleteSucceeded;
		#endregion

		#region IEditable implementation
		bool IEditable.Edit()
		{
			_form_state = FormStates.EditPendingChange;
			return true;
		}

		string IEditable.EditButtonText
		{
			get { return "Edit Plan"; }
			set {}
		}
		#endregion

		#region ISaveable implementation
		bool ISaveable.Save()
		{
			_form_state = FormStates.Idle;
			if(SaveSucceeded != null)
				SaveSucceeded(this,null);
			return true;
		}

		bool ISaveable.Undo()
		{
			_form_state = FormStates.Idle;
			if(Undone != null)
				Undone(this,null);
			return true;
		}

		string ISaveable.SaveButtonText
		{
			get { return "&Save"; }
			set {}
		}
		
		public event EventHandler Dirtied;
		public event EventHandler SaveFailed;
		public event EventHandler SaveSucceeded;
		public event EventHandler Undone;
		#endregion

		#region ISkinnable implementation
		bool ISkinnable.ApplyTheme(DisplayTheme theme)
		{
			bool theme_ok = true;
			try
			{
				this.BackColor = theme.FormBackColor;
				if(!_app.IsOnXP)
				{
					if(theme.FlatControls)
					{
						tvwAccount.BorderStyle = BorderStyle.FixedSingle;
						txtBillDate.BorderStyle = BorderStyle.FixedSingle;
						txtCAN.BorderStyle = BorderStyle.FixedSingle;
						txtInServiceDate.BorderStyle = BorderStyle.FixedSingle;
						chkInFranchise.FlatStyle = FlatStyle.Flat;
					}
					else
					{
						tvwAccount.BorderStyle = BorderStyle.Fixed3D;
						txtBillDate.BorderStyle = BorderStyle.Fixed3D;
						txtCAN.BorderStyle = BorderStyle.Fixed3D;
						txtInServiceDate.BorderStyle = BorderStyle.Fixed3D;
						chkInFranchise.FlatStyle = FlatStyle.Standard;
					}
				}
				_theme = theme;
			}
			catch
			{
				theme_ok = false;
			}

			return theme_ok;
		}

		DisplayTheme ISkinnable.CurrentTheme
		{
			get { return _theme; }
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AccountCtl));
			this.lblCAN = new System.Windows.Forms.Label();
			this.txtCAN = new System.Windows.Forms.TextBox();
			this.lblBillDate = new System.Windows.Forms.Label();
			this.txtBillDate = new System.Windows.Forms.TextBox();
			this.chkInFranchise = new System.Windows.Forms.CheckBox();
			this.lblInServiceDate = new System.Windows.Forms.Label();
			this.txtInServiceDate = new System.Windows.Forms.TextBox();
			this.lblBTN = new System.Windows.Forms.Label();
			this.mnuAccount = new System.Windows.Forms.ContextMenu();
			this.menuItem_New = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.EditItem = new System.Windows.Forms.MenuItem();
			this.DeleteItem = new System.Windows.Forms.MenuItem();
			this.HelpItem = new System.Windows.Forms.MenuItem();
			this.imglstTVW = new System.Windows.Forms.ImageList(this.components);
			this.tvwAccount = new System.Windows.Forms.TreeView();
			this.afcboBTN = new Afni.Controls.AfniFlatCombo();
			this.SuspendLayout();
			// 
			// lblCAN
			// 
			this.lblCAN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCAN.Location = new System.Drawing.Point(8, 16);
			this.lblCAN.Name = "lblCAN";
			this.lblCAN.Size = new System.Drawing.Size(96, 16);
			this.lblCAN.TabIndex = 0;
			this.lblCAN.Text = "Account Number:";
			// 
			// txtCAN
			// 
			this.txtCAN.Location = new System.Drawing.Point(112, 16);
			this.txtCAN.Name = "txtCAN";
			this.txtCAN.Size = new System.Drawing.Size(128, 21);
			this.txtCAN.TabIndex = 1;
			this.txtCAN.Text = "";
			this.txtCAN.TextChanged += new System.EventHandler(this.textbox_TextChanged);
			// 
			// lblBillDate
			// 
			this.lblBillDate.Location = new System.Drawing.Point(8, 48);
			this.lblBillDate.Name = "lblBillDate";
			this.lblBillDate.Size = new System.Drawing.Size(112, 16);
			this.lblBillDate.TabIndex = 2;
			this.lblBillDate.Text = "Bill Date:";
			// 
			// txtBillDate
			// 
			this.txtBillDate.Location = new System.Drawing.Point(112, 48);
			this.txtBillDate.Name = "txtBillDate";
			this.txtBillDate.Size = new System.Drawing.Size(128, 21);
			this.txtBillDate.TabIndex = 3;
			this.txtBillDate.Text = "";
			// 
			// chkInFranchise
			// 
			this.chkInFranchise.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkInFranchise.Location = new System.Drawing.Point(264, 16);
			this.chkInFranchise.Name = "chkInFranchise";
			this.chkInFranchise.Size = new System.Drawing.Size(224, 16);
			this.chkInFranchise.TabIndex = 4;
			this.chkInFranchise.Text = "In Franchise";
			// 
			// lblInServiceDate
			// 
			this.lblInServiceDate.Location = new System.Drawing.Point(264, 48);
			this.lblInServiceDate.Name = "lblInServiceDate";
			this.lblInServiceDate.Size = new System.Drawing.Size(96, 16);
			this.lblInServiceDate.TabIndex = 5;
			this.lblInServiceDate.Text = "In Service Date:";
			// 
			// txtInServiceDate
			// 
			this.txtInServiceDate.Location = new System.Drawing.Point(368, 48);
			this.txtInServiceDate.Name = "txtInServiceDate";
			this.txtInServiceDate.Size = new System.Drawing.Size(104, 21);
			this.txtInServiceDate.TabIndex = 6;
			this.txtInServiceDate.Text = "";
			// 
			// lblBTN
			// 
			this.lblBTN.Location = new System.Drawing.Point(8, 80);
			this.lblBTN.Name = "lblBTN";
			this.lblBTN.Size = new System.Drawing.Size(88, 16);
			this.lblBTN.TabIndex = 7;
			this.lblBTN.Text = "BTN:";
			// 
			// mnuAccount
			// 
			this.mnuAccount.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem_New,
																					   this.EditItem,
																					   this.DeleteItem,
																					   this.HelpItem});
			// 
			// menuItem_New
			// 
			this.menuItem_New.Index = 0;
			this.menuItem_New.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			this.menuItem_New.Text = "&New";
			this.menuItem_New.Click += new System.EventHandler(this.AddWTNItem_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "WTN";
			// 
			// EditItem
			// 
			this.EditItem.Index = 1;
			this.EditItem.Text = "&Edit Selected Item";
			this.EditItem.Click += new System.EventHandler(this.EditItem_Click);
			// 
			// DeleteItem
			// 
			this.DeleteItem.Index = 2;
			this.DeleteItem.Text = "&Delete Selected Item";
			this.DeleteItem.Click += new System.EventHandler(this.DeleteItem_Click);
			// 
			// HelpItem
			// 
			this.HelpItem.Index = 3;
			this.HelpItem.Text = "&Help";
			this.HelpItem.Click += new System.EventHandler(this.HelpItem_Click);
			// 
			// imglstTVW
			// 
			this.imglstTVW.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imglstTVW.ImageSize = new System.Drawing.Size(16, 16);
			this.imglstTVW.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstTVW.ImageStream")));
			this.imglstTVW.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tvwAccount
			// 
			this.tvwAccount.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.tvwAccount.ContextMenu = this.mnuAccount;
			this.tvwAccount.HideSelection = false;
			this.tvwAccount.ImageList = this.imglstTVW;
			this.tvwAccount.Location = new System.Drawing.Point(8, 120);
			this.tvwAccount.Name = "tvwAccount";
			this.tvwAccount.Scrollable = false;
			this.tvwAccount.Size = new System.Drawing.Size(544, 272);
			this.tvwAccount.TabIndex = 9;
			this.tvwAccount.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwAccount_AfterSelect);
			// 
			// afcboBTN
			// 
			this.afcboBTN.BorderColor = System.Drawing.Color.Black;
			this.afcboBTN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.afcboBTN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.afcboBTN.Location = new System.Drawing.Point(112, 80);
			this.afcboBTN.Name = "afcboBTN";
			this.afcboBTN.Size = new System.Drawing.Size(128, 22);
			this.afcboBTN.TabIndex = 10;
			this.afcboBTN.TextColor = System.Drawing.Color.Black;
			// 
			// AccountCtl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.afcboBTN,
																		  this.tvwAccount,
																		  this.lblBTN,
																		  this.txtInServiceDate,
																		  this.lblInServiceDate,
																		  this.chkInFranchise,
																		  this.txtBillDate,
																		  this.lblBillDate,
																		  this.txtCAN,
																		  this.lblCAN});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "AccountCtl";
			this.Size = new System.Drawing.Size(560, 400);
			this.ResumeLayout(false);

		}
		#endregion

		#region events
		private void AddWTNItem_Click(object sender, System.EventArgs e)
		{
			Viewing.View add_view;
			add_view = (Viewing.View)_app.Views[ViewTypes.AddWTN];
			_app.LoadView(add_view);
		}

		private void OnNewItemClick(object sender, System.EventArgs e)
		{

		}

		private void EditItem_Click(object sender, System.EventArgs e)
		{
			((IEditable)this).Edit();
		}

		private void DeleteItem_Click(object sender, System.EventArgs e)
		{
			((IDeletable)this).Delete();
		}

		private void HelpItem_Click(object sender, System.EventArgs e)
		{
			((IForm)this).ShowHelp();
		}

		private void textbox_TextChanged(object sender, System.EventArgs e)
		{
			if(_form_state != FormStates.EditInProgress && _form_state != FormStates.Busy)
			{
				_form_state = FormStates.EditInProgress;
			}
		}
		#endregion

		private void tvwAccount_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if(tvwAccount.SelectedNode.Tag is WTN)
			{
				mnuAccount.MenuItems[2].Enabled =true;
				mnuAccount.MenuItems[3].Enabled =true;
				mnuAccount.MenuItems[2].Text = "Edit WTN";
				mnuAccount.MenuItems[3].Text = "Delete WTN";
			}
			else if(tvwAccount.SelectedNode.Tag is CurrentPlan)
			{
				mnuAccount.MenuItems[2].Enabled =true;
				mnuAccount.MenuItems[3].Enabled =true;
				mnuAccount.MenuItems[2].Text = "Edit Plan";
				mnuAccount.MenuItems[3].Text = "Delete Plan";
			}
			else if (tvwAccount.SelectedNode.Tag is CurrentPlanDetail)
			{
				mnuAccount.MenuItems[2].Enabled =true;
				mnuAccount.MenuItems[3].Enabled =true;
				mnuAccount.MenuItems[2].Text = "Edit Plan Detail";
				mnuAccount.MenuItems[3].Text = "Delete Plan";
			}
			else if(tvwAccount.SelectedNode.Tag is ProductType)
			{
				mnuAccount.MenuItems[2].Enabled =false;
				mnuAccount.MenuItems[3].Enabled =false;
			}
			else
			{
				mnuAccount.MenuItems[2].Enabled =false;
				mnuAccount.MenuItems[3].Enabled =false;
			}
		}
	}
}
