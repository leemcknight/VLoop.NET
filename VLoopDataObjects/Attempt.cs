using System;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	/// <summary>
	/// Summary description for Attempt.
	/// </summary>
	public class Attempt : VLoopDataObject
	{
		private long _id;
		private DateTime _attempt_date;
		private string _comments;

		public Attempt()
		{
			
		}

		public long AttemptID
		{
			get { return _id; }
		}

		public DateTime AttemptDate
		{
			get { return _attempt_date; }
			set
			{
				if(_attempt_date != value)
				{
					_attempt_date = value;
					this.Dirty = true;
				}
			}
		}

		public string Comments
		{
			get { return _comments; }
			set
			{
				if(_comments != value)
				{
					_comments = value; 
					this.Dirty = true;
				}
			}
		}
	}
}
