using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for uctlWTN.
	/// </summary>
	public class WTNControl : UserControl, IForm, ISaveable, ISkinnable
	{
		#region Member Variables

		private System.Windows.Forms.Label lblIntraPIC;
		private System.Windows.Forms.TextBox txtIntraPIC;
		private System.Windows.Forms.Label lblInterLataPIC;
		private System.Windows.Forms.TextBox txtInterLataPIC;
		private System.Windows.Forms.Label lblIntraLataANI;
		private System.Windows.Forms.TextBox txtIntraLataANI;
		private System.Windows.Forms.Label lblInterLataANI;
		private System.Windows.Forms.TextBox txtInterLataANI;
		private Afni.FormData.FormStates _form_state;
		private System.Windows.Forms.Label lblIntraCIC;
		private System.Windows.Forms.TextBox txtIntraCIC;
		private System.Windows.Forms.Label lblIntraLataCIC;
		private System.Windows.Forms.TextBox txtInterCIC;
		private System.Windows.Forms.CheckBox chkInternationalBlock;
		private System.Windows.Forms.Label lblInstructions;
		private System.Windows.Forms.Label lblWTN;
		private System.Windows.Forms.TextBox txtWTN;
		private System.Windows.Forms.Label lblOther;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblIntlPIC;
		private System.Windows.Forms.TextBox txtIntlPIC;
		private System.Windows.Forms.Label lblINTLANI;
		private System.Windows.Forms.TextBox txtIntlANI;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtIntlCIC;
		private System.Windows.Forms.Label label2;
		private Afni.Applications.VLoop.DisplayTheme _theme;
		private Afni.Applications.VLoop.Application _app;
		private Afni.Applications.VLoop.VLoopSmartDTO.WTN _wtn;
		#endregion
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WTNControl(Afni.Applications.VLoop.Application app)
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

		public WTN CurrentWTN
		{
			get { return _wtn; }
			set 
			{
				_wtn = value; 
				((IForm)this).Refresh();
			}
		}

		#region IForm implementation
		FormStates IForm.FormState
		{
			get { return _form_state; }
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		bool IForm.Refresh()
		{
			if(_wtn != null)
			{
				txtWTN.Text = _wtn.WorkingTelephoneNumber;
				txtInterCIC.Text = _wtn.InterLataCIC;
				txtInterLataANI.Text = _wtn.InterLataANI;
				txtInterLataPIC.Text = _wtn.InterLataPIC;
				txtIntlANI.Text = "FIXME";
				txtIntlCIC.Text = _wtn.InternationalCIC;
				txtIntlPIC.Text = _wtn.InternationalPIC;
				txtIntraCIC.Text = _wtn.IntraLataCIC;
				txtIntraLataANI.Text = _wtn.IntraLataANI;
				txtIntraPIC.Text = _wtn.IntraLataPIC;
			}
			else
			{
				txtWTN.Text = "";
				txtInterCIC.Text = "";
				txtInterLataANI.Text = "";
				txtInterLataPIC.Text = "";
				txtIntlANI.Text = "";
				txtIntlCIC.Text = "";
				txtIntlPIC.Text = "";
				txtIntraCIC.Text = "";
				txtIntraLataANI.Text = "";
				txtIntraPIC.Text = "";
			}
			return true;
		}

		string IForm.Name
		{
			get { return "WTN";}
			set { }
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
			get { return "Save WTN"; }
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
						txtInterCIC.BorderStyle = BorderStyle.FixedSingle;
						txtInterLataANI.BorderStyle = BorderStyle.FixedSingle;
						txtInterLataPIC.BorderStyle = BorderStyle.FixedSingle;
						txtIntlANI.BorderStyle = BorderStyle.FixedSingle;
						txtIntlCIC.BorderStyle = BorderStyle.FixedSingle;
						txtIntlPIC.BorderStyle = BorderStyle.FixedSingle;
						txtIntraCIC.BorderStyle = BorderStyle.FixedSingle;
						txtIntraLataANI.BorderStyle = BorderStyle.FixedSingle;
						txtIntraPIC.BorderStyle = BorderStyle.FixedSingle;
						txtWTN.BorderStyle = BorderStyle.FixedSingle;
						chkInternationalBlock.FlatStyle = FlatStyle.Flat;
					}
					else
					{
						txtInterCIC.BorderStyle = BorderStyle.Fixed3D;
						txtInterLataANI.BorderStyle = BorderStyle.Fixed3D;
						txtInterLataPIC.BorderStyle = BorderStyle.Fixed3D;
						txtIntlANI.BorderStyle = BorderStyle.Fixed3D;
						txtIntlCIC.BorderStyle = BorderStyle.Fixed3D;
						txtIntlPIC.BorderStyle = BorderStyle.Fixed3D;
						txtIntraCIC.BorderStyle = BorderStyle.Fixed3D;
						txtIntraLataANI.BorderStyle = BorderStyle.Fixed3D;
						txtIntraPIC.BorderStyle = BorderStyle.Fixed3D;
						txtWTN.BorderStyle = BorderStyle.Fixed3D;
						chkInternationalBlock.FlatStyle = FlatStyle.Standard;
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
			this.lblIntraPIC = new System.Windows.Forms.Label();
			this.txtIntraPIC = new System.Windows.Forms.TextBox();
			this.lblInterLataPIC = new System.Windows.Forms.Label();
			this.txtInterLataPIC = new System.Windows.Forms.TextBox();
			this.lblIntraLataANI = new System.Windows.Forms.Label();
			this.txtIntraLataANI = new System.Windows.Forms.TextBox();
			this.lblInterLataANI = new System.Windows.Forms.Label();
			this.txtInterLataANI = new System.Windows.Forms.TextBox();
			this.lblIntraCIC = new System.Windows.Forms.Label();
			this.txtIntraCIC = new System.Windows.Forms.TextBox();
			this.lblIntraLataCIC = new System.Windows.Forms.Label();
			this.txtInterCIC = new System.Windows.Forms.TextBox();
			this.chkInternationalBlock = new System.Windows.Forms.CheckBox();
			this.lblInstructions = new System.Windows.Forms.Label();
			this.lblWTN = new System.Windows.Forms.Label();
			this.txtWTN = new System.Windows.Forms.TextBox();
			this.lblOther = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblIntlPIC = new System.Windows.Forms.Label();
			this.txtIntlPIC = new System.Windows.Forms.TextBox();
			this.lblINTLANI = new System.Windows.Forms.Label();
			this.txtIntlANI = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtIntlCIC = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblIntraPIC
			// 
			this.lblIntraPIC.Location = new System.Drawing.Point(16, 160);
			this.lblIntraPIC.Name = "lblIntraPIC";
			this.lblIntraPIC.Size = new System.Drawing.Size(104, 16);
			this.lblIntraPIC.TabIndex = 2;
			this.lblIntraPIC.Text = "IntraLata PIC:";
			// 
			// txtIntraPIC
			// 
			this.txtIntraPIC.Location = new System.Drawing.Point(152, 160);
			this.txtIntraPIC.MaxLength = 1;
			this.txtIntraPIC.Name = "txtIntraPIC";
			this.txtIntraPIC.Size = new System.Drawing.Size(48, 21);
			this.txtIntraPIC.TabIndex = 3;
			this.txtIntraPIC.Text = "";
			this.txtIntraPIC.TextChanged += new System.EventHandler(this.textbox_TextChanged);
			// 
			// lblInterLataPIC
			// 
			this.lblInterLataPIC.Location = new System.Drawing.Point(240, 160);
			this.lblInterLataPIC.Name = "lblInterLataPIC";
			this.lblInterLataPIC.Size = new System.Drawing.Size(88, 16);
			this.lblInterLataPIC.TabIndex = 4;
			this.lblInterLataPIC.Text = "InterLata PIC:";
			// 
			// txtInterLataPIC
			// 
			this.txtInterLataPIC.Location = new System.Drawing.Point(360, 160);
			this.txtInterLataPIC.MaxLength = 1;
			this.txtInterLataPIC.Name = "txtInterLataPIC";
			this.txtInterLataPIC.Size = new System.Drawing.Size(48, 21);
			this.txtInterLataPIC.TabIndex = 5;
			this.txtInterLataPIC.Text = "";
			this.txtInterLataPIC.TextChanged += new System.EventHandler(this.textbox_TextChanged);
			// 
			// lblIntraLataANI
			// 
			this.lblIntraLataANI.Location = new System.Drawing.Point(16, 192);
			this.lblIntraLataANI.Name = "lblIntraLataANI";
			this.lblIntraLataANI.Size = new System.Drawing.Size(80, 16);
			this.lblIntraLataANI.TabIndex = 6;
			this.lblIntraLataANI.Text = "IntraLata ANI:";
			// 
			// txtIntraLataANI
			// 
			this.txtIntraLataANI.Location = new System.Drawing.Point(152, 192);
			this.txtIntraLataANI.MaxLength = 1;
			this.txtIntraLataANI.Name = "txtIntraLataANI";
			this.txtIntraLataANI.Size = new System.Drawing.Size(48, 21);
			this.txtIntraLataANI.TabIndex = 7;
			this.txtIntraLataANI.Text = "";
			this.txtIntraLataANI.TextChanged += new System.EventHandler(this.textbox_TextChanged);
			// 
			// lblInterLataANI
			// 
			this.lblInterLataANI.Location = new System.Drawing.Point(240, 192);
			this.lblInterLataANI.Name = "lblInterLataANI";
			this.lblInterLataANI.Size = new System.Drawing.Size(80, 16);
			this.lblInterLataANI.TabIndex = 8;
			this.lblInterLataANI.Text = "InterLata ANI:";
			// 
			// txtInterLataANI
			// 
			this.txtInterLataANI.Location = new System.Drawing.Point(360, 192);
			this.txtInterLataANI.MaxLength = 1;
			this.txtInterLataANI.Name = "txtInterLataANI";
			this.txtInterLataANI.Size = new System.Drawing.Size(48, 21);
			this.txtInterLataANI.TabIndex = 9;
			this.txtInterLataANI.Text = "";
			this.txtInterLataANI.TextChanged += new System.EventHandler(this.textbox_TextChanged);
			// 
			// lblIntraCIC
			// 
			this.lblIntraCIC.Location = new System.Drawing.Point(16, 224);
			this.lblIntraCIC.Name = "lblIntraCIC";
			this.lblIntraCIC.Size = new System.Drawing.Size(112, 16);
			this.lblIntraCIC.TabIndex = 10;
			this.lblIntraCIC.Text = "IntraLata CIC Code:";
			// 
			// txtIntraCIC
			// 
			this.txtIntraCIC.Location = new System.Drawing.Point(152, 224);
			this.txtIntraCIC.Name = "txtIntraCIC";
			this.txtIntraCIC.Size = new System.Drawing.Size(48, 21);
			this.txtIntraCIC.TabIndex = 11;
			this.txtIntraCIC.Text = "";
			this.txtIntraCIC.TextChanged += new System.EventHandler(this.textbox_TextChanged);
			// 
			// lblIntraLataCIC
			// 
			this.lblIntraLataCIC.Location = new System.Drawing.Point(240, 224);
			this.lblIntraLataCIC.Name = "lblIntraLataCIC";
			this.lblIntraLataCIC.Size = new System.Drawing.Size(104, 16);
			this.lblIntraLataCIC.TabIndex = 12;
			this.lblIntraLataCIC.Text = "InterLata CIC Code:";
			// 
			// txtInterCIC
			// 
			this.txtInterCIC.Location = new System.Drawing.Point(360, 224);
			this.txtInterCIC.Name = "txtInterCIC";
			this.txtInterCIC.Size = new System.Drawing.Size(48, 21);
			this.txtInterCIC.TabIndex = 13;
			this.txtInterCIC.Text = "";
			this.txtInterCIC.TextChanged += new System.EventHandler(this.textbox_TextChanged);
			// 
			// chkInternationalBlock
			// 
			this.chkInternationalBlock.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkInternationalBlock.Location = new System.Drawing.Point(16, 360);
			this.chkInternationalBlock.Name = "chkInternationalBlock";
			this.chkInternationalBlock.Size = new System.Drawing.Size(136, 16);
			this.chkInternationalBlock.TabIndex = 14;
			this.chkInternationalBlock.Text = "International Block";
			this.chkInternationalBlock.CheckedChanged += new System.EventHandler(this.chkInternationalBlock_CheckedChanged);
			// 
			// lblInstructions
			// 
			this.lblInstructions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblInstructions.Location = new System.Drawing.Point(8, 48);
			this.lblInstructions.Name = "lblInstructions";
			this.lblInstructions.Size = new System.Drawing.Size(472, 40);
			this.lblInstructions.TabIndex = 15;
			this.lblInstructions.Text = "Enter all the known information about the WTN, and press the save button.";
			// 
			// lblWTN
			// 
			this.lblWTN.Location = new System.Drawing.Point(16, 88);
			this.lblWTN.Name = "lblWTN";
			this.lblWTN.Size = new System.Drawing.Size(40, 16);
			this.lblWTN.TabIndex = 16;
			this.lblWTN.Text = "WTN:";
			// 
			// txtWTN
			// 
			this.txtWTN.Location = new System.Drawing.Point(64, 88);
			this.txtWTN.Name = "txtWTN";
			this.txtWTN.Size = new System.Drawing.Size(104, 21);
			this.txtWTN.TabIndex = 17;
			this.txtWTN.Text = "";
			this.txtWTN.TextChanged += new System.EventHandler(this.textbox_TextChanged);
			// 
			// lblOther
			// 
			this.lblOther.Location = new System.Drawing.Point(16, 128);
			this.lblOther.Name = "lblOther";
			this.lblOther.Size = new System.Drawing.Size(128, 16);
			this.lblOther.TabIndex = 18;
			this.lblOther.Text = "Other WTN Information";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.groupBox1.Location = new System.Drawing.Point(152, 128);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(336, 8);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			// 
			// lblIntlPIC
			// 
			this.lblIntlPIC.Location = new System.Drawing.Point(16, 264);
			this.lblIntlPIC.Name = "lblIntlPIC";
			this.lblIntlPIC.Size = new System.Drawing.Size(104, 16);
			this.lblIntlPIC.TabIndex = 20;
			this.lblIntlPIC.Text = "International PIC:";
			// 
			// txtIntlPIC
			// 
			this.txtIntlPIC.Location = new System.Drawing.Point(152, 264);
			this.txtIntlPIC.Name = "txtIntlPIC";
			this.txtIntlPIC.Size = new System.Drawing.Size(48, 21);
			this.txtIntlPIC.TabIndex = 21;
			this.txtIntlPIC.Text = "";
			this.txtIntlPIC.TextChanged += new System.EventHandler(this.textbox_TextChanged);
			// 
			// lblINTLANI
			// 
			this.lblINTLANI.Location = new System.Drawing.Point(16, 296);
			this.lblINTLANI.Name = "lblINTLANI";
			this.lblINTLANI.Size = new System.Drawing.Size(104, 16);
			this.lblINTLANI.TabIndex = 22;
			this.lblINTLANI.Text = "International ANI:";
			// 
			// txtIntlANI
			// 
			this.txtIntlANI.Location = new System.Drawing.Point(152, 296);
			this.txtIntlANI.Name = "txtIntlANI";
			this.txtIntlANI.Size = new System.Drawing.Size(48, 21);
			this.txtIntlANI.TabIndex = 23;
			this.txtIntlANI.Text = "";
			this.txtIntlANI.TextChanged += new System.EventHandler(this.textbox_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 328);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 16);
			this.label1.TabIndex = 24;
			this.label1.Text = "International CIC Code:";
			// 
			// txtIntlCIC
			// 
			this.txtIntlCIC.Location = new System.Drawing.Point(152, 328);
			this.txtIntlCIC.Name = "txtIntlCIC";
			this.txtIntlCIC.Size = new System.Drawing.Size(48, 21);
			this.txtIntlCIC.TabIndex = 25;
			this.txtIntlCIC.Text = "";
			this.txtIntlCIC.TextChanged += new System.EventHandler(this.textbox_TextChanged);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
			this.label2.Location = new System.Drawing.Point(16, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(344, 32);
			this.label2.TabIndex = 26;
			this.label2.Text = "Working Telephone Number";
			// 
			// WTNControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label2,
																		  this.txtIntlCIC,
																		  this.label1,
																		  this.txtIntlANI,
																		  this.lblINTLANI,
																		  this.txtIntlPIC,
																		  this.lblIntlPIC,
																		  this.groupBox1,
																		  this.lblOther,
																		  this.txtWTN,
																		  this.lblWTN,
																		  this.lblInstructions,
																		  this.chkInternationalBlock,
																		  this.txtInterCIC,
																		  this.lblIntraLataCIC,
																		  this.txtIntraCIC,
																		  this.lblIntraCIC,
																		  this.txtInterLataANI,
																		  this.lblInterLataANI,
																		  this.txtIntraLataANI,
																		  this.lblIntraLataANI,
																		  this.txtInterLataPIC,
																		  this.lblInterLataPIC,
																		  this.txtIntraPIC,
																		  this.lblIntraPIC});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "WTNControl";
			this.Size = new System.Drawing.Size(496, 480);
			this.ResumeLayout(false);

		}
		#endregion

		#region events
		private void textbox_TextChanged(object sender, System.EventArgs e)
		{
			Safe_SetDirty();
		}

		private void chkInternationalBlock_CheckedChanged(object sender, System.EventArgs e)
		{
			Safe_SetDirty();
		}

		private void Safe_SetDirty()
		{
			if(_form_state != FormStates.EditInProgress)
			{
				_form_state = FormStates.EditInProgress;
				if(Dirtied != null)
				{
					Dirtied(this,null);
				}
			}
		}
		#endregion
	}
}
