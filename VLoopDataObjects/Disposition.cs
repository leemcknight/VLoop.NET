using System;
using System.Data;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	/// <summary>
	/// Summary description for Disposition.
	/// </summary>
	public class Disposition : VLoopDataObject
	{
		private string _disp;
		
		public Disposition()
		{
			
		}

		public Disposition(DataRow row)
		{
			_pk = Convert.ToInt32(row["iDispositionID"]);
			_disp = Convert.ToString(row["DispositionText"]);
		}

		public override string ToString()
		{
			return _disp;
		}

		public long DispositionID
		{
			get { return _pk; }
		}

		public string DispositionText
		{
			get { return _disp; }
			set
			{
				if(_disp != value)
				{
					_disp = value;
					this.Dirty = true;
				}
			}
		}
	}
}
