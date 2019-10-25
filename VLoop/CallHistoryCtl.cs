using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for CallHistoryCtl.
	/// </summary>
	public class CallHistoryCtl : UserControl, IForm, ISkinnable
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ListBox lstCampaigns;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lblNotes;
		private System.Windows.Forms.TextBox txtNotes;
		private System.Windows.Forms.Label lblContactTypeHDR;
		private System.Windows.Forms.Label lblTransferTypeHDR;
		private System.Windows.Forms.Label lblCallResultHDR;
		private System.Windows.Forms.ColumnHeader hdrCallDate;
		private System.Windows.Forms.ColumnHeader hdrCampaign;
		private System.Windows.Forms.ColumnHeader hdrDisposition;
		private System.Windows.Forms.ColumnHeader hdrRep;
		private Afni.FormData.FormStates _form_state;
		private Afni.Applications.VLoop.DisplayTheme _theme;

		public CallHistoryCtl()
		{
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

		string IForm.Name
		{
			get { return "Call History"; }
			set {}
		}

		#endregion

		#region ISkinnable implementation
		bool ISkinnable.ApplyTheme(DisplayTheme theme)
		{
			bool theme_ok = true;
			try
			{
				this.BackColor = theme.FormBackColor;
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.lstCampaigns = new System.Windows.Forms.ListBox();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lblNotes = new System.Windows.Forms.Label();
			this.txtNotes = new System.Windows.Forms.TextBox();
			this.lblContactTypeHDR = new System.Windows.Forms.Label();
			this.lblTransferTypeHDR = new System.Windows.Forms.Label();
			this.lblCallResultHDR = new System.Windows.Forms.Label();
			this.hdrCallDate = new System.Windows.Forms.ColumnHeader();
			this.hdrCampaign = new System.Windows.Forms.ColumnHeader();
			this.hdrDisposition = new System.Windows.Forms.ColumnHeader();
			this.hdrRep = new System.Windows.Forms.ColumnHeader();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.label1,
																				 this.listView1,
																				 this.lstCampaigns});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(592, 192);
			this.panel1.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Show Attempts for:";
			// 
			// listView1
			// 
			this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.hdrCallDate,
																						this.hdrCampaign,
																						this.hdrDisposition,
																						this.hdrRep});
			this.listView1.Location = new System.Drawing.Point(136, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(448, 176);
			this.listView1.TabIndex = 3;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// lstCampaigns
			// 
			this.lstCampaigns.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left);
			this.lstCampaigns.IntegralHeight = false;
			this.lstCampaigns.Location = new System.Drawing.Point(8, 32);
			this.lstCampaigns.Name = "lstCampaigns";
			this.lstCampaigns.Size = new System.Drawing.Size(120, 152);
			this.lstCampaigns.TabIndex = 2;
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 192);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(592, 5);
			this.splitter1.TabIndex = 9;
			this.splitter1.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.lblCallResultHDR,
																				 this.lblTransferTypeHDR,
																				 this.lblContactTypeHDR,
																				 this.lblNotes,
																				 this.txtNotes});
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 197);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(592, 251);
			this.panel2.TabIndex = 10;
			// 
			// lblNotes
			// 
			this.lblNotes.Location = new System.Drawing.Point(8, 56);
			this.lblNotes.Name = "lblNotes";
			this.lblNotes.Size = new System.Drawing.Size(40, 16);
			this.lblNotes.TabIndex = 5;
			this.lblNotes.Text = "Notes:";
			// 
			// txtNotes
			// 
			this.txtNotes.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtNotes.Location = new System.Drawing.Point(8, 80);
			this.txtNotes.Multiline = true;
			this.txtNotes.Name = "txtNotes";
			this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtNotes.Size = new System.Drawing.Size(576, 168);
			this.txtNotes.TabIndex = 4;
			this.txtNotes.Text = "";
			// 
			// lblContactTypeHDR
			// 
			this.lblContactTypeHDR.Location = new System.Drawing.Point(8, 8);
			this.lblContactTypeHDR.Name = "lblContactTypeHDR";
			this.lblContactTypeHDR.Size = new System.Drawing.Size(80, 16);
			this.lblContactTypeHDR.TabIndex = 6;
			this.lblContactTypeHDR.Text = "Contact Type:";
			// 
			// lblTransferTypeHDR
			// 
			this.lblTransferTypeHDR.Location = new System.Drawing.Point(256, 8);
			this.lblTransferTypeHDR.Name = "lblTransferTypeHDR";
			this.lblTransferTypeHDR.Size = new System.Drawing.Size(96, 16);
			this.lblTransferTypeHDR.TabIndex = 7;
			this.lblTransferTypeHDR.Text = "Transfer Type:";
			// 
			// lblCallResultHDR
			// 
			this.lblCallResultHDR.Location = new System.Drawing.Point(8, 32);
			this.lblCallResultHDR.Name = "lblCallResultHDR";
			this.lblCallResultHDR.Size = new System.Drawing.Size(88, 24);
			this.lblCallResultHDR.TabIndex = 8;
			this.lblCallResultHDR.Text = "Call Result:";
			// 
			// hdrCallDate
			// 
			this.hdrCallDate.Text = "Call Date/Time";
			this.hdrCallDate.Width = 101;
			// 
			// hdrCampaign
			// 
			this.hdrCampaign.Text = "Campaign";
			this.hdrCampaign.Width = 132;
			// 
			// hdrDisposition
			// 
			this.hdrDisposition.Text = "Disposition";
			this.hdrDisposition.Width = 96;
			// 
			// hdrRep
			// 
			this.hdrRep.Text = "Call Rep";
			this.hdrRep.Width = 111;
			// 
			// CallHistoryCtl
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel2,
																		  this.splitter1,
																		  this.panel1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "CallHistoryCtl";
			this.Size = new System.Drawing.Size(592, 448);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
