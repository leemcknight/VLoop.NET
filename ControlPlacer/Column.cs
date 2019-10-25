using System;
using System.Windows.Forms;
using Afni.ControlPlacer.Questions;

namespace Afni.ControlPlacer
{

	/// <summary>
	/// Represents a single column in the area.
	/// </summary>
	public class Column
	{
		private int _index = 0;
		private int _labelWidth = 0;
		private int _controlWidth = 0;
		private int _width = 0;
		private int _tolerance = 0;
		private int _startX = 0;
		private int _startY = 0;
		private Column _nextColumn = null;
		protected const int COLUMNBUFFER = 20;

		public Column()
		{
			
		}

		public Column(int Index)
		{
			_index = Index;
		}

		/// <summary>
		/// Gets or sets the index of the Column
		/// </summary>
		public int Index
		{
			get { return _index; }
			set { _index = value; }
		}

		/// <summary>
		/// Gets or sets the width of the
		/// widest label in the column.
		/// </summary>
		public int LabelWidth
		{
			get { return _labelWidth; }
			set 
			{ 
				int x;
				_labelWidth = value; 

				x = _labelWidth + _controlWidth;
				if(x > _width)
				{
					_width = x;
					MoveNextColumn();
				}
			}
		}

		/// <summary>
		/// Gets or sets the width of the
		/// widest control in the column that
		/// requires a label  (i.e. textbox,
		/// combobox, multilinetext, etc...)
		/// </summary>
		public int ControlWidth
		{
			get { return _controlWidth; }
			set 
			{
				int x;
				_controlWidth = value; 
				x = _labelWidth + _controlWidth;

				if (x > _width)
				{
					_width = x;
					MoveNextColumn();
				}
				
			}
		}


		/// <summary>
		/// Gets or sets the columns neighbor
		/// to the right.
		/// </summary>
		public Column NextColumn
		{
			get { return _nextColumn; }
			set 
			{
				_nextColumn = value; 
			}
		}

		/// <summary>
		/// Gets or sets the width of the column.
		/// </summary>
		public int Width
		{
			get { return _width; }
			set 
			{ 
				_width = value; 
				MoveNextColumn();
			}
		}

		/// <summary>
		/// Moves the starting X position of the 
		/// next column to the right, based on the 
		/// position and width of the current column.
		/// </summary>
		private void MoveNextColumn()
		{
			int next_start = 0;
			if(_nextColumn != null)
			{
				next_start = _startX + _width;
				next_start += COLUMNBUFFER;
				_nextColumn.StartingX = next_start;
			}
		}

		/// <summary>
		/// Gets or sets the starting X Position 
		/// of the column.  Automatically resizes 
		/// the other columns as needed.
		/// </summary>
		public int StartingX
		{
			get { return _startX; }
			set 
			{
				_startX = value; 
				MoveNextColumn();
			}
		}

		/// <summary>
		/// Gets or sets the starting Y position 
		/// of the column.
		/// </summary>
		public int StartingY
		{
			get { return _startY; }
			set { _startY = value; }
		}

		/// <summary>
		/// Gets or sets the "tolerance" of the column
		/// width.  This allows the width of the column
		/// to grow by the specified amount before requiring
		/// a control to require multiple columns.
		/// </summary>
		public int Tolerance
		{
			get { return _tolerance; }
			set { _tolerance = value; }
		}

	}
}
