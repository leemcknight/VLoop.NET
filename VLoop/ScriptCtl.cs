using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;
using System.Drawing.Drawing2D;

namespace Afni.Applications.VLoop
{
	public class ScriptNavigator : System.Windows.Forms.UserControl, IForm
	{
		private Afni.FormData.FormStates _form_state;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Label lblSelectedScript;
		private System.Windows.Forms.Label lblAvailScripts;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.ListBox lbScripts;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TextBox txtScript;
		private System.ComponentModel.IContainer components;

		public ScriptNavigator()
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

		FormStates IForm.FormState
		{
			get { return _form_state; }
		}

		bool IForm.Refresh()
		{
			return true;
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		string IForm.Name
		{
			get { return "Scripts"; }
			set { }
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ScriptNavigator));
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtScript = new System.Windows.Forms.TextBox();
			this.btnUp = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.lblSelectedScript = new System.Windows.Forms.Label();
			this.lblAvailScripts = new System.Windows.Forms.Label();
			this.btnDown = new System.Windows.Forms.Button();
			this.lbScripts = new System.Windows.Forms.ListBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.txtScript,
																				 this.btnUp,
																				 this.lblSelectedScript,
																				 this.lblAvailScripts,
																				 this.btnDown,
																				 this.lbScripts});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.ForeColor = System.Drawing.Color.Transparent;
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(608, 224);
			this.panel1.TabIndex = 0;
			// 
			// txtScript
			// 
			this.txtScript.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtScript.BackColor = System.Drawing.SystemColors.Info;
			this.txtScript.Location = new System.Drawing.Point(168, 24);
			this.txtScript.Multiline = true;
			this.txtScript.Name = "txtScript";
			this.txtScript.ReadOnly = true;
			this.txtScript.Size = new System.Drawing.Size(432, 192);
			this.txtScript.TabIndex = 19;
			this.txtScript.Text = "";
			// 
			// btnUp
			// 
			this.btnUp.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.btnUp.BackColor = System.Drawing.Color.Transparent;
			this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnUp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnUp.ForeColor = System.Drawing.Color.Transparent;
			this.btnUp.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnUp.Image")));
			this.btnUp.ImageIndex = 1;
			this.btnUp.ImageList = this.imageList1;
			this.btnUp.Location = new System.Drawing.Point(88, 192);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(32, 26);
			this.btnUp.TabIndex = 18;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// lblSelectedScript
			// 
			this.lblSelectedScript.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSelectedScript.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblSelectedScript.Location = new System.Drawing.Point(168, 8);
			this.lblSelectedScript.Name = "lblSelectedScript";
			this.lblSelectedScript.Size = new System.Drawing.Size(232, 16);
			this.lblSelectedScript.TabIndex = 17;
			this.lblSelectedScript.Text = "Selected Script:";
			// 
			// lblAvailScripts
			// 
			this.lblAvailScripts.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblAvailScripts.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblAvailScripts.Location = new System.Drawing.Point(8, 8);
			this.lblAvailScripts.Name = "lblAvailScripts";
			this.lblAvailScripts.Size = new System.Drawing.Size(152, 16);
			this.lblAvailScripts.TabIndex = 16;
			this.lblAvailScripts.Text = "Available Scripts:";
			// 
			// btnDown
			// 
			this.btnDown.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.btnDown.BackColor = System.Drawing.Color.Transparent;
			this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDown.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnDown.ForeColor = System.Drawing.Color.Transparent;
			this.btnDown.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnDown.Image")));
			this.btnDown.ImageIndex = 0;
			this.btnDown.ImageList = this.imageList1;
			this.btnDown.Location = new System.Drawing.Point(40, 192);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(32, 26);
			this.btnDown.TabIndex = 15;
			// 
			// lbScripts
			// 
			this.lbScripts.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left);
			this.lbScripts.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbScripts.IntegralHeight = false;
			this.lbScripts.Location = new System.Drawing.Point(8, 26);
			this.lbScripts.Name = "lbScripts";
			this.lbScripts.Size = new System.Drawing.Size(152, 158);
			this.lbScripts.TabIndex = 14;
			// 
			// ScriptNavigator
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ScriptNavigator";
			this.Size = new System.Drawing.Size(608, 224);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
