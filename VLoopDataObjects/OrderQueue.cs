using System;
using System.Data;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	public class OrderQueue : VLoopDataObject
	{
		private string _name;
		private bool _editable;
		private bool _workable;

		public OrderQueue()
		{
			
		}

		public OrderQueue(DataRow row)
		{
			_pk = Convert.ToInt32(row["iQueueID"]);
			_name = Convert.ToString(row["szQueueName"]);
			_editable = Convert.ToBoolean(row["blnEditable"]);
			_workable = Convert.ToBoolean(row["blnWorkable"]);
		}

		public long OrderQueueID
		{
			get { return _pk; }
		}

		public string OrderQueueName
		{
			get { return _name; }
			set
			{
				if(_name != value)
				{
					_name = value; 
					this.Dirty = true;
				}
			}
		}

		public bool IsWorkable
		{
			get { return _workable; }
			set
			{
				if(_workable != value)
				{
					_workable = value;
					this.Dirty = true;
				}
			}
		}

		public bool IsEditable
		{
			get { return _editable; }
			set
			{
				if(_editable != value)
				{
					_editable = value; 
					this.Dirty = true;
				}
			}
		}
	}
}
