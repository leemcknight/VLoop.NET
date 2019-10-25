using System;
using System.Collections;
using System.Data;
using Afni.Applications.VLoop.VLoopServer;
using Afni.Formatting;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	public class WTNBase : DeletableObject
	{
		private string _wtn;
		private long _acct_id;
		private string _inter_pic;
		private string _inter_ani;
		private string _intra_pic;
		private string _intra_ani;
		private string _international_pic;
		private string _intra_cic;
		private string _inter_cic;
		private string _international_cic;
		private bool _international_block;
		

		public WTNBase()
		{
		
		}

		public WTNBase(DataRow row)
		{
			_pk = Convert.ToInt32(row["iWTNID"]);
			_wtn = Convert.ToString(row["sWTN"]);
			_acct_id = Convert.ToInt64(row["iAccountID"]);
			if(row["szLastUpdatedBy"] != DBNull.Value)
				_lastUpdatedBy = Convert.ToString(row["szLastUpdatedBy"]);
			if(row["dtUpdated"] != DBNull.Value)
				_dateUpdated = Convert.ToDateTime(row["dtUpdated"]);

			if(row["blnRetired"] != DBNull.Value)
				_retired = Convert.ToBoolean(row["blnRetired"]);
			else
				_retired = false;

			if(row["sInterlataPIC"] != DBNull.Value)
				_inter_pic = Convert.ToString(row["sInterlataPIC"]);

			if(row["sInterlataANI"] != DBNull.Value)
				_inter_ani = Convert.ToString(row["sInterlataANI"]);

			if(row["sIntralataPIC"] != DBNull.Value)
				_intra_pic = Convert.ToString(row["sIntralataPIC"]);

			if(row["sIntralataANI"] != DBNull.Value)
				_intra_ani = Convert.ToString(row["sIntralataANI"]);

			if(row["sInternationalPIC"] != DBNull.Value)
				_international_pic = Convert.ToString(row["sInternationalPIC"]);

			if(row["sIntralataCIC"] != DBNull.Value)
				_intra_cic = Convert.ToString(row["sIntralataCIC"]);

			if(row["sInterlataCIC"] != DBNull.Value)
				_inter_cic = Convert.ToString(row["sInterlataCIC"]);

			if(row["sInternationalCIC"] != DBNull.Value)
				_international_cic = Convert.ToString(row["sInternationalCIC"]);

			if(row["blnInternationalBlock"] != DBNull.Value)
				_international_block = Convert.ToBoolean(row["blnInternationalBlock"]);
			else
				_international_block = false;


		}

		public override string ToString()
		{
			return AfFormat.ToMaskedPhoneNumber(_wtn);
		}

		public int WtnID
		{
			get { return _pk; }
			set
			{
				_pk = value;
				this.Dirty = true;
			}
		}

		public string WorkingTelephoneNumber
		{
			get { return _wtn; }
			set
			{
				if(_wtn != value)
				{
					_wtn = value; 
					this.Dirty = true;
				}
			}
		}

		public long AccountID
		{
			get { return _acct_id; }
		}

		public string InterLataPIC
		{
			get { return _inter_pic; }
			set
			{
				if(_inter_pic != value)
				{
					_inter_pic = value;
					this.Dirty = true;
				}
			}
		}

		public string InterLataANI
		{
			get { return _inter_ani; }
			set
			{
				if(_inter_ani != value)
				{
					_inter_ani = value;
					this.Dirty = true;
				}
			}
		}

		public string IntraLataPIC
		{
			get { return _intra_pic; }
			set
			{
				if(_intra_pic != value)
				{
					_intra_pic = value;
					this.Dirty = true;
				}
			}
		}

		public string IntraLataANI
		{
			get { return _intra_ani; }
			set
			{
				if(_intra_ani != value)
				{
					_intra_ani = value;
					this.Dirty = true;
				}
			}
		}

		public string InternationalPIC
		{
			get { return _international_pic; }
			set
			{
				if(_international_pic != value)
				{
					_international_pic = value;
					this.Dirty = true;
				}
			}
		}

		public string IntraLataCIC
		{
			get { return _intra_cic; }
			set
			{
				if(_intra_cic != value)
				{
					_intra_cic = value;
					this.Dirty = true;
				}
			}

		}

		public string InterLataCIC
		{
			get { return _inter_cic; }
			set
			{
				if(_inter_cic != value)
				{
					_inter_cic = value;
					this.Dirty = true;
				}
			}
		}

		public string InternationalCIC
		{
			get { return _international_cic; }
			set
			{
				if (_international_cic != value)
				{
					_international_cic = value;
					this.Dirty = true;
				}
			}
		}

		public bool InternationalBlock
		{
			get { return _international_block; }
			set
			{
				if(_international_block != value)
				{
					_international_block = value; 
					this.Dirty = true;
				}
			}
		}

		
	}
}
