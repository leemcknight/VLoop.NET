using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Afni.Controls
{
	public class TaskBoxGroup : ContainerControl
	{
		private TaskBoxCollection _groups;
		private int _next_top = 20;
		private Color _top_gradient;
		private Color _bottom_gradient;
		private const int TASKBOX_VERTICAL_BUFFER = 10;

		public TaskBoxGroup()
		{
			_groups = new TaskBoxCollection();
			this.BackColor = Color.FromArgb(140,170,230);
			this.Dock= DockStyle.Left;
			_groups.Changed += new System.EventHandler(this.OnGroupsChanged);
			_top_gradient = Color.FromArgb(140,170,230);
			_bottom_gradient = Color.FromArgb(99,117, 214);
		}

		private void OnGroupsChanged(object sender, System.EventArgs e)
		{
			int top = 20;
			this.Controls.Clear();
			foreach(TaskBox box in _groups)
			{
				this.Controls.Add(box);
				box.Top = top;
				box.TaskBoxCollapsed += new System.EventHandler(this.OnTaskBoxChanged);
				box.TaskBoxExpanded += new System.EventHandler(this.OnTaskBoxChanged);
				box.TaskAdded += new System.EventHandler(this.OnTaskBoxChanged);
				top += box.Height;
				top += TASKBOX_VERTICAL_BUFFER;
			}
		}

		private void OnTaskBoxChanged(object sender, System.EventArgs e)
		{
			Redraw();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			LinearGradientBrush div_brush = new LinearGradientBrush(
											new Rectangle(new Point(0,0),new Size(1,this.Height)),
											Color.FromArgb(93,179,255),
											_bottom_gradient,
											(float)90,
											false);
					
			LinearGradientBrush outer_brush;
			Rectangle outer_rect = 
				new Rectangle(new Point(0,0),this.Size);

			outer_brush = new LinearGradientBrush(outer_rect,
												_top_gradient,
												_bottom_gradient,
												(float)90,
												false);
													
			Graphics g = Graphics.FromHwnd(this.Handle);
			g.FillRectangle(outer_brush, outer_rect);
			g.DrawLine(new Pen(div_brush),this.Width-1,0,this.Width-1,this.Height);
		}

		[Description("Task Boxes")]
		public TaskBoxCollection TaskBoxes
		{	
			get 
			{
				if(_groups == null)
					_groups = new TaskBoxCollection();
				return _groups; 
			}
		}

		public Color TopColor
		{
			get { return _top_gradient; }
			set 
			{ 
				_top_gradient = value; 
				Invalidate();
				Redraw();
			}
		}

		public Color BottomColor
		{
			get { return _bottom_gradient; }
			set 
			{
				_bottom_gradient = value; 
				Invalidate();
				Redraw();
			}
		}

		internal int NextTopBuffer
		{
			get { return _next_top; }
			set { _next_top = value; }
		}

		internal void Redraw()
		{
			int y = 20;
			
			foreach(TaskBox tb in _groups)
			{
				tb.Top = y;
				y += (tb.Height + TASKBOX_VERTICAL_BUFFER);
			}
		}
	}
	
}
