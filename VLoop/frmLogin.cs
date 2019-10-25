using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Afni.FormData;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopBusinessObjects;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	/// 
	public class frmLogin : System.Windows.Forms.Form, IForm
	{
		private System.Windows.Forms.Label lblSSN;
		private System.Windows.Forms.TextBox txtSSN;
		private System.Windows.Forms.Label lblCampaign;
		private System.Windows.Forms.ComboBox cboCampaign;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblInstruct;
		private Afni.FormData.FormStates _form_state;
		private Afni.Applications.VLoop.Application _app;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.PictureBox pictureBox2;
		private ArrayList _campaigns;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmLogin(Afni.Applications.VLoop.Application app)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			_campaigns = new ArrayList();
			_app = app;
			FillCampaignCombo();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		protected void frmLogin_Paint(object sender, System.Windows.Forms.PaintEventArgs pe)
		{
			Rectangle rect1;
			Rectangle rect2;
			LinearGradientBrush gradient;
			LinearGradientBrush gradient2;
			
			rect1 = new Rectangle(new Point(0,panel1.Height - 2), new Size(this.Width / 2, 2));
			rect2 = new Rectangle(new Point(this.Width/2, panel1.Height-2), new Size(this.Width / 2, 2));
			gradient = new LinearGradientBrush(rect1,Color.White,Color.CornflowerBlue,(float)0,false);
			gradient2 = new LinearGradientBrush(rect2, Color.CornflowerBlue, Color.White,(float)0,false);
			Graphics g = Graphics.FromHwnd(panel1.Handle);
			g.FillRectangle(gradient,rect1);
			g.FillRectangle(gradient2, rect2);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmLogin));
			this.lblSSN = new System.Windows.Forms.Label();
			this.txtSSN = new System.Windows.Forms.TextBox();
			this.lblCampaign = new System.Windows.Forms.Label();
			this.cboCampaign = new System.Windows.Forms.ComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblInstruct = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblSSN
			// 
			this.lblSSN.Location = new System.Drawing.Point(64, 72);
			this.lblSSN.Name = "lblSSN";
			this.lblSSN.Size = new System.Drawing.Size(32, 16);
			this.lblSSN.TabIndex = 0;
			this.lblSSN.Text = "SSN:";
			// 
			// txtSSN
			// 
			this.txtSSN.Location = new System.Drawing.Point(136, 72);
			this.txtSSN.MaxLength = 9;
			this.txtSSN.Name = "txtSSN";
			this.txtSSN.PasswordChar = '*';
			this.txtSSN.Size = new System.Drawing.Size(176, 21);
			this.txtSSN.TabIndex = 1;
			this.txtSSN.Text = "";
			// 
			// lblCampaign
			// 
			this.lblCampaign.Location = new System.Drawing.Point(64, 104);
			this.lblCampaign.Name = "lblCampaign";
			this.lblCampaign.Size = new System.Drawing.Size(64, 16);
			this.lblCampaign.TabIndex = 3;
			this.lblCampaign.Text = "Campaign:";
			// 
			// cboCampaign
			// 
			this.cboCampaign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCampaign.Location = new System.Drawing.Point(136, 104);
			this.cboCampaign.Name = "cboCampaign";
			this.cboCampaign.Size = new System.Drawing.Size(176, 21);
			this.cboCampaign.TabIndex = 4;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.pictureBox1,
																				 this.lblInstruct});
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(368, 56);
			this.panel1.TabIndex = 5;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(184, 5);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(128, 40);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// lblInstruct
			// 
			this.lblInstruct.Location = new System.Drawing.Point(8, 8);
			this.lblInstruct.Name = "lblInstruct";
			this.lblInstruct.Size = new System.Drawing.Size(168, 32);
			this.lblInstruct.TabIndex = 1;
			this.lblInstruct.Text = "Please enter your social security number and select a campaign.";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(176, 168);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(64, 24);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(248, 168);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(64, 24);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "&Cancel";
			// 
			// lblStatus
			// 
			this.lblStatus.BackColor = System.Drawing.Color.Navy;
			this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.White;
			this.lblStatus.Location = new System.Drawing.Point(8, 136);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(304, 21);
			this.lblStatus.TabIndex = 8;
			this.lblStatus.Text = "VLoop: Login Please";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(8, 72);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(48, 48);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 9;
			this.pictureBox2.TabStop = false;
			// 
			// frmLogin
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(322, 200);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox2,
																		  this.lblStatus,
																		  this.btnCancel,
																		  this.btnOK,
																		  this.panel1,
																		  this.cboCampaign,
																		  this.lblCampaign,
																		  this.txtSSN,
																		  this.lblSSN});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmLogin";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "VLoop Login";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmLogin_Paint);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		
		private void FillCampaignCombo()
		{
			IManager camp_bso = new CampaignBSO();
			_campaigns = (ArrayList)camp_bso.GetAll();
			cboCampaign.DataSource = _campaigns;
			cboCampaign.DisplayMember = "CampaignName";
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if(txtSSN.Text.Length != 9)
			{
				lblStatus.Text = "Please enter a valid SSN...";
			}
			else
			{
				lblStatus.Text = "Validating SSN...";
				_app.CurrentCampaign = new Campaign((CampaignBase)cboCampaign.SelectedItem);
				_app.User.SSN = txtSSN.Text;
				_app.User.Name = "Lee J. McKnight";
				this.DialogResult = DialogResult.OK;
			}
		}

		#region IForm implementation
		Afni.FormData.FormStates IForm.FormState
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
			get { return "Login"; }
			set {}
		}
		#endregion
	}
}
