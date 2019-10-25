using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;
using Afni.ControlPlacer;
using Afni.ControlPlacer.Questions;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for CustomerCtl.
	/// </summary>
	public class CustomerCtl : UserControl, IForm, ISaveable, ISkinnable
	{
		private Afni.FormData.FormStates _form_state;
		private System.Windows.Forms.Label label1;
		private Afni.Controls.AfniDivider afniDivider1;
		private Afni.Applications.VLoop.DisplayTheme _theme;
		private System.Collections.ArrayList _observers;
		private Afni.Applications.VLoop.Application _app;
		private Customer _cust;
		private System.Windows.Forms.TextBox txtServZip;
		private System.Windows.Forms.Label lblServZip;
		private System.Windows.Forms.TextBox txtBillZip;
		private System.Windows.Forms.Label lblBillingZip;
		private Afni.Controls.AfniFlatCombo cboServState;
		private System.Windows.Forms.Label lblServState;
		private Afni.Controls.AfniFlatCombo cboBillState;
		private System.Windows.Forms.Label lblBillState;
		private System.Windows.Forms.TextBox txtServCity;
		private System.Windows.Forms.Label lblServiceCity;
		private System.Windows.Forms.TextBox txtBillCity;
		private System.Windows.Forms.Label lblBillingCity;
		private System.Windows.Forms.TextBox txtService2;
		private System.Windows.Forms.Label lblService2;
		private System.Windows.Forms.TextBox txtBilling2;
		private System.Windows.Forms.Label lblBilling2;
		private System.Windows.Forms.TextBox txtService1;
		private System.Windows.Forms.Label lblService1;
		private System.Windows.Forms.CheckBox chkCopyBilling;
		private System.Windows.Forms.TextBox txtBilling1;
		private System.Windows.Forms.Label BillinglblAddress1;
		private System.Windows.Forms.Label lblFormerTDM;
		private System.Windows.Forms.CheckBox chkDoNotCall;
		private System.Windows.Forms.TextBox txtAltPhone;
		private System.Windows.Forms.Label lblAltPhone;
		private Afni.Controls.AfniFlatCombo cboLanguage;
		private System.Windows.Forms.Label lblLanguage;
		private System.Windows.Forms.TextBox txtEmail;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.TextBox txtCompany;
		private System.Windows.Forms.Label lblCompany;
		private System.Windows.Forms.TextBox txtLastName;
		private System.Windows.Forms.Label lblLastName;
		private System.Windows.Forms.TextBox txtFirstName;
		private System.Windows.Forms.Label lblFirstName;
		private System.Windows.Forms.TextBox txtFormerTDM;
		private System.Windows.Forms.Panel AddressPanel;
		private System.Windows.Forms.Panel CustPanel;
		private System.Windows.Forms.Label label2;
		private Afni.Controls.AfniDivider afniDivider2;
		private Area _dyn_area;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CustomerCtl(Afni.Applications.VLoop.Application app)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			_observers = new ArrayList();
			_form_state = FormStates.Idle;
			_app = app;
			_app.CampaignSwitched += new EventHandler(this.app_CampaignChanged);
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

		#region Private Methods
		private void RefreshDynCtls()
		{
			ILookupQuestion lookup;
			Afni.ControlPlacer.Questions.Question dyn_q;
			_dyn_area = new Area(3, this, new Point(16,400));

			foreach(CustomerQuestion cq in _app.CurrentCampaign.CustomerQuestions)
			{
				dyn_q = _dyn_area.AddQuestion(cq.QuestionID,
										cq.QuestionText,
										VLoopDynControlTypes.FromControlTypeID(cq.QuestionTypeID),
										cq.Sequence);

				foreach(AvailableLookupInfo ali in cq.AvailableLookups)
				{
					//cast to an ILookup
					lookup = (ILookupQuestion)dyn_q;

					//add the lookup
					lookup.AddQuestionData(ali.AvailableLookupInfoID,
						ali.LookupValue);
				}

			}

			_dyn_area.Draw();
		}
		#endregion

		#region IForm implementation
		bool IForm.Refresh()
		{
			_form_state = FormStates.Busy;
			_cust = _app.Call.CurrentCustCampaign.Customer;
			txtAltPhone.Text = _cust.AltPhone;
			txtBillCity.Text = _cust.BillingCity;
			txtBilling1.Text = _cust.BillingAddress1;
			txtBilling2.Text = _cust.BillingAddress2;
			txtBillZip.Text = _cust.BillingZip;
			txtCompany.Text = _cust.Company;
			txtEmail.Text = _cust.Email;
			txtFirstName.Text = _cust.FirstName;
			txtFormerTDM.Text = _cust.FormerTDM;
			txtLastName.Text = _cust.LastName;
			txtServCity.Text = _cust.ServiceCity;
			txtService1.Text = _cust.ServiceAddress1;
			txtService2.Text = _cust.ServiceAddress2;
			txtServZip.Text = _cust.ServiceZip;
			chkDoNotCall.Checked = _cust.DoNotCall;
			_form_state = FormStates.Idle;
			
			foreach(CustomerAnswer dynAns in _cust.CustomerAnswers)
			{
				if(dynAns.LookupID > 0)
					_dyn_area.AddAnswer(dynAns.QuestionID,dynAns.LookupID);
				else
					_dyn_area.AddAnswer(dynAns.QuestionID,dynAns.AnswerText);
			}

			return true;
		}

		string IForm.Name
		{
			get { return "Customer"; }
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

		#region ISaveable implementation
		bool ISaveable.Save()
		{

			//update the form state before we fire the
			//event, in case the listener wants our updated
			//form state.
			_form_state = FormStates.Idle;
			if(SaveSucceeded != null)
				SaveSucceeded(this,null);
			return true;
		}

		bool ISaveable.Undo()
		{
			_form_state = FormStates.Busy;
			((IForm)this).Refresh();
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
				afniDivider1.FirstColor = theme.DividerDarkColor;
				afniDivider1.SecondColor = theme.DividerLightColor;
				afniDivider2.FirstColor = theme.DividerDarkColor;
				afniDivider2.SecondColor = theme.DividerLightColor;

				//apply flat control properties only if we're not
				//on win xp
				if(!_app.IsOnXP)
				{
					if(theme.FlatControls)
					{
						txtAltPhone.BorderStyle = BorderStyle.FixedSingle;
						txtBillCity.BorderStyle = BorderStyle.FixedSingle;
						txtBilling1.BorderStyle = BorderStyle.FixedSingle;
						txtBilling2.BorderStyle = BorderStyle.FixedSingle;
						txtBillZip.BorderStyle = BorderStyle.FixedSingle;
						txtCompany.BorderStyle = BorderStyle.FixedSingle;
						txtEmail.BorderStyle = BorderStyle.FixedSingle;
						txtFirstName.BorderStyle = BorderStyle.FixedSingle;
						txtFormerTDM.BorderStyle = BorderStyle.FixedSingle;
						txtLastName.BorderStyle = BorderStyle.FixedSingle;
						txtServCity.BorderStyle = BorderStyle.FixedSingle;
						txtService1.BorderStyle = BorderStyle.FixedSingle;
						txtService2.BorderStyle = BorderStyle.FixedSingle;
						txtServZip.BorderStyle = BorderStyle.FixedSingle;
						chkDoNotCall.FlatStyle = FlatStyle.Flat;
						chkCopyBilling.FlatStyle = FlatStyle.Flat;
					}
					else
					{
						txtAltPhone.BorderStyle = BorderStyle.Fixed3D;
						txtBillCity.BorderStyle = BorderStyle.Fixed3D;
						txtBilling1.BorderStyle = BorderStyle.Fixed3D;
						txtBilling2.BorderStyle = BorderStyle.Fixed3D;
						txtBillZip.BorderStyle = BorderStyle.Fixed3D;
						txtCompany.BorderStyle = BorderStyle.Fixed3D;
						txtEmail.BorderStyle = BorderStyle.Fixed3D;
						txtFirstName.BorderStyle = BorderStyle.Fixed3D;
						txtFormerTDM.BorderStyle = BorderStyle.Fixed3D;
						txtLastName.BorderStyle = BorderStyle.Fixed3D;
						txtServCity.BorderStyle = BorderStyle.Fixed3D;
						txtService1.BorderStyle = BorderStyle.Fixed3D;
						txtService2.BorderStyle = BorderStyle.Fixed3D;
						txtServZip.BorderStyle = BorderStyle.Fixed3D;
						chkDoNotCall.FlatStyle = FlatStyle.Standard;
						chkCopyBilling.FlatStyle = FlatStyle.Standard;
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
			this.label1 = new System.Windows.Forms.Label();
			this.afniDivider1 = new Afni.Controls.AfniDivider();
			this.AddressPanel = new System.Windows.Forms.Panel();
			this.txtServZip = new System.Windows.Forms.TextBox();
			this.lblServZip = new System.Windows.Forms.Label();
			this.txtBillZip = new System.Windows.Forms.TextBox();
			this.lblBillingZip = new System.Windows.Forms.Label();
			this.cboServState = new Afni.Controls.AfniFlatCombo();
			this.lblServState = new System.Windows.Forms.Label();
			this.cboBillState = new Afni.Controls.AfniFlatCombo();
			this.lblBillState = new System.Windows.Forms.Label();
			this.txtServCity = new System.Windows.Forms.TextBox();
			this.lblServiceCity = new System.Windows.Forms.Label();
			this.txtBillCity = new System.Windows.Forms.TextBox();
			this.lblBillingCity = new System.Windows.Forms.Label();
			this.txtService2 = new System.Windows.Forms.TextBox();
			this.lblService2 = new System.Windows.Forms.Label();
			this.txtBilling2 = new System.Windows.Forms.TextBox();
			this.lblBilling2 = new System.Windows.Forms.Label();
			this.txtService1 = new System.Windows.Forms.TextBox();
			this.lblService1 = new System.Windows.Forms.Label();
			this.chkCopyBilling = new System.Windows.Forms.CheckBox();
			this.txtBilling1 = new System.Windows.Forms.TextBox();
			this.BillinglblAddress1 = new System.Windows.Forms.Label();
			this.CustPanel = new System.Windows.Forms.Panel();
			this.lblFormerTDM = new System.Windows.Forms.Label();
			this.chkDoNotCall = new System.Windows.Forms.CheckBox();
			this.txtAltPhone = new System.Windows.Forms.TextBox();
			this.lblAltPhone = new System.Windows.Forms.Label();
			this.cboLanguage = new Afni.Controls.AfniFlatCombo();
			this.lblLanguage = new System.Windows.Forms.Label();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.lblEmail = new System.Windows.Forms.Label();
			this.txtCompany = new System.Windows.Forms.TextBox();
			this.lblCompany = new System.Windows.Forms.Label();
			this.txtLastName = new System.Windows.Forms.TextBox();
			this.lblLastName = new System.Windows.Forms.Label();
			this.txtFirstName = new System.Windows.Forms.TextBox();
			this.lblFirstName = new System.Windows.Forms.Label();
			this.txtFormerTDM = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.afniDivider2 = new Afni.Controls.AfniDivider();
			this.AddressPanel.SuspendLayout();
			this.CustPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 168);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 16);
			this.label1.TabIndex = 89;
			this.label1.Text = "Address Information";
			// 
			// afniDivider1
			// 
			this.afniDivider1.DividerStyle = Afni.Controls.DividerStyles.Horizontal;
			this.afniDivider1.FirstColor = System.Drawing.Color.FromArgb(((System.Byte)(1)), ((System.Byte)(72)), ((System.Byte)(178)));
			this.afniDivider1.Location = new System.Drawing.Point(136, 176);
			this.afniDivider1.Name = "afniDivider1";
			this.afniDivider1.SecondColor = System.Drawing.Color.White;
			this.afniDivider1.Size = new System.Drawing.Size(440, 1);
			this.afniDivider1.TabIndex = 90;
			this.afniDivider1.Text = "afDiv";
			// 
			// AddressPanel
			// 
			this.AddressPanel.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.AddressPanel.BackColor = System.Drawing.Color.Transparent;
			this.AddressPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
																					   this.txtServZip,
																					   this.lblServZip,
																					   this.txtBillZip,
																					   this.lblBillingZip,
																					   this.cboServState,
																					   this.lblServState,
																					   this.cboBillState,
																					   this.lblBillState,
																					   this.txtServCity,
																					   this.lblServiceCity,
																					   this.txtBillCity,
																					   this.lblBillingCity,
																					   this.txtService2,
																					   this.lblService2,
																					   this.txtBilling2,
																					   this.lblBilling2,
																					   this.txtService1,
																					   this.lblService1,
																					   this.chkCopyBilling,
																					   this.txtBilling1,
																					   this.BillinglblAddress1});
			this.AddressPanel.ForeColor = System.Drawing.Color.Black;
			this.AddressPanel.Location = new System.Drawing.Point(8, 184);
			this.AddressPanel.Name = "AddressPanel";
			this.AddressPanel.Size = new System.Drawing.Size(576, 192);
			this.AddressPanel.TabIndex = 91;
			// 
			// txtServZip
			// 
			this.txtServZip.Location = new System.Drawing.Point(384, 136);
			this.txtServZip.Name = "txtServZip";
			this.txtServZip.Size = new System.Drawing.Size(176, 21);
			this.txtServZip.TabIndex = 109;
			this.txtServZip.Text = "";
			// 
			// lblServZip
			// 
			this.lblServZip.Location = new System.Drawing.Point(280, 136);
			this.lblServZip.Name = "lblServZip";
			this.lblServZip.Size = new System.Drawing.Size(104, 16);
			this.lblServZip.TabIndex = 108;
			this.lblServZip.Text = "Service Zip:";
			// 
			// txtBillZip
			// 
			this.txtBillZip.Location = new System.Drawing.Point(104, 136);
			this.txtBillZip.Name = "txtBillZip";
			this.txtBillZip.Size = new System.Drawing.Size(152, 21);
			this.txtBillZip.TabIndex = 107;
			this.txtBillZip.Text = "";
			// 
			// lblBillingZip
			// 
			this.lblBillingZip.Location = new System.Drawing.Point(8, 136);
			this.lblBillingZip.Name = "lblBillingZip";
			this.lblBillingZip.Size = new System.Drawing.Size(80, 16);
			this.lblBillingZip.TabIndex = 106;
			this.lblBillingZip.Text = "Billing Zip:";
			// 
			// cboServState
			// 
			this.cboServState.BorderColor = System.Drawing.Color.Black;
			this.cboServState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboServState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboServState.Location = new System.Drawing.Point(384, 104);
			this.cboServState.Name = "cboServState";
			this.cboServState.Size = new System.Drawing.Size(176, 22);
			this.cboServState.TabIndex = 105;
			this.cboServState.TextColor = System.Drawing.Color.Black;
			// 
			// lblServState
			// 
			this.lblServState.Location = new System.Drawing.Point(280, 104);
			this.lblServState.Name = "lblServState";
			this.lblServState.Size = new System.Drawing.Size(104, 16);
			this.lblServState.TabIndex = 104;
			this.lblServState.Text = "Service State:";
			// 
			// cboBillState
			// 
			this.cboBillState.BorderColor = System.Drawing.Color.Black;
			this.cboBillState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboBillState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboBillState.Location = new System.Drawing.Point(104, 104);
			this.cboBillState.Name = "cboBillState";
			this.cboBillState.Size = new System.Drawing.Size(152, 22);
			this.cboBillState.TabIndex = 103;
			this.cboBillState.TextColor = System.Drawing.Color.Black;
			// 
			// lblBillState
			// 
			this.lblBillState.Location = new System.Drawing.Point(8, 104);
			this.lblBillState.Name = "lblBillState";
			this.lblBillState.Size = new System.Drawing.Size(88, 16);
			this.lblBillState.TabIndex = 102;
			this.lblBillState.Text = "Billing State:";
			// 
			// txtServCity
			// 
			this.txtServCity.Location = new System.Drawing.Point(384, 72);
			this.txtServCity.Name = "txtServCity";
			this.txtServCity.Size = new System.Drawing.Size(176, 21);
			this.txtServCity.TabIndex = 101;
			this.txtServCity.Text = "";
			// 
			// lblServiceCity
			// 
			this.lblServiceCity.Location = new System.Drawing.Point(280, 72);
			this.lblServiceCity.Name = "lblServiceCity";
			this.lblServiceCity.Size = new System.Drawing.Size(104, 16);
			this.lblServiceCity.TabIndex = 100;
			this.lblServiceCity.Text = "Service City:";
			// 
			// txtBillCity
			// 
			this.txtBillCity.Location = new System.Drawing.Point(104, 72);
			this.txtBillCity.Name = "txtBillCity";
			this.txtBillCity.Size = new System.Drawing.Size(152, 21);
			this.txtBillCity.TabIndex = 99;
			this.txtBillCity.Text = "";
			// 
			// lblBillingCity
			// 
			this.lblBillingCity.Location = new System.Drawing.Point(8, 72);
			this.lblBillingCity.Name = "lblBillingCity";
			this.lblBillingCity.Size = new System.Drawing.Size(88, 16);
			this.lblBillingCity.TabIndex = 98;
			this.lblBillingCity.Text = "Billing City:";
			// 
			// txtService2
			// 
			this.txtService2.Location = new System.Drawing.Point(384, 40);
			this.txtService2.Name = "txtService2";
			this.txtService2.Size = new System.Drawing.Size(176, 21);
			this.txtService2.TabIndex = 97;
			this.txtService2.Text = "";
			// 
			// lblService2
			// 
			this.lblService2.Location = new System.Drawing.Point(280, 40);
			this.lblService2.Name = "lblService2";
			this.lblService2.Size = new System.Drawing.Size(104, 16);
			this.lblService2.TabIndex = 96;
			this.lblService2.Text = "Service Address 2:";
			// 
			// txtBilling2
			// 
			this.txtBilling2.Location = new System.Drawing.Point(104, 40);
			this.txtBilling2.Name = "txtBilling2";
			this.txtBilling2.Size = new System.Drawing.Size(152, 21);
			this.txtBilling2.TabIndex = 95;
			this.txtBilling2.Text = "";
			// 
			// lblBilling2
			// 
			this.lblBilling2.Location = new System.Drawing.Point(8, 40);
			this.lblBilling2.Name = "lblBilling2";
			this.lblBilling2.Size = new System.Drawing.Size(96, 16);
			this.lblBilling2.TabIndex = 94;
			this.lblBilling2.Text = "Billing Address 2:";
			// 
			// txtService1
			// 
			this.txtService1.Location = new System.Drawing.Point(384, 8);
			this.txtService1.Name = "txtService1";
			this.txtService1.Size = new System.Drawing.Size(176, 21);
			this.txtService1.TabIndex = 93;
			this.txtService1.Text = "";
			// 
			// lblService1
			// 
			this.lblService1.Location = new System.Drawing.Point(280, 8);
			this.lblService1.Name = "lblService1";
			this.lblService1.Size = new System.Drawing.Size(96, 16);
			this.lblService1.TabIndex = 92;
			this.lblService1.Text = "Service Address:";
			// 
			// chkCopyBilling
			// 
			this.chkCopyBilling.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkCopyBilling.Location = new System.Drawing.Point(280, 168);
			this.chkCopyBilling.Name = "chkCopyBilling";
			this.chkCopyBilling.Size = new System.Drawing.Size(104, 16);
			this.chkCopyBilling.TabIndex = 91;
			this.chkCopyBilling.Text = "Same as Billing";
			// 
			// txtBilling1
			// 
			this.txtBilling1.Location = new System.Drawing.Point(104, 8);
			this.txtBilling1.Name = "txtBilling1";
			this.txtBilling1.Size = new System.Drawing.Size(152, 21);
			this.txtBilling1.TabIndex = 90;
			this.txtBilling1.Text = "";
			// 
			// BillinglblAddress1
			// 
			this.BillinglblAddress1.Location = new System.Drawing.Point(8, 8);
			this.BillinglblAddress1.Name = "BillinglblAddress1";
			this.BillinglblAddress1.Size = new System.Drawing.Size(88, 16);
			this.BillinglblAddress1.TabIndex = 89;
			this.BillinglblAddress1.Text = "Billing Address:";
			// 
			// CustPanel
			// 
			this.CustPanel.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.CustPanel.BackColor = System.Drawing.Color.Transparent;
			this.CustPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.lblFormerTDM,
																					this.chkDoNotCall,
																					this.txtAltPhone,
																					this.lblAltPhone,
																					this.cboLanguage,
																					this.lblLanguage,
																					this.txtEmail,
																					this.lblEmail,
																					this.txtCompany,
																					this.lblCompany,
																					this.txtLastName,
																					this.lblLastName,
																					this.txtFirstName,
																					this.lblFirstName,
																					this.txtFormerTDM});
			this.CustPanel.Location = new System.Drawing.Point(8, 8);
			this.CustPanel.Name = "CustPanel";
			this.CustPanel.Size = new System.Drawing.Size(576, 152);
			this.CustPanel.TabIndex = 92;
			// 
			// lblFormerTDM
			// 
			this.lblFormerTDM.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblFormerTDM.Location = new System.Drawing.Point(280, 112);
			this.lblFormerTDM.Name = "lblFormerTDM";
			this.lblFormerTDM.Size = new System.Drawing.Size(80, 13);
			this.lblFormerTDM.TabIndex = 77;
			this.lblFormerTDM.Text = "Former TDM:";
			// 
			// chkDoNotCall
			// 
			this.chkDoNotCall.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkDoNotCall.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkDoNotCall.Location = new System.Drawing.Point(280, 16);
			this.chkDoNotCall.Name = "chkDoNotCall";
			this.chkDoNotCall.Size = new System.Drawing.Size(168, 16);
			this.chkDoNotCall.TabIndex = 76;
			this.chkDoNotCall.Text = "Do Not Call";
			// 
			// txtAltPhone
			// 
			this.txtAltPhone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtAltPhone.Location = new System.Drawing.Point(384, 80);
			this.txtAltPhone.Name = "txtAltPhone";
			this.txtAltPhone.Size = new System.Drawing.Size(168, 21);
			this.txtAltPhone.TabIndex = 75;
			this.txtAltPhone.Text = "";
			// 
			// lblAltPhone
			// 
			this.lblAltPhone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblAltPhone.Location = new System.Drawing.Point(280, 80);
			this.lblAltPhone.Name = "lblAltPhone";
			this.lblAltPhone.Size = new System.Drawing.Size(96, 16);
			this.lblAltPhone.TabIndex = 74;
			this.lblAltPhone.Text = "Alternate Phone:";
			// 
			// cboLanguage
			// 
			this.cboLanguage.BorderColor = System.Drawing.Color.Black;
			this.cboLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLanguage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboLanguage.ItemHeight = 13;
			this.cboLanguage.Location = new System.Drawing.Point(104, 112);
			this.cboLanguage.Name = "cboLanguage";
			this.cboLanguage.Size = new System.Drawing.Size(152, 19);
			this.cboLanguage.TabIndex = 73;
			this.cboLanguage.TextColor = System.Drawing.Color.Black;
			// 
			// lblLanguage
			// 
			this.lblLanguage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLanguage.Location = new System.Drawing.Point(8, 112);
			this.lblLanguage.Name = "lblLanguage";
			this.lblLanguage.Size = new System.Drawing.Size(64, 16);
			this.lblLanguage.TabIndex = 72;
			this.lblLanguage.Text = "Language:";
			// 
			// txtEmail
			// 
			this.txtEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtEmail.Location = new System.Drawing.Point(384, 48);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(168, 21);
			this.txtEmail.TabIndex = 71;
			this.txtEmail.Text = "";
			// 
			// lblEmail
			// 
			this.lblEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblEmail.Location = new System.Drawing.Point(280, 48);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(64, 16);
			this.lblEmail.TabIndex = 70;
			this.lblEmail.Text = "Email:";
			// 
			// txtCompany
			// 
			this.txtCompany.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtCompany.Location = new System.Drawing.Point(104, 80);
			this.txtCompany.Name = "txtCompany";
			this.txtCompany.Size = new System.Drawing.Size(152, 21);
			this.txtCompany.TabIndex = 69;
			this.txtCompany.Text = "";
			// 
			// lblCompany
			// 
			this.lblCompany.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCompany.Location = new System.Drawing.Point(8, 80);
			this.lblCompany.Name = "lblCompany";
			this.lblCompany.Size = new System.Drawing.Size(64, 16);
			this.lblCompany.TabIndex = 68;
			this.lblCompany.Text = "Company:";
			// 
			// txtLastName
			// 
			this.txtLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtLastName.Location = new System.Drawing.Point(104, 48);
			this.txtLastName.Name = "txtLastName";
			this.txtLastName.Size = new System.Drawing.Size(152, 21);
			this.txtLastName.TabIndex = 67;
			this.txtLastName.Text = "";
			// 
			// lblLastName
			// 
			this.lblLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLastName.Location = new System.Drawing.Point(8, 48);
			this.lblLastName.Name = "lblLastName";
			this.lblLastName.Size = new System.Drawing.Size(64, 16);
			this.lblLastName.TabIndex = 66;
			this.lblLastName.Text = "Last Name:";
			// 
			// txtFirstName
			// 
			this.txtFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtFirstName.Location = new System.Drawing.Point(104, 16);
			this.txtFirstName.Name = "txtFirstName";
			this.txtFirstName.Size = new System.Drawing.Size(152, 21);
			this.txtFirstName.TabIndex = 65;
			this.txtFirstName.Text = "";
			// 
			// lblFirstName
			// 
			this.lblFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblFirstName.Location = new System.Drawing.Point(8, 16);
			this.lblFirstName.Name = "lblFirstName";
			this.lblFirstName.Size = new System.Drawing.Size(64, 16);
			this.lblFirstName.TabIndex = 64;
			this.lblFirstName.Text = "First Name:";
			// 
			// txtFormerTDM
			// 
			this.txtFormerTDM.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtFormerTDM.Location = new System.Drawing.Point(384, 112);
			this.txtFormerTDM.Name = "txtFormerTDM";
			this.txtFormerTDM.Size = new System.Drawing.Size(168, 21);
			this.txtFormerTDM.TabIndex = 78;
			this.txtFormerTDM.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 384);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(136, 16);
			this.label2.TabIndex = 93;
			this.label2.Text = "Additional Information";
			// 
			// afniDivider2
			// 
			this.afniDivider2.DividerStyle = Afni.Controls.DividerStyles.Horizontal;
			this.afniDivider2.FirstColor = System.Drawing.Color.FromArgb(((System.Byte)(1)), ((System.Byte)(72)), ((System.Byte)(178)));
			this.afniDivider2.Location = new System.Drawing.Point(152, 392);
			this.afniDivider2.Name = "afniDivider2";
			this.afniDivider2.SecondColor = System.Drawing.Color.White;
			this.afniDivider2.Size = new System.Drawing.Size(416, 1);
			this.afniDivider2.TabIndex = 94;
			this.afniDivider2.Text = "afniDivider2";
			// 
			// CustomerCtl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.afniDivider2,
																		  this.label2,
																		  this.CustPanel,
																		  this.AddressPanel,
																		  this.afniDivider1,
																		  this.label1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "CustomerCtl";
			this.Size = new System.Drawing.Size(600, 504);
			this.AddressPanel.ResumeLayout(false);
			this.CustPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Events
		private void textbox_TextChanged(object sender, System.EventArgs e)
		{
			if(_form_state != FormStates.EditInProgress && _form_state != FormStates.Busy)
			{
				_form_state = FormStates.EditInProgress;
				if(Dirtied != null)
					Dirtied(this,null);
			}
		}

		private void combo_SelectedValueChanged(object sender, System.EventArgs e)
		{
			if(_form_state != FormStates.EditInProgress && _form_state != FormStates.Busy)
			{
				_form_state = FormStates.EditInProgress;
				if(Dirtied != null)
					Dirtied(this, null);
			}
		}

		private void app_CampaignChanged(object sender, EventArgs e)
		{
			RefreshDynCtls();
		}
		#endregion
	}
}
