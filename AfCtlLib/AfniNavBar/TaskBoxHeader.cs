using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Afni.Controls
{
	public class TaskBoxHeader
	{
		private string _text; 
		private Size _size;
		private Point _location;
		private System.Drawing.Rectangle _gradient_rect;
		private System.Drawing.Rectangle _back_rect;
		private System.Drawing.Font _header_font;
		private TaskBox _parent; 
		private HeaderStates _state;
		private Color _leftcolor = Color.White;
		private Color _rightcolor = Color.FromArgb(197,210,240);
		private Color _textcolor;
		private Color _hovercolor = Color.FromArgb(93,179,255);
		private Color _nonhovercolor = Color.FromArgb(33,93,198);
		private Icon _chevron_up_nonhover;
		private Icon _chevron_up_hover;
		private Icon _chevron_down_nonhover;
		private Icon _chevron_down_hover;

		public TaskBoxHeader(TaskBox Parent)
		{
			_parent = Parent; 
			_header_font = new Font("Tahoma",(float)8.25,FontStyle.Bold);
			_text = "Group Header";
			_textcolor = Color.FromArgb(33,93,198);		
		}

		public HeaderStates State
		{
			get { return _state; }
		}

		public string Text
		{
			get { return _text; }
			set 
			{
				_text = value;
				Draw();}
		}

		public Font Font
		{
			get { return _header_font; }
			set { 
				_header_font = value; 
				Draw();
			}
		}
	
		public Color LeftGradientColor
		{
			get { return _leftcolor; }
			set 
			{
				_leftcolor = value; 
				Draw();
			}
		}

		public Color RightGradientColor
		{
			get { return _rightcolor; }
			set 
			{
				_rightcolor = value; 
				Draw();
			}
		}

		public Size Size
		{
			get { return _size; }
			set { _size = value; }
		}

		public Point Location
		{
			get { return _location; }
			set { _location = value;}
		}

		public void Highlight()
		{
			_state = HeaderStates.Hovered;
			_textcolor = _hovercolor;
			Draw();
		}

		public void UnHighlight()
		{
			_state = HeaderStates.UnHovered;
			_textcolor = _nonhovercolor;
			Draw();
		}

		public Color HoverTextColor
		{
			get { return _hovercolor; }
			set 
			{
				_hovercolor = value; 
				Draw();
			}
		}

		public Color TextColor
		{
			get { return _nonhovercolor; }
			set 
			{
				_nonhovercolor = value; 
				Draw();
			}
		}

		public Icon ChevronDown
		{
			get { return _chevron_down_nonhover; }
			set 
			{
				_chevron_down_nonhover = value; 
				Draw();
			}
		}

		public Icon ChevronDownHover
		{
			get { return _chevron_down_hover; }
			set 
			{
				_chevron_down_hover = value; 
				Draw();
			}
		}

		public Icon ChevronUp
		{
			get { return _chevron_up_nonhover; }
			set
			{
				_chevron_up_nonhover = value; 
				Draw();
			}
		}

		public Icon ChevronUpHover
		{
			get { return _chevron_up_hover; }
			set
			{
				_chevron_up_hover = value; 
				Draw();
			}
		}

		public void Draw()
		{
			_size = new Size(_parent.Width, 25);
			_gradient_rect.Size= new Size( _size.Width / 2, 25);
			_gradient_rect.Location = new Point( _size.Width / 2, 0);
			_back_rect.Size =  new Size(_size.Width / 2 , 25);
			PointF text_loc = new PointF(_back_rect.Location.X + 5, _back_rect.Location.Y + 5);
			System.Drawing.Graphics g = Graphics.FromHwnd(_parent.Handle);

			LinearGradientBrush gradient_brush = 
					new LinearGradientBrush(_gradient_rect,
											_leftcolor,
											_rightcolor,
											(float)0,
											false);

			LinearGradientBrush font_brush =
						new LinearGradientBrush(_back_rect,
												_textcolor,
												_textcolor,
												(float)0,
												false);

			g.FillRectangle(new SolidBrush(_leftcolor), _back_rect);
			g.FillRectangle(gradient_brush, _gradient_rect);
			g.DrawString(_text,_header_font,font_brush,text_loc);
			
			Icon icon;

			if(_parent.State == TaskBoxStates.Expanded ||
				_parent.State == TaskBoxStates.Expanding)
			{
				if(_state == HeaderStates.Hovered)
					icon = _chevron_up_hover;
				else
					icon = _chevron_up_nonhover;
			}
			else
			{
				if(_state == HeaderStates.Hovered)
					icon = _chevron_down_hover;
				else
					icon = _chevron_down_nonhover;
			}

			if(icon != null)
				g.DrawIcon(icon, _size.Width - icon.Width - 3, 3);
							
		}
		
	}

	public enum HeaderStates
	{
		Hovered,
		UnHovered
	}

}
