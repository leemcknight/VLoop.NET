using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;

namespace Afni.Controls
{
	public class AfniMenu : MainMenu
	{
	
	}

	public class AfniMenuItem : MenuItem
	{
		protected object _key;
		protected Icon _icon;

		public AfniMenuItem()
		{
			OwnerDraw = true;
		}

		protected override void OnDrawItem(DrawItemEventArgs de)
		{
			Graphics g = de.Graphics;
			StringFormat fmt = new StringFormat();
			Brush brush;
			RectangleF text_rect = new RectangleF();
			Rectangle icon_rect;

			fmt.HotkeyPrefix = HotkeyPrefix.Show;
			
	
			text_rect.Width = de.Bounds.Width - 20;
			text_rect.Height = de.Bounds.Height;
			text_rect.Location = new PointF(16F, (float)de.Bounds.Top + 2);

			if((de.State & DrawItemState.Selected) > 0)
			{
				g.FillRectangle(SystemBrushes.Highlight, de.Bounds);
				brush = SystemBrushes.HighlightText;
			}
			else
			{
				g.FillRectangle(SystemBrushes.Menu,de.Bounds);
				brush = SystemBrushes.ControlText;
			}

			if((de.State & DrawItemState.Disabled) > 0)

				ControlPaint.DrawStringDisabled(g,
												this.Text,
												new Font("Tahoma",8.25F),
												SystemColors.Menu,
												text_rect,
												fmt);

			else
				g.DrawString(this.Text,new Font("Tahoma",8.25F),brush,text_rect,fmt);
		
			if(_icon != null)
			{
				icon_rect = new Rectangle(0,de.Bounds.Y,16,16);
				g.DrawIcon(_icon,icon_rect);
			}

			base.OnDrawItem(de);	
			
		}

		protected override void OnMeasureItem(MeasureItemEventArgs me)
		{
			int y_offset = 4;
			int y_item_pos;
			
			y_item_pos = (int)me.Graphics.MeasureString(this.Text,new Font("Tahoma",8.25F)).Height;
			y_item_pos += y_offset;

			me.ItemHeight = (int)me.Graphics.MeasureString(this.Text,new Font("Tahoma",8.25F)).Height;
			me.ItemHeight += y_offset;
			me.ItemWidth = (int)me.Graphics.MeasureString(this.Text, new Font("Tahoma",8.25F)).Width;

			me.ItemWidth += 25; //account for icon and buffer area
			y_item_pos = (me.Index * me.ItemHeight);
			base.OnMeasureItem(me);
		}

		public object Key
		{
			get { return _key; }
			set { _key = value; }
		}

		public Icon Icon
		{
			get { return _icon; }
			set { _icon = value; }
		}

		
	}
}
