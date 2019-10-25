using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Afni.ControlPlacer.Lookups;
using Afni.ControlPlacer.Answers;

namespace Afni.ControlPlacer.Questions
{
	/// <summary>
	/// A dynamic radio group question.  Performs 
	/// the same as a combo box, but all possible 
	/// values are listed as radio buttons.
	/// </summary>
	public class RadioQuestion : Question, ILookupQuestion
	{
		private int _child_maxlength = 0;
		private ArrayList _lookup_data = null;
		private GroupBox _radio_parent;
		
		public RadioQuestion(long questionID, int sequence, string text, Control parent)
			:base(questionID, sequence, text, parent)
		{
			this.Answer = new Answers.LookupAnswer();
			Answer.QuestionID = QuestionID;
		}

		/// <summary>
		/// Draws the Radio Button Group 
		/// Question on the Area.
		/// </summary>
		/// <param name="origin"></param>
		public override bool Draw(Point origin)
		{	
			int vert_pos = 20;

			//radio group box
			_radio_parent = new GroupBox();
			_radio_parent.Location = origin;
			_radio_parent.Text = Text;
			_radio_parent.Parent = Parent;
			_radio_parent.Visible = true;
			_radio_parent.Show();

			//child option buttons
			//one for each possible answer
			foreach(RadioLookup data in _lookup_data)
			{
				data.Radio.Parent = _radio_parent;
				data.Radio.Visible = true;
				data.Radio.Location = new Point(10,vert_pos);
				data.Radio.Show();
				if (data.Radio.Width > Width)
				{
					Width = data.Radio.Width;
				}
				vert_pos += 20;
			}

			Height = vert_pos;

			_radio_parent.Size = new Size(Width + 20,Height + 20);
			return true;
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

			foreach(RadioLookup lookup in _lookup_data)
			{
				lookup.Selected = lookup.Radio.Checked;		
				answers.Add(lookup);
			}
		}

		/// <summary>
		/// Validates the answer to the question
		/// based on whether or not the question is 
		/// required.
		/// </summary>
		/// <returns></returns>
		public override bool Validate()
		{
			if(AnswerRequired)
			{
				foreach(RadioLookup data in _lookup_data)
				{
					data.Selected = data.Radio.Checked;
				}
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// Reverts the answer to the question back to 
		/// the last saved answer.
		/// </summary>
		public override void Undo()
		{
			ArrayList answers;
			RadioLookup lookup;

			answers = ((LookupAnswer)this.Answer).AnswerData;

			if(answers.Count > 0)
			{
				lookup = answers[0] as RadioLookup;
				lookup.Radio.Checked = true;
			}
			else
			{
				foreach(RadioLookup rl in _lookup_data)
				{
					rl.Radio.Checked = false;
				}
			}
		}

		protected override void RefreshProperties()
		{

			RequiredColumns= 1;
			RequiredRows= Height / GridSizes.GridHeight;
				
			if((Height %  GridSizes.GridHeight) != 0)
				RequiredRows++;



			foreach(RadioLookup lookup in _lookup_data)
			{
				lookup.Radio.Font = Font;
				lookup.Radio.Enabled = Enabled;
			}



			if( _radio_parent != null )
			{
				_radio_parent.Font = Font;
				_radio_parent.Enabled = Enabled;
			}
			
		}

		public void AddQuestionData(long AnswerID, string Text)
		{
			Graphics g = Graphics.FromHwnd(Parent.Handle);
			RadioLookup data = null;
			int char_width = 0;
			int buffered_width = 0;

			if(_lookup_data == null)
				_lookup_data = new ArrayList();

			data = new RadioLookup(AnswerID,Text);
			data.Radio.Click += new System.EventHandler(this.OnRadioClick);
			_lookup_data.Add(data);
			
			Height += data.Radio.Height;

			char_width = g.MeasureString(Text, Parent.Font).ToSize().Width;

			data.Radio.Width = char_width + 20;

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

		private void OnRadioClick(object sender, System.EventArgs e)
		{
			base.OnChanged();
		}
	}
}
