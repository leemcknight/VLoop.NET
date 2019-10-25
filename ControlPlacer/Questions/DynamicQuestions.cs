using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Xml;
using Afni.ControlPlacer.Answers;

namespace Afni.ControlPlacer.Questions
{
	
	/// <summary>
	/// Contains the different types of questions
	/// that can be used on the dynamic question 
	/// grids.
	/// </summary>
	public enum QuestionTypes
	{
		Text,
		Radio,
		Combo,
		CheckBox,
		MultiSelect,
		MultiLineText,
		PhoneMask,
		DateMask
	}

	/// <summary>
	/// Abstract base class for all
	/// question objects.
	/// </summary>
	public abstract class Question
	{
		#region Member variables
		private long _question_id = 0;
		private int _sequence = 0;
		private string _text = "";
		private int _width = 0;
		private int _height = 0;
		private Column _column_data = null;
		private bool _required = false;
		private Font _font;
		private bool _enabled = true;
		private Answer _answer;
		private int _required_columns;
		private int _required_rows;
		private Control _parent;
		private ErrorProvider _err_provider;
		private string _err_text;
		private QuestionTypes _type;
		#endregion

		#region Events
		public event EventHandler Changed;
		protected void OnChanged() {if (Changed != null) { Changed(this, null ); } }
		#endregion

		#region constructor
		public Question(long questionID, int sequence, string text, Control parent)
		{
			_required_columns = 1;
			_required_rows = 1;
			_question_id = questionID;
			_sequence = sequence;
			_text = text;
			_parent = parent;
		}

		#endregion

		#region properties
		public long QuestionID
		{
			get { return _question_id; }
			set {_question_id = value; }
		}

		/// <summary>
		/// The sequence of the question on the dynamic form
		/// </summary>
		public int Sequence
		{
			get { return _sequence; }
			set { _sequence = value; }
		}

		/// <summary>
		/// The title of the question.
		/// </summary>
		public string Text
		{
			get { return _text; }
			set 
			{ 
				_text = value; 
				RefreshProperties();
			}
		}

		public ErrorProvider ErrorProvider
		{
			get { return _err_provider; }
			set { _err_provider = value; }
		}

		/// <summary>
		/// The text that will be displayed to the user in 
		/// the event that validation fails on this dynamic
		/// control.
		/// </summary>
		public string ValidationFailedText
		{
			get { return _err_text; }
			set { _err_text = value; }
		}

		/// <summary>
		/// The number of rows required by this question.  This
		/// property is read-only.
		/// </summary>
		public int RequiredRows
		{
			get { return _required_rows; }
			set { _required_rows = value; }
		}

		/// <summary>
		/// The number of columns required by this question.  This
		/// property is read-only.
		/// </summary>
		public int RequiredColumns
		{
			get { return _required_columns; }
			set { _required_rows = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating if an answer is required
		/// for this question in order for it to pass validation.
		/// </summary>
		public bool AnswerRequired
		{
			get { return _required; }
			set { _required = value; }
		}

		/// <summary>
		/// The font that will be displayed with the question
		/// </summary>
		public Font Font
		{
			get { return _font; }
			set 
			{
				_font = value; 
				RefreshProperties();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating if the question is enabled.
		/// </summary>
		public  bool Enabled
		{
			get { return _enabled; }
			set 
			{ 
				_enabled = value; 
				RefreshProperties();
			}
		}

		/// <summary>
		/// Gets or sets an object representing the answer to the question.
		/// </summary>
		public virtual Answer Answer
		{
			get { return _answer; }
			set { _answer = value; }
		}

		/// <summary>
		/// Gets or sets the column that the question belongs to.
		/// </summary>
		public Column Column
		{
			get { return _column_data; }
			set 
			{ 
				_column_data = value; 
				RefreshProperties();
			}
		}

		/// <summary>
		/// Gets or sets the parent control the question will belong to.
		/// </summary>
		public Control Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		#endregion

		#region Methods
		protected virtual void RefreshProperties()
		{

		}

		public virtual bool Draw(Point origin)
		{
			return true;
		}

		public virtual void Undo()
		{

		}

		public virtual bool Validate()
		{
			return true;
		}

		public virtual void AddChangeEvent(System.EventHandler e)
		{
		}

		public virtual void RegisterSave()
		{
		}

		public System.Xml.XmlNode ToXML(System.Xml.XmlNode parentNode)
		{
			System.Xml.XmlNode questionNode;
			System.Xml.XmlAttribute idAttrib;
			System.Xml.XmlAttribute nameAttrib;
			

			questionNode = parentNode.OwnerDocument.CreateElement("question");

			//ID attribute
			idAttrib = parentNode.OwnerDocument.CreateAttribute("id");
			idAttrib.Value = _question_id.ToString();
			questionNode.Attributes.Append(idAttrib);

			//Name attribute
			nameAttrib = parentNode.OwnerDocument.CreateAttribute("name");
			nameAttrib.Value = _text;
			questionNode.Attributes.Append(nameAttrib);
			
			_answer.ToXML(questionNode);

			parentNode.AppendChild(questionNode);

			return questionNode;
		}
		#endregion

		#region Protected properties
		protected int Width
		{
			get { return _width; }
			set { _width = value; }
		}

		protected int Height
		{
			get { return _height; }
			set { _height = value; }
		}
		#endregion
		
	}


	/// <summary>
	/// Interface to a special type of Dynamic Question
	/// called LookupQuestion.  Lookup Questions are 
	/// dynamic questions that have pre-determined possible
	/// values, such as combo boxes, radio buttons, 
	/// and checklists.
	/// </summary>
	public interface ILookupQuestion 
	{
		void AddQuestionData(long LookupID, string Text);
		int MaxLookupDataLength { get; }
		ArrayList Lookups { get; }
	}

	public class QuestionFactory
	{
		public static Question CreateQuestion(QuestionTypes type,int Sequence, int iQuestionID, string Text, Control parent)
		{
			Question question = null;

			switch(type)
			{
				case(QuestionTypes.CheckBox):
				{
					question = new CheckBoxQuestion(iQuestionID,Sequence,Text,parent);
					break;
				}
				case(QuestionTypes.Combo):
				{
					question = new ComboQuestion(iQuestionID, Sequence, Text,parent);
					break;
				}
				case(QuestionTypes.MultiLineText):
				{
					question = new MultiLineTextQuestion(iQuestionID, Sequence, Text,parent);
					break;
				}
				case(QuestionTypes.MultiSelect):
				{
					question = new MultiSelectQuestion(iQuestionID,Sequence,Text,parent);
					break;
				}
				case(QuestionTypes.Radio):
				{
					question = new RadioQuestion(iQuestionID,Sequence,Text,parent);
					break;
				}
				case(QuestionTypes.Text):
				{
					question = new TextQuestion(iQuestionID,Sequence,Text,parent);
					break;
				}
				
			}
			
			return question;
		}
	}
}
