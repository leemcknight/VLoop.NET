using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Afni.ControlPlacer.Lookups;
using Afni.ControlPlacer.Answers;

namespace Afni.ControlPlacer.Questions
{
	/// <summary>
	/// A dynamic Multi-select question.  This holds
	/// a group box of checkboxes related to the question.
	/// (i.e. Pizza Toppings)
	/// </summary>
	public class MultiSelectQuestion : Question, ILookupQuestion
	{
		private int _child_maxlength = 0;
		private GroupBox _group = null;
		private ArrayList _lookup_data = null;
		private System.Exception _last_error;

		public event EventHandler Changed;

		public MultiSelectQuestion(long questionID, int sequence, string text, Control parent)
			:base(questionID,sequence,text,parent)
		{
			_group = new GroupBox();
			_group.Text = Text;
			_lookup_data = new ArrayList();
			this.Answer = new Answers.LookupAnswer();
			this.Answer.QuestionID = QuestionID;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			Graphics g;
			g = Graphics.FromHwnd(Parent.Handle);
			Width = g.MeasureString(Text, Parent.Font).ToSize().Width;
			Width += GroupBoxBuffers.LeftGroupBoxBuffer;
			Width+= GroupBoxBuffers.RightGroupBoxBuffer;
		}

		/// <summary>
		/// Draws the Group box for the multi-select checkboxes
		/// and the checkboxes themselves at the specified position.
		/// </summary>
		/// <param name="origin"></param>
		public override bool Draw(Point origin)
		{
			bool draw_ok = true;
			int vert_pos = GroupBoxBuffers.TopGroupBoxBuffer;
			int inner_width = 0;

			try
			{
				DrawGroup(origin);
			
				foreach(MultiListLookup _data in _lookup_data)
				{
					_data.ListCheckBox.Parent = _group;
					_data.ListCheckBox.Location = new Point(10,vert_pos);
					_data.ListCheckBox.Visible = true;
					_data.ListCheckBox.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
					_data.ListCheckBox.Show();
					vert_pos += 20;
				}

				Height = vert_pos + GroupBoxBuffers.BottomGroupBoxBuffer;
			
				inner_width = _child_maxlength + GroupBoxBuffers.LeftGroupBoxBuffer;
				inner_width += GroupBoxBuffers.RightGroupBoxBuffer;

				if(Width < inner_width)
				{
					Width = inner_width;
				}

				_group.Size = new Size( Width, Height );
			}
			catch(System.Exception ex)
			{
				_last_error = ex;
				draw_ok = false;
			}

			return draw_ok;
		}

		public override void RegisterSave()
		{
			ArrayList answers;

			if(Answer == null)
			{
				base.Answer = new LookupAnswer();
				Answer.QuestionID = QuestionID;
			}

			answers = ((LookupAnswer)this.Answer).AnswerData;
			answers.Clear();

			foreach(MultiListLookup lookup in _lookup_data)
			{
				lookup.Selected = lookup.ListCheckBox.Checked;
				answers.Add(lookup);
			}
		}

	
		/// <summary>
		/// Validates the answer(s) to the question.
		/// </summary>
		/// <returns></returns>
		public override bool Validate()
		{
			bool is_valid = !AnswerRequired;

			if(AnswerRequired)
			{
				foreach(MultiListLookup data in _lookup_data)
				{
					if(data.ListCheckBox.Checked)
					{
						is_valid = true;
						break;
					}
				}	
			}

			return is_valid;
		}

		void DrawGroup(Point origin)
		{
			int width = 0;
			Graphics g = Graphics.FromHwnd(Parent.Handle);

			_group = new GroupBox();
			_group.Text = Text;
			_group.Location = origin;
			
			//set the width of the group
			width = g.MeasureString(Text,_group.Font).ToSize().Width;
			width += 15;
			if(width > Width)
				Width= width;
			_group.Width = Width;

			//show the group
			_group.Parent = Parent;
			_group.Visible = true;
			_group.Show();
		}

		public override void Undo()
		{
			ArrayList answers;

			answers = ((LookupAnswer)this.Answer).AnswerData;

			foreach(MultiListLookup lookup in _lookup_data)
				lookup.ListCheckBox.Checked = false;

			foreach(MultiListLookup lookup in answers)
				lookup.ListCheckBox.Checked = true;
		}

		protected override void RefreshProperties()
		{

			if(Column != null && Column.Width < Width)
				Column.Width = Width;

			RequiredColumns = 1;
			RequiredRows = Height / GridSizes.GridHeight;
				
			if(( Height % GridSizes.GridHeight ) != 0)
				RequiredRows++;

			_group.Font = Font;
			_group.Enabled = Enabled;

			foreach(MultiListLookup lookup in _lookup_data)
			{
				lookup.ListCheckBox.Font = Font;
				lookup.ListCheckBox.Enabled = Enabled;
			}
		}

		void ILookupQuestion.AddQuestionData(long AnswerID, string Text)
		{
			Graphics g = Graphics.FromHwnd(Parent.Handle);
			MultiListLookup data = null;
			int char_width = 0;
			int buffered_width = 0;

			if(_lookup_data == null)
				_lookup_data = new ArrayList();
			
			data = new MultiListLookup(AnswerID, Text);
			_lookup_data.Add(data);

			Height += data.ListCheckBox.Height;

			char_width = g.MeasureString(Text, Parent.Font).ToSize().Width;

			data.ListCheckBox.Width = char_width + 20;

			if(char_width > _child_maxlength)
				_child_maxlength = char_width;

			buffered_width = char_width + GroupBoxBuffers.LeftGroupBoxBuffer;
			buffered_width += GroupBoxBuffers.RightGroupBoxBuffer;

			if(Width < buffered_width)
				Width = buffered_width;

			RefreshProperties();

		}

		public int MaxLookupDataLength
		{
			get { return _child_maxlength; }
		}

		public ArrayList Lookups
		{
			get { return _lookup_data; }
		}

		private void CheckBoxChanged(object sender, System.EventArgs e)
		{
			base.OnChanged();
		}
	}
}
