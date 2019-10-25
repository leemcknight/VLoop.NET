using System;
using System.Windows.Forms;
using System.Collections;
using Afni.ControlPlacer;
using Afni.ControlPlacer.Answers;
using Afni.ControlPlacer.Questions;

namespace Afni.ControlPlacer
{
	public class SelectedRecordChangedArgs : EventArgs
	{
		private Record _record;

		public Record Record
		{
			get { return _record; }
			set { _record = value; }
		}
	}

	public delegate void SelectedRecordChangedHandler(object sender, SelectedRecordChangedArgs e);

	/// <summary>
	/// Interface to a "view" of records.  Records
	///  can be shown in many different views, such as
	/// listviews, list boxes, and back:next buttons.
	/// </summary>
	public interface IRecordView
	{
		void DrawView();
		Control Parent { get; set; }
		RecordCollection Records { get; }
		Record ActiveRecord { get; }
		RecordEditModes EditMode { get; set; }
	}


	/// <summary>
	/// Base class for all Grouped Answer Views.  This class 
	/// cannot be directly created.
	/// </summary>
	public abstract class RecordView : IRecordView
	{
		private Control _parent;
		private RecordCollection _records;
		private ArrayList _questions;
		private Record _activeRecord;
		private RecordEditModes _editMode;

		public event SelectedRecordChangedHandler SelectedRecordChanged;

		public RecordView(Control parent, ArrayList questions)
		{
			_parent = parent;
			_questions = questions;
			_records = new RecordCollection();
			_records.RecordAdded += new System.EventHandler(this.RecordsChanged);
			_records.RecordRemoved += new System.EventHandler(this.RecordsChanged);
		}

		public virtual void DrawView()
		{
		}
		
		public Control Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		public RecordCollection Records
		{
			get { return _records; }
		}

		/// <summary>
		/// Gets the active group in the grouped answer view.
		/// </summary>
		public Record ActiveRecord
		{
			get { return _activeRecord; }
			set { _activeRecord = value; }
		}

		protected virtual void Refresh()
		{
		}

		/// <summary>
		/// The questions associated with the grouped answer view.
		/// </summary>
		protected ArrayList Questions
		{
			get { return _questions; }
		}

		private void RecordsChanged(object sender, System.EventArgs e)
		{
			Refresh();
		}

		/// <summary>
		/// Edit mode for the view.  This should be overridden in the sub classes
		/// to implement specific functionality when the edit mode changes.
		/// </summary>
		public virtual RecordEditModes EditMode
		{
			get { return _editMode; }
			set { _editMode = value; }
		}

		protected virtual void OnSelectedRecordChanged()
		{
			SelectedRecordChangedArgs args;

			if(SelectedRecordChanged != null)
			{
				args = new SelectedRecordChangedArgs();
				args.Record = _activeRecord;
				SelectedRecordChanged(this, args);
			}
		}

	}

	/// <summary>
	/// Summary description for GroupedAnswerViews.
	/// </summary>
	public class ListViewRecordView : RecordView
	{
		private ListView _lvw;
		
		public ListViewRecordView(Control parent, ArrayList questions) 
			: base(parent, questions)
		{
			
		}

		public override void DrawView()
		{
			_lvw = new ListView();
			_lvw.Parent = this.Parent;
			_lvw.Location = new System.Drawing.Point( 10,10 );
			_lvw.Size = new System.Drawing.Size(5, Parent.Height / 2 );
			_lvw.Dock = DockStyle.Top;
			_lvw.SelectedIndexChanged += new System.EventHandler(this.OnSelectedItemChanged);
			_lvw.Visible = true;
			_lvw.AllowColumnReorder = false;
			_lvw.FullRowSelect = true;
			_lvw.View = View.Details;

			_lvw.Columns.Add("RecordID",0, HorizontalAlignment.Left);
			foreach ( Question question in Questions )
			{
				_lvw.Columns.Add(question.Text, 100, HorizontalAlignment.Left );
			}

			_lvw.Show();

			Refresh();
		}

		public override RecordEditModes EditMode
		{
			get { return base.EditMode; }
			set
			{
				base.EditMode = value;
				if(base.EditMode == RecordEditModes.View)
				{
					_lvw.Enabled = true;
				}
				else
				{
					_lvw.Enabled = false;
				}
			}
		}

		protected override void Refresh()
		{
			_lvw.Items.Clear();
			ListViewItem item;

			foreach(Record record in Records)
			{
				item = new ListViewItem(record.RecordID.ToString());
				item.Tag = record;
				foreach(Answer answer in record.Answers)
				{
					item.SubItems.Add( answer.ToString() );
				}

				item.Tag = record;
				_lvw.Items.Add(item);		
			}
		}

		private void OnSelectedItemChanged(object sender, System.EventArgs e)
		{
			Record newRecord = null;
			if(_lvw.SelectedItems.Count > 0)
			{
				newRecord = (Record)_lvw.SelectedItems[0].Tag;
			}
			
			ActiveRecord = newRecord;

			base.OnSelectedRecordChanged();
		}
	}

	/// <summary>
	/// Provides a Record view with a list box displaying the records in the
	/// area.
	/// </summary>
	public class ListBoxRecordView : RecordView
	{
		private ListBox _lb;

		public ListBoxRecordView(Control parent, ArrayList questions) 
			: base(parent, questions)
		{
		}

		public override void DrawView()
		{
			_lb = new ListBox();
			_lb.Parent = this.Parent;
			_lb.Location = new System.Drawing.Point( 10,10 );
			_lb.Size = new System.Drawing.Size(5, Parent.Height / 2 );
			_lb.Visible = true;
			_lb.SelectedIndexChanged += new System.EventHandler(this.OnSelectedItemChanged);
			_lb.Dock = DockStyle.Top;
			_lb.Show();

			Refresh();
		}

		public override RecordEditModes EditMode
		{
			get { return base.EditMode; }
			set
			{
				base.EditMode = value;
				if(base.EditMode == RecordEditModes.View)
				{
					_lb.Enabled = true;
				}
				else
				{
					_lb.Enabled = false;
				}
			}
		}

		protected override void Refresh()
		{
			_lb.Items.Clear();

			foreach(Record record in Records)
			{
				_lb.Items.Add(record);
			}
		}

		private void OnSelectedItemChanged(object sender, System.EventArgs e)
		{
			Record newRecord = null;
			if(_lb.SelectedItems.Count > 0)
			{
				newRecord = (Record)_lb.SelectedItem;
			}
			
			ActiveRecord = newRecord;

			base.OnSelectedRecordChanged();
		}
	}
//
//	public class NavButtonGroupedAnswerView : IGroupedAnswerView
//	{
//		private Button _btnNext;
//		private Button _btnPrev;
//
//		public NavButtonGroupedAnswerView()
//		{
//		}
//	}

	/// <summary>
	/// Strongly typed collection holding Dynamic Answer groups.
	/// </summary>
	public class RecordCollection : System.Collections.CollectionBase
	{
		public event EventHandler RecordAdded;
		public event EventHandler RecordRemoved;

		public int Add(Afni.ControlPlacer.Answers.Record record)
		{
			int listIndex;

			listIndex = List.Add(record);
			if(RecordAdded != null)
			{
				RecordAdded(record, null);
			}

			return listIndex;
		}

		public Afni.ControlPlacer.Answers.Record this[int index]
		{
			get { return (Afni.ControlPlacer.Answers.Record)List[index]; }
		}

		public void Remove(Afni.ControlPlacer.Answers.Record record)
		{
			List.Remove(record);
			if(RecordRemoved != null)
			{
				RecordRemoved(record, null);
			}
		}
	}
}
