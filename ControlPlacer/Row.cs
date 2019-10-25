using System;
using System.Collections;
using Afni.ControlPlacer.Questions;

namespace Afni.ControlPlacer
{
	/// <summary>
	/// Represents a ControlPlacer Row 
	/// object within a grid.
	/// </summary>
	public class Row
	{
		#region Member Variables
		private int _index = 0;
		private int _lastColumn = 0;
		private ArrayList _cells = null;
		private Column _firstColumn = null;
		#endregion

		#region Constructors
		public Row(int rowIndex, Column firstColumn)
		{
			_firstColumn = firstColumn;
			_index = rowIndex;
			BuildColumns();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets a reference to the 
		/// first column in the row.
		/// </summary>
		public Column FirstColumn
		{
			get { return _firstColumn; }
			set { _firstColumn = value; }
		}

		/// <summary>
		/// Gets the index of the row in the Area.  
		/// The index is 0 based.
		/// </summary>
		public int RowIndex
		{
			get { return _index; }
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Returns the cell with the specified column index for the row.
		/// </summary>
		/// <param name="ColumnIndex"></param>
		/// <returns></returns>
		public Cell CellAtColumn(int columnIndex)
		{
			Cell cell = _cells[columnIndex] as Cell;
			return cell;
		}

		/// <summary>
		/// Returns the index of the leftmost cell that can
		/// hold the specified question in the row.
		/// </summary>
		/// <param name="question"></param>
		/// <returns></returns>
		public int GetStartingCellForQuestion(Question question)
		{
			int startindex = -1;
			int index = 0;
			
			while((index < _cells.Count) && (startindex < 0))
			{
				if( CanAddQuestionAtColumn( index,question ) )
					startindex = index;
				else
					index++;
			}
			
			return startindex;
		}

		/// <summary>
		/// Returns TRUE if the column with the specified index can
		/// contain the specified question.  
		/// </summary>
		/// <param name="columnindex"></param>
		/// <param name="question"></param>
		/// <returns></returns>
		public bool CanAddQuestionAtColumn(int columnindex, Question question)
		{
			Cell cell = null;

			for( int i = 0; i < question.RequiredColumns; i++ )
			{
				cell = _cells[i+columnindex] as Cell;
				if(cell.Question != null)
					return false;
			}
			return true;
		}

		/// <summary>
		/// Draws the row onto the Area.
		/// </summary>
		public void Draw()
		{
			foreach(Cell cell in _cells)
			{
				cell.Draw();
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Creates the columns for the row, based 
		/// on the number of columns the row is to 
		/// contain.
		/// </summary>
		/// <param name="columns"></param>
		private void BuildColumns()
		{
			if (_cells == null)
				_cells = new ArrayList();

			Column column = _firstColumn;

			while(column != null)
			{
				AppendColumn();
				column = column.NextColumn;
			}

		}

		/// <summary>
		/// Appends a column to the end of the row.
		/// </summary>
		private void AppendColumn()
		{
			Column column = _firstColumn;

			Cell newcell = new Cell(_index,_lastColumn);
			_cells.Add(newcell);

			while(column != null)
			{
				if(column.Index == _lastColumn)
					break;
				column = column.NextColumn;
			}

			newcell.Column = column;
			_lastColumn++;
		}
		#endregion

	}
}
