using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;
using Afni.DataUtility;
using Afni.Applications.VLoop.Viewing;
using Afni.Applications.VLoop.VLoopBusinessObjects;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Controls;

namespace Afni.Applications.VLoop
{
	
	public class NewPlanWiz1Ctl : System.Windows.Forms.UserControl, IForm
	{
		private System.Windows.Forms.ImageList imglstNewPlan;
		private System.Windows.Forms.Label lblInstructions1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox lstPlan;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label lblHdr;
		private FormStates _form_state = FormStates.Idle;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.Button btnCancel;
		private Afni.Applications.VLoop.Application _app;
		private Afni.Applications.VLoop.NewPlanManager _wiz;
		private Product _prod;

		public NewPlanWiz1Ctl(Afni.Applications.VLoop.Application app)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			_app = app;
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

		#region Properties
		public NewPlanManager WizMgr
		{
			get { return _wiz; }
			set { _wiz = value; }
		}

		public Product SelectedProduct
		{
			get { return _prod; }
		}
		#endregion

		#region IForm implementation
			bool IForm.Refresh()
		{
			int start_x = 8;
			int first_col_width = 0;
			int start_y = 90;
			int end_y = 96;
			int col = 1;
			int row = 0;
			int rows = 1;

			//refresh the list box
			lstPlan.Items.Clear();
			foreach(Product product in _app.CurrentCampaign.Products)
				lstPlan.Items.Add(product);
			

			Graphics g = Graphics.FromHwnd(this.Handle);
			IManager bso = new ProdTypeBSO();
			ArrayList types;
			DisplayTheme theme = _app.Theme;
			
			types = (ArrayList)bso.GetAll();		//product types
			rows = types.Count / 2;
			foreach(ProductType type in types)
			{
				AfniLink link = new AfniLink();
				link.Text = type.ProductTypeDescription;
				link.Height = 20;
				link.Width = (int)g.MeasureString(link.Text,link.Font).Width + 50;
				if(link.Width > first_col_width)
					first_col_width = link.Width;
				link.Tag = type;
				link.Icon = VLoopIcons.Next;
				link.ForeColor = Color.Blue;
				link.LinkColor = Color.Blue;
				link.ActiveLinkColor = Color.Blue;
				link.Font = new Font("Tahoma",8.25F);
				link.Left = (col == 1 ? start_x : first_col_width + 20);
				link.Top = start_y + (row * (link.Height + 10));
				link.LinkClicked += new EventHandler(this.OnLinkClicked);
				this.Controls.Add(link);
				if(row == (rows-1))
				{
					row = 0;
					col = 2;
				}
				else
					row++;
			}

			return true;
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		string IForm.Name
		{
			get { return "Add a new Plan"; }
			set {}
		}

		FormStates IForm.FormState
		{
			get { return _form_state; }
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NewPlanWiz1Ctl));
			this.imglstNewPlan = new System.Windows.Forms.ImageList(this.components);
			this.lblInstructions1 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lstPlan = new System.Windows.Forms.ListBox();
			this.lblHdr = new System.Windows.Forms.Label();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnBack = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// imglstNewPlan
			// 
			this.imglstNewPlan.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imglstNewPlan.ImageSize = new System.Drawing.Size(16, 16);
			this.imglstNewPlan.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstNewPlan.ImageStream")));
			this.imglstNewPlan.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// lblInstructions1
			// 
			this.lblInstructions1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblInstructions1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblInstructions1.Location = new System.Drawing.Point(8, 64);
			this.lblInstructions1.Name = "lblInstructions1";
			this.lblInstructions1.Size = new System.Drawing.Size(280, 16);
			this.lblInstructions1.TabIndex = 3;
			this.lblInstructions1.Text = "Choose from the types of plans below...";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label1.Location = new System.Drawing.Point(8, 152);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(256, 24);
			this.label1.TabIndex = 6;
			this.label1.Text = "or select a plan from the list:";
			// 
			// lstPlan
			// 
			this.lstPlan.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lstPlan.IntegralHeight = false;
			this.lstPlan.Location = new System.Drawing.Point(8, 176);
			this.lstPlan.Name = "lstPlan";
			this.lstPlan.ScrollAlwaysVisible = true;
			this.lstPlan.Size = new System.Drawing.Size(416, 256);
			this.lstPlan.Sorted = true;
			this.lstPlan.TabIndex = 7;
			// 
			// lblHdr
			// 
			this.lblHdr.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblHdr.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.lblHdr.Location = new System.Drawing.Point(8, 8);
			this.lblHdr.Name = "lblHdr";
			this.lblHdr.Size = new System.Drawing.Size(440, 24);
			this.lblHdr.TabIndex = 0;
			this.lblHdr.Text = "What type of plan do you want to add?";
			// 
			// btnNext
			// 
			this.btnNext.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnNext.BackColor = System.Drawing.SystemColors.Control;
			this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnNext.Location = new System.Drawing.Point(264, 448);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(72, 24);
			this.btnNext.TabIndex = 8;
			this.btnNext.Text = "&Next >";
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// btnBack
			// 
			this.btnBack.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnBack.BackColor = System.Drawing.SystemColors.Control;
			this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnBack.Location = new System.Drawing.Point(192, 448);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(72, 24);
			this.btnBack.TabIndex = 9;
			this.btnBack.Text = "< &Back";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(352, 448);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "&Cancel";
			// 
			// NewPlanWiz1Ctl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnCancel,
																		  this.btnBack,
																		  this.btnNext,
																		  this.lstPlan,
																		  this.label1,
																		  this.lblInstructions1,
																		  this.lblHdr});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "NewPlanWiz1Ctl";
			this.Size = new System.Drawing.Size(432, 480);
			this.ResumeLayout(false);

		}
		#endregion

		#region Events
		private void OnLinkClicked(object sender, EventArgs e)
		{
			_prod = (Product)(((AfniLink)sender).Tag);
			_wiz.MoveNextStep();	
		}

		private void btnNext_Click(object sender, System.EventArgs e)
		{
			_prod = (Product)lstPlan.SelectedItem;
			_wiz.MoveNextStep();
		}
		#endregion
	}
}
