using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Afni.Controls
{
	public class TaskBox : ContainerControl
	{
		private TaskBoxHeader _header;
		private TaskCollection _tasks;
		private string _header_text;
		private TaskBoxStates _state;
		private int _expanded_height;
		private Hashtable _details;
		private System.Timers.Timer _timer;
		private Color _base_color = Color.FromArgb(215,222,248);
		private Color _border_color = Color.White;
		private Color _task_color = Color.FromArgb(33,93,198);
		private Color _active_task_color = Color.FromArgb(93,179,255);
		public event EventHandler TaskBoxCollapsed;
		public event EventHandler TaskBoxExpanded;
		public event EventHandler TaskAdded;
		public event EventHandler DetailAdded;
		
		public TaskBox() 
		{
			InitializeComponent();										  
		}

		private void InitializeComponent()
		{
			this.Left = 10;
			this.Width = 185;
	
			_tasks = new TaskCollection();
			_details = new Hashtable();

			//properties
			this.BackColor = _base_color;
			this.Visible = true;

			//events
			_tasks.Changed += new EventHandler(this.OnTasksChanged);

			//internal properties
			_state = TaskBoxStates.Expanded;
			_timer = new System.Timers.Timer();
			_timer.Interval = 75;
			_timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimerFire);
			_header = new TaskBoxHeader(this);
			_header.Location = this.Location;
		}

		protected void OnTasksChanged(object sender, EventArgs e)
		{
			int new_top = 30;
			Graphics g = Graphics.FromHwnd(this.Handle);
			int width;

			this.Controls.Clear();
			foreach(AfniLink task in _tasks)
			{	
				width = (int)g.MeasureString(task.Text,task.Font).Width;
				task.Font = new Font("Tahoma", 8.25F, FontStyle.Regular);
				task.Height = 20;
				task.ActiveLinkColor = _active_task_color;
				task.LinkColor = _task_color;
				task.Width = width + 40;
				task.Left = 10;
				task.Top = new_top;
				this.Controls.Add(task);
				new_top += 20;
			}

			this.Height = new_top + 10;
			_expanded_height = new_top + 10;

			Invalidate();

			if(TaskAdded != null)
				TaskAdded(this,null);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left  && IsPointOnHeader(e.X,e.Y))
			{
				if(_state == TaskBoxStates.Collapsed || _state == TaskBoxStates.Collapsing)
				{
					ExpandTaskBox();
				}
				else
				{
					CollapseTaskBox();
				}
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if(IsPointOnHeader(e.X,e.Y))
			{
				if(_header.State == HeaderStates.UnHovered)
				{
					_header.Highlight();
					this.Cursor = Cursors.Hand;
				}
			}
			else
			{
				if(_header.State == HeaderStates.Hovered)
				{
					_header.UnHighlight();
					this.Cursor = Cursors.Default;
				}
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			if(_header.State == HeaderStates.Hovered)
			{
				_header.UnHighlight();
				this.Cursor = Cursors.Default;
			}
			base.OnMouseLeave(e);
		}

		protected void OnTimerFire(object sender, System.Timers.ElapsedEventArgs e)
		{
			if(_state == TaskBoxStates.Collapsing)
			{
				this.Size = new Size(this.Width, this.Height - 2);
				if(this.Height <= _header.Size.Height)
				{
					_state = TaskBoxStates.Collapsed;
					_timer.Enabled = false;
					Invalidate();
					if(TaskBoxCollapsed != null)
						TaskBoxCollapsed(this,null);
			
				}
			}
			else if (_state == TaskBoxStates.Expanding)
			{
				this.Size = new Size(this.Width, this.Height + 2);
				if(this.Height >= _expanded_height)
				{
					_state = TaskBoxStates.Expanded;
					_timer.Enabled = false;
					Invalidate();
					if(TaskBoxExpanded != null)
						TaskBoxExpanded(this,null);
				}
			}
		}

		protected override void OnLocationChanged(System.EventArgs e)
		{
			Redraw();
			base.OnLocationChanged(e);
		}

		protected void ExpandTaskBox()
		{
			//_state = TaskBoxStates.Expanding;
			_state = TaskBoxStates.Expanded;
			this.Height = _expanded_height;
			TaskBoxExpanded(this,null);
			Invalidate();
			//_timer.Enabled = true;
		}

		protected void CollapseTaskBox()
		{
			//_state = TaskBoxStates.Collapsing;
			_state = TaskBoxStates.Collapsed;
			this.Height = _header.Size.Height;
			TaskBoxCollapsed(this,null);
			Invalidate();
			//_timer.Enabled = true;
		}

		protected bool IsPointOnHeader(int x, int y)
		{
			return( y <= (int)_header.Size.Height);
		}

		protected bool IsPointOnHeader(Point pt)
		{
			return IsPointOnHeader(pt.X, pt.Y);
		}

		public TaskBoxStates State
		{
			get { return _state; }
		}

		public override string Text
		{
			get { return _header_text; }
			set 
			{
				_header_text = value;
				_header.Text = value; 
			}
		}

		public Color LinkColor
		{
			get { return _task_color; }
			set 
			{
				_task_color = value; 
				foreach(AfniLink task in _tasks)
					task.LinkColor = _task_color;
			}
		}

		public Color ActiveLinkColor
		{
			get { return _active_task_color; }
			set
			{
				_active_task_color = value; 
				foreach(AfniLink task in _tasks)
					task.ActiveLinkColor = _active_task_color;
			}
		}

		public Color HeaderTextColor
		{
			get { return _header.TextColor; }
			set 
			{
				_header.TextColor = value; 
				Invalidate();
			}
		}

		public Color HeaderActiveTextColor
		{
			get { return _header.HoverTextColor; }
			set 
			{
				_header.HoverTextColor = value; 
				Invalidate();
			}
		}

		public Color BorderColor
		{
			get { return _border_color; }
			set { 
				_border_color = value; 
				Invalidate();
			}
		}

		public Color HeaderLeftGradient
		{
			get { return _header.LeftGradientColor; }
			set 
			{
				_header.LeftGradientColor = value; 
				Invalidate();
			}
		}

		public Color HeaderRightGradient
		{
			get { return _header.RightGradientColor; }
			set 
			{
				_header.RightGradientColor = value; 
				Invalidate();
			}
		}

		public Icon ChevronDown
		{
			get { return _header.ChevronDown; }
			set { _header.ChevronDown = value; }
		}

		public Icon ChevronDownHover
		{
			get { return _header.ChevronDownHover; }
			set { _header.ChevronDownHover = value; }
		}

		public Icon ChevronUp
		{
			get { return _header.ChevronUp; }
			set { _header.ChevronUp = value; }
		}

		public Icon ChevronUpHover
		{
			get { return _header.ChevronUpHover; }
			set { _header.ChevronUpHover = value; }
		}

		protected override void OnSizeChanged(System.EventArgs e)
		{
			if(_expanded_height == 0)
				_expanded_height = this.Height;
			base.OnSizeChanged(e);
		}

		public TaskCollection Tasks
		{
			get { return _tasks; }
		}

		public Hashtable Details
		{
			get { return _details; }
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Redraw();
			base.OnPaint(pe);
		}

		protected void Redraw()
		{
			/* draw the white border (if the taskbox is expanded) */
			if(this.State == TaskBoxStates.Expanded)
			{
				Graphics g = Graphics.FromHwnd(this.Handle);
				Pen border_pen = new Pen(new SolidBrush(_border_color));
				Point pt1, pt2, pt3, pt4;
			
				pt1 = new Point(0, _header.Size.Height);
				pt2 = new Point(0, _expanded_height - 1);
				pt3 = new Point(this.Width-2, _expanded_height - 1);
				pt4 = new Point(this.Width-2, _header.Size.Height);
				g.DrawLines(border_pen,new System.Drawing.Point[]
																	{ 
																		pt1,
																		pt2,
																		pt3,
																		pt4 
																	} );
						
				foreach(AfniLink task in _tasks)
				{
					task.Invalidate();
				}
			}

			if(_header != null)
				_header.Draw();
		}
	}

	public enum TaskBoxStates
	{
		Collapsed,
		Expanded,
		Collapsing,
		Expanding,
	}

}
