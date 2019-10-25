using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Afni.Controls
{
	public class afGoBtn : Control
	{
		private bool _hovered = false;
		private bool _pressed = false;

		public afGoBtn()
		{
			this.Cursor= Cursors.Hand;
		}

		private void Redraw()
		{
			Graphics g = Graphics.FromHwnd(this.Handle);
			Icon icon;
			Rectangle iconRect;
			Rectangle borderRect;

			int textWidth;
			int newWidth;

			if( _hovered  && _pressed)
				icon = ButtonIcons.PressedIcon;
			else if (_hovered)
				icon = ButtonIcons.HoverIcon;
			else
				icon = ButtonIcons.NormalIcon;
			
			textWidth = (int)g.MeasureString(this.Text,this.Font).Width;
			g.DrawString(this.Text,this.Font,Brushes.Black,26F,2F,StringFormat.GenericDefault);
			
			newWidth = textWidth + 25;
			iconRect = new Rectangle(0,0,24,24);

			if(newWidth != this.Width)
				this.Width = newWidth;

			borderRect = this.Bounds;
			borderRect.Inflate(-1,-1);
			if(_hovered)
			{
				ControlPaint.DrawBorder3D(g,borderRect,Border3DStyle.RaisedOuter);
			}

			if(this.Focused)
				ControlPaint.DrawFocusRectangle(g,borderRect);

			g.DrawIcon(icon,iconRect);
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Redraw();
			base.OnPaint(pe);
		}

		protected override void OnMouseDown(MouseEventArgs me)
		{
			base.OnMouseDown(me);
			if(me.Button == MouseButtons.Left)
			{
				_pressed = true;
				Invalidate();
			}
		}

		protected override void OnMouseUp(MouseEventArgs me)
		{
			base.OnMouseUp(me);
			if(me.Button == MouseButtons.Left)
			{
				_pressed = false;
				Invalidate();
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_hovered = true;
			Invalidate();
			
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			_pressed = false;
			_hovered = false;
			Invalidate();
		}
	}

	internal class ButtonIcons
	{
		internal static Icon NormalIcon
		{
			get { return GetIcon("arrow_green_normal_bmp_0.ico"); }
		}

		internal static Icon HoverIcon
		{
			get { return GetIcon("arrow_green_mouseover_bmp_0.ico"); }
		}

		internal static Icon PressedIcon
		{
			get { return GetIcon("arrow_green_mousedown_bmp_0.ico"); }
		}

		private static Icon GetIcon(string name)
		{
			string qname;
			
			qname = "Afni.Controls.AfniGoButton." + name;
			Assembly assembly = Assembly.GetCallingAssembly();
			string[] a = assembly.GetManifestResourceNames();
			Stream iconStream =
				assembly.GetManifestResourceStream(qname);
			Icon resourceIcon = new Icon(iconStream);
			return resourceIcon;
		}
	}
}
