using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Afni.Controls
{
	
	public class AfniDivider : System.Windows.Forms.Control
	{
		private System.ComponentModel.Container components = null;
		private Color _clr1 = Color.White;
		private Color _clr2 = Color.FromArgb(1,72,178);
		private DividerStyles _style= DividerStyles.Horizontal;

		public AfniDivider()
		{
			InitializeComponent();
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
			float angle;
			Graphics g = Graphics.FromHwnd(this.Handle);
			angle = (_style == DividerStyles.Horizontal ? 0F : 90F);
			LinearGradientBrush brush = 
				new LinearGradientBrush(new Rectangle(new Point(0,0),this.Size),_clr1,_clr2,angle,false);
			g.DrawLine(new Pen(brush,(float)this.Width),
						new Point(0,0),
						new Point(this.Width, this.Height));
		}

		public Color FirstColor
		{
			get { return _clr1; }
			set 
			{ 
				_clr1 = value; 
				Refresh();
			}
		}

		public Color SecondColor
		{
			get { return _clr2; }
			set 
			{
				_clr2 = value; 
				Refresh();
			}
		}

		public DividerStyles DividerStyle
		{
			get { return _style; }
			set 
			{
				_style = value; 
				Refresh();
			}
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}

	public enum DividerStyles
	{
		Horizontal,
		Vertical
	}
}
