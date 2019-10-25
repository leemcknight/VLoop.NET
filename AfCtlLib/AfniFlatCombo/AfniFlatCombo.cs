using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace Afni.Controls
{
	
	public class AfniFlatCombo : ComboBox
	{
		protected bool _hovered = false;
		protected Color _border_color = Color.Black;
		protected Color _text_color = Color.Black;
		private Color BUTTON_COLOR_HOVER = Color.FromArgb(193,210,238);
		
		public AfniFlatCombo()
		{
			InitializeComponent();
		}
		
		[Category("Afni Properties")]
		public Color BorderColor
		{
			get { return _border_color; }
			set 
			{
				_border_color = value; 
				Invalidate();
			}
		}

		[Category("Afni Properties")]
		public Color TextColor
		{
			get { return _text_color; }
			set
			{
				_text_color = value; 
				Invalidate();
			}
		}
		
		protected override void OnDrawItem(DrawItemEventArgs de)
		{
			string text;
			
			Graphics g = de.Graphics;
			Color text_color;
			
			if(de.Index > -1)
			{
				base.OnDrawItem(de);
				de.DrawBackground();
				text = this.Items[de.Index].ToString();
				text_color = Color.Black;
				if((int)(de.State & DrawItemState.Selected) > 0)
				{
					text_color = SystemColors.HighlightText;
					g.FillRectangle(new SolidBrush(SystemColors.Highlight),de.Bounds);									
				}

				g.DrawString(text,de.Font,new SolidBrush(text_color),de.Bounds);

			}
			else
			{
				g.FillRectangle(SystemBrushes.ControlLightLight,de.Bounds);
			}
		}

		protected override void OnMeasureItem(MeasureItemEventArgs me)
		{
			base.OnMeasureItem(me);
			if(me.Index < 0 )
			{
				me.ItemHeight = 50;
				me.ItemWidth = 200;
			}
			
		}

		protected override void OnEnabledChanged(EventArgs e)
		{
			Invalidate();
			base.OnEnabledChanged(e);
		}
		
		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			Rectangle outer_rect;
			Rectangle inner_rect;
			Rectangle button_rect;
			Rectangle inner_text_rect;
			Point arrow_origin;
			Color border_color;
			Color button_color;
			Color text_color = _text_color;

			/* outer rect */
			border_color = (_hovered ? SystemColors.Highlight : _border_color);
			button_color = (_hovered ? BUTTON_COLOR_HOVER : SystemColors.Control);

			outer_rect = new Rectangle(0,0, this.Width -1, this.Height - 1);
			inner_rect = new Rectangle(1,1,this.Width-2,this.Height-2);
			inner_text_rect = new Rectangle(2,2,this.Width-2,this.Height-2);
			g.DrawRectangle(new Pen(new SolidBrush(border_color)), outer_rect);
			
			if(!Enabled)
			{
				g.FillRectangle(new SolidBrush(SystemColors.Info),inner_rect);
				text_color = SystemColors.GrayText;
			}

			/* push button */
			button_rect = new Rectangle(this.Width - 20,0, 20,this.Height);
			
			g.DrawRectangle(new Pen(new SolidBrush(border_color)), button_rect);
		
			arrow_origin = new Point(((button_rect.Width / 2) + button_rect.X) - 3,
				button_rect.Height / 2 - 1);
			
			button_rect.Inflate(new Size(-1,-1));
			g.FillRectangle(new SolidBrush(button_color),button_rect);
			
			Point[] pts = new Point[] {
										  new Point(arrow_origin.X, arrow_origin.Y),
										  new Point(arrow_origin.X + 7,arrow_origin.Y),
										  new Point(arrow_origin.X + 3, arrow_origin.Y + 3),
										  new Point(arrow_origin.X, arrow_origin.Y)
									  };
										
			g.FillPolygon(Brushes.Black, pts, FillMode.Winding);					   
			
			if(this.SelectedItem != null)
			{
				g.DrawString(this.SelectedItem.ToString(), 
					this.Font,
					new SolidBrush(_text_color),
					inner_text_rect);
			}

			base.OnPaint(pe);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			_hovered = true;
			Invalidate();
		}
		
		protected override void OnMouseLeave(EventArgs e)
		{
			_hovered = false;
			Invalidate();
		}
		
		private void InitializeComponent()
		{
			
			this.DropDownStyle = ComboBoxStyle.DropDownList;

			if(!this.IsOnXP)
			{
				this.DrawMode = DrawMode.OwnerDrawFixed;
				this.SetStyle(ControlStyles.AllPaintingInWmPaint|
					ControlStyles.UserPaint|
					ControlStyles.DoubleBuffer,
					true);
			}
		}

		private bool IsOnXP
		{
			get 
			{
				Version ver;

				ver = Environment.OSVersion.Version;

				if(ver.Major >= 5 && ver.Minor >= 1)
					return true;
				else if (ver.Major >=6)
					return true;
				else
					return false;
			}
		}

	}
}

