using System;
using System.Data;
using Afni.Applications.VLoop.VLoopServer;
using System.Collections;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	public class OrderBase : DeletableObject
	{
		private long _accountID;
		private long _custCampaignID;
		private DateTime _orderDate;
		private string _comments;
		private long _currentQueueID;
		
		public OrderBase ()
		{
		}

		public OrderBase(DataRow row)
		{
			_pk = Convert.ToInt32(row["iOrderID"]);
			_accountID = Convert.ToInt64(row["iAccountID"]);
			_custCampaignID = Convert.ToInt64(row["iCustomerCampaignID"]);
			_orderDate = Convert.ToDateTime(row["dtOrderDate"]);

			if(row["txtComment"] != DBNull.Value)
				_comments = Convert.ToString(row["txtComment"]);

			if(row["szLastUpdatedBy"] != DBNull.Value)
				_lastUpdatedBy = Convert.ToString(row["szLastUpdatedBy"]);

			if(row["dtUpdated"] != DBNull.Value)
				_dateUpdated = Convert.ToDateTime(row["dtUpdated"]);

			_currentQueueID = Convert.ToInt64(row["iCurrentQueueID"]);

			if(row["blnRetired"] != DBNull.Value)
				_retired = Convert.ToBoolean(row["blnRetired"]);
		}

		public long OrderID
		{
			get { return _pk; }
		}

		public long AccountID
		{
			get { return _accountID; }
			set
			{
				if(_accountID != value)
				{
					this.Dirty = true;
					_accountID = value;
				}
			}
		}

		public long CustomerCampaignID
		{
			get { return _custCampaignID; }
			set
			{
				if(_custCampaignID != value)
				{
					this.Dirty = true;
					_custCampaignID = value; 
				}
			}
		}

		public DateTime OrderDate
		{
			get { return _orderDate; }
			set
			{
				if(_orderDate != value)
				{
					_orderDate = value;
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

		public long CurrentQueueID
		{
			get { return _currentQueueID; }
			set
			{
				if(_currentQueueID != value)
				{
					_currentQueueID = value; 
					this.Dirty = true;
				}
			}
		}
		
		
		
	}
}
