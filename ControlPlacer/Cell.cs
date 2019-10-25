using System;
using Afni.ControlPlacer.Questions;

namespace Afni.ControlPlacer
{
	/// <summary>
	/// Represents an individual cell within the dynamic 
	/// control grid.  A cell contains a reference
	/// to a question.
	/// </summary>
	public class Cell
	{
		protected const int TOPGRIDBUFFER = 20;
		protected const int ROWBUFFER = 5;
		private Question _question = null;
		private bool _drawingcell = false;
		private int _rowindex = 0;
		private int _columnindex = 0;
		private Column _column = null;
		
		public Cell(int RowIndex,int ColumnIndex)
		{
			_columnindex = ColumnIndex;
			_rowindex = RowIndex;
		}

		/// <summary>
		/// Gets the index of the row that the
		/// cell belongs to
		/// </summary>
		public int RowIndex
		{
			get{ return _rowindex; }
		}

		/// <summary>
		/// Gets the index of the column that the
		/// cell belongs to
		/// </summary>
		public int ColumnIndex
		{
			get{ return _columnindex; }
		}

		/// <summary>
		/// Gets or sets a value determining 
		/// if this is the upperleftmost cell
		/// of a question, and therefore, the
		/// drawing cell.
		/// </summary>
		public bool IsDrawingCell
		{
			get { return _drawingcell; }
			set { _drawingcell = value; }
		}

		/// <summary>
		/// Gets or sets a reference to a 
		/// question at the cell
		/// </summary>
		public Question Question
		{
			get { return _question; }
			set { _question = value; }
		}

		/// <summary>
		/// Gets or sets the column that the
		/// cell belongs to
		/// </summary>
		public Column Column
		{
			get { return _column; }
			set { _column = value; }
		}

		/// <summary>
		/// Draws the cell onto the grid.
		/// </summary>
		public void Draw()
		{
			if (_drawingcell)
			{
				int x = 0;
				int y = 0;
				System.Drawing.Point origin;

				x = _column.StartingX;

				y = TOPGRIDBUFFER + _column.StartingY;
				y += (_rowindex) * ROWBUFFER;
				y += (_rowindex) * GridSizes.GridHeight;

				origin = new System.Drawing.Point(x,y);
				_question.Draw(origin);
			}
		}
	}
}
