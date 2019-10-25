using System;
using System.Data;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	/// <summary>
	/// Summary description for CustomerCampaign.
	/// </summary>
	public class CustomerCampaignBase : VLoopDataObject
	{
		private long _cust_id;
		private long _camp_id;
		private long _disp_id;
		private DateTime _disp_date;
		private string _filename;
		private DateTime _fileload_date;
		

		public CustomerCampaignBase()
		{
			
		}

		public CustomerCampaignBase(DataRow row)
		{
			_pk = Convert.ToInt32(row["iCustomerCampaignID"]);
			_camp_id = Convert.ToInt64(row["iCampaignID"]);
			_cust_id = Convert.ToInt64(row["iCustomerID"]);
			_disp_id = Convert.ToInt64(row["iDispositionID"]);
			_disp_date = Convert.ToDateTime(row["dtDispositionDate"]);
			_filename = Convert.ToString(row["szFileName"]);
			_fileload_date = Convert.ToDateTime(row["dtFileLoadDate"]);
			_lastUpdatedBy = Convert.ToString(row["szLastUpdatedBy"]);
			_dateUpdated = Convert.ToDateTime(row["dtUpdated"]);
		}

		public long CustomerCampaignID
		{
			get { return _pk; }
		}

		public long CustomerID
		{
			get { return _cust_id; }
			set
			{
				_cust_id = value;
				this.Dirty = true;
			}
		}

		public long CampaignID
		{
			get { return _camp_id; }
			set
			{
				_camp_id = value; 
				this.Dirty = true;
			}
		}

		public long DispositionID
		{
			get { return _disp_id; }
			set 
			{
				if(_disp_id != value)
				{
					_disp_id = value;
					this.Dirty = true;
				}
			}
		}

		public DateTime DispositionDate
		{
			get { return _disp_date; }
			set
			{
				if(_disp_date != value)
				{
					_disp_date = value;
					this.Dirty = true;
				}
			}
		}

		public string FileName
		{
			get { return _filename; }
		}

		public DateTime FileLoadDate
		{
			get { return _fileload_date; }
		}
	}
}
