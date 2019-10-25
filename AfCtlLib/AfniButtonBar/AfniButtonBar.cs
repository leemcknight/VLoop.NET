using System;
using System.Windows.Forms;
using System.Drawing;

namespace Afni.Controls
{
	
	public class AfniButtonBar : ContainerControl
	{
		private Color _topGradient;
		private Color _bottomGraident;
		private Color _borderColor;
		private ButtonCollection _items;
		
		public AfniButtonBar()
		{
			//allow for scrollbars
			HScroll = true;
			VScroll = true;

			//button items
			_items = new ButtonCollection();
			_items.Changed += new EventHandler(this.OnItemChange);
		}

		private void OnItemChange(object sender, System.EventArgs e)
		{
			int yLoc = Height - 20;

			Controls.Clear();
			foreach(ButtonBarItem item in _items)
			{
				item.Location = new Point(1, yLoc);
				Controls.Add(item);
				yLoc -= 20;
			}
		}

		public ButtonCollection ButtonBarItems
		{
			get { return _items; }
		}

		public Color TopGradientButtonColor
		{
			get { return _topGradient; }
			set
			{
				_topGradient = value;
				Invalidate();
			}
		}

		public Color BottomGradientButtonColor
		{
			get { return _bottomGraident; }
			set
			{
				_bottomGraident = value;
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;

			g.DrawRectangle(new Pen(_borderColor), Bounds);
		}
		
	}
}
