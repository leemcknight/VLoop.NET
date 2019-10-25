using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Afni.Controls
{
	/// <summary>
	/// Summary description for Button.
	/// </summary>
	public class ButtonBarItem : Control
	{
		private Color _topHeaderColor;
		private Color _bottomHeaderColor;
		private Color _borderColor;
		private bool _selected;
		private Icon _icon;

	
		public ButtonBarItem()
		{
				
		}

		internal Color TopHeaderColor
		{
			get { return _topHeaderColor; }
			set 
			{
				_topHeaderColor = value; 
				Invalidate();
			}
		}

		internal Color BottomHeaderColor
		{
			get { return _bottomHeaderColor; }
			set
			{
				_bottomHeaderColor = value;
				Invalidate();
			}
		}

		internal Color BorderColor
		{
			get { return _borderColor; }
			set 
			{
				_borderColor = value;
				Invalidate();
			}
		}

		internal bool Selected
		{
			get { return _selected; }
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			LinearGradientBrush gradient;
			Rectangle innerRect;
			Rectangle iconRect;
			RectangleF textRect;

			//outer rectangle
			g.DrawRectangle( new Pen(_borderColor), Bounds );


			//inner gradient
			innerRect = this.Bounds;
			innerRect.Inflate(-1,-1);

			gradient = new LinearGradientBrush(innerRect,
												_topHeaderColor,
												_bottomHeaderColor,
												90F);

			g.FillRectangle( gradient, innerRect );

			//icon
			if(_icon != null)
			{
				iconRect = new Rectangle( new Point(3,3), new Size(24,24) );
				g.DrawIcon( _icon, iconRect );
			}

			//text
			textRect = new RectangleF( new PointF( 30F, 3F ),
										g.MeasureString(Text,Font));

			g.DrawString(Text, Font, new SolidBrush( ForeColor ), textRect, StringFormat.GenericDefault );

			base.OnPaint(pe);
		}

		public Icon Icon
		{
			get { return _icon; }
			set
			{
				_icon = value; 
				Invalidate();
			}
		}
	}
}
