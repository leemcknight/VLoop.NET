using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;
using Afni.Applications.VLoop;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for HomeCtl.
	/// </summary>
	public class ctlHome : System.Windows.Forms.UserControl, IForm
	{
		private System.Windows.Forms.Label _lblCustName;
		private Afni.Applications.VLoop.Application _app;
		private System.Windows.Forms.Label _lblBTN;
		private System.Windows.Forms.LinkLabel lnkCampaign;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlHome(Afni.Applications.VLoop.Application app)
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

		#region IForm implementation
		bool IForm.Refresh()
		{
			Customer cust;
			Graphics g = System.Drawing.Graphics.FromHwnd(this.Handle);
			cust = _app.Call.CurrentCustCampaign.Customer;
			_lblCustName.Text = cust.FirstName + " " + cust.LastName;
			_lblCustName.Width = 5 + (int)g.MeasureString(_lblCustName.Text,
												_lblCustName.Font).Width;
			
			_lblBTN.Text = cust.Account.BTN.ToString();
			return true;
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		string IForm.Name
		{
			get { return "VLoop Home"; }
			set {}
		}

		FormStates IForm.FormState
		{
			get { return FormStates.Idle; }
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._lblCustName = new System.Windows.Forms.Label();
			this._lblBTN = new System.Windows.Forms.Label();
			this.lnkCampaign = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// _lblCustName
			// 
			this._lblCustName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCustName.ForeColor = System.Drawing.Color.CornflowerBlue;
			this._lblCustName.Location = new System.Drawing.Point(24, 24);
			this._lblCustName.Name = "_lblCustName";
			this._lblCustName.Size = new System.Drawing.Size(256, 32);
			this._lblCustName.TabIndex = 0;
			// 
			// _lblBTN
			// 
			this._lblBTN.Location = new System.Drawing.Point(32, 80);
			this._lblBTN.Name = "_lblBTN";
			this._lblBTN.Size = new System.Drawing.Size(88, 16);
			this._lblBTN.TabIndex = 1;
			this._lblBTN.Text = "(309)735-8836";
			// 
			// lnkCampaign
			// 
			this.lnkCampaign.ActiveLinkColor = System.Drawing.Color.Blue;
			this.lnkCampaign.LinkArea = new System.Windows.Forms.LinkArea(20, 42);
			this.lnkCampaign.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkCampaign.Location = new System.Drawing.Point(32, 112);
			this.lnkCampaign.Name = "lnkCampaign";
			this.lnkCampaign.Size = new System.Drawing.Size(240, 16);
			this.lnkCampaign.TabIndex = 2;
			this.lnkCampaign.TabStop = true;
			this.lnkCampaign.Text = "You are calling for West Business Welcome.";
			this.lnkCampaign.VisitedLinkColor = System.Drawing.Color.Blue;
			// 
			// ctlHome
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lnkCampaign,
																		  this._lblBTN,
																		  this._lblCustName});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ctlHome";
			this.Size = new System.Drawing.Size(568, 544);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
