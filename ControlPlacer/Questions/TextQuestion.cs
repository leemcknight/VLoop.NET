using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Afni.ControlPlacer.Answers;
using System.Text.RegularExpressions;
using System.Text;

namespace Afni.ControlPlacer.Questions
{

	/// <summary>
	/// An interface used by controls that
	/// can use text masks (i.e. currency,
	/// numeric, dates).  
	/// </summary>
	public interface IMaskable
	{
		/// <summary>
		/// AllowableChars is useful if the 
		/// mask is only to filter out certain
		/// chars.
		/// </summary>
		string AllowableChars{get; set;}
		MaskTypes Mask { get; set; }
		int MaxLength {get; set;}
		int MinLength {get; set; }
	}

	/// <summary>
	/// A Dynamic Text Question.  Used for single line
	/// responses (i.e. "First Name").
	/// </summary>
	public class TextQuestion : Question , IMaskable
	{
		private const int TEXTBOXWIDTH = 120;
		public const int TEXTBOXHEIGHT = 21;
		protected Label _label;
		protected TextBox _control = null;
		protected string _allowable_chars = "";
		protected int _max_len = 0;
		protected int _min_len = 0;
		protected System.Exception _last_error;
		private MaskTypes _mask;

		public TextQuestion(int questionID, int sequence, string text, Control parent)
			:base(questionID,sequence,text,parent)
		{
			_mask = MaskTypes.None;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			Graphics g;
			g =  Graphics.FromHwnd(Parent.Handle);
			
			Width = g.MeasureString(Text,Parent.Font).ToSize().Width + 3;
			base.Answer = new TextAnswer();
			base.Answer.QuestionID = QuestionID;
		}

		/// <summary>
		/// Gets or sets the answer to a text question.
		/// </summary>
		public override Answer Answer
		{
			get {	return (Answer)base.Answer; 	}
			set 
			{
				base.Answer = value;
				if(base.Answer != null)
				{
					_control.Text = ((TextAnswer)this.Answer).Text;
				}
				else
				{
					_control.Text = "";
				}
			}
		}

		/// <summary>
		/// This draws a label for the text question.
		/// </summary>
		/// <param name="origin"></param>
		protected void DrawLabel(Point origin)
		{
			_label = new Label();
			_label.Parent = Parent;
			_label.Text = Text;
			if(!Text.EndsWith(":"))
				_label.Text += ":";
			_label.AutoSize = true;
			_label.Visible = true;
			_label.Location = origin;
			_label.Show();
		}

		/// <summary>
		/// Draws the text question (label and text box)
		/// at the specified location.
		/// </summary>
		/// <param name="origin"></param>
		public override bool Draw(Point origin)
		{
			bool draw_ok=true;
			int x_pos;
			Point control_origin;
			
			try
			{
				
				DrawLabel(origin);
			
				_control = new TextBox();
				_control.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
				_control.Leave += new System.EventHandler(this.OnLeave);
				_control.TextChanged += new System.EventHandler(this.OnTextChange);
				_control.Enter += new System.EventHandler(this.OnEnter);
				_control.Parent = Parent;
				ErrorProvider = new ErrorProvider();
			
				//calculate x position of text box, based on 
				//column data and origin passed in...
				x_pos = origin.X + Column.LabelWidth + 3;

				control_origin = new Point(x_pos,origin.Y);
				_control.MaxLength = _max_len;
				_control.Location = control_origin;
				_control.Size = new Size(Column.ControlWidth,TEXTBOXHEIGHT);
				_control.Visible = true;
				_control.Show();

				//AFNI STANDARD
				//All required fields start with the error provider
				//icon shown
				if(AnswerRequired)
				{
					ErrorProvider.SetError(_control, Text + " is a required field.");
				}
			}
			catch(System.Exception ex)
			{
				_last_error = ex;
				draw_ok = false;
			}

			return draw_ok;
		}

		/// <summary>
		/// Registers a save to the text questsion
		/// </summary>
		public override void RegisterSave()
		{
			if(Answer != null)
			{							  
				((TextAnswer)Answer).Text = _control.Text;
			}
			else
			{
				base.Answer = new TextAnswer();
				Answer.QuestionID = QuestionID;
				((TextAnswer)Answer).Text = _control.Text;
			}
		}

		/// <summary>
		/// Reverts the answer back to the last 
		/// saved answer
		/// </summary>
		public override void Undo()
		{
			if(Answer != null)
				_control.Text = ((TextAnswer)this.Answer).Text;
			else
				_control.Text = "";
		}

		/// <summary>
		/// Validates the text of the answer
		/// to any rules(i.e. maxlength, required, etc...)
		/// </summary>
		/// <returns></returns>
		public override bool Validate()
		{
			if(AnswerRequired)
			{
				if(_control.Text.Trim().Length == 0)
				{
					return false;
				}
			}
			return true;		
		}

	
		/// <summary>
		/// Refreshes the properties of all objects
		/// contained within the text question.
		/// </summary>
		protected override void RefreshProperties()
		{
			int x_size; 
			int label_width = 0;
			Graphics g = Graphics.FromHwnd(Parent.Handle);
		
			label_width =g.MeasureString(this.Text,this.Parent.Font).ToSize().Width+3;

			x_size = label_width + TEXTBOXWIDTH;

			if(Column != null)
			{
				if(label_width > Column.LabelWidth) 
					Column.LabelWidth = label_width;

				if(Column.ControlWidth < TEXTBOXWIDTH)
					Column.ControlWidth = TEXTBOXWIDTH;

				if(Column.Width < x_size)
					Column.Width = x_size;
			}

			if(_label != null)
			{
				_label.Font = Font;
			}

			if(_control != null)
			{
				_control.Font = Font;
				_control.BackColor = Enabled ? SystemColors.Window : SystemColors.Info;
			}
			
		}

		private void OnTextChange(object sender, System.EventArgs e)
		{
			if(!Validate())
			{
				if( ErrorProvider.GetError(_control) == "" )
				{
					if(Mask != MaskTypes.None )
					{
						ErrorProvider.SetError(_control, "Please enter a valid value for " + Text);
					}
					else
					{
						ErrorProvider.SetError(_control, Text + " is a required field.");
					}
				}
			}
			else
			{
				if( ErrorProvider.GetError(_control) != "" )
				{
					ErrorProvider.SetError(_control, "");
				}
			}
			base.OnChanged();
		}

		private void OnLeave(object sender, System.EventArgs e)
		{
			string text = _control.Text;
			if(_mask != MaskTypes.None)
			{
				Regex rexp = new Regex(MaskHelper.GetRegExStringForMask(_mask));
				if(!rexp.IsMatch(text))
					ErrorProvider.SetError( _control, "Invalid value for " + this.Text );
				else
					ErrorProvider.SetError( _control, "" );
			}
			else if(AnswerRequired && (_control.Text.Length > 0))
			{
				ErrorProvider.SetError(_control,"");
			}
			else if(AnswerRequired && (_control.Text.Length == 0))
			{
				ErrorProvider.SetError(_control, Text + " is a required field.");
			}

			//AFNI standard
			//If a field holds more information than can be displayed, 
			//make it show the beginning of the field when the user
			//tabs off.
			_control.SelectionStart = 0;
		}

		private void OnEnter(object sender, System.EventArgs e)
		{
			//AFNI standard: when a textbox receives focus, 
			//the text should be highlighted
			_control.SelectAll();
		}

		private void OnKeyPress(object sender, KeyPressEventArgs kpea)
		{
			string key;
			string new_text;
			int cursor_index;

			if (!Enabled)
			{
				kpea.Handled = true;
				return;
			}

			key = kpea.KeyChar.ToString();
			if( char.IsControl(kpea.KeyChar) )
				return;

			if((_allowable_chars != "") && (_allowable_chars.IndexOf(key) >= 0) )
				kpea.Handled = false;
			else if (_allowable_chars != "")
				kpea.Handled = true;

			if(_mask != MaskTypes.None)
			{
				kpea.Handled = true;
				cursor_index = _control.SelectionStart;
				//text before the cursor stays the same...
				new_text = _control.Text.Substring(0,cursor_index);

				//text at the cursor is replaced
				new_text += kpea.KeyChar;
				
				//text past the cursor stays the same.
				if(_control.Text.Length > cursor_index)
					new_text += _control.Text.Substring(cursor_index+ 1);

				//kpea.Handled = !MaskHelper.FitsMask(new_text, _mask);
				_control.Text = MaskHelper.ApplyMask(new_text, _mask);

				_control.SelectionStart = cursor_index + 1;
			}
		}

		#region IMaskable implementation
		public string AllowableChars
		{
			get{ return _allowable_chars; }
			set { _allowable_chars = value; }
		}

		public int MaxLength
		{
			get { return _max_len; }
			set
			{
				_max_len = value;
				if(_control != null)
					_control.MaxLength = _max_len;
			}
		}

		public int MinLength
		{
			get { return _min_len; }
			set { _min_len = value;	}
		}

		public MaskTypes Mask
		{
			get { return _mask; }
			set { _mask = value; }
		}
		#endregion
	}


	/// <summary>
	/// A Multi-line text question.  Used for text questions
	/// that need more than 1 line to hold the information.
	/// (i.e. address)
	/// </summary>
	public class MultiLineTextQuestion : TextQuestion , IMaskable
	{
		private const int MULTILINETEXTBOXHEIGHT = 80;
		private const int MULTILINETEXTBOXWIDTH = 120;

		public MultiLineTextQuestion(int questionID, int sequence, string text, Control parent)
			:base(questionID,sequence,text,parent)
		{
			_label = new Label();
			_label.Parent = Parent;
			_label.Text = Text;
			_label.AutoSize = true;
		}

		public override bool Draw(Point origin)
		{
			Point control_origin;

			DrawLabel(origin);
			_control = new TextBox();
			_control.Multiline = true;
			_control.ScrollBars = ScrollBars.Vertical;
			_control.Size = new Size(_control.Width,MULTILINETEXTBOXHEIGHT);
			_control.Parent = Parent;
			control_origin = new Point(origin.X + _label.Width + 2,origin.Y);
			_control.Location = control_origin;
			_control.Visible = true;
			_control.Show();
			return true;
		}
	}

	/// <summary>
	/// Contains constants for the regular expression engine.  
	/// </summary>
	public class RegExMasks
	{
		public const string SSN = @"\d{3}-\d{2}-\d{4}";
		public const string Phone = @"\d{3}-\d{3}-\d{4}"; 
		public const string Date = @"\d{2}/\d{2}/\d{4}"; 
	}

	/// <summary>
	/// Contains differnt mask types.
	/// </summary>
	public enum MaskTypes
	{
		None,
		Phone,
		SSN,
		Date,
		Currency,
		Number
	}

	/// <summary>
	/// Provides common functions for use in applying masks to strings.
	/// </summary>
	public class MaskHelper
	{
		public static string ApplyMask(string input, MaskTypes maskType)
		{
			string result = "";
			switch(maskType)
			{
				case MaskTypes.Phone:
					result = MaskPhoneNumber( input );
					break;
				case MaskTypes.SSN:
					result = MaskSSN( input );
					break;
				case MaskTypes.Date:
					result = MaskDate( input );
					break;
			}

			return result;
		}

		public static string GetRegExStringForMask(MaskTypes maskType)
		{
			string regExMask = "";

			switch( maskType )
			{
				case MaskTypes.Phone:
					regExMask = RegExMasks.Phone;
					break;
				case MaskTypes.Date:
					regExMask = RegExMasks.Date;
					break;
			}

			return regExMask;

		}

		private static string MaskPhoneNumber(string input)
		{
			string baseMask = "(___)___-____";
			char[] items = input.ToCharArray();
			int maxIdx = items.GetUpperBound(0);
			int currentIdx = 1;

			if(input.Length > baseMask.Length)
				return input.Substring(0, baseMask.Length);

			for(int i = 0; i <= maxIdx; i++)
			{
				if ( char.IsDigit(items[i]) )
				{
					baseMask = baseMask.Substring(0, currentIdx) + 
								char.ToString(items[i]) + 
								baseMask.Substring(currentIdx + 1);


					currentIdx++;	

					//take care of parenthesis and dashes
					if( currentIdx == 4 || currentIdx == 8 )
						currentIdx++;
				}
			}

			return baseMask;
		}

		private static string MaskSSN(string input)
		{
			string baseMask = "___-__-____";
			char[] items = input.ToCharArray();
			int maxIdx = items.GetUpperBound(0);
			int currentIdx = 1;

			if(input.Length > baseMask.Length)
				return input.Substring(0, baseMask.Length);

			for( int i = 0; i <= maxIdx; i++)
			{
				if( char.IsDigit(items[i] ) )
				{
					baseMask = baseMask.Substring(0, currentIdx) + 
						char.ToString(items[i]) + 
						baseMask.Substring(currentIdx + 1);


					currentIdx++;	

					//take care of parenthesis and dashes
					if( currentIdx == 3 || currentIdx == 6 )
						currentIdx++;
				}
			}
			return baseMask;
		}

		private static string MaskDate(string input)
		{
			string baseMask = "__/__/____";
			char[] items = input.ToCharArray();
			int maxIdx = items.GetUpperBound(0);
			int currentIdx = 1;

			if(input.Length > baseMask.Length)
				return input.Substring(0, baseMask.Length);

			for( int i = 0; i <= maxIdx; i++)
			{
				if( char.IsDigit(items[i] ) )
				{
					baseMask = baseMask.Substring(0, currentIdx) + 
						char.ToString(items[i]) + 
						baseMask.Substring(currentIdx + 1);


					currentIdx++;	

					//take care of parenthesis and dashes
					if( currentIdx == 3 || currentIdx == 6 )
						currentIdx++;
				}
			}
			return baseMask;

		}
	}
}
