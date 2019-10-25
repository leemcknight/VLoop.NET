using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Xml;
using Afni.ControlPlacer.Answers;

namespace Afni.ControlPlacer.Questions
{
	/// <summary>
	/// A Dynamic Check Box Question.  Used for yes/no 
	/// or true/false type of answers.  
	/// </summary>
	public class CheckBoxQuestion : Question
	{
		CheckBox _control = null;	
		public event EventHandler Changed;
		
		public CheckBoxQuestion(int questionID, int sequence, string text, Control parent)
			:base(questionID,sequence,text,parent)
		{
			Graphics g = null;
			base.Answer = new TextAnswer();
			base.Answer.QuestionID = QuestionID;
			g = Graphics.FromHwnd(Parent.Handle);
			Width = g.MeasureString(Text, Parent.Font).ToSize().Width + 20;
		}

		/// <summary>
		/// Draws the checkbox question on the form
		/// </summary>
		/// <param name="origin"></param>
		public override bool Draw(Point origin)
		{
			//create the control...
			if(_control == null)
			{
				_control = new CheckBox();
				_control.Parent = Parent;
			}
			_control.Text = Text;
			_control.Location = origin;
			_control.Width = Width;
			_control.Visible = true;
			_control.CheckedChanged += new System.EventHandler(this.OnCheckedChanged);
			_control.Show();
			return true;
		}

		/// <summary>
		/// Resets the "checked" status of the checkbox back to the 
		/// last answer that was saved.
		/// </summary>
		public override void Undo()
		{
			_control.Checked = Convert.ToBoolean(((TextAnswer)this.Answer).Text);
		}

		/// <summary>
		/// Tells the question to treat current data as 
		/// most recent "commited" data
		/// </summary>
		public override void RegisterSave()
		{
			((TextAnswer)this.Answer).Text= _control.Checked.ToString();
		}

		/// <summary>
		/// Validates the CheckBox control.  Only returns true if the 
		/// control is checked.  
		/// </summary>
		/// <returns></returns>
		public override bool Validate()
		{
			if(AnswerRequired)
			{
				return _control.Checked;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// Gets or sets the answer to the checkbox question.
		/// </summary>
		public override Answer Answer
		{
			get { return base.Answer; }
			set
			{
				base.Answer = value; 
				if(Answer != null)
				{
					_control.Checked = (((TextAnswer)base.Answer).Text.ToUpper() == "YES");
				}
				else
				{
					_control.Checked = false;
				}
			}
		}

		protected override void RefreshProperties()
		{
			if(Column.Width < Width)
				Column.Width	= Width;

			if(_control != null)
			{
				_control.Enabled = Enabled;
				_control.Font = Font;
				_control.Text = Text;
				_control.Width = Width;
				_control.Visible = true;
			}

		}

		private void OnCheckedChanged(object sender, System.EventArgs e)
		{
			if(Changed != null)
			{
				Changed(this, null);
			}
		}
	}

}
