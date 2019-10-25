using System;
using System.Collections;
using System.Data;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	
	public abstract class Question : VLoopDataObject
	{
		protected long _id; 
		protected ArrayList _lookups;
		protected string _text;
		protected int _question_type_id;
		protected bool _required;
		protected int _sequence;
		protected string _category;

		public long QuestionID
		{
			get { return _id; }
		}

		public ArrayList AvailableLookups
		{
			get 
			{ 
				if(_lookups == null)
				{
					_lookups = new ArrayList();
					DataSet ds = AvailableLookupInfoServer.GetByQuestionID(_id);
					foreach(DataRow row in ds.Tables["tblAvailableLookupInfo"].Rows)
					{
						_lookups.Add(new AvailableLookupInfo(row));
					}
				}
				return _lookups; 
			}
		}

		public string QuestionText
		{
			get { return _text; }
			set
			{
				if(_text != value)
				{
					_text = value; 
					this.Dirty = true;
				}
			}
		}

		public int QuestionTypeID
		{
			get { return _question_type_id; }
			set
			{
				if(_question_type_id != value)
				{
					_question_type_id = value; 
					this.Dirty = true;
				}
			}
		}

		public bool Required
		{
			get { return _required; }
			set
			{
				if(_required != value)
				{
					_required = value; 
					this.Dirty = true;
				}
			}
		}

		public int Sequence
		{
			get { return _sequence; }
			set
			{
				if(_sequence != value)
				{
					_sequence = value; 
					this.Dirty = true;
				}
			}
		}

		public string Category
		{
			get { return _category; }
			set
			{
				if(_category != value)
				{
					_category = value; 
					this.Dirty = true;
				}
			}
		}
	}

	public class CustomerQuestion : Question
	{
		private long _camp_id; 
		public CustomerQuestion()
		{

		}

		public CustomerQuestion(DataRow row)
		{
			_pk = Convert.ToInt32(row["iCustomerQuestionsLookupID"]);
			_id = Convert.ToInt64(row["iQuestionID"]);
			_category = Convert.ToString(row["CategoryText"]);
			_question_type_id = Convert.ToInt16(row["iControlQuestionTypeID"]);
			_required = Convert.ToBoolean(row["blnRequired"]);
			_sequence =  Convert.ToInt16(row["iSequence"]);
			_text = Convert.ToString(row["QuestionText"]);

			if(row["szLastUpdatedBy"] != DBNull.Value)
				_lastUpdatedBy = Convert.ToString(row["szLastUpdatedBy"]);

			if(row["dtUpdated"] != DBNull.Value)
				_dateUpdated = Convert.ToDateTime(row["dtUpdated"]);
			_camp_id = Convert.ToInt64(row["iCampaignID"]);
		}

		public long CampaignID
		{
			get { return _camp_id; }
		}

	}

	public class CampaignQuestion : Question
	{
		private long _camp_id; 

		public CampaignQuestion()
		{
		}

		public CampaignQuestion(DataRow row)
		{
			_pk = Convert.ToInt32(row["iCampaignQuestionsLookupID"]);
			_id = Convert.ToInt64(row["iQuestionID"]);
			_category = Convert.ToString(row["CategoryText"]);
			_question_type_id = Convert.ToInt16(row["iControlQuestionTypeID"]);
			_required = Convert.ToBoolean(row["blnRequired"]);
			_sequence =  Convert.ToInt16(row["iSequence"]);
			_text = Convert.ToString(row["QuestionText"]);
			_lastUpdatedBy = Convert.ToString(row["szLastUpdateBy"]);

			if(row["dtUpdated"] != DBNull.Value)
				_dateUpdated = Convert.ToDateTime(row["dtUpdated"]);
			_camp_id = Convert.ToInt64(row["iCampaignID"]);
		}

		public long CampaignID
		{
			get { return _camp_id; }
		}
	}

	public class ProductQuestion : Question
	{
		private long _prod_type_id;

		public ProductQuestion()
		{
		}

		public ProductQuestion(DataRow row)
		{
			_prod_type_id = Convert.ToInt64(row["iProdTypeID"]);
		}

		public long ProdTypeID
		{
			get { return _prod_type_id; }
		}
	}

	public class OrderDetailQuestion : Question
	{
		private long _product_id;

		public OrderDetailQuestion()
		{
		}

		public OrderDetailQuestion(DataRow row)
		{
			_product_id = Convert.ToInt64(row["iProductsID"]);
		}

		public long ProductID
		{
			get { return _product_id; }
		}
	}

}
