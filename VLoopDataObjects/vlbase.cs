using System;
using Afni.DataUtility;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	/// <summary>
	/// Base class for all VLoop Data Objects.  This is an abstract class and
	/// cannot be created directly.  It must be inherited from.
	/// </summary>
	public abstract class VLoopDataObject : Afni.DataUtility.DataObject
	{
		//protected bool this.Dirty = false;
		protected int _pk;	//primary key of the data object

		//fields common to all vloop tables
		protected string _lastUpdatedBy;
		protected DateTime _dateUpdated;
		
		//public events
		public event EventHandler Changed;
		public event EventHandler Invalidated;

		public VLoopDataObject()
		{
	
		}

		public string LastUpdatedBy
		{
			get { return _lastUpdatedBy; }
			set
			{
				if(_lastUpdatedBy != value)
				{
					_lastUpdatedBy = value;
					this.Dirty = true;
				}
			}
		}

		public DateTime DateUpdated
		{
			get { return _dateUpdated; }
			set
			{
				if(_dateUpdated != value)
				{
					_dateUpdated = value;
					this.Dirty = true;
				}
			}
		}
	}

	public abstract class DeletableObject : VLoopDataObject
	{
		protected bool _retired;

		public bool Retired
		{
			get { return _retired; }
			set
			{
				if(_retired != value)
				{
					_retired = value;
					this.Dirty = true;
				}
			}
		}
	}
}
