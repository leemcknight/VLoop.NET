using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for SplashScreen.
	/// </summary>
	public class SplashScreen : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private string _version;
		private Font _title_font;
		private Font _ver_font;

		private const string COPYRIGHT = "Warning: This computer program is protected by copyright law and international treaties. Unauthorized reproduction or distribution of this program, or any portion of it, may result in severe civil and criminal penalties, and will be prosecuted to the maximum extent of the law.";

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SplashScreen()
		{
			InitializeComponent();
			this.Paint += new PaintEventHandler(this.OnPaint);
			_version = System.Windows.Forms.Application.ProductVersion;

			_title_font = new Font("Arial",
									28.25F,
									FontStyle.Bold,
									GraphicsUnit.Point,
									0);

			_ver_font = new Font("Tahoma",8.25F,FontStyle.Regular,GraphicsUnit.Point,0);

		}

		private void OnPaint(object sender, PaintEventArgs pe)
		{
			int title_size = 0;
			int title_x = 0;
			int ver_size = 0;
			int ver_x = 0;
			int copyright_size = 0;
			RectangleF copyright_rect;
			string version = "Version " + _version;

			LinearGradientBrush gradient;
			System.Drawing.Rectangle rect = new Rectangle(new Point(0,45),
														 new Size(panel1.Width,panel1.Height-45));
			gradient = new LinearGradientBrush(rect,
											Color.White,
											Color.FromArgb(85,130,210),
											(float)90,
											false);

			System.Drawing.Graphics g = Graphics.FromHwnd(panel1.Handle);			
			g.FillRectangle(gradient,rect);

			title_size = (int)g.MeasureString("VLoop",_title_font).Width;
			title_x = (panel1.Width / 2) - (title_size / 2);
			g.DrawString("VLoop",_title_font,Brushes.White,title_x, (float)200);

			ver_size = (int)g.MeasureString(version,_ver_font).Width;
			ver_x = (panel1.Width / 2) - (ver_size/2);
			g.DrawString("Version " + _version, _ver_font,Brushes.White,ver_x,(float)250);

			copyright_size = (int)g.MeasureString(COPYRIGHT,_ver_font).Width;

			copyright_rect = new RectangleF(new PointF(10F,300F),new SizeF(panel1.Width - 20, 50));
			g.DrawString(COPYRIGHT,_ver_font,Brushes.White,copyright_rect);

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SplashScreen));
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.pictureBox1});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.ForeColor = System.Drawing.SystemColors.Desktop;
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(520, 392);
			this.panel1.TabIndex = 0;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(176, 48);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
		
			// 
			// SplashScreen
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(520, 392);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SplashScreen";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SplashScreen";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
