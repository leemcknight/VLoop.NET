using System;
using System.Collections;
using System.Data;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	
	public abstract class Answer : DeletableObject
	{
		protected long _question_id;
		protected string _text;
		protected string _question_text;
		protected string _lookup_text;
		protected long _control_type_id;
		protected long _lookup_id;

		public long AnswerID
		{
			get { return _pk; }
		}

		public long QuestionID
		{
			get { return _question_id; }
		}

		public string AnswerText
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

		public string QuestionText
		{
			get { return _question_text; }
		}

		public long LookupID
		{
			get { return _lookup_id; }
			set
			{
				if(_lookup_id != value)
				{
					_lookup_id = value;
					this.Dirty = true;
				}
			}
		}

		public string LookupText
		{
			get { return _lookup_text; }
		}

		public long ControlTypeID
		{
			get { return _control_type_id; }
		}

	}

	public class CurrentPlanDetail : Answer
	{
		long _current_plan_id;

		public CurrentPlanDetail()
		{
				
		}

		public CurrentPlanDetail(DataRow row)
		{
			_pk = Convert.ToInt32(row["iCurrentPlanDetailID"]);
			_question_id = Convert.ToInt64(row["iQuestionID"]);

			if(row["iAvailableLookupInfoID"] != DBNull.Value)
				_lookup_id = Convert.ToInt64(row["iAvailableLookupInfoID"]);

			_current_plan_id = Convert.ToInt64(row["iCurrentPlanID"]);
	
			_question_text = Convert.ToString(row["szQuestion"]);

			if(row["szAdditionalInfo"] != DBNull.Value)
				_text = Convert.ToString(row["szAdditionalInfo"]);

		}	

		public long CurrentPlanID
		{
			get { return _current_plan_id; }
			set
			{
				if(_current_plan_id != value)
				{
					_current_plan_id = value;
					this.Dirty = true;
				}
			}
		}

		public override string ToString()
		{
			return _text;
		}
	}

	public class CustomerAnswer : Answer
	{
		private long _customerID;

		public CustomerAnswer()
		{
		}

		public CustomerAnswer(DataRow row)
		{
			_pk = Convert.ToInt32(row["iCustomerAnswersID"]);
			_customerID = Convert.ToInt64(row["iCustomerID"]);
			_question_id = Convert.ToInt64(row["iQuestionID"]);

			if(row["iAvailableLookupInfoID"] != DBNull.Value)
				_lookup_id = Convert.ToInt64(row["iAvailableLookupInfoID"]);

			if(row["szAnswer"] != DBNull.Value)
				_text = Convert.ToString(row["szAnswer"]);

			//_question_text = Convert.ToString(row["szQuestion"]);
		}

		public long CustomerID
		{
			get { return _customerID; }
		}

		public override string ToString()
		{
			return _text; 
		}
	}
}
