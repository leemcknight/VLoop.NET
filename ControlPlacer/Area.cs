using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Afni.ControlPlacer.Questions;
using Afni.ControlPlacer.Answers;
using System.Xml;

namespace Afni.ControlPlacer
{

	internal class GridSizes
	{
		public const int GridHeight = 30;
	}

	/// <summary>
	/// Buffers between bounds on group boxes.
	/// </summary>
	internal class GroupBoxBuffers
	{
		public const int TopGroupBoxBuffer = 15;
		public const int LeftGroupBoxBuffer = 20;
		public const int RightGroupBoxBuffer = 20;
		public const int BottomGroupBoxBuffer = 15;
	}

	/// <summary>
	/// Represents an area of dynamic controls.
	/// </summary>
	public class Area
	{
		#region Member variables
		protected const int TOPGRIDBUFFER = 20;
		protected const int LEFTGRIDBUFFER = 5;
		protected const int RIGHTGRIDBUFFER = 5;
		protected const int BOTTOMGRIDBUFFER = 5;
		protected const int ROWBUFFER = 5;
		protected const int COLUMNBUFFER = 15;

		//the parent control for all child question 
		//controls...
		private Control _parent;

		private ArrayList _questions;
		private ArrayList _rows;
		private Column _first_column = null;

		//the parent form or user control the area is going on.
		private ContainerControl _container;

		private Size _orig_minsize; 
		private Size _orig_maxsize;
		private Point _origin;
		private bool _enabled = true;
		private bool _show_grid = false;
		private bool _allow_small_resize = true;
		private int _columns = 0;
		private Font _font; 
		private ArrayList _errors;
		private Exception _last_error;
		private ErrorProvider _err_provider;
		protected const string AFNIXmlNS = "http://www.afniupsourcing.com";
		#endregion

		#region Public Events
		public event EventHandler Changed;
		#endregion

		#region Constructors
		public Area(ContainerControl Container)
		{
			InitializeObject(3,Container,new Point(0,0));
		}

		public Area(int Columns, ContainerControl Container)
		{
			InitializeObject(Columns, Container, new Point(0,0));
		}

		public Area(int Columns, ContainerControl Container, Point origin)
		{
			InitializeObject(Columns,Container,origin);
		}

		public Area(int Columns, ContainerControl Container, Point origin, Control questionParent)
		{
			_parent = questionParent;
			InitializeObject(Columns, Container, origin);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Converts all questions and their corresponding
		/// answers to a XML string
		/// </summary>
		/// <returns></returns>
		public virtual string ToXML()
		{
			System.Xml.XmlDocument doc;
			System.Xml.XmlNode rootNode;
			System.Xml.XmlNode questionRoot;
			
			doc = new System.Xml.XmlDocument();
			rootNode = doc.CreateNode(XmlNodeType.Element, "area", AFNIXmlNS);
			doc.AppendChild(rootNode);

			//question root node
			questionRoot = rootNode.OwnerDocument.CreateElement("questions");
			rootNode.AppendChild(questionRoot);
			
			foreach(Question question in _questions)
			{
				question.ToXML(questionRoot);
			}

			return doc.InnerXml;
			
		}

		/// <summary>
		/// Returns a string representation of the answer(s)
		/// to the question corresponding to the given 
		/// question ID.  For multi-answer questions, 
		/// it is comma delimited.
		/// </summary>
		/// <param name="QuestionID"></param>
		/// <returns></returns>
		public string AnswerFromQuestionID(long QuestionID)
		{
			Question question;
			string ans = "";

			question = QuestionFromID(QuestionID);

			if(question != null)
			{
				ans =  question.Answer.ToString();
			}

			return ans;
		}

		/// <summary>
		/// Performs an undo on all questions in the area.
		/// </summary>
		public virtual void Undo()
		{
			foreach(Question q in _questions)
				q.Undo();
		}

		/// <summary>
		/// Returns a string representation of the area.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return _parent.Text;
		}

		/// <summary>
		/// Gets or sets the value indicating 
		/// whether or not to show gridlines on
		/// the area.
		/// </summary>
		public bool ShowGrid
		{
			get { return _show_grid; }
			set 
			{ 
				int y_offset;
				y_offset = _container.Location.Y;

				_show_grid = value; 

			}
		}

		/// <summary>
		/// Indicates to the area that a save has 
		/// occured, marking the current answers
		/// as the latest saved answers.
		/// </summary>
		/// <returns></returns>
		public virtual bool RegisterSave()
		{
			bool save_ok = false;

			save_ok = Validate();

			if(save_ok)
			{
				foreach(Question q in _questions)
					q.RegisterSave();
			}

			return save_ok;
		}
	
		/// <summary>
		/// Creates and adds a new Dynamic Question to the area
		/// based on information about the question.
		/// </summary>
		/// <param name="iQuestionID"></param>
		/// <param name="QuestionText"></param>
		/// <param name="Type"></param>
		/// <param name="Sequence"></param>
		/// <returns></returns>
		public Question AddQuestion(int iQuestionID,
			string QuestionText,
			QuestionTypes Type,
			int Sequence)
		{
			Question question = null;
			question = QuestionFactory.CreateQuestion(Type,
				Sequence,
				iQuestionID,
				QuestionText,
				_parent);
			_questions.Add(question);
			question.Changed += new System.EventHandler(this.HandleQuestionChange);
			return question;
		}

		/// <summary>
		/// Adds the answer to the corresponding (non-lookup) question
		/// </summary>
		/// <param name="iQuestionID"></param>
		/// <param name="AnswerText"></param>
		/// <returns></returns>
		public bool AddAnswer(long questionID, string AnswerText, long answerID)
		{
			bool addOK = false;
			try
			{
				TextAnswer newAnswer = new TextAnswer();
				newAnswer.QuestionID = questionID;
				newAnswer.Text = AnswerText;
				newAnswer.AnswerID = answerID;	
				AddAnswer(newAnswer);
				addOK = true;
			}
			catch(System.Exception ex)
			{
				_errors.Add(ex);
				_last_error = ex;
				addOK = false;
			}

			return addOK;
		}

		/// <summary>
		/// Adds the answer to the corresponding question
		/// </summary>
		/// <param name="iQuestionID"></param>
		/// <param name="AnswerLookupID"></param>
		/// <returns></returns>
		public bool AddAnswer(long iQuestionID, long AnswerLookupID)
		{
			bool add_ok = false;
			ILookupQuestion question = QuestionFromID(iQuestionID) as ILookupQuestion;

			if (question != null)
			{
				question.AddQuestionData(AnswerLookupID, "");
				add_ok = true;
			}
			else
			{
				System.Exception ex = new System.Exception("This question does not allow lookup data.");
				ex.Source = "Area.AddAnswer()";
				_errors.Add(ex);
				_last_error = ex;
				add_ok = false;
			}

			return add_ok;
		}

		/// <summary>
		/// Adds the answer object.
		/// </summary>
		/// <param name="answer"></param>
		public void AddAnswer(Answer answer)
		{
			Question question = QuestionFromID(answer.QuestionID);
			question.Answer = answer;
		}

		/// <summary>
		/// Adds a question object to the dynamic area.
		/// </summary>
		/// <param name="question"></param>
		/// <returns></returns>
		public Question AddQuestion(Question question)
		{
			if(question.Parent == null)
			{
				question.Parent = _parent;
			}

			_questions.Add(question);
			question.Changed += new System.EventHandler(this.HandleQuestionChange);
			return question;
		}

		/// <summary>
		/// Returns the Dynamic Question object with the 
		/// question ID passed in.
		/// </summary>
		/// <param name="iQuestionID"></param>
		/// <returns></returns>
		public Question QuestionFromID(long iQuestionID)
		{
			foreach(Question question in _questions)
			{
				if(question.QuestionID == iQuestionID)
				{
					return question;
				}
			}

			return null;
		}

		/// <summary>
		/// Paints all the dynamic questions
		/// to the parent.  Orders the questions right
		/// to left, top to bottom based on the sequence
		/// of the questions in the arraylist and the 
		/// number of rows and columns.
		/// </summary>
		public virtual void Draw()
		{
			int width = 0;
			int height = 0;

			PlaceControls();

			Column last_column = ColumnFromIndex(_columns - 1);

			_first_column.StartingX = LEFTGRIDBUFFER;

			width = last_column.StartingX + last_column.Width;
		
			foreach(Row row in _rows)
			{
				height += GridSizes.GridHeight;
				row.Draw();
			}
			
			width += RIGHTGRIDBUFFER;
			width += LEFTGRIDBUFFER;

			if(_parent.Width > width)
				width = _parent.Width;

			if(_parent.Height > height)
				height = _parent.Height;

			_parent.Size = new Size(width,height);

		}

		/// <summary>
		/// Validates the answers to all the dynamic 
		/// questions.
		/// </summary>
		/// <returns></returns>
		public bool Validate()
		{
			bool isvalid = true;
			foreach(Question question in _questions)
			{
				isvalid &= question.Validate();
			}
			return isvalid;
		}

		#endregion

		#region Private Methods
		protected void InitializeObject(int Columns, ContainerControl Container, Point origin)
		{
			Form form = null;
			Column data = null;
			_container = Container;
			form = _container as Form;
			
			if(form != null)
			{
				_orig_maxsize = form.MaximumSize;
				_orig_minsize = form.MinimumSize;
			}
			BuildParent(origin);
			_columns = Columns;
			_questions = new ArrayList();
			_rows = new ArrayList();
			_err_provider = new ErrorProvider(_container);
			
			//fill the list with new
			//column data objects.  
			for(int i = 0; i < _columns; i++)
			{
				Column new_column = new Column(i);

				if(_first_column == null)
				{
					_first_column = new_column;
					data = _first_column;
				} 
				else
				{
					data.NextColumn = new_column;
					data = new_column;
				}
			}
		}

		/// <summary>
		/// Builds the parent container to place
		/// the controls on.
		/// </summary>
		/// <param name="origin"></param>
		private void BuildParent(Point origin)
		{
			int x_size, y_size;
			_container.Controls.Add(_parent);
			
			//calculate the size of the parent group...
			x_size = _container.Width;
			x_size -= (origin.X * 2);

			y_size = _container.Height;
			y_size -= (origin.Y + 30);

			_parent.Size = new Size(x_size, y_size);

			_parent.Anchor = (AnchorStyles.Left |
				AnchorStyles.Right |
				AnchorStyles.Top |
				AnchorStyles.Bottom);
		}

		/// <summary>
		/// Places the controls onto 
		/// cells in the grid based on
		/// size, sequence, and control type.
		/// </summary>
		private void PlaceControls()
		{
			int currentsequence = 0;
			Question question = null;
			Row row = null;
			Cell cell = null;
			Column column_data = null;

			//get the first question in the list
			question = GetNextQuestion( currentsequence );
			
			if( question != null )
			{
				//start the first row
				row = new Row( 0, _first_column );
				_rows.Add(row);
			}
			while( question != null )
			{
				/*retrieve the starting cell for the question
				* and set it to the drawing cell... */
				cell = GetStartingCell(question,row);
				cell.IsDrawingCell = true;

				/*reset the row to the row the cell belongs to 
				since it might be a different row... */
				row = _rows[cell.RowIndex] as Row;	
				
				/*retrieve the column data object from the array 
				and pass it to the question */
				column_data = ColumnFromIndex(cell.ColumnIndex);
				question.Column = column_data;
				
				/*Add the question reference to all cells
				it is contained in... */
				BuildQuestionRefs(question, cell);

				/*get the next question in the list */
				currentsequence = question.Sequence;
				question = GetNextQuestion( currentsequence );
			}
		}

		/// <summary>
		/// Gets the column at the given index
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		private Column ColumnFromIndex(int index)
		{
			Column data = _first_column;
			
			while(data != null)
			{
				if(data.Index == index)	
					break;

				data = data.NextColumn;
			}

			return data;
		}

		/// <summary>
		/// Adds a reference to the question to all the cells that 
		/// the question is contained in.
		/// </summary>
		/// <param name="question"></param>
		/// <param name="cell"></param>
		private void BuildQuestionRefs(Question question, Cell origin_cell)
		{
			int resolved_index = 0;

			for(int column_offset = 0; column_offset < question.RequiredColumns; column_offset++)
			{
				resolved_index = column_offset + origin_cell.ColumnIndex;
				AddQuestionToColumn(question,resolved_index,origin_cell);
			}
		}

		/// <summary>
		/// Adds the question reference to all rows in the column
		/// corresponding to the column offset that the question 
		/// belongs to.
		/// </summary>
		/// <param name="index_offset"></param>
		private void AddQuestionToColumn(Question question,int column_index, Cell origin_cell)
		{
			int resolved_row_index = 0;

			for(int row_offset = 0; row_offset < question.RequiredRows; row_offset++)
			{
				resolved_row_index = row_offset + origin_cell.RowIndex;
				AddQuestionToCell(question,resolved_row_index, column_index);	
			}
		}

		/// <summary>
		/// Adds the question reference to a specific cell
		/// based on the offset into the row collection.  
		/// If the row/cell hasn't been created yet, it is 
		/// created and then given the reference.
		/// </summary>
		/// <param name="index_offset"></param>
		private void AddQuestionToCell(Question question, int row_index, int column_index)
		{
			Row row;
			Cell cell;

			if( row_index + 1 > _rows.Count)
			{
				//we're past the end of the arraylist.  
				//we need to append a row.
				row = new Row(row_index+1,_first_column);
				_rows.Add(row);
			}
			else
			{
				//the row already exists.  Go get it.
				row = _rows[row_index] as Row;
			}

			cell = row.CellAtColumn(column_index);
			cell.Question=question;

		}

		/// <summary>
		/// Returns the "drawing cell" (topleftmost cell) 
		/// that can safely contain the question, based on
		/// the size (rows and columns) of the question,
		/// and the row to start looking in.
		/// </summary>
		/// <param name="question"></param>
		/// <param name="starting_candidate"></param>
		/// <returns></returns>
		private Cell GetStartingCell(Question question,Row starting_candidate)
		{
			/// the current row being checked
			/// to fit the question
			Row candidate_row = null;	

			///the topleftmost cell that will 
			///hold the question
			Cell starting_cell = null;

			/// the current column index 
			int column_index=0;
			bool fits = false;

			candidate_row = starting_candidate;
			
			while(starting_cell == null)
			{
				//try the candidate row
				fits = false;
				for(column_index = 0; column_index < _columns; column_index++)
				{
					
					fits = CheckStartingCell(question,column_index,candidate_row);
					if (fits)
					{
						starting_cell = candidate_row.CellAtColumn(column_index);
						break;
					}
				}		
				
				if(starting_cell != null)
					break;
				

				if(candidate_row.RowIndex + 1 < _rows.Count)
				{
					candidate_row = _rows[candidate_row.RowIndex+1] as Row;
				}
				else
				{
					Row new_row = new Row(
						candidate_row.RowIndex+1,
						_first_column);

					_rows.Add(new_row);
					starting_cell = new_row.CellAtColumn(0);
				}
			}

			return starting_cell;
		}

		
		/// <summary>
		/// Given a row and column index, checks the cell
		/// at that location to see if the question will fit.
		/// If the question requires multiple rows/columns, it
		/// checks them, too.  Returns 
		/// </summary>
		/// <param name="question"></param>
		/// <param name="starting_candidate"></param>
		/// <returns></returns>
		private bool CheckStartingCell(Question question,int column_index, Row candidate_row)
		{
			bool cell_ok = false;
			
			if(candidate_row.CanAddQuestionAtColumn(column_index,question))
			{
				//the control fits in this row.
				//if it fits the columns, we've 
				//found our starting cell.
				if(question.RequiredRows > 1)
					cell_ok = FitsSiblingRows(question,column_index, candidate_row);
				else
					cell_ok = true;
			}
			return cell_ok;
		}

		/// <summary>
		/// Checks the columns in the grid below
		/// the starting row to see if they can contain
		/// the question.
		/// </summary>
		/// <returns>bool</returns>
		private bool FitsSiblingRows(Question question, int column_index, Row candidate_row)
		{
			bool fits_rows = true;
			for(int j = 0; j < question.RequiredRows; j++)
			{
				//check the row
				if(candidate_row.RowIndex + 1 < _rows.Count)
				{
					candidate_row = _rows[candidate_row.RowIndex+1] as Row;
					if(!candidate_row.CanAddQuestionAtColumn(column_index,question))
					{
						fits_rows = false;
						break;
					}
				}
				else
				{
					//we need a new row for the question.
					//make a new row and return the first cell.
					candidate_row = new Row(candidate_row.RowIndex+1,_first_column);
					_rows.Add(candidate_row);
				}

			}

			return fits_rows;
		}
		
		/// <summary>
		/// Returns the next question in the 
		/// arraylist based on the sequence number of the previous
		/// question.  "0" will return the first question, and
		/// a null returned means we're at the end of the list.
		/// </summary>
		/// <param name="CurrentSequence">sequence number of the 
		///	previous control in the array.</param>
		/// <returns>IDynamicQustion</returns>
		private Question GetNextQuestion(int CurrentSequence)
		{
			//the candidate holds the question
			//that is the next in line sequence-wise
			//at any point in time moving through the
			//arraylist.
			Question candidate = null;

			foreach(Question question in _questions)
			{
				if (question.Sequence > CurrentSequence)
				{
					if(candidate == null || candidate.Sequence > question.Sequence)
						candidate = question;
				}
			}
			
			return candidate;
		}

		/// <summary>
		/// Generic Event handler for changes to questions' answers.  Bubbles
		/// the event up to the client.  That way, they can listen to one event
		/// on the area that really listens to events from all controls on the 
		/// area.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HandleQuestionChange(object sender, System.EventArgs e)
		{
			if( Changed != null )
				Changed( sender, null );
		}
	
		#endregion

		#region Properties

		/// <summary>
		/// Gets a collection of all the
		/// errors that have occured.
		/// </summary>
		public ArrayList Errors
		{
			get { return _errors; }
		}

		/// <summary>
		/// Returns the error provider for the area
		/// </summary>
		public ErrorProvider ErrorProvider
		{
			get { return _err_provider; }
		}

		/// <summary>
		/// Retrieves the last error 
		/// that occured.
		/// </summary>
		public Exception LastError
		{
			get { return _last_error; }
		}

		/// <summary>
		/// Gets or sets the origin of the 
		/// dynamic area on the parent control.
		/// </summary>
		public Point Origin
		{
			get { return _origin; }
			set 
			{
				_origin = value;
				_parent.Location = _origin;
				_parent.Height = (_container.Height - _origin.Y) - 5;
			}
		}

		/// <summary>
		/// Gets the number of columns in the
		/// dynamic area.
		/// </summary>
		public int Columns
		{
			get { return _columns; }
			set { _columns = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether or not
		/// the controls on the dynamic area are locked.
		/// </summary>
		public bool Enabled
		{
			get { return _enabled; }
			set 
			{
				_enabled = value; 

				foreach(Question q in _questions)
					q.Enabled = _enabled;
			}
		}

		/// <summary>
		/// Gets or sets the font to be shown
		/// on the area and all controls within 
		/// the area.
		/// </summary>
		public System.Drawing.Font Font
		{
			get { return _font; }
			set { _font = value; }
		}

		/// <summary>
		/// Gets or sets a value determining wheter 
		/// or not the area can be resized to a point
		/// where controls would not be able to be 
		/// seen
		/// </summary>
		public bool AllowSmallResize
		{
			get { return _allow_small_resize; }
			set
			{
				Form form = _container as Form;

				_allow_small_resize = value;
				if(_allow_small_resize)
				{
					if(form != null)
						form.MinimumSize = _orig_minsize;
				}
				else
				{
					//calculate the smallest size that
					//the form would be allowed to be.
					//if the smallest size is smaller than
					//the current minimum size, we won't 
					//do anything.
					int x_size, y_size, grid_height;
					Column column = _first_column;
					
					//start with the height of a row itself...
					grid_height = GridSizes.GridHeight;

					//factor in top and bottom grid buffers
					grid_height += ROWBUFFER;
					
					y_size = _parent.Top;
					y_size += _rows.Count * grid_height;
					y_size += TOPGRIDBUFFER;
					y_size += BOTTOMGRIDBUFFER;
					
					x_size = _parent.Left;
					while(column != null)
					{
						x_size += (column.Width + COLUMNBUFFER);
						column = column.NextColumn;
					}

					//add the left buffer back in so we have 
					//same amt. of space on both sides...
					x_size += _parent.Left;

					if(_orig_minsize.Width > x_size)
						x_size = _orig_minsize.Width;

					if(_orig_minsize.Height > y_size)
						y_size = _orig_minsize.Height;

					if(form != null)
						form.MinimumSize = new Size(x_size,y_size);

				}
			}
		}

		#endregion

		#region Protected Properties
		protected ArrayList Questions
		{
			get { return _questions; }
		}

		protected Control Parent
		{
			get { return _parent; }
		}

		protected Column FirstColumn
		{
			get { return _first_column; }
		}
		#endregion
	}
}
