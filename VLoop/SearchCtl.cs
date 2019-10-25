using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopBusinessObjects;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;
using Afni.Formatting;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for SearchCtl.
	/// </summary>
	public class SearchCtl : UserControl , IForm, ISkinnable
	{
		private System.Windows.Forms.Label lblBTN;
		private System.Windows.Forms.TextBox txtBTN;
		private System.Windows.Forms.Label lblFirstName;
		private System.Windows.Forms.TextBox txtFirstName;
		private System.Windows.Forms.Label lblLastName;
		private System.Windows.Forms.TextBox txtLastName;
		private System.Windows.Forms.Label lblCompany;
		private System.Windows.Forms.TextBox txtCompany;
		private System.Windows.Forms.Label lblCity;
		private System.Windows.Forms.TextBox txtCity;
		private System.Windows.Forms.Label lblState;
		private System.Windows.Forms.ComboBox cboState;
		private System.Windows.Forms.Label lblZip;
		private System.Windows.Forms.TextBox txtZip;
		private Afni.FormData.FormStates _form_state;
		private System.Windows.Forms.Label lblBTNInstruct;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ImageList imglstSearch;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView lvwResults;
		private System.Windows.Forms.ColumnHeader hdrName;
		private System.Windows.Forms.ColumnHeader hdrBTN;
		private System.Windows.Forms.ColumnHeader hdrDisposition;
		private System.Windows.Forms.ColumnHeader hdrLastCall;
		private System.ComponentModel.IContainer components;
		private Afni.Applications.VLoop.DisplayTheme _theme;
		private Afni.Applications.VLoop.Application _app;
		private System.Windows.Forms.ColumnHeader hdrLoadDate;
		private Afni.Controls.afGoBtn afGoBtn;
		private Afni.Controls.afGoBtn afGoBtn2;

		private ArrayList _customers;

		public SearchCtl(Afni.Applications.VLoop.Application app)
		{
			_app = app;

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SearchCtl));
			this.lblBTN = new System.Windows.Forms.Label();
			this.txtBTN = new System.Windows.Forms.TextBox();
			this.lblFirstName = new System.Windows.Forms.Label();
			this.txtFirstName = new System.Windows.Forms.TextBox();
			this.lblLastName = new System.Windows.Forms.Label();
			this.txtLastName = new System.Windows.Forms.TextBox();
			this.lblCompany = new System.Windows.Forms.Label();
			this.txtCompany = new System.Windows.Forms.TextBox();
			this.lblCity = new System.Windows.Forms.Label();
			this.txtCity = new System.Windows.Forms.TextBox();
			this.lblState = new System.Windows.Forms.Label();
			this.cboState = new System.Windows.Forms.ComboBox();
			this.lblZip = new System.Windows.Forms.Label();
			this.txtZip = new System.Windows.Forms.TextBox();
			this.lvwResults = new System.Windows.Forms.ListView();
			this.hdrName = new System.Windows.Forms.ColumnHeader();
			this.hdrBTN = new System.Windows.Forms.ColumnHeader();
			this.hdrLoadDate = new System.Windows.Forms.ColumnHeader();
			this.hdrLastCall = new System.Windows.Forms.ColumnHeader();
			this.hdrDisposition = new System.Windows.Forms.ColumnHeader();
			this.imglstSearch = new System.Windows.Forms.ImageList(this.components);
			this.lblBTNInstruct = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.afGoBtn = new Afni.Controls.afGoBtn();
			this.afGoBtn2 = new Afni.Controls.afGoBtn();
			this.SuspendLayout();
			// 
			// lblBTN
			// 
			this.lblBTN.Location = new System.Drawing.Point(8, 80);
			this.lblBTN.Name = "lblBTN";
			this.lblBTN.Size = new System.Drawing.Size(56, 16);
			this.lblBTN.TabIndex = 0;
			this.lblBTN.Text = "BTN:";
			// 
			// txtBTN
			// 
			this.txtBTN.Location = new System.Drawing.Point(88, 80);
			this.txtBTN.Name = "txtBTN";
			this.txtBTN.Size = new System.Drawing.Size(136, 21);
			this.txtBTN.TabIndex = 1;
			this.txtBTN.Text = "";
			// 
			// lblFirstName
			// 
			this.lblFirstName.Location = new System.Drawing.Point(8, 136);
			this.lblFirstName.Name = "lblFirstName";
			this.lblFirstName.Size = new System.Drawing.Size(80, 16);
			this.lblFirstName.TabIndex = 2;
			this.lblFirstName.Text = "First Name:";
			// 
			// txtFirstName
			// 
			this.txtFirstName.Location = new System.Drawing.Point(88, 136);
			this.txtFirstName.Name = "txtFirstName";
			this.txtFirstName.Size = new System.Drawing.Size(136, 21);
			this.txtFirstName.TabIndex = 3;
			this.txtFirstName.Text = "";
			// 
			// lblLastName
			// 
			this.lblLastName.Location = new System.Drawing.Point(8, 168);
			this.lblLastName.Name = "lblLastName";
			this.lblLastName.Size = new System.Drawing.Size(64, 16);
			this.lblLastName.TabIndex = 4;
			this.lblLastName.Text = "Last Name:";
			// 
			// txtLastName
			// 
			this.txtLastName.Location = new System.Drawing.Point(88, 168);
			this.txtLastName.Name = "txtLastName";
			this.txtLastName.Size = new System.Drawing.Size(136, 21);
			this.txtLastName.TabIndex = 5;
			this.txtLastName.Text = "";
			// 
			// lblCompany
			// 
			this.lblCompany.Location = new System.Drawing.Point(8, 200);
			this.lblCompany.Name = "lblCompany";
			this.lblCompany.Size = new System.Drawing.Size(80, 16);
			this.lblCompany.TabIndex = 6;
			this.lblCompany.Text = "Company:";
			// 
			// txtCompany
			// 
			this.txtCompany.Location = new System.Drawing.Point(88, 200);
			this.txtCompany.Name = "txtCompany";
			this.txtCompany.Size = new System.Drawing.Size(136, 21);
			this.txtCompany.TabIndex = 7;
			this.txtCompany.Text = "";
			// 
			// lblCity
			// 
			this.lblCity.Location = new System.Drawing.Point(248, 136);
			this.lblCity.Name = "lblCity";
			this.lblCity.Size = new System.Drawing.Size(40, 16);
			this.lblCity.TabIndex = 8;
			this.lblCity.Text = "City:";
			// 
			// txtCity
			// 
			this.txtCity.Location = new System.Drawing.Point(304, 136);
			this.txtCity.Name = "txtCity";
			this.txtCity.Size = new System.Drawing.Size(200, 21);
			this.txtCity.TabIndex = 9;
			this.txtCity.Text = "";
			// 
			// lblState
			// 
			this.lblState.Location = new System.Drawing.Point(248, 176);
			this.lblState.Name = "lblState";
			this.lblState.Size = new System.Drawing.Size(40, 16);
			this.lblState.TabIndex = 10;
			this.lblState.Text = "State:";
			// 
			// cboState
			// 
			this.cboState.Location = new System.Drawing.Point(304, 168);
			this.cboState.Name = "cboState";
			this.cboState.Size = new System.Drawing.Size(80, 21);
			this.cboState.TabIndex = 11;
			// 
			// lblZip
			// 
			this.lblZip.Location = new System.Drawing.Point(248, 200);
			this.lblZip.Name = "lblZip";
			this.lblZip.Size = new System.Drawing.Size(80, 16);
			this.lblZip.TabIndex = 12;
			this.lblZip.Text = "Zip:";
			// 
			// txtZip
			// 
			this.txtZip.Location = new System.Drawing.Point(304, 200);
			this.txtZip.Name = "txtZip";
			this.txtZip.Size = new System.Drawing.Size(80, 21);
			this.txtZip.TabIndex = 13;
			this.txtZip.Text = "";
			// 
			// lvwResults
			// 
			this.lvwResults.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lvwResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.hdrName,
																						 this.hdrBTN,
																						 this.hdrLoadDate,
																						 this.hdrLastCall,
																						 this.hdrDisposition});
			this.lvwResults.FullRowSelect = true;
			this.lvwResults.HideSelection = false;
			this.lvwResults.Location = new System.Drawing.Point(8, 256);
			this.lvwResults.MultiSelect = false;
			this.lvwResults.Name = "lvwResults";
			this.lvwResults.Size = new System.Drawing.Size(536, 312);
			this.lvwResults.TabIndex = 14;
			this.lvwResults.View = System.Windows.Forms.View.Details;
			this.lvwResults.DoubleClick += new System.EventHandler(this.searchItem_Selected);
			// 
			// hdrName
			// 
			this.hdrName.Text = "Customer Name";
			this.hdrName.Width = 150;
			// 
			// hdrBTN
			// 
			this.hdrBTN.Text = "BTN";
			this.hdrBTN.Width = 100;
			// 
			// hdrLoadDate
			// 
			this.hdrLoadDate.Text = "Load Date";
			this.hdrLoadDate.Width = 80;
			// 
			// hdrLastCall
			// 
			this.hdrLastCall.Text = "Disp. Date";
			this.hdrLastCall.Width = 80;
			// 
			// hdrDisposition
			// 
			this.hdrDisposition.Text = "Disposition";
			this.hdrDisposition.Width = 150;
			// 
			// imglstSearch
			// 
			this.imglstSearch.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imglstSearch.ImageSize = new System.Drawing.Size(24, 24);
			this.imglstSearch.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSearch.ImageStream")));
			this.imglstSearch.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// lblBTNInstruct
			// 
			this.lblBTNInstruct.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lblBTNInstruct.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblBTNInstruct.Location = new System.Drawing.Point(8, 56);
			this.lblBTNInstruct.Name = "lblBTNInstruct";
			this.lblBTNInstruct.Size = new System.Drawing.Size(536, 23);
			this.lblBTNInstruct.TabIndex = 16;
			this.lblBTNInstruct.Text = "If you know the BTN, enter it here.";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 112);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(552, 16);
			this.label1.TabIndex = 18;
			this.label1.Text = "otherwise, enter all the information you know here:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 240);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(256, 16);
			this.label2.TabIndex = 19;
			this.label2.Text = "Double click the customer to open the account:";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Location = new System.Drawing.Point(8, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(424, 32);
			this.label3.TabIndex = 20;
			this.label3.Text = "Search for a Customer";
			// 
			// afGoBtn
			// 
			this.afGoBtn.Cursor = System.Windows.Forms.Cursors.Hand;
			this.afGoBtn.Location = new System.Drawing.Point(232, 80);
			this.afGoBtn.Name = "afGoBtn";
			this.afGoBtn.Size = new System.Drawing.Size(81, 24);
			this.afGoBtn.TabIndex = 22;
			this.afGoBtn.Text = "&Find BTN";
			// 
			// afGoBtn2
			// 
			this.afGoBtn2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.afGoBtn2.Location = new System.Drawing.Point(392, 200);
			this.afGoBtn2.Name = "afGoBtn2";
			this.afGoBtn2.Size = new System.Drawing.Size(25, 24);
			this.afGoBtn2.TabIndex = 23;
			// 
			// SearchCtl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.afGoBtn2,
																		  this.afGoBtn,
																		  this.label3,
																		  this.label2,
																		  this.label1,
																		  this.lblBTNInstruct,
																		  this.lvwResults,
																		  this.txtZip,
																		  this.lblZip,
																		  this.cboState,
																		  this.lblState,
																		  this.txtCity,
																		  this.lblCity,
																		  this.txtCompany,
																		  this.lblCompany,
																		  this.txtLastName,
																		  this.lblLastName,
																		  this.txtFirstName,
																		  this.lblFirstName,
																		  this.txtBTN,
																		  this.lblBTN});
			this.DockPadding.All = 3;
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "SearchCtl";
			this.Size = new System.Drawing.Size(552, 576);
			this.ResumeLayout(false);

		}
		#endregion

		private void searchItem_Selected(object sender, System.EventArgs e)
		{
			SearchItem item;
			CustomerCampaignBase custCampaign;
			CustomerCampaignBSO bso = new CustomerCampaignBSO();
			
			item = (SearchItem)lvwResults.SelectedItems[0].Tag;
			custCampaign = bso.GetByID(item.CustomerCampaignID);
			_app.Call.StartCall(custCampaign);
		}

		private void btnBTNSearch_Click(object sender, System.EventArgs e)
		{
			_app.SetBusy("Searching for customers...");
			_customers = new ArrayList();
			IManager bso = new CustomerBSO();
			Hashtable parms = new Hashtable();

			parms.Add("btn", AfFormat.ToUnmaskedPhoneNumber(txtBTN.Text));
			_customers = (ArrayList)bso.Search(parms);
			FillListView();
			_app.SetIdle();
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			_app.SetBusy("Searching for customers...");
			_customers = new ArrayList();
			CustomerBSO bso = new CustomerBSO();
			//_customers = bso.GetFromSearch(txtFirstName.Text,
			//								txtLastName.Text,"",cboState.Text,txtCity.Text,txtZip.Text,"","","","",txtCompany.Text,_app.CurrentCampaign.CampaignID);
											
			FillListView();
			_app.SetIdle();
		}

		
		private void FillListView()
		{
			ListViewItem item;
			lvwResults.Items.Clear();
			foreach(SearchItem customer in _customers)
			{
				item = new ListViewItem();
				item.Text = customer.FirstName + " " + customer.LastName;
				item.SubItems.Add(AfFormat.ToMaskedPhoneNumber( customer.BTN));
				item.SubItems.Add(customer.LoadDate.ToShortDateString());
				item.SubItems.Add(customer.DispositionDate.ToShortDateString());
				item.SubItems.Add(customer.CurrentDisposition);
				item.Tag = customer;
				lvwResults.Items.Add(item);
			}
		}

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
						txtBTN.BorderStyle = BorderStyle.FixedSingle;
						txtCity.BorderStyle = BorderStyle.FixedSingle;
						txtCompany.BorderStyle = BorderStyle.FixedSingle;
						txtFirstName.BorderStyle = BorderStyle.FixedSingle;
						txtLastName.BorderStyle = BorderStyle.FixedSingle;
						txtZip.BorderStyle = BorderStyle.FixedSingle;
						lvwResults.BorderStyle = BorderStyle.FixedSingle;
					}
					else
					{
						txtBTN.BorderStyle = BorderStyle.Fixed3D;
						txtCity.BorderStyle = BorderStyle.Fixed3D;
						txtCompany.BorderStyle = BorderStyle.Fixed3D;
						txtFirstName.BorderStyle = BorderStyle.Fixed3D;
						txtLastName.BorderStyle = BorderStyle.Fixed3D;
						txtZip.BorderStyle = BorderStyle.Fixed3D;
						lvwResults.BorderStyle = BorderStyle.Fixed3D;
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

		#region IForm implementation
		string IForm.Name
		{
			get { return "Customer Search"; }
			set {}
		}

		bool IForm.Refresh()
		{
			lvwResults.Items.Clear();
			txtBTN.Text = "";
			txtCity.Text = "";
			txtCompany.Text = "";
			txtFirstName.Text = "";
			txtLastName.Text = "";
			txtZip.Text = "";
			return true;
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
	}
}
