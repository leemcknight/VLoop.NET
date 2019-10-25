using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Afni.Applications.VLoopMaintenance
{
	/// <summary>
	/// Summary description for ctlGeneral.
	/// </summary>
	public class ctlGeneral : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label _lblCampaignName;
		private System.Windows.Forms.TextBox _txtCampName;
		private System.Windows.Forms.Label _lblCode;
		private System.Windows.Forms.TextBox _txtCode;
		private System.Windows.Forms.CheckBox _chkActive;
		private System.Windows.Forms.Label _lblFileDays;
		private System.Windows.Forms.TextBox _txtfileexp;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlGeneral()
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
			this._lblCampaignName = new System.Windows.Forms.Label();
			this._txtCampName = new System.Windows.Forms.TextBox();
			this._lblCode = new System.Windows.Forms.Label();
			this._txtCode = new System.Windows.Forms.TextBox();
			this._chkActive = new System.Windows.Forms.CheckBox();
			this._lblFileDays = new System.Windows.Forms.Label();
			this._txtfileexp = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// _lblCampaignName
			// 
			this._lblCampaignName.Location = new System.Drawing.Point(16, 16);
			this._lblCampaignName.Name = "_lblCampaignName";
			this._lblCampaignName.Size = new System.Drawing.Size(96, 16);
			this._lblCampaignName.TabIndex = 0;
			this._lblCampaignName.Text = "Campaign Name:";
			// 
			// _txtCampName
			// 
			this._txtCampName.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this._txtCampName.Location = new System.Drawing.Point(144, 16);
			this._txtCampName.Name = "_txtCampName";
			this._txtCampName.Size = new System.Drawing.Size(224, 21);
			this._txtCampName.TabIndex = 1;
			this._txtCampName.Text = "";
			// 
			// _lblCode
			// 
			this._lblCode.Location = new System.Drawing.Point(16, 48);
			this._lblCode.Name = "_lblCode";
			this._lblCode.Size = new System.Drawing.Size(88, 16);
			this._lblCode.TabIndex = 2;
			this._lblCode.Text = "Campaign Code:";
			// 
			// _txtCode
			// 
			this._txtCode.Location = new System.Drawing.Point(144, 48);
			this._txtCode.Name = "_txtCode";
			this._txtCode.Size = new System.Drawing.Size(80, 21);
			this._txtCode.TabIndex = 3;
			this._txtCode.Text = "";
			// 
			// _chkActive
			// 
			this._chkActive.Location = new System.Drawing.Point(16, 112);
			this._chkActive.Name = "_chkActive";
			this._chkActive.Size = new System.Drawing.Size(136, 16);
			this._chkActive.TabIndex = 4;
			this._chkActive.Text = "Active";
			// 
			// _lblFileDays
			// 
			this._lblFileDays.Location = new System.Drawing.Point(16, 80);
			this._lblFileDays.Name = "_lblFileDays";
			this._lblFileDays.Size = new System.Drawing.Size(112, 16);
			this._lblFileDays.TabIndex = 5;
			this._lblFileDays.Text = "File Expiration Days:";
			// 
			// _txtfileexp
			// 
			this._txtfileexp.Location = new System.Drawing.Point(144, 80);
			this._txtfileexp.Name = "_txtfileexp";
			this._txtfileexp.Size = new System.Drawing.Size(80, 21);
			this._txtfileexp.TabIndex = 6;
			this._txtfileexp.Text = "";
			// 
			// ctlGeneral
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._txtfileexp,
																		  this._lblFileDays,
																		  this._chkActive,
																		  this._txtCode,
																		  this._lblCode,
																		  this._txtCampName,
																		  this._lblCampaignName});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ctlGeneral";
			this.Size = new System.Drawing.Size(376, 288);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
