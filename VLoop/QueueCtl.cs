using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.Applications.VLoop.Commands;
using Afni.Applications.VLoop.Viewing;
using Afni.Applications.VLoop.States;
using Afni.Applications.VLoop;
using Afni.FormData;
using Afni.Applications.VLoop.VLoopDataObjects;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for QueueCtl.
	/// </summary>
	public class QueueCtl : UserControl , IForm, ISkinnable
	{
		private Afni.Controls.AfniFlatCombo cboState;
		private System.Windows.Forms.Label label1;
		private Afni.Controls.AfniFlatCombo cboDisposition;
		private System.Windows.Forms.Label lblDisposition;
		private Afni.Controls.AfniFlatCombo cboLanguage;
		private System.Windows.Forms.Label lblLanguage;
		private Afni.Controls.AfniFlatCombo cboFilename;
		private System.Windows.Forms.Label lblFilename;
		private System.Windows.Forms.GroupBox groupTimeZones;
		private System.Windows.Forms.CheckBox checkBox7;
		private System.Windows.Forms.CheckBox checkBox6;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblInstructions;
		private FormStates _form_state = FormStates.Idle;
		private Afni.Controls.AfniDivider afniDivider1;
		private Afni.Applications.VLoop.Application _app;
		private Afni.Applications.VLoop.DisplayTheme _theme;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label2;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public QueueCtl(Afni.Applications.VLoop.Application app)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			_app = app;
			_app.CampaignSwitched += new System.EventHandler(this.OnCampaignSwitch);
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

		FormStates IForm.FormState
		{
			get { return _form_state; }
		}

		bool IForm.Refresh()
		{
			cboLanguage.DataSource = _app.CurrentCampaign.Languages;
			cboFilename.DataSource = _app.CurrentCampaign.Files;
			cboDisposition.DataSource = _app.CurrentCampaign.Dispositions;
			return true;
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		string IForm.Name
		{
			get { return "Queue Selection and Setup"; }
			set { }
		}

		#region ISkinnable implementation
		bool ISkinnable.ApplyTheme(DisplayTheme theme)
		{
			bool theme_ok = true;
			try
			{
				this.BackColor = theme.FormBackColor;
				afniDivider1.FirstColor = theme.DividerDarkColor;
				afniDivider1.SecondColor = theme.DividerLightColor;

				if(!_app.IsOnXP)
				{
					if(theme.FlatControls)
					{
						checkBox1.FlatStyle = FlatStyle.Flat;
						checkBox2.FlatStyle = FlatStyle.Flat;
						checkBox3.FlatStyle = FlatStyle.Flat;
						checkBox4.FlatStyle = FlatStyle.Flat;
						checkBox5.FlatStyle = FlatStyle.Flat;
						checkBox6.FlatStyle = FlatStyle.Flat;
						checkBox7.FlatStyle = FlatStyle.Flat;
						btnOK.FlatStyle = FlatStyle.Flat;
						btnCancel.FlatStyle = FlatStyle.Flat;
					}
					else
					{
						checkBox1.FlatStyle = FlatStyle.Standard;
						checkBox2.FlatStyle = FlatStyle.Standard;
						checkBox3.FlatStyle = FlatStyle.Standard;
						checkBox4.FlatStyle = FlatStyle.Standard;
						checkBox5.FlatStyle = FlatStyle.Standard;
						checkBox6.FlatStyle = FlatStyle.Standard;
						checkBox7.FlatStyle = FlatStyle.Standard;
						btnOK.FlatStyle = FlatStyle.Standard;
						btnCancel.FlatStyle = FlatStyle.Standard;
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
			this.cboState = new Afni.Controls.AfniFlatCombo();
			this.label1 = new System.Windows.Forms.Label();
			this.cboDisposition = new Afni.Controls.AfniFlatCombo();
			this.lblDisposition = new System.Windows.Forms.Label();
			this.cboLanguage = new Afni.Controls.AfniFlatCombo();
			this.lblLanguage = new System.Windows.Forms.Label();
			this.cboFilename = new Afni.Controls.AfniFlatCombo();
			this.lblFilename = new System.Windows.Forms.Label();
			this.groupTimeZones = new System.Windows.Forms.GroupBox();
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblInstructions = new System.Windows.Forms.Label();
			this.afniDivider1 = new Afni.Controls.AfniDivider();
			this.label2 = new System.Windows.Forms.Label();
			this.groupTimeZones.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboState
			// 
			this.cboState.BorderColor = System.Drawing.Color.Black;
			this.cboState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboState.Items.AddRange(new object[] {
														  "All",
														  "CA",
														  "IL",
														  "NY",
														  "VA"});
			this.cboState.Location = new System.Drawing.Point(96, 136);
			this.cboState.Name = "cboState";
			this.cboState.Size = new System.Drawing.Size(184, 22);
			this.cboState.Sorted = true;
			this.cboState.TabIndex = 29;
			this.cboState.TextColor = System.Drawing.Color.Black;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 136);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 28;
			this.label1.Text = "State:";
			// 
			// cboDisposition
			// 
			this.cboDisposition.BorderColor = System.Drawing.Color.Black;
			this.cboDisposition.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboDisposition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDisposition.Location = new System.Drawing.Point(96, 168);
			this.cboDisposition.Name = "cboDisposition";
			this.cboDisposition.Size = new System.Drawing.Size(184, 22);
			this.cboDisposition.TabIndex = 27;
			this.cboDisposition.TextColor = System.Drawing.Color.Black;
			// 
			// lblDisposition
			// 
			this.lblDisposition.Location = new System.Drawing.Point(16, 168);
			this.lblDisposition.Name = "lblDisposition";
			this.lblDisposition.Size = new System.Drawing.Size(64, 16);
			this.lblDisposition.TabIndex = 26;
			this.lblDisposition.Text = "Disposition:";
			// 
			// cboLanguage
			// 
			this.cboLanguage.BorderColor = System.Drawing.Color.Black;
			this.cboLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLanguage.Location = new System.Drawing.Point(96, 200);
			this.cboLanguage.Name = "cboLanguage";
			this.cboLanguage.Size = new System.Drawing.Size(184, 22);
			this.cboLanguage.TabIndex = 25;
			this.cboLanguage.TextColor = System.Drawing.Color.Black;
			// 
			// lblLanguage
			// 
			this.lblLanguage.Location = new System.Drawing.Point(16, 200);
			this.lblLanguage.Name = "lblLanguage";
			this.lblLanguage.Size = new System.Drawing.Size(64, 16);
			this.lblLanguage.TabIndex = 24;
			this.lblLanguage.Text = "Language:";
			// 
			// cboFilename
			// 
			this.cboFilename.BorderColor = System.Drawing.Color.Black;
			this.cboFilename.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboFilename.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFilename.Location = new System.Drawing.Point(96, 104);
			this.cboFilename.Name = "cboFilename";
			this.cboFilename.Size = new System.Drawing.Size(184, 22);
			this.cboFilename.TabIndex = 23;
			this.cboFilename.TextColor = System.Drawing.Color.Black;
			// 
			// lblFilename
			// 
			this.lblFilename.Location = new System.Drawing.Point(16, 104);
			this.lblFilename.Name = "lblFilename";
			this.lblFilename.Size = new System.Drawing.Size(56, 16);
			this.lblFilename.TabIndex = 22;
			this.lblFilename.Text = "Filename:";
			// 
			// groupTimeZones
			// 
			this.groupTimeZones.Controls.AddRange(new System.Windows.Forms.Control[] {
																						 this.checkBox7,
																						 this.checkBox6,
																						 this.checkBox5,
																						 this.checkBox4,
																						 this.checkBox3,
																						 this.checkBox2,
																						 this.checkBox1});
			this.groupTimeZones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.groupTimeZones.ForeColor = System.Drawing.SystemColors.ControlText;
			this.groupTimeZones.Location = new System.Drawing.Point(304, 88);
			this.groupTimeZones.Name = "groupTimeZones";
			this.groupTimeZones.Size = new System.Drawing.Size(104, 136);
			this.groupTimeZones.TabIndex = 21;
			this.groupTimeZones.TabStop = false;
			this.groupTimeZones.Text = "Time Zones";
			// 
			// checkBox7
			// 
			this.checkBox7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox7.Location = new System.Drawing.Point(8, 112);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(80, 16);
			this.checkBox7.TabIndex = 15;
			this.checkBox7.Text = "Alaska";
			// 
			// checkBox6
			// 
			this.checkBox6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox6.Location = new System.Drawing.Point(8, 96);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(80, 16);
			this.checkBox6.TabIndex = 14;
			this.checkBox6.Text = "Hawaii";
			// 
			// checkBox5
			// 
			this.checkBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox5.Location = new System.Drawing.Point(8, 64);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(88, 16);
			this.checkBox5.TabIndex = 13;
			this.checkBox5.Text = "Mountain";
			// 
			// checkBox4
			// 
			this.checkBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox4.Location = new System.Drawing.Point(8, 32);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(64, 16);
			this.checkBox4.TabIndex = 12;
			this.checkBox4.Text = "Eastern";
			// 
			// checkBox3
			// 
			this.checkBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox3.Location = new System.Drawing.Point(8, 48);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(80, 16);
			this.checkBox3.TabIndex = 11;
			this.checkBox3.Text = "Central";
			// 
			// checkBox2
			// 
			this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox2.Location = new System.Drawing.Point(8, 80);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(88, 16);
			this.checkBox2.TabIndex = 10;
			this.checkBox2.Text = "Pacific";
			// 
			// checkBox1
			// 
			this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox1.Location = new System.Drawing.Point(8, 16);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(88, 16);
			this.checkBox1.TabIndex = 9;
			this.checkBox1.Text = "Atlantic";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(328, 256);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(80, 24);
			this.btnOK.TabIndex = 17;
			this.btnOK.Text = "&Start Queue";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(240, 256);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 24);
			this.btnCancel.TabIndex = 18;
			this.btnCancel.Text = "< &Back";
			// 
			// lblInstructions
			// 
			this.lblInstructions.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lblInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblInstructions.ForeColor = System.Drawing.Color.Black;
			this.lblInstructions.Location = new System.Drawing.Point(16, 8);
			this.lblInstructions.Name = "lblInstructions";
			this.lblInstructions.Size = new System.Drawing.Size(416, 32);
			this.lblInstructions.TabIndex = 30;
			this.lblInstructions.Text = "Set up your call queue";
			// 
			// afniDivider1
			// 
			this.afniDivider1.DividerStyle = Afni.Controls.DividerStyles.Horizontal;
			this.afniDivider1.FirstColor = System.Drawing.Color.White;
			this.afniDivider1.Location = new System.Drawing.Point(8, 240);
			this.afniDivider1.Name = "afniDivider1";
			this.afniDivider1.SecondColor = System.Drawing.Color.FromArgb(((System.Byte)(1)), ((System.Byte)(72)), ((System.Byte)(178)));
			this.afniDivider1.Size = new System.Drawing.Size(400, 1);
			this.afniDivider1.TabIndex = 31;
			this.afniDivider1.Text = "afniDivider1";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(408, 32);
			this.label2.TabIndex = 32;
			this.label2.Text = "In order for VLoop to create an call queue, you must enter some parameters used w" +
				"hen selecting the next customer to call.";
			// 
			// QueueCtl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label2,
																		  this.afniDivider1,
																		  this.lblInstructions,
																		  this.cboState,
																		  this.label1,
																		  this.cboDisposition,
																		  this.lblDisposition,
																		  this.cboLanguage,
																		  this.lblLanguage,
																		  this.cboFilename,
																		  this.lblFilename,
																		  this.groupTimeZones,
																		  this.btnOK,
																		  this.btnCancel});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "QueueCtl";
			this.Size = new System.Drawing.Size(432, 296);
			this.groupTimeZones.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			CallQueueManager mgr = (CallQueueManager)_app.QueueManager;
			mgr.DispositionID = ((Disposition)cboDisposition.SelectedItem).DispositionID;
			mgr.LanguageID = ((LanguageBase)cboLanguage.SelectedItem).LanguageID;
			mgr.FileName = cboFilename.Text;
			mgr.State = cboState.Text;
			mgr.ShowNextCustomer();
		}

		private void OnCampaignSwitch(object sender, System.EventArgs e)
		{
			cboFilename.DataSource = _app.CurrentCampaign.Files;
			cboLanguage.DataSource = _app.CurrentCampaign.Languages;
			cboDisposition.DataSource = _app.CurrentCampaign.Dispositions;
		}
	}
}
