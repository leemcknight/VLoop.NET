using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for ctlOrderValidation.
	/// </summary>
	public class ctlOrderValidation : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ProgressBar _progOrder;
		private System.Windows.Forms.Label _lblStatus;
		private System.Windows.Forms.Label _lblHDR;
		private System.Windows.Forms.Label label1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlOrderValidation()
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
			this._progOrder = new System.Windows.Forms.ProgressBar();
			this._lblStatus = new System.Windows.Forms.Label();
			this._lblHDR = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _progOrder
			// 
			this._progOrder.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this._progOrder.Location = new System.Drawing.Point(8, 168);
			this._progOrder.Name = "_progOrder";
			this._progOrder.Size = new System.Drawing.Size(400, 24);
			this._progOrder.TabIndex = 0;
			// 
			// _lblStatus
			// 
			this._lblStatus.Location = new System.Drawing.Point(8, 144);
			this._lblStatus.Name = "_lblStatus";
			this._lblStatus.Size = new System.Drawing.Size(184, 16);
			this._lblStatus.TabIndex = 1;
			this._lblStatus.Text = "Compiling Order...";
			// 
			// _lblHDR
			// 
			this._lblHDR.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblHDR.ForeColor = System.Drawing.Color.CornflowerBlue;
			this._lblHDR.Location = new System.Drawing.Point(16, 8);
			this._lblHDR.Name = "_lblHDR";
			this._lblHDR.Size = new System.Drawing.Size(384, 32);
			this._lblHDR.TabIndex = 2;
			this._lblHDR.Text = "Validating your order";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(376, 40);
			this.label1.TabIndex = 3;
			this.label1.Text = "Please wait while VLoop compiles your order and checks it for errors.  This could" +
				" take several seconds.";
			// 
			// ctlOrderValidation
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label1,
																		  this._lblHDR,
																		  this._lblStatus,
																		  this._progOrder});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ctlOrderValidation";
			this.Size = new System.Drawing.Size(416, 504);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
