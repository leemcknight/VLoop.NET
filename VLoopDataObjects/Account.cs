using System;
using System.Collections;
using System.Data;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	/// <summary>
	/// Summary description for Account.
	/// </summary>
	public class AccountBase : VLoopDataObject
	{
		private string _acct_num;
		private DateTime _in_serv_date;
		private DateTime _bill_date;
		private bool _franchise;
		private long _btnID;
		

		public AccountBase()
		{
			
		}

		public AccountBase(DataRow row)
		{
			_pk = Convert.ToInt32(row["iAccountID"]);
			_acct_num = Convert.ToString(row["szAccountNumber"]);

			if(row["dtInServiceDate"] != DBNull.Value)
				_in_serv_date = Convert.ToDateTime(row["dtInServiceDate"]);

			if(row["dtBillDate"] != DBNull.Value)
				_bill_date = Convert.ToDateTime(row["dtBillDate"]);

			_franchise = Convert.ToBoolean(row["blnInFranchise"]);
			_btnID = Convert.ToInt64(row["iBTNID"]);
		}

		public int AccountID
		{
			get { return _pk; }
			set
			{
				_pk = value; 
				this.Dirty = true;
			}
		}

		public string AccountNumber
		{
			get { return _acct_num; }
			set 
			{
				if(_acct_num != value)
				{
					_acct_num = value;
					this.Dirty = true;
				}
			}
		}

		public DateTime InServiceDate
		{
			get { return _in_serv_date; }
			set
			{
				if(_in_serv_date != value)
				{
					_in_serv_date = value; 
					this.Dirty = true;
				}
			}
		}

		public DateTime BillDate
		{
			get { return _bill_date; }
			set
			{
				if(_bill_date != value)
				{
					_bill_date = value; 
					this.Dirty = true;
				}
			}
		}

		public bool InFranchise
		{
			get { return _franchise; }
			set
			{
				if(_franchise != value)
				{
					_franchise = value;
					this.Dirty = true;
				}
			}
		}

		public long BTNID
		{
			get { return _btnID; }
			set 
			{ 
				_btnID = value;
				this.Dirty = true; 
			}
		}
	}
}
