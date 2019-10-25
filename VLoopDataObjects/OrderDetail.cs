using System;
using System.Data;
using System.Collections;

namespace Afni.Applications.VLoop.VLoopDataObjects
{

	public class OrderDetail : DeletableObject
	{
		private long _productID;
		private long _orderID;
		private long _accountID;
		private long _wtnID;
		private string _desc;

		public OrderDetail()
		{
		}

		public OrderDetail(DataRow row)
		{
			_pk = Convert.ToInt32(row["iOrderDetailID"]);
			_productID = Convert.ToInt64(row["iProductsID"]);	
			_orderID = Convert.ToInt64(row["iOrderID"]);
			_accountID = Convert.ToInt64(row["iAccountID"]);
			_wtnID = Convert.ToInt64(row["iWTNID"]);
			_lastUpdatedBy = Convert.ToString(row["szLastUpdatedBy"]);
			_dateUpdated = Convert.ToDateTime(row["dtUpdated"]);
			_desc = Convert.ToString(row["szDescription"]);

			if(row["blnRetired"] != DBNull.Value)
				_retired = Convert.ToBoolean(row["blnRetired"]);
		}

		public long OrderDetailID
		{
			get { return _pk; }
		}

		public long ProductID
		{
			get { return _productID; }
			set
			{
				if(_productID != value)
				{
					_productID = value;
					this.Dirty =true;
				}
			}
		}

		public long OrderID
		{
			get { return _orderID; }
			set
			{
				if(_orderID != value)
				{
					_orderID = value;
					this.Dirty = true;
				}
			}
		}

		public long AccountID
		{
			get { return _accountID; }
			set
			{
				if(_accountID != value)
				{
					_accountID  =value; 
					this.Dirty = true;
				}
			}
		}

		public long WTNID
		{
			get { return _wtnID; }
			set
			{
				if(_wtnID != value)
				{
					_wtnID = value;
					this.Dirty = true;
				}
			}
		}

		public string Description
		{
			get { return _desc; }
		}

		public override string ToString()
		{
			return _desc;
		}
	}
}
