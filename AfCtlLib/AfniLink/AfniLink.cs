using System;
using System.Windows.Forms;
using System.Drawing;

namespace Afni.Controls
{
	public class AfniLink : System.Windows.Forms.Control
	{
		protected Icon _icon;
		protected Graphics _g;
		protected Color _active_color = Color.White;
		protected Color _color = Color.White;
		protected Boolean _hovered = false;
		protected int _img_size=16;

		protected const int ICON_LEFT_BUFFER = 10;

		public event EventHandler LinkClicked;

		public AfniLink()
		{
			InitializeComponent();	
		}

		private void InitializeComponent()
		{
			this.Cursor = Cursors.Hand;
			this.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);
			_g = Graphics.FromHwnd(this.Handle);
		}

		public Color ActiveLinkColor
		{
			get { return _active_color; }
			set
			{
				_active_color = value; 
				Invalidate();
			}
		}

		public Color LinkColor
		{
			get { return _color; }
			set
			{
				_color = value; 
				Invalidate();
			}
		}
		
		public Icon Icon
		{
			get { return _icon; }
			set { _icon = value; }
		}

		public int IconSize
		{
			get { return _img_size; }
			set
			{
				_img_size = value;
				Invalidate();
			}
		}

		public override string ToString()
		{
			return this.Text;
		}

		protected void Redraw()
		{
			PointF txt_loc;
			SizeF txt_size;
			RectangleF txt_rect;
			Rectangle iconRect = new Rectangle();
			Brush brush;
			int x_loc;
			int y_loc;
			int y_offset;
			float x_avail;

			if(_hovered)
				brush = new SolidBrush(_active_color);
			else
				brush = new SolidBrush(_color);

			_g = Graphics.FromHwnd(this.Handle);
			if(_icon != null)
			{
				y_offset = (_img_size <= 16 ? 0 : 10);

				iconRect = new Rectangle(new Point(0,y_offset),
										new Size(_img_size,_img_size));

				_g.DrawIcon(_icon,iconRect);
				x_loc = _img_size + 5;
			}
			else
			{
				y_offset = 0;
				x_loc = ICON_LEFT_BUFFER;
			}
			
			txt_size = _g.MeasureString(this.Text,this.Font);
			x_avail =(float)this.Size.Width - (float)x_loc;
			
			y_loc = (this.Height /2) - (this.Font.Height / 2);
			txt_loc = new PointF(x_loc, y_loc);
			txt_rect = new RectangleF(txt_loc, txt_size);
			_g.DrawString(this.Text,this.Font,brush, x_loc, y_loc);
	
		}

		protected override void OnMouseDown(MouseEventArgs me)
		{
			if(LinkClicked != null)
				LinkClicked(this, null);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			FontStyle style;
			style = FontStyle.Regular;
			if(this.Font.Bold)
				style |= FontStyle.Bold;
			if(this.Font.Italic)
				style |= FontStyle.Italic;
			_hovered = false;
			
			this.Font = new Font("Tahoma", 8.25F, style);	
			this.Invalidate();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			FontStyle style;
			style = FontStyle.Regular;
			style |= FontStyle.Underline;
			if(this.Font.Bold)
				style |= FontStyle.Bold;
			if(this.Font.Italic)
				style |= FontStyle.Italic;
			_hovered = true;
			this.Font = new Font("Tahoma", 8.25F, style);
			this.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Redraw();
			base.OnPaint(pe);
		}
	}
}
