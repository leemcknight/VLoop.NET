using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace Afni.Controls.AfniMarquee
{
	public class afMarquee : System.Windows.Forms.Control
	{
		private ArrayList _marquee_items;
		private Timer _marqueeTimer;
		private MarqueeSpeeds _marqueeSpeed;

		public afMarquee()
		{
			_marquee_items = new ArrayList();
			_marqueeSpeed = MarqueeSpeeds.Medium;
			_marqueeTimer = new Timer();
			_marqueeTimer.Tick += new System.EventHandler(this.TimerFire);
			SetTimer();
		}

		public ArrayList MarqueeItems
		{
			get { return _marquee_items; }
		}
		
		public MarqueeSpeeds MarqueeSpeed
		{
			get { return _marqueeSpeed; }
			set { _marqueeSpeed = value; }
		}

		private void SetTimer()
		{
			_marqueeTimer.Stop();
			switch(_marqueeSpeed)
			{
				case MarqueeSpeeds.Fast:
					_marqueeTimer.Interval = 20;
					break;
				case MarqueeSpeeds.Medium:
					_marqueeTimer.Interval = 30;
					break;
				case MarqueeSpeeds.Slow:
					_marqueeTimer.Interval = 40;
					break;
				case MarqueeSpeeds.VeryFast:
					_marqueeTimer.Interval = 10;
					break;
			}
			_marqueeTimer.Start();
		}

		private void TimerFire(object sender, System.EventArgs e)
		{
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			Graphics g = pe.Graphics;


		}

	}

	public enum MarqueeSpeeds
	{
		Slow,
		Medium,
		Fast,
		VeryFast
	}
}
