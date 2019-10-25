using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Afni.ControlPlacer.Lookups;
using Afni.ControlPlacer.Answers;

namespace Afni.ControlPlacer.Questions
{
	/// <summary>
	/// A dynamic combo question.  Holds possible answers
	/// in a combo box.
	/// </summary>
	public class ComboQuestion : Question ,ILookupQuestion
	{
		private const int COMBOBOXHEIGHT = 21;
		private const int COMBOBOXBUTTONWIDTH = 20;
		private ComboBox _control = null;
		private ArrayList _lookup_data = null;
		private Label _label = null;
		private int _child_maxlength = 0;
		private System.Exception _last_error;
		
		public event EventHandler Changed;

		public ComboQuestion(long questionID, int sequence, string text, Control parent)
			:base(questionID, sequence,text,parent)
		{
			Graphics g = null;
			_lookup_data = new ArrayList();
			base.Answer = new Answers.LookupAnswer();
			base.Answer.QuestionID=QuestionID;

			g =  Graphics.FromHwnd(Parent.Handle);
			Width = g.MeasureString(Text,Parent.Font).ToSize().Width + 3;
		}

	
		/// <summary>
		/// Draws the combobox question on the 
		/// screen at the desired origin
		/// </summary>
		/// <param name="origin"></param>
		public override bool Draw(Point origin)
		{
			bool draw_ok = true;;

			try
			{
				DrawLabel(origin);
				BuildCombo(origin);
			
				//add an empty item if the 
				//answer is not required...
				if(! AnswerRequired)
					_control.Items.Add(new LookupData(0,""));

				foreach(LookupData data in _lookup_data)
				{
					_control.Items.Add(data);
				}

				RefreshProperties();
				
			}
			catch(System.Exception ex)
			{
				_last_error = ex;
				draw_ok = false;
			}

			return draw_ok;
		}
		
		/// <summary>
		/// Saves the current selection of the combobox into
		/// a new answer object.
		/// </summary>
		public override void RegisterSave()
		{
			ArrayList ans;

			LookupData selectedData = (LookupData)_control.SelectedItem;
			selectedData.Selected = true;

			ans = ((LookupAnswer)Answer).AnswerData;
			if (ans.Count > 0)
				ans[0] = selectedData;
			else
				ans.Add(selectedData);
		}

		/// <summary>
		/// Creates an empty combo box at the specified 
		/// vertical position
		/// </summary>
		/// <param name="vert_pos"></param>
		private void BuildCombo(Point origin)
		{
			int vert_pos = origin.Y;
			Point control_origin;

			_control = new ComboBox();
			_control.DropDownStyle = ComboBoxStyle.DropDownList;
			_control.Parent = Parent;
			control_origin = new Point(Column.LabelWidth + 3 + origin.X,vert_pos);
			_control.Location = control_origin;
			_control.Size = new Size(Column.ControlWidth, COMBOBOXHEIGHT);
			_control.SelectedIndexChanged += new System.EventHandler(this.ComboChanged);
			_control.Sorted = true;		//AFNI Standard
			_control.Visible = true;
			_control.Show();
		}


		/// <summary>
		/// Changes the value in the combo box back 
		/// to the last saved value.
		/// </summary>
		public override void Undo()
		{
			LookupAnswer ans;
			ans = (LookupAnswer)Answer;

			_control.SelectedItem = ans.AnswerData[0];
		}

		/// <summary>
		/// Validates the answer to the question.
		/// </summary>
		/// <returns></returns>
		public override bool Validate()
		{
			if(AnswerRequired)
			{
				return ( _control.SelectedItem != null );
			}
			else
				return true;
		}

		/// <summary>
		/// Draws the label for the combo box
		/// </summary>
		/// <param name="origin"></param>
		private void DrawLabel(Point origin)
		{
			_label = new Label();
			_label.Parent = Parent;
			
			_label.Text = Text;
			if(!Text.EndsWith(":"))
			{
				_label.Text += ":";
			}
			_label.AutoSize = true;

			_label.Visible = true;
			_label.Location = origin;
			_label.Show();
		}

		protected override void RefreshProperties()
		{
			Graphics g = Graphics.FromHwnd(Parent.Handle);

			int label_width = 0;
			label_width =g.MeasureString(Text,Parent.Font).ToSize().Width+3;

			if(Column != null)
			{
				//update the column data reference 
				//properties, if need be...
				if(label_width > Column.LabelWidth)
					Column.LabelWidth = label_width;

				if(Column.ControlWidth < _child_maxlength + COMBOBOXBUTTONWIDTH)
					Column.ControlWidth = _child_maxlength + COMBOBOXBUTTONWIDTH;

				if(Column.Width < Width)
					Column.Width = Width;
			}

			if(_control != null)
			{
				_control.Font = Font;
				_control.Enabled = Enabled;
			}
				
			if(_label != null)
			{
				_label.Enabled = Enabled;
				_label.Font = Font;
			}

		}

		/// <summary>
		/// Adds a possible answer to the combo box.
		/// </summary>
		/// <param name="AnswerID"></param>
		/// <param name="Text"></param>
		public void AddQuestionData(long AnswerID, string Text)
		{
			Graphics g = System.Drawing.Graphics.FromHwnd(Parent.Handle);
			int char_width = 0;

			if(_lookup_data == null)
				_lookup_data = new ArrayList();

			_lookup_data.Add(new LookupData(AnswerID,Text));

			char_width = g.MeasureString(Text,Parent.Font).ToSize().Width;

			if(char_width > _child_maxlength)
			{
				_child_maxlength = char_width;
				Width = g.MeasureString(Text,Parent.Font).ToSize().Width + 3;
				Width += (char_width + COMBOBOXBUTTONWIDTH);
			}

		}

		public int MaxLookupDataLength
		{
			get { return _child_maxlength; }
		}

		/// <summary>
		/// gets a list of the lookup data associated with the 
		/// combobox question.  The lookup data will end up being
		/// individual items in the combobox.
		/// </summary>
		public ArrayList Lookups
		{
			get { return _lookup_data; }
		}

		private void ComboChanged(object sender, System.EventArgs e)
		{
			base.OnChanged();
		}
	}

}
