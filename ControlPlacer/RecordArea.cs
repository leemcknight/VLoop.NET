using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Afni.ControlPlacer.Questions;
using Afni.ControlPlacer.Answers;
using System.Xml;

namespace Afni.ControlPlacer
{

	public enum RecordEditModes
	{
		Add,
		Edit,
		View
	}

	/// <summary>
	/// Represents an area that can have multiple answers to 
	/// a given question.  
	/// </summary>
	public class RecordArea : Area
	{
		#region Member Variables
		private RecordDisplayStyles _style;
		private RecordView _view;
		private RecordEditModes _mode = RecordEditModes.View;
		#endregion

		#region Constructors
		
		public RecordArea(ContainerControl container) : base(container)
		{
			_style = RecordDisplayStyles.ListView;
		}

		public RecordArea(int columns, ContainerControl container)
			:base(columns, container)
		{
			_style = RecordDisplayStyles.ListView;
		}

		public RecordArea(int Columns, ContainerControl Container, Point origin, RecordDisplayStyles display)
			:base(Columns, Container, origin)
		{
			_style = display;	
		}

		public RecordArea(int Columns, ContainerControl Container, Point origin, RecordDisplayStyles display, Control questionParent)
			:base(Columns,Container,origin,questionParent)
		{
			_style = display;
		}
		#endregion

		#region Properties
		
		/// <summary>
		/// Gets or sets the display style of the area.  Determines
		/// in which manner the group of records are shown.
		/// </summary>
		public RecordDisplayStyles DisplayStyle
		{
			get { return _style; }
			set { _style = value; }
		}

		/// <summary>
		/// Gets the active group.  This property is read-only.
		/// </summary>
		public Record ActiveRecord
		{
			get { return _view.ActiveRecord; }
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Overloaded.  Draws the Grouped area on the parent.
		/// </summary>
		public override void Draw()
		{
			switch( _style )
			{
				case RecordDisplayStyles.ListView:
					_view = new ListViewRecordView(this.Parent, this.Questions);	
					break;
				case RecordDisplayStyles.ListBox:
					_view = new ListBoxRecordView(this.Parent, this.Questions);
					break;
			}
			_view.DrawView();
			_view.EditMode = RecordEditModes.View;
			_view.SelectedRecordChanged += new SelectedRecordChangedHandler(this.HandleRecordChange);
			Draw( this.Parent.Size.Height / 2 );
			_mode = RecordEditModes.View;
			LoadEditMode(_mode);
		}

		/// <summary>
		/// Draws the area with the specified y offset
		/// for the controls.  
		/// </summary>
		/// <param name="yOffset"></param>
		protected void Draw(int yOffset)
		{
			Column column = this.FirstColumn;

			do
			{
				column.StartingY = yOffset;
				column = column.NextColumn;

			} while (column != null );

			base.Draw();
		}

		/// <summary>
		/// Registers a save of the current answer group.  Depending on the edit 
		/// mode of the grouped area, this may add a new answer group to the 
		/// area, or save the existing answer group.
		/// </summary>
		/// <returns></returns>
		public override bool RegisterSave()
		{
			Record record = _view.ActiveRecord;

			if(_mode == RecordEditModes.Add)
			{
				foreach(Question question in Questions)
				{
					question.RegisterSave();
					record.AddAnswer(question.Answer);
				}
				_view.Records.Add(record);
			}
			else
			{
				foreach(Question question in Questions)
				{
					question.RegisterSave();
				}
			}

			LoadEditMode(RecordEditModes.View);

			return true;
		}

		/// <summary>
		/// Adds a lookup answer to the group represented by the GroupID
		/// </summary>
		/// <param name="GroupID"></param>
		/// <param name="iQuestionID"></param>
		/// <param name="AnswerLookupID"></param>
		/// <returns></returns>
		public bool AddAnswerToRecord(long RecordID, long iQuestionID, long AnswerLookupID)
		{
			return true;
		}

		/// <summary>
		/// Adds a Text answer to the group represented by the GroupID
		/// </summary>
		/// <param name="GroupID"></param>
		/// <param name="iQuestionID"></param>
		/// <param name="AnswerText"></param>
		/// <returns></returns>
		public bool AddAnswerToRecord(long recordID, long questionID, string answerText)
		{
			Record record = RecordFromID(recordID);
			Answer newAnswer = new TextAnswer();
			newAnswer.RecordID = recordID;
			newAnswer.QuestionID = questionID;
			((TextAnswer)newAnswer).Text = answerText;
			record.AddAnswer(newAnswer);
			return true;
		}

		/// <summary>
		/// Returns a xml representation of the record area, including
		/// xml for the area and all the records in the area.
		/// </summary>
		/// <returns></returns>
		public override string ToXML()
		{
			System.Xml.XmlDocument doc;
			System.Xml.XmlNode rootNode;
			System.Xml.XmlNode recordRoot;
			
			doc = new System.Xml.XmlDocument();
			rootNode = doc.CreateNode(XmlNodeType.Element, "area", AFNIXmlNS);
			doc.AppendChild(rootNode);

			//record root node
			recordRoot = rootNode.OwnerDocument.CreateElement("records");
			rootNode.AppendChild(recordRoot);

			foreach(Record record in _view.Records)
			{
				record.ToXML(recordRoot);
			}

			return doc.InnerXml;
		}

		/// <summary>
		/// Adds a new answer group 
		/// </summary>
		public void New()
		{
			foreach(Question question in this.Questions)
			{
				question.Answer = null;	
			}

			Record newRecord = new Record(0);
			_view.ActiveRecord = newRecord;
			LoadEditMode(RecordEditModes.Add);
		}

		/// <summary>
		/// Edits the currently selected record
		/// </summary>
		public void Edit()
		{
			LoadEditMode(RecordEditModes.Edit);
		}

		/// <summary>
		/// Deletes the active record from the view
		/// </summary>
		public void Delete()
		{
			_view.Records.Remove(_view.ActiveRecord);
			LoadEditMode(RecordEditModes.View);
		}

		public override void Undo()
		{
			LoadEditMode(RecordEditModes.View);
			base.Undo();
		}

		#endregion

		#region Private Methods
		/// <summary>
		/// Returns the group represented by the group id
		/// </summary>
		/// <param name="GroupID"></param>
		/// <returns></returns>
		private Record RecordFromID(long recordID)
		{
			Record found_record = null;

			foreach(Record record in _view.Records)
			{
				if(record.RecordID == recordID)
				{
					found_record = record;
					break;
				}
			}

			return found_record;
		}
		
		/// <summary>
		/// updates the child controls to use the current edit mode
		/// </summary>
		private void LoadEditMode(RecordEditModes editMode)
		{
			_mode = editMode;
			_view.EditMode = editMode;
			foreach(Question question in this.Questions)
			{
				question.Enabled = 
					(editMode == RecordEditModes.View ? false : true);
			}
		}

		private void HandleRecordChange(object sender, SelectedRecordChangedArgs e)
		{
			Record newRecord = e.Record;

			if(newRecord != null)
			{
				//match up the answers with the questions
				foreach(Answer answer in newRecord.Answers)
				{
					foreach(Question question in this.Questions)
					{
						if(question.QuestionID == answer.QuestionID)
						{
							question.Answer = answer;
						}
					}
				}
			}
		}

		#endregion
	}	

	/// <summary>
	/// Contains different display styles 
	/// for a grouped area.
	/// </summary>
	public enum RecordDisplayStyles
	{
		ListView,
		ListBox,
		NavButtons,
		None
	}
}
