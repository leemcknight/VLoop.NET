using System;
using System.Data;


namespace Afni.Applications.VLoop.VLoopDataObjects
{
	public class AvailableLookupInfo : VLoopDataObject
	{
		private long _question_id;
		private string _value; 

		public AvailableLookupInfo()
		{
		
		}

		public AvailableLookupInfo(DataRow row)
		{
			_pk = Convert.ToInt32(row["iAvailableLookupID"]);
			_question_id = Convert.ToInt64(row["iQuestionID"]);
			_value = Convert.ToString(row["szValue"]);
			_lastUpdatedBy = Convert.ToString(row["szLastUpdatedBy"]);
			_dateUpdated = Convert.ToDateTime(row["dtUpdated"]);
		}

		public long AvailableLookupInfoID
		{
			get { return _pk; }
		}

		public string LookupValue
		{
			get { return _value; }
			set
			{
				if(_value != value)
				{
					_value = value; 
					this.Dirty = true;
				}
			}
		}
	}
}
