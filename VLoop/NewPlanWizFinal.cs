using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;
using Afni.Applications.VLoop;
using Afni.Applications.VLoop.Viewing;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;


namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for NewPlanWizFinal.
	/// </summary>
	public class NewPlanWizFinalCtl : UserControl , IForm, ISkinnable
	{
		private System.Windows.Forms.Label lblWTNSELInstruct;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel panelDetails;
		private FormStates _form_state = FormStates.Idle;
		private Afni.Applications.VLoop.Application _app;
		private Afni.Applications.VLoop.DisplayTheme _theme;
		private System.Windows.Forms.Button _btnFinish;
		private System.Windows.Forms.Button _btnBack;
		private System.Windows.Forms.Button _btnCancel;
		private System.Windows.Forms.ComboBox _cboWTN;
		private Afni.Applications.VLoop.NewPlanManager _wiz;
		private Afni.Applications.VLoop.VLoopSmartDTO.CurrentPlan _plan;
		private System.Windows.Forms.Label _lblName;
		private System.Windows.Forms.Label _lblType;
		private System.Windows.Forms.Label _lblID;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NewPlanWizFinalCtl(Afni.Applications.VLoop.Application app)
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

		public NewPlanManager PlanWiz
		{
			get { return _wiz; }
			set { _wiz = value; }
		}

		public CurrentPlan NewPlan
		{
			get { return _plan; }
			set { _plan = value; }
		}

		public WTN SelectedWTN
		{
			get { return (WTN)_cboWTN.SelectedItem; }
		}

		#region IForm implementation
		bool IForm.Refresh()
		{
			_lblID.Text = _plan.ProdTypeDescription;
			_lblName.Text = _plan.Description;
			_lblType.Text = _plan.ProdTypeDescription;
			_cboWTN.DataSource = _app.Call.CurrentCustCampaign.Customer.Account.WTNs;
			return true;
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		string IForm.Name
		{
			get { return "New Plan Confirmation"; }
			set {}
		}

		FormStates IForm.FormState
		{
			get { return _form_state; }
		}

		#endregion

		#region ISkinnable implementation
		bool ISkinnable.ApplyTheme(DisplayTheme theme)
		{
			bool theme_ok = true;
			try
			{
				this.BackColor = theme.FormBackColor;
				if(theme.FlatControls)
				{
					_btnFinish.FlatStyle = FlatStyle.Flat;
					_btnBack.FlatStyle = FlatStyle.Flat;
					_btnCancel.FlatStyle = FlatStyle.Flat;
				}
				else
				{
					_btnFinish.FlatStyle = FlatStyle.Standard;
					_btnBack.FlatStyle = FlatStyle.Standard;
					_btnCancel.FlatStyle = FlatStyle.Standard;
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
			this._btnFinish = new System.Windows.Forms.Button();
			this._btnBack = new System.Windows.Forms.Button();
			this._btnCancel = new System.Windows.Forms.Button();
			this.lblWTNSELInstruct = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this._cboWTN = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.panelDetails = new System.Windows.Forms.Panel();
			this._lblName = new System.Windows.Forms.Label();
			this._lblType = new System.Windows.Forms.Label();
			this._lblID = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _btnFinish
			// 
			this._btnFinish.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this._btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._btnFinish.Location = new System.Drawing.Point(344, 440);
			this._btnFinish.Name = "_btnFinish";
			this._btnFinish.Size = new System.Drawing.Size(72, 24);
			this._btnFinish.TabIndex = 0;
			this._btnFinish.Text = "&Finish";
			this._btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
			// 
			// _btnBack
			// 
			this._btnBack.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this._btnBack.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._btnBack.Location = new System.Drawing.Point(272, 440);
			this._btnBack.Name = "_btnBack";
			this._btnBack.Size = new System.Drawing.Size(72, 24);
			this._btnBack.TabIndex = 1;
			this._btnBack.Text = "< &Back";
			this._btnBack.Click += new System.EventHandler(this._btnBack_Click);
			// 
			// _btnCancel
			// 
			this._btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this._btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._btnCancel.Location = new System.Drawing.Point(432, 440);
			this._btnCancel.Name = "_btnCancel";
			this._btnCancel.Size = new System.Drawing.Size(72, 24);
			this._btnCancel.TabIndex = 2;
			this._btnCancel.Text = "&Cancel";
			// 
			// lblWTNSELInstruct
			// 
			this.lblWTNSELInstruct.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lblWTNSELInstruct.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblWTNSELInstruct.Location = new System.Drawing.Point(8, 328);
			this.lblWTNSELInstruct.Name = "lblWTNSELInstruct";
			this.lblWTNSELInstruct.Size = new System.Drawing.Size(488, 32);
			this.lblWTNSELInstruct.TabIndex = 3;
			this.lblWTNSELInstruct.Text = "If the above information is correct, select a WTN to add the plan to, and press t" +
				"he \"Finish\" button.";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.groupBox1.Location = new System.Drawing.Point(0, 416);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(512, 8);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 376);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Select a WTN:";
			// 
			// _cboWTN
			// 
			this._cboWTN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboWTN.Location = new System.Drawing.Point(96, 376);
			this._cboWTN.Name = "_cboWTN";
			this._cboWTN.Size = new System.Drawing.Size(160, 21);
			this._cboWTN.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Location = new System.Drawing.Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(496, 32);
			this.label2.TabIndex = 7;
			this.label2.Text = "Confirm new plan and select a WTN.";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(352, 16);
			this.label3.TabIndex = 8;
			this.label3.Text = "Review the details of the plan you are adding to the account:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Plan Name:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 144);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 16);
			this.label5.TabIndex = 10;
			this.label5.Text = "Plan Type:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 176);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 16);
			this.label6.TabIndex = 11;
			this.label6.Text = "Plan ID:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 208);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 16);
			this.label7.TabIndex = 12;
			this.label7.Text = "Plan Details:";
			// 
			// panelDetails
			// 
			this.panelDetails.AutoScroll = true;
			this.panelDetails.Location = new System.Drawing.Point(88, 208);
			this.panelDetails.Name = "panelDetails";
			this.panelDetails.Size = new System.Drawing.Size(416, 112);
			this.panelDetails.TabIndex = 13;
			// 
			// _lblName
			// 
			this._lblName.Location = new System.Drawing.Point(88, 112);
			this._lblName.Name = "_lblName";
			this._lblName.Size = new System.Drawing.Size(416, 16);
			this._lblName.TabIndex = 14;
			this._lblName.Text = "_lblName";
			// 
			// _lblType
			// 
			this._lblType.Location = new System.Drawing.Point(88, 144);
			this._lblType.Name = "_lblType";
			this._lblType.Size = new System.Drawing.Size(416, 16);
			this._lblType.TabIndex = 15;
			this._lblType.Text = "label9";
			// 
			// _lblID
			// 
			this._lblID.Location = new System.Drawing.Point(88, 176);
			this._lblID.Name = "_lblID";
			this._lblID.Size = new System.Drawing.Size(360, 16);
			this._lblID.TabIndex = 16;
			this._lblID.Text = "label8";
			// 
			// NewPlanWizFinalCtl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._lblID,
																		  this._lblType,
																		  this._lblName,
																		  this.panelDetails,
																		  this.label7,
																		  this.label6,
																		  this.label5,
																		  this.label4,
																		  this.label3,
																		  this.label2,
																		  this._cboWTN,
																		  this.label1,
																		  this.groupBox1,
																		  this.lblWTNSELInstruct,
																		  this._btnCancel,
																		  this._btnBack,
																		  this._btnFinish});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "NewPlanWizFinalCtl";
			this.Size = new System.Drawing.Size(520, 480);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnFinish_Click(object sender, System.EventArgs e)
		{
			_wiz.Commit();
		}

		private void _btnBack_Click(object sender, System.EventArgs e)
		{
			_wiz.MovePrevStep();
		}
	}
}
