using System;
using System.Collections;
using System.Data;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopDataObjects
{

	public class CampaignBase : VLoopDataObject
	{
		private string _name;
		

		public CampaignBase()
		{
	
		}

		public CampaignBase(DataRow row)
		{
			this.Dirty = false;
			_pk = Convert.ToInt32(row["iCampaignID"]);
			_name = Convert.ToString(row["szCampaignName"]);
		}

		public int CampaignID
		{
			get { return _pk; }
			set { 
				_pk = value; 
				this.Dirty = true;
			}
		}

		public string CampaignName
		{
			get { return _name; }
			set
			{
				if(_name != value )
				{
					_name = value; 
					this.Dirty = true;
				}
			}
		}
	}

	public class ApplicationSetting : VLoopDataObject
	{
		private long _id;
		private string _key;
		private string _value;
		private string _category;

		public ApplicationSetting()
		{
		}

		public ApplicationSetting(DataRow row)
		{
			_id = Convert.ToInt64(row["iApplicationSettingsID"]);
			_key = Convert.ToString(row["szKey"]);
			_value = Convert.ToString(row["szValue"]);
			_category = Convert.ToString(row["szCategory"]);
		}

		public long ApplicationSettingID
		{
			get { return _id; }
		}

		public string Key
		{
			get { return _key; }
		}

		public string Value
		{
			get { return _value; }
		}

		public string Category
		{
			get { return _category; }
		}
	}

	public class ApplicationSettings
	{
		public const string UsesDialer = "UsesDialer";
		public const string ShowAccount = "ShowAccount";
		public const string AllowNSL = "NSL_CHECK";
		public const string ShowWTN = "SHOWWTN";
		public const string ShowOrders = "ShowOrder";
		public const string StartingOrderQueue = "STARTING_ORDER_QUEUE";
	}
}
