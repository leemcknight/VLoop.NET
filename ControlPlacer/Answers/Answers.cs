using System;
using System.Collections;

namespace Afni.ControlPlacer.Answers
{

	/// <summary>
	/// Represents a group of answers for a given question.
	/// </summary>
	public class Record
	{
		long _ID;
		ArrayList _answers;
	
		public Record()
		{
			_answers = new ArrayList();
			_ID = 0;
		}

		public Record(long GroupID)
		{
			_answers = new ArrayList();
			_ID = GroupID;
		}

		public void ToXML(System.Xml.XmlNode parentNode)
		{
			System.Xml.XmlNode answerRoot;
			System.Xml.XmlNode recordRoot;
			System.Xml.XmlAttribute idAttrib;
			System.Xml.XmlAttribute answerCountAttrib;

			recordRoot = parentNode.OwnerDocument.CreateElement("record");
			parentNode.AppendChild(recordRoot);

			idAttrib = parentNode.OwnerDocument.CreateAttribute("recordID");
			idAttrib.Value = _ID.ToString();
			recordRoot.Attributes.Append(idAttrib);

			answerRoot = parentNode.OwnerDocument.CreateElement("answers");

			answerCountAttrib = parentNode.OwnerDocument.CreateAttribute("count");
			answerCountAttrib.Value = _answers.Count.ToString();
			answerRoot.Attributes.Append(answerCountAttrib);

			foreach(Answer answer in _answers)
			{
				answer.ToXML(answerRoot);
			}

			recordRoot.AppendChild(answerRoot);
		}

		/// <summary>
		/// Gets or sets the ID for the 
		/// group of answers
		/// </summary>
		public long RecordID
		{ 
			get { return _ID; }
			set { _ID = value; }
		}

		/// <summary>
		/// Adds a new answer to the group
		/// </summary>
		/// <param name="answer"></param>
		/// <returns></returns>
		public bool AddAnswer(Answer answer)
		{
			bool add_ok = true;

			if(GetAnswer(answer.QuestionID) != null)
				add_ok = false;

			if(add_ok)
				_answers.Add(answer);
			
			return add_ok;
		}

		/// <summary>
		/// Gets the answer from the group 
		/// corresponding to the Question ID 
		/// passed in.
		/// </summary>
		/// <param name="iQuestionID"></param>
		/// <returns></returns>
		public Answer GetAnswer(long iQuestionID)
		{
			Answer found_answer = null;

			foreach(Answer answer in _answers)
			{
				if(answer.QuestionID == iQuestionID)
				{
					found_answer = answer;
					break;
				}
			}

			return found_answer;
		}

		public ArrayList Answers
		{
			get { return _answers; }
		}

		public override string ToString()
		{
			if(Answers.Count > 0)
			{
				return Answers[0].ToString();
			}
			else
			{
				return "";
			}
		}

	}

	
	/// <summary>
	/// Abstract base class for all answers.  Answer is an abstract
	/// object that encapsulates common dynamic answer information,
	/// such as the question ID, Group ID, and the XML representation.
	/// The answer itself can take many forms, mean to be implemented
	/// in the subclasses.
	/// </summary>
	public abstract class Answer
	{
		protected long _question_id;
		protected long _recordID = 0; 
	
		public long QuestionID
		{
			get { return _question_id; }
			set { _question_id = value; }
		}

		public long RecordID
		{
			get { return _recordID; }
			set { _recordID = value; }
		}

		public override string ToString()
		{
			return Convert.ToString(_question_id);
		}

		public virtual System.Xml.XmlNode ToXML(System.Xml.XmlNode parentNode)
		{
			return parentNode;
		}
	}

	/// <summary>
	/// Represents an answer that contains
	/// free-form text.
	/// </summary>
	public class TextAnswer : Answer
	{
		protected string _text; 
		protected long _answer_id;

		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}

		public long AnswerID
		{
			get { return _answer_id; }
			set { _answer_id = value; }
		}

		public override string ToString()
		{
			return _text;
		}

		public override System.Xml.XmlNode ToXML(System.Xml.XmlNode parentNode)
		{
			System.Xml.XmlNode answerRoot;
			System.Xml.XmlNode answerNode;
			System.Xml.XmlAttribute idAttrib;
			System.Xml.XmlAttribute valAttrib;

			answerRoot = parentNode.OwnerDocument.CreateElement("answers");
			parentNode.AppendChild(answerRoot);

			answerNode = parentNode.OwnerDocument.CreateElement("answer");
			idAttrib = parentNode.OwnerDocument.CreateAttribute("id");
			idAttrib.Value = _answer_id.ToString();
			answerNode.Attributes.Append(idAttrib);

			valAttrib = parentNode.OwnerDocument.CreateAttribute("value");
			valAttrib.Value = _text;
			answerNode.Attributes.Append(valAttrib);

			answerRoot.AppendChild(answerNode);

			return parentNode;
		}
	}


	/// <summary>
	/// Represents an answer that contains lookup information
	/// to pre-set possible values
	/// </summary>
	public class LookupAnswer : Answer
	{
		private ArrayList _lookups;
	
		public LookupAnswer()
		{
			_lookups = new ArrayList();
		}

		/// <summary>
		/// returns a string representation of the answer
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string ans="";
			foreach(Lookups.LookupData lookup in _lookups)
			{	
				if(lookup.Selected)
				{
					ans += Convert.ToString(lookup.Text);
					ans += ",";
				}
			}

			if(_lookups.Count > 0)
				ans = ans.Substring(0,ans.Length-1);

			return ans;
		}

		/// <summary>
		/// returns all the answer data
		/// </summary>
		public ArrayList AnswerData
		{
			get
			{
				return _lookups;
			}
		}

		/// <summary>
		/// Returns an XML string representation 
		/// of the answer
		/// </summary>
		/// <returns></returns>
		public override System.Xml.XmlNode ToXML(System.Xml.XmlNode parentNode)
		{
			System.Xml.XmlNode answerRoot;
			System.Xml.XmlNode answerNode;
			System.Xml.XmlAttribute idAttrib;
			System.Xml.XmlAttribute valAttrib;
			System.Xml.XmlAttribute selAttrib;

			answerRoot = parentNode.OwnerDocument.CreateElement("answers");
			parentNode.AppendChild(answerRoot);

			foreach(Lookups.LookupData lookup in _lookups)
			{
				answerNode = parentNode.OwnerDocument.CreateElement("answer");
				idAttrib = parentNode.OwnerDocument.CreateAttribute("lookupID");
				idAttrib.Value = lookup.LookupID.ToString();
				answerNode.Attributes.Append(idAttrib);

				valAttrib = parentNode.OwnerDocument.CreateAttribute("value");
				valAttrib.Value = lookup.Text;
				answerNode.Attributes.Append(valAttrib);

				selAttrib = parentNode.OwnerDocument.CreateAttribute("selected");
				selAttrib.Value = lookup.Selected.ToString();
				answerNode.Attributes.Append(selAttrib);

				answerRoot.AppendChild(answerNode);
			}

			return parentNode;
		}

	}
}
