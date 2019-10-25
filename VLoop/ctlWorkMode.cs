using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;
using Afni.Controls;
using Afni.Applications.VLoop.VLoopDataObjects;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for ctlWorkMode.
	/// </summary>
	public class ctlWorkMode : System.Windows.Forms.UserControl , IForm, ISkinnable
	{
		private Afni.Controls.AfniLink lnkQueue;
		private Afni.Controls.AfniLink lnkDialer;
		private Afni.Controls.AfniLink lnkOrderQueue;
		private Afni.Controls.AfniLink lnkCustSearch;
		private System.ComponentModel.IContainer components;
		private FormStates _form_state;
		private Afni.Applications.VLoop.Application _app;
		private System.Windows.Forms.Label lblHdr;
		private Afni.Applications.VLoop.DisplayTheme _theme;

		public ctlWorkMode(Afni.Applications.VLoop.Application app)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			_app = app;
			lnkCustSearch.Icon = VLoopIcons.Next;
			lnkDialer.Icon = VLoopIcons.Next;
			lnkOrderQueue.Icon = VLoopIcons.Next;
			lnkQueue.Icon = VLoopIcons.Next;
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
			bool refresh_ok = true;
			ApplicationSetting setting;

			try	{

				setting = (ApplicationSetting)_app.CurrentCampaign.AppSettings[ApplicationSettings.ShowOrders];
				if(setting != null && setting.Value.ToUpper() == "YES")
					lnkOrderQueue.Visible = true;
				else
					lnkOrderQueue.Visible = false;

				setting = (ApplicationSetting)_app.CurrentCampaign.AppSettings[ApplicationSettings.UsesDialer];
				if(setting != null && setting.Value.ToUpper() == "YES")
					lnkDialer.Visible = true;
				else
					lnkDialer.Visible = false;
			}
			catch
			{
				refresh_ok = false;
				MessageBox.Show("Unable to show work modes for " +
								_app.CurrentCampaign.CampaignName +	".",
								"VLoop Error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
			}

			return refresh_ok;
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		FormStates IForm.FormState
		{
			get { return _form_state; }
		}

		string IForm.Name
		{
			get { return "VLoop Work Mode Selection"; }
			set {}
		}

		#endregion

		#region ISkinnable implementation
		bool ISkinnable.ApplyTheme(DisplayTheme theme)
		{
			try 
			{
				this.BackColor = theme.SpecialFormBackColor;
				this.lblHdr.ForeColor = theme.SpecialFormHeaderColor;
				lnkCustSearch.ForeColor = theme.SpecialFormFontColor;
				lnkCustSearch.LinkColor = theme.SpecialFormFontColor;
				lnkCustSearch.ActiveLinkColor = theme.SpecialFormFontColor;
				lnkQueue.ForeColor = theme.SpecialFormFontColor;
				lnkQueue.LinkColor = theme.SpecialFormFontColor;
				lnkQueue.ActiveLinkColor = theme.SpecialFormFontColor;
				if(lnkDialer.Visible)
				{
					lnkDialer.ForeColor = theme.SpecialFormFontColor;
					lnkDialer.LinkColor = theme.SpecialFormFontColor;
					lnkDialer.ActiveLinkColor = theme.SpecialFormFontColor;
				}

				if(lnkOrderQueue.Visible)
				{
					lnkOrderQueue.ForeColor = theme.SpecialFormFontColor;
					lnkOrderQueue.LinkColor = theme.SpecialFormFontColor;
					lnkOrderQueue.ActiveLinkColor = theme.SpecialFormFontColor;
				}
			}
			catch 
			{
				return false;
			}


			return true;

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
			this.lnkQueue = new Afni.Controls.AfniLink();
			this.lnkDialer = new Afni.Controls.AfniLink();
			this.lnkOrderQueue = new Afni.Controls.AfniLink();
			this.lnkCustSearch = new Afni.Controls.AfniLink();
			this.lblHdr = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lnkQueue
			// 
			this.lnkQueue.ActiveLinkColor = System.Drawing.Color.White;
			this.lnkQueue.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lnkQueue.Icon = null;
			this.lnkQueue.IconSize = 16;
			this.lnkQueue.LinkColor = System.Drawing.Color.White;
			this.lnkQueue.Location = new System.Drawing.Point(24, 104);
			this.lnkQueue.Name = "lnkQueue";
			this.lnkQueue.Size = new System.Drawing.Size(424, 16);
			this.lnkQueue.TabIndex = 38;
			this.lnkQueue.Text = "I want to build a call queue and have VLoop select my customers.";
			this.lnkQueue.Click += new System.EventHandler(this.lnkQueue_Click);
			// 
			// lnkDialer
			// 
			this.lnkDialer.ActiveLinkColor = System.Drawing.Color.White;
			this.lnkDialer.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lnkDialer.Icon = null;
			this.lnkDialer.IconSize = 16;
			this.lnkDialer.LinkColor = System.Drawing.Color.White;
			this.lnkDialer.Location = new System.Drawing.Point(24, 128);
			this.lnkDialer.Name = "lnkDialer";
			this.lnkDialer.Size = new System.Drawing.Size(336, 16);
			this.lnkDialer.TabIndex = 37;
			this.lnkDialer.Text = "I want to use the dialer.";
			this.lnkDialer.Click += new System.EventHandler(this.lnkDialer_Click);
			// 
			// lnkOrderQueue
			// 
			this.lnkOrderQueue.ActiveLinkColor = System.Drawing.Color.White;
			this.lnkOrderQueue.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lnkOrderQueue.Icon = null;
			this.lnkOrderQueue.IconSize = 16;
			this.lnkOrderQueue.LinkColor = System.Drawing.Color.White;
			this.lnkOrderQueue.Location = new System.Drawing.Point(24, 152);
			this.lnkOrderQueue.Name = "lnkOrderQueue";
			this.lnkOrderQueue.Size = new System.Drawing.Size(368, 16);
			this.lnkOrderQueue.TabIndex = 39;
			this.lnkOrderQueue.Text = "I want to work orders that are in a specific order queue.";
			this.lnkOrderQueue.Click += new System.EventHandler(this.lnkOrderQueue_Click);
			// 
			// lnkCustSearch
			// 
			this.lnkCustSearch.ActiveLinkColor = System.Drawing.Color.White;
			this.lnkCustSearch.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lnkCustSearch.Icon = null;
			this.lnkCustSearch.IconSize = 16;
			this.lnkCustSearch.LinkColor = System.Drawing.Color.White;
			this.lnkCustSearch.Location = new System.Drawing.Point(24, 80);
			this.lnkCustSearch.Name = "lnkCustSearch";
			this.lnkCustSearch.Size = new System.Drawing.Size(384, 16);
			this.lnkCustSearch.TabIndex = 36;
			this.lnkCustSearch.Text = "I want to manually search for customers after each call.";
			this.lnkCustSearch.Click += new System.EventHandler(this.lnkCustSearch_Click);
			// 
			// lblHdr
			// 
			this.lblHdr.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblHdr.Location = new System.Drawing.Point(24, 24);
			this.lblHdr.Name = "lblHdr";
			this.lblHdr.Size = new System.Drawing.Size(440, 32);
			this.lblHdr.TabIndex = 40;
			this.lblHdr.Text = "Select your work mode";
			// 
			// ctlWorkMode
			// 
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(99)), ((System.Byte)(117)), ((System.Byte)(214)));
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lblHdr,
																		  this.lnkOrderQueue,
																		  this.lnkQueue,
																		  this.lnkDialer,
																		  this.lnkCustSearch});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ctlWorkMode";
			this.Size = new System.Drawing.Size(672, 560);
			this.ResumeLayout(false);

		}
		#endregion

		private void lnkCustSearch_Click(object sender, System.EventArgs e)
		{
			_app.WorkMode = WorkModes.Manual;
		}

		private void lnkQueue_Click(object sender, System.EventArgs e)
		{
			_app.WorkMode = WorkModes.CallQueue;
		}

		private void lnkDialer_Click(object sender, System.EventArgs e)
		{
			_app.WorkMode = WorkModes.Dialer;
		}

		private void lnkOrderQueue_Click(object sender, System.EventArgs e)
		{
			_app.WorkMode = WorkModes.OrderQueue;
		}
	}
}
