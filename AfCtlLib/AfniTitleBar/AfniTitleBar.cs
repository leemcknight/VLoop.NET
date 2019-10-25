using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Afni.Controls
{
	
	public class AfniTitleBar : Panel
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private string _title;
		private Icon _image;
		private Color _clr1, _clr2;
		private Font _font;
		private int _imgSize = 16;

		public AfniTitleBar()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			_clr1 = Color.FromArgb(40, 91, 197);
			_clr2 = Color.FromArgb(99,117, 214);
			_font = new Font("Tahoma",(float)8.25,FontStyle.Bold);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Draw();
			base.OnPaint(pe);
		}

		public Color LeftColor
		{
			get { return _clr1; }
			set 
			{ 
				_clr1 = value; 
				Invalidate();
			}
		}

		public Color RightColor
		{
			get { return _clr2; }
			set 
			{
				_clr2 = value; 
				Invalidate();
			}
		}

		public Font TitleFont
		{
			get { return _font; }
			set { _font = value; }
		}

		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				Invalidate();
			}
		}

		public Icon TitleIcon
		{
			get { return _image; }
			set 
			{
				_image = value; 
				Invalidate();
			}
		}

		public int ImageSize
		{
			get { return _imgSize; }
			set 
			{
				_imgSize = value;
				Invalidate();
			}
		}

		protected void Draw()
		{
			int y;
			int y_icon_offset = 0;
			Rectangle iconRect;
			y = (this.Height /2) - (_font.Height / 2);
			Rectangle rect = new Rectangle(new Point(0,0), this.Size);
			Graphics g = Graphics.FromHwnd(this.Handle);
			PointF text_loc = new PointF(30, y);
			LinearGradientBrush title_gradient;


			title_gradient = new LinearGradientBrush(rect,_clr1,_clr2,(float)0,false);
			g.FillRectangle(title_gradient,rect);
			g.DrawString(_title,_font,Brushes.White,text_loc);

			y_icon_offset = (this.Height / 2) - (_imgSize / 2);

			iconRect = new Rectangle(3,y_icon_offset,_imgSize, _imgSize);
			
			if(_image != null)
				g.DrawIcon(_image,iconRect);

		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Dock = DockStyle.Top;

		}
		#endregion
	}
}
