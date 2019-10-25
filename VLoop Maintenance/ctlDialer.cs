using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Afni.Applications.VLoopMaintenance
{
	/// <summary>
	/// Summary description for ctlDialer.
	/// </summary>
	public class ctlDialer : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.ListBox _lstSplits;
		private System.Windows.Forms.Label lblDialerSplit;
		private System.Windows.Forms.Button _btnAddSplit;
		private System.Windows.Forms.Button _btnRemove;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlDialer()
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
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this._lstSplits = new System.Windows.Forms.ListBox();
			this.lblDialerSplit = new System.Windows.Forms.Label();
			this._btnAddSplit = new System.Windows.Forms.Button();
			this._btnRemove = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(16, 16);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(184, 16);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "Use Dialer";
			// 
			// _lstSplits
			// 
			this._lstSplits.Location = new System.Drawing.Point(136, 56);
			this._lstSplits.Name = "_lstSplits";
			this._lstSplits.Size = new System.Drawing.Size(184, 56);
			this._lstSplits.TabIndex = 1;
			// 
			// lblDialerSplit
			// 
			this.lblDialerSplit.Location = new System.Drawing.Point(48, 56);
			this.lblDialerSplit.Name = "lblDialerSplit";
			this.lblDialerSplit.Size = new System.Drawing.Size(80, 16);
			this.lblDialerSplit.TabIndex = 2;
			this.lblDialerSplit.Text = "Dialer Split(s):";
			// 
			// _btnAddSplit
			// 
			this._btnAddSplit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._btnAddSplit.Location = new System.Drawing.Point(328, 56);
			this._btnAddSplit.Name = "_btnAddSplit";
			this._btnAddSplit.Size = new System.Drawing.Size(56, 24);
			this._btnAddSplit.TabIndex = 3;
			this._btnAddSplit.Text = "&Add";
			// 
			// _btnRemove
			// 
			this._btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._btnRemove.Location = new System.Drawing.Point(328, 88);
			this._btnRemove.Name = "_btnRemove";
			this._btnRemove.Size = new System.Drawing.Size(56, 24);
			this._btnRemove.TabIndex = 4;
			this._btnRemove.Text = "&Remove";
			// 
			// ctlDialer
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._btnRemove,
																		  this._btnAddSplit,
																		  this.lblDialerSplit,
																		  this._lstSplits,
																		  this.checkBox1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ctlDialer";
			this.Size = new System.Drawing.Size(472, 320);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
