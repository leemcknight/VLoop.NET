using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Afni.Applications.VLoopMaintenance
{
	/// <summary>
	/// Summary description for ctlScreens.
	/// </summary>
	public class ctlScreens : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.CheckBox _chkAccount;
		private System.Windows.Forms.CheckBox _chkWTN;
		private System.Windows.Forms.CheckBox _chkJobAids;
		private System.Windows.Forms.Label _lblJobAidsURL;
		private System.Windows.Forms.TextBox _txtJobAidsURL;
		private System.Windows.Forms.CheckBox _chkVIS;
		private System.Windows.Forms.Label _lblVISURL;
		private System.Windows.Forms.TextBox _txtVISURL;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlScreens()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

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
			this._chkAccount = new System.Windows.Forms.CheckBox();
			this._chkWTN = new System.Windows.Forms.CheckBox();
			this._chkJobAids = new System.Windows.Forms.CheckBox();
			this._lblJobAidsURL = new System.Windows.Forms.Label();
			this._txtJobAidsURL = new System.Windows.Forms.TextBox();
			this._chkVIS = new System.Windows.Forms.CheckBox();
			this._lblVISURL = new System.Windows.Forms.Label();
			this._txtVISURL = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// _chkAccount
			// 
			this._chkAccount.Location = new System.Drawing.Point(16, 16);
			this._chkAccount.Name = "_chkAccount";
			this._chkAccount.Size = new System.Drawing.Size(216, 16);
			this._chkAccount.TabIndex = 0;
			this._chkAccount.Text = "Show Account Screen";
			// 
			// _chkWTN
			// 
			this._chkWTN.Location = new System.Drawing.Point(16, 40);
			this._chkWTN.Name = "_chkWTN";
			this._chkWTN.Size = new System.Drawing.Size(168, 16);
			this._chkWTN.TabIndex = 1;
			this._chkWTN.Text = "Show WTN Screen";
			// 
			// _chkJobAids
			// 
			this._chkJobAids.Location = new System.Drawing.Point(16, 64);
			this._chkJobAids.Name = "_chkJobAids";
			this._chkJobAids.Size = new System.Drawing.Size(120, 16);
			this._chkJobAids.TabIndex = 2;
			this._chkJobAids.Text = "Show Job Aids";
			this._chkJobAids.CheckedChanged += new System.EventHandler(this._chkJobAids_CheckedChanged);
			// 
			// _lblJobAidsURL
			// 
			this._lblJobAidsURL.Location = new System.Drawing.Point(56, 88);
			this._lblJobAidsURL.Name = "_lblJobAidsURL";
			this._lblJobAidsURL.Size = new System.Drawing.Size(32, 16);
			this._lblJobAidsURL.TabIndex = 3;
			this._lblJobAidsURL.Text = "URL:";
			// 
			// _txtJobAidsURL
			// 
			this._txtJobAidsURL.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this._txtJobAidsURL.Location = new System.Drawing.Point(88, 88);
			this._txtJobAidsURL.Name = "_txtJobAidsURL";
			this._txtJobAidsURL.Size = new System.Drawing.Size(408, 21);
			this._txtJobAidsURL.TabIndex = 4;
			this._txtJobAidsURL.Text = "";
			// 
			// _chkVIS
			// 
			this._chkVIS.Location = new System.Drawing.Point(16, 120);
			this._chkVIS.Name = "_chkVIS";
			this._chkVIS.Size = new System.Drawing.Size(96, 16);
			this._chkVIS.TabIndex = 5;
			this._chkVIS.Text = "Show VIS Link";
			this._chkVIS.CheckedChanged += new System.EventHandler(this._chkVIS_CheckedChanged);
			// 
			// _lblVISURL
			// 
			this._lblVISURL.Location = new System.Drawing.Point(56, 144);
			this._lblVISURL.Name = "_lblVISURL";
			this._lblVISURL.Size = new System.Drawing.Size(32, 16);
			this._lblVISURL.TabIndex = 6;
			this._lblVISURL.Text = "URL:";
			// 
			// _txtVISURL
			// 
			this._txtVISURL.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this._txtVISURL.Location = new System.Drawing.Point(88, 144);
			this._txtVISURL.Name = "_txtVISURL";
			this._txtVISURL.Size = new System.Drawing.Size(408, 21);
			this._txtVISURL.TabIndex = 7;
			this._txtVISURL.Text = "";
			// 
			// ctlScreens
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._txtVISURL,
																		  this._lblVISURL,
																		  this._chkVIS,
																		  this._txtJobAidsURL,
																		  this._lblJobAidsURL,
																		  this._chkJobAids,
																		  this._chkWTN,
																		  this._chkAccount});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ctlScreens";
			this.Size = new System.Drawing.Size(504, 248);
			this.ResumeLayout(false);

		}
		#endregion

		private void _chkJobAids_CheckedChanged(object sender, System.EventArgs e)
		{
			_txtJobAidsURL.Enabled = _chkJobAids.Checked;
			_lblJobAidsURL.Enabled = _chkJobAids.Checked;
		}

		private void _chkVIS_CheckedChanged(object sender, System.EventArgs e)
		{
			_txtVISURL.Enabled = _chkVIS.Checked;
			_lblVISURL.Enabled = _chkVIS.Checked;
		}
	}
}
