using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Afni.FormData;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for frmOpt.
	/// </summary>
	public class frmOpt : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView tvwOptions;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;
		private ISaveable _current_ctl;
		private System.Windows.Forms.Panel panelCTL;
		private Afni.Applications.VLoop.Application _app;

		public frmOpt(Afni.Applications.VLoop.Application app)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			_app = app;
			ctlTheme theme_ctl = new ctlTheme(_app);
			theme_ctl.Parent = panelCTL;
			theme_ctl.Dock = DockStyle.Fill;
			_current_ctl = (ISaveable)theme_ctl;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmOpt));
			this.tvwOptions = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.panelCTL = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// tvwOptions
			// 
			this.tvwOptions.ImageList = this.imageList1;
			this.tvwOptions.Location = new System.Drawing.Point(8, 8);
			this.tvwOptions.Name = "tvwOptions";
			this.tvwOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																				   new System.Windows.Forms.TreeNode("Environment", 0, 1, new System.Windows.Forms.TreeNode[] {
																																												  new System.Windows.Forms.TreeNode("Graphics Scheme")})});
			this.tvwOptions.Scrollable = false;
			this.tvwOptions.ShowLines = false;
			this.tvwOptions.ShowPlusMinus = false;
			this.tvwOptions.ShowRootLines = false;
			this.tvwOptions.Size = new System.Drawing.Size(160, 288);
			this.tvwOptions.TabIndex = 0;
			this.tvwOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwOptions_AfterSelect);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// groupBox1
			// 
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(176, 288);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(408, 8);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(512, 312);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(432, 312);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 24);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// panelCTL
			// 
			this.panelCTL.Location = new System.Drawing.Point(176, 8);
			this.panelCTL.Name = "panelCTL";
			this.panelCTL.Size = new System.Drawing.Size(408, 280);
			this.panelCTL.TabIndex = 4;
			// 
			// frmOpt
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(592, 350);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panelCTL,
																		  this.btnOK,
																		  this.btnCancel,
																		  this.groupBox1,
																		  this.tvwOptions});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmOpt";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "VLoop Options";
			this.ResumeLayout(false);

		}
		#endregion

		private void tvwOptions_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
		
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			_current_ctl.Save();
		}
	}
}
