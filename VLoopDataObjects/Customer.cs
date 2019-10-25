using System;
using System.Data;
using System.Collections;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopDataObjects
{

	public class CustomerBase : VLoopDataObject
	{
		private string _first_name;
		private string _last_name;
		private string _email;
		private string _bill_add_1;
		private string _bill_add_2;
		private string _bill_city;
		private string _bill_state;
		private string _bill_zip;
		private string _serv_add_1;
		private string _serv_add_2;
		private string _serv_city;
		private string _serv_state;
		private string _serv_zip;
		private string _company;
		private string _locked_by;
		private long _lang_id;
		private bool _nocall;
		private string _alt_phone;
		private string _former_tdm;
		

		public CustomerBase()
		{
		}

		public CustomerBase(DataRow row)
		{
			_pk = Convert.ToInt32(row["iCustomerID"]);
			_first_name = Convert.ToString(row["szFirstName"]);
			_last_name = Convert.ToString(row["szLastName"]);
			_email = Convert.ToString(row["szEmailAddress"]);
			_alt_phone = Convert.ToString(row["sAlternatePhone"]);
			_former_tdm = Convert.ToString(row["szFormerTDM"]);
			_bill_add_1 = Convert.ToString(row["szBillingAddress"]);
			_bill_add_2 = Convert.ToString(row["szBillingSubLocation"]);
			_bill_city = Convert.ToString(row["szBillingCity"]);
			_bill_state = Convert.ToString(row["sBillingState"]);
			_bill_zip = Convert.ToString(row["sBillingZip"]);
			_serv_add_1 = Convert.ToString(row["szServiceAddress"]);
			_serv_add_2 = Convert.ToString(row["szServiceSubLocation"]);
			_serv_city = Convert.ToString(row["szServiceCity"]);
			_serv_state = Convert.ToString(row["sServiceState"]);
			_serv_zip = Convert.ToString(row["sServiceZip"]);

			if(row["szLastUpdatedBy"] != DBNull.Value)
				_lastUpdatedBy = Convert.ToString(row["szLastUpdatedBy"]);

			if(row["dtUpdated"] != DBNull.Value)
				_dateUpdated = Convert.ToDateTime(row["dtUpdated"]);

			if(row["szBusinessName"] != DBNull.Value)
				_company = Convert.ToString(row["szBusinessName"]);

			if(row["szLockedBy"] != DBNull.Value)
				_locked_by = Convert.ToString(row["szLockedBy"]);

			if(row["blnDoNotCall"] != DBNull.Value)
				_nocall = Convert.ToBoolean(row["blnDoNotCall"]);
			else
				_nocall = false;

		}

		public int CustomerID
		{
			get { return _pk; }
			set { _pk = value; }
		}

		public string FirstName
		{
			get { return _first_name; }
			set 
			{
				if(_first_name != value)
				{
					_first_name = value;
					this.Dirty = true;
				}
			}
		}

		public string LastName
		{
			get { return _last_name; }
			set 
			{
				if(_last_name != value)
				{
					_last_name= value;
					this.Dirty = true;
				}
			}
		}

		public string Email
		{
			get { return _email; }
			set 
			{
				if(_email != value)
				{
					_email = value; 
					this.Dirty = true;
				}
			}
		}

		public string ServiceAddress1
		{
			get { return _serv_add_1; }
			set 
			{
				if(_serv_add_1 != value)
				{
					_serv_add_1 = value;
					this.Dirty = true;
				}
			}
		}

		public string ServiceAddress2
		{
			get { return _serv_add_2; }
			set 
			{
				if(_serv_add_2 != value)
				{
					_serv_add_2 = value; 
					this.Dirty = true;
				}
			}
		}

		public string ServiceCity
		{
			get { return _serv_city; }
			set 
			{
				if(_serv_city != value)
				{
					_serv_city = value; 
					this.Dirty = true;
				}
			}
		}

		public string ServiceState
		{
			get { return _serv_state; }
			set
			{
				if(_serv_state != value)
				{
					_serv_state = value;
					this.Dirty = true;
				}
			}
		}

		public string ServiceZip
		{
			get { return _serv_zip; }
			set 
			{
				if(_serv_zip != value)
				{
					_serv_zip = value;
					this.Dirty = true;
				}
			}
		}

		public string BillingAddress1
		{
			get { return _bill_add_1; }
			set 
			{
				if(_bill_add_1 != value)
				{
					_bill_add_1 = value;
					this.Dirty = true;
				}
			}
		}

		public string BillingAddress2
		{
			get { return _bill_add_2; }
			set
			{
				if(_bill_add_2 != value)
				{
					_bill_add_2 = value;
					this.Dirty = true;
				}
			}
		}

		public string BillingCity
		{
			get { return _bill_city; }
			set
			{
				if(_bill_city != value)
				{
					_bill_city = value;
					this.Dirty = true;
				}
			}
		}

		public string BillingState
		{
			get { return _bill_state; }
			set
			{
				if(_bill_state != value)
				{
					_bill_state = value;
					this.Dirty = true;
				}
			}
		}

		public string BillingZip
		{
			get { return _bill_zip; }
			set
			{
				if(_bill_zip != value)
				{
					_bill_zip = value;
					this.Dirty = true;
				}

			}
		}

		public string Company
		{
			get { return _company; }
			set
			{
				if(_company != value)
				{
					_company = value;
					this.Dirty = true;
				}
			}
		}

		public string LockedBy
		{
			get { return _locked_by; }
			set
			{
				if(_locked_by != value)
				{
					_locked_by = value;
					this.Dirty = true;
				}
			}
		}

		public bool DoNotCall
		{
			get { return _nocall; }
			set
			{
				if(_nocall != value)
				{
					_nocall = value;
					this.Dirty =true;
				}
			}
		}

		public string AltPhone
		{
			get { return _alt_phone; }
			set
			{
				if(_alt_phone != value)
				{
					_alt_phone = value;
					this.Dirty = true;
				}
			}
		}

		public string FormerTDM
		{
			get { return _former_tdm; }
			set
			{
				if(_former_tdm != value)
				{
					_former_tdm = value;
					this.Dirty = true;
				}
			}
		}

		
	}


	public class SearchItem : VLoopDataObject
	{
		private string _firstName;
		private string _lastName;
		private string _btn;
		private string _disposition;
		private string _callResult;
		private DateTime _loadDate;
		private DateTime _dispDate;
		

		public SearchItem(DataRow row)
		{
			_pk = Convert.ToInt32(row["iCustomerCampaignID"]);
			_firstName = Convert.ToString(row["szFirstName"]);
			_lastName = Convert.ToString(row["szlastname"]);
			_btn = Convert.ToString(row["sWTN"]);
			_disposition = Convert.ToString(row["szDisposition"]);
			_callResult = Convert.ToString(row["szCallResult"]);
			_loadDate = Convert.ToDateTime(row["dtFileLoadDate"]);
			_dispDate = Convert.ToDateTime(row["dtDispositionDate"]);
		}

		public long CustomerCampaignID
		{
			get { return _pk; }
		}

		public string FirstName
		{
			get { return _firstName; }
		}

		public string LastName
		{
			get { return _lastName; }
		}

		public string BTN
		{
			get { return _btn; }
		}

		public string CurrentDisposition
		{
			get { return _disposition; }
		}

		public string CallResult
		{
			get { return _callResult; }
		}

		public DateTime LoadDate
		{
			get { return _loadDate; }
		}

		public DateTime DispositionDate
		{
			get { return _dispDate; }
		}

	}
}
