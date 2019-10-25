using System;
using System.Collections;
using System.Data;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	/// <summary>
	/// Summary description for CurrentPlan.
	/// </summary>
	public class CurrentPlanBase : DeletableObject
	{
		private long _wtn_id; 
		private long _prod_id;
		private string _prod_text;
		private string _prod_type_text;
		
		
		public CurrentPlanBase()
		{
			
		}

		public CurrentPlanBase(DataRow row)
		{
			_pk = Convert.ToInt32(row["iCurrentPlanID"]);
			_wtn_id = Convert.ToInt64(row["iWTNID"]);
			_prod_id = Convert.ToInt64(row["iProductID"]);

			if(row["szLastUpdatedBy"] != DBNull.Value)
				_lastUpdatedBy = Convert.ToString(row["szLastUpdatedBy"]);

			if(row["dtUpdated"] != DBNull.Value)
				_dateUpdated = Convert.ToDateTime(row["dtUpdated"]);

			if(row["blnRetired"] != DBNull.Value)
				_retired = Convert.ToBoolean(row["blnRetired"]);
			else
				_retired = false;

			/*
			_prod_type_text = Convert.ToString(row["szProdTypeDescription"]);
			_prod_text = Convert.ToString(row["szDescription"]);
			*/
			_prod_type_text = "proc change!";
			_prod_text = "proc change!";
		}

		public long CurrentPlanID
		{
			get { return _pk; }
		}

		public long WTNID
		{
			get { return _wtn_id; }
			set { 
				if(value != _wtn_id)
				{
					_wtn_id = value; 
					this.Dirty = true; 
				}
			}
		}

		public long ProductID
		{
			get { return _prod_id; }
			set { 
				if(value != _prod_id)
				{
					_prod_id = value; 
					this.Dirty = true;
				}

			}
		}

		public string Description
		{
			get { return _prod_text; }
		}

		public string ProdTypeDescription
		{
			get { return _prod_type_text; }
		}

		public override string ToString()
		{
			return _prod_text;
		}

	}
}
