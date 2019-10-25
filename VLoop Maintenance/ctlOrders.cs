using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Afni.Applications.VLoopMaintenance
{
	/// <summary>
	/// Summary description for ctlOrders.
	/// </summary>
	public class ctlOrders : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.CheckBox _chkAllowOrders;
		private System.Windows.Forms.Label _lblStartOrderQueue;
		private System.Windows.Forms.ComboBox _cboOrderQueue;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblRegion;
		private System.Windows.Forms.ComboBox _cboRegion;
		private System.Windows.Forms.CheckedListBox _lstQueues;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlOrders()
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._chkAllowOrders = new System.Windows.Forms.CheckBox();
			this._lblStartOrderQueue = new System.Windows.Forms.Label();
			this._cboOrderQueue = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this._lstQueues = new System.Windows.Forms.CheckedListBox();
			this.lblRegion = new System.Windows.Forms.Label();
			this._cboRegion = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// _chkAllowOrders
			// 
			this._chkAllowOrders.Location = new System.Drawing.Point(8, 8);
			this._chkAllowOrders.Name = "_chkAllowOrders";
			this._chkAllowOrders.Size = new System.Drawing.Size(112, 16);
			this._chkAllowOrders.TabIndex = 0;
			this._chkAllowOrders.Text = "Allow Order Entry";
			this._chkAllowOrders.CheckedChanged += new System.EventHandler(this._chkAllowOrders_CheckedChanged);
			// 
			// _lblStartOrderQueue
			// 
			this._lblStartOrderQueue.Location = new System.Drawing.Point(40, 40);
			this._lblStartOrderQueue.Name = "_lblStartOrderQueue";
			this._lblStartOrderQueue.Size = new System.Drawing.Size(120, 16);
			this._lblStartOrderQueue.TabIndex = 1;
			this._lblStartOrderQueue.Text = "Starting Order Queue:";
			// 
			// _cboOrderQueue
			// 
			this._cboOrderQueue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboOrderQueue.Enabled = false;
			this._cboOrderQueue.Location = new System.Drawing.Point(160, 40);
			this._cboOrderQueue.Name = "_cboOrderQueue";
			this._cboOrderQueue.Size = new System.Drawing.Size(144, 21);
			this._cboOrderQueue.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(40, 104);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Use these Queues:";
			// 
			// _lstQueues
			// 
			this._lstQueues.Enabled = false;
			this._lstQueues.Location = new System.Drawing.Point(160, 104);
			this._lstQueues.Name = "_lstQueues";
			this._lstQueues.Size = new System.Drawing.Size(144, 84);
			this._lstQueues.TabIndex = 5;
			// 
			// lblRegion
			// 
			this.lblRegion.Location = new System.Drawing.Point(40, 72);
			this.lblRegion.Name = "lblRegion";
			this.lblRegion.Size = new System.Drawing.Size(48, 16);
			this.lblRegion.TabIndex = 6;
			this.lblRegion.Text = "Region:";
			// 
			// _cboRegion
			// 
			this._cboRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboRegion.Enabled = false;
			this._cboRegion.Location = new System.Drawing.Point(160, 72);
			this._cboRegion.Name = "_cboRegion";
			this._cboRegion.Size = new System.Drawing.Size(144, 21);
			this._cboRegion.TabIndex = 7;
			// 
			// ctlOrders
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._cboRegion,
																		  this.lblRegion,
																		  this._lstQueues,
																		  this.label1,
																		  this._cboOrderQueue,
																		  this._lblStartOrderQueue,
																		  this._chkAllowOrders});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ctlOrders";
			this.Size = new System.Drawing.Size(512, 288);
			this.ResumeLayout(false);

		}
		#endregion

		private void _chkAllowOrders_CheckedChanged(object sender, System.EventArgs e)
		{
			_cboOrderQueue.Enabled = _chkAllowOrders.Checked;
			_cboRegion.Enabled = _chkAllowOrders.Checked;
			_lstQueues.Enabled = _chkAllowOrders.Checked;	
		}
	}
}
