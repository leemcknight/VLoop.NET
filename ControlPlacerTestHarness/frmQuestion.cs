using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Afni.ControlPlacer;
using Afni.ControlPlacer.Questions;

namespace ControlPlacerTestHarness
{
	/// <summary>
	/// Summary description for frmQuestion.
	/// </summary>
	public class frmQuestion : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button _btnOK;
		private System.Windows.Forms.Button _btnCancel;
		private Afni.ControlPlacer.Questions.Question _question;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button _btnRemove;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button _btnAdd;
		private System.Windows.Forms.TextBox _txtLookupValue;
		private System.Windows.Forms.ListBox _lbLookups;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox _cboQuestionType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox _txtQuestionTitle;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabControl tabControl1;
		private Panel _panel;
		private int _seq;
		private System.Windows.Forms.CheckBox _chkRequired;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmQuestion(Panel parent, int sequence, QuestionTypes questionType)
		{
			_panel = parent;
			_seq = sequence;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			switch(questionType)
			{
				case QuestionTypes.CheckBox:
					_cboQuestionType.Text = "CheckBox";
					break;
				case QuestionTypes.Combo:
					_cboQuestionType.Text = "Combobox";
					break;
				case QuestionTypes.DateMask:
					_cboQuestionType.Text = "Date";
					break;
				case QuestionTypes.MultiLineText:
					_cboQuestionType.Text = "Multiline Text";
					break;
				case QuestionTypes.MultiSelect:
					_cboQuestionType.Text = "Multi-Select";
					break;
				case QuestionTypes.PhoneMask:
					_cboQuestionType.Text = "Phone";
					break;
				case QuestionTypes.Radio:
					_cboQuestionType.Text = "Radio";
					break;
				case QuestionTypes.Text:
					_cboQuestionType.Text = "Text";
					break;
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

	
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._btnOK = new System.Windows.Forms.Button();
			this._btnCancel = new System.Windows.Forms.Button();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this._chkRequired = new System.Windows.Forms.CheckBox();
			this._btnRemove = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this._btnAdd = new System.Windows.Forms.Button();
			this._txtLookupValue = new System.Windows.Forms.TextBox();
			this._lbLookups = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this._cboQuestionType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this._txtQuestionTitle = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _btnOK
			// 
			this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._btnOK.Location = new System.Drawing.Point(168, 360);
			this._btnOK.Name = "_btnOK";
			this._btnOK.Size = new System.Drawing.Size(80, 24);
			this._btnOK.TabIndex = 8;
			this._btnOK.Text = "&OK";
			this._btnOK.Click += new System.EventHandler(this._btnOK_Click);
			// 
			// _btnCancel
			// 
			this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._btnCancel.Location = new System.Drawing.Point(256, 360);
			this._btnCancel.Name = "_btnCancel";
			this._btnCancel.Size = new System.Drawing.Size(80, 24);
			this._btnCancel.TabIndex = 9;
			this._btnCancel.Text = "&Cancel";
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this._chkRequired,
																				   this._btnRemove,
																				   this.label4,
																				   this._btnAdd,
																				   this._txtLookupValue,
																				   this._lbLookups,
																				   this.groupBox1,
																				   this.label3,
																				   this._cboQuestionType,
																				   this.label2,
																				   this._txtQuestionTitle,
																				   this.label1});
			this.tabPage1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(328, 318);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Question";
			// 
			// _chkRequired
			// 
			this._chkRequired.Location = new System.Drawing.Point(96, 80);
			this._chkRequired.Name = "_chkRequired";
			this._chkRequired.Size = new System.Drawing.Size(144, 16);
			this._chkRequired.TabIndex = 3;
			this._chkRequired.Text = "Required";
			// 
			// _btnRemove
			// 
			this._btnRemove.Location = new System.Drawing.Point(216, 288);
			this._btnRemove.Name = "_btnRemove";
			this._btnRemove.Size = new System.Drawing.Size(104, 24);
			this._btnRemove.TabIndex = 7;
			this._btnRemove.Text = "&Remove Selected";
			this._btnRemove.Click += new System.EventHandler(this._btnRemove_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Lookup Value:";
			// 
			// _btnAdd
			// 
			this._btnAdd.Location = new System.Drawing.Point(280, 128);
			this._btnAdd.Name = "_btnAdd";
			this._btnAdd.Size = new System.Drawing.Size(40, 24);
			this._btnAdd.TabIndex = 5;
			this._btnAdd.Text = "&Add";
			this._btnAdd.Click += new System.EventHandler(this._btnAdd_Click);
			// 
			// _txtLookupValue
			// 
			this._txtLookupValue.Location = new System.Drawing.Point(96, 128);
			this._txtLookupValue.Name = "_txtLookupValue";
			this._txtLookupValue.Size = new System.Drawing.Size(176, 21);
			this._txtLookupValue.TabIndex = 4;
			this._txtLookupValue.Text = "";
			// 
			// _lbLookups
			// 
			this._lbLookups.Location = new System.Drawing.Point(8, 160);
			this._lbLookups.Name = "_lbLookups";
			this._lbLookups.Size = new System.Drawing.Size(312, 121);
			this._lbLookups.TabIndex = 6;
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(88, 104);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(232, 8);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Lookup Data";
			// 
			// _cboQuestionType
			// 
			this._cboQuestionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboQuestionType.ItemHeight = 13;
			this._cboQuestionType.Items.AddRange(new object[] {
																  "CheckBox",
																  "Combobox",
																  "Currency",
																  "Date",
																  "Multi-Select",
																  "Multiline Text",
																  "Numeric",
																  "Phone",
																  "Radio",
																  "Text"});
			this._cboQuestionType.Location = new System.Drawing.Point(96, 48);
			this._cboQuestionType.Name = "_cboQuestionType";
			this._cboQuestionType.Size = new System.Drawing.Size(224, 21);
			this._cboQuestionType.Sorted = true;
			this._cboQuestionType.TabIndex = 2;
			this._cboQuestionType.SelectedIndexChanged += new System.EventHandler(this._cboQuestionType_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Question Type:";
			// 
			// _txtQuestionTitle
			// 
			this._txtQuestionTitle.Location = new System.Drawing.Point(96, 16);
			this._txtQuestionTitle.Name = "_txtQuestionTitle";
			this._txtQuestionTitle.Size = new System.Drawing.Size(224, 21);
			this._txtQuestionTitle.TabIndex = 1;
			this._txtQuestionTitle.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Question Title:";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPage1});
			this.tabControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tabControl1.ItemSize = new System.Drawing.Size(55, 18);
			this.tabControl1.Location = new System.Drawing.Point(3, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(336, 344);
			this.tabControl1.TabIndex = 2;
			// 
			// frmQuestion
			// 
			this.AcceptButton = this._btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this._btnCancel;
			this.ClientSize = new System.Drawing.Size(341, 391);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabControl1,
																		  this._btnCancel,
																		  this._btnOK});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmQuestion";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Question Properties";
			this.tabPage1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void _btnOK_Click(object sender, System.EventArgs e)
		{
			ILookupQuestion lookupQuestion;
			switch(_cboQuestionType.Text)
			{
				case "CheckBox":
					_question = new CheckBoxQuestion(_seq, _seq, _txtQuestionTitle.Text, _panel);
					break;
				case  "Combobox":
					_question = new ComboQuestion(_seq, _seq, _txtQuestionTitle.Text, _panel);
					break;
				case  "Currency":
					_question = new TextQuestion(_seq, _seq, _txtQuestionTitle.Text, _panel);
					((TextQuestion)_question).Mask = MaskTypes.Currency;
					break;
				case "Date":
					_question = new TextQuestion(_seq, _seq, _txtQuestionTitle.Text, _panel);
					((TextQuestion)_question).Mask = MaskTypes.Date;
					break;
				case "Multi-Select":
					_question = new MultiSelectQuestion(_seq, _seq, _txtQuestionTitle.Text, _panel);
					break;
				case "Multiline Text":
					_question = new MultiLineTextQuestion(_seq, _seq, _txtQuestionTitle.Text, _panel);
					break;
				case "Numeric":
					_question = new TextQuestion(_seq, _seq, _txtQuestionTitle.Text, _panel);
					((TextQuestion)_question).Mask = MaskTypes.Number;
					break;
				case "Radio":
					_question = new RadioQuestion(_seq, _seq, _txtQuestionTitle.Text, _panel);
					break;
				case "Text":
					_question = new TextQuestion(_seq, _seq, _txtQuestionTitle.Text, _panel);
					((TextQuestion)_question).Mask = MaskTypes.None;
					break;
				case "Phone":
					_question = new TextQuestion(_seq, _seq, _txtQuestionTitle.Text, _panel);
					((TextQuestion)_question).Mask = MaskTypes.Phone;
					break;
			}

			_question.AnswerRequired = _chkRequired.Checked;
			lookupQuestion = _question as ILookupQuestion; 

			if ( lookupQuestion != null )
			{
				foreach(string s in _lbLookups.Items)
				{
					((ILookupQuestion)_question).AddQuestionData(1, s);
				}
			}
			
		}

		public Question Question
		{
			get { return _question; }
		}

		private void _btnAdd_Click(object sender, System.EventArgs e)
		{
			_lbLookups.Items.Add( _txtLookupValue.Text );
			_txtLookupValue.Text = "";
			_txtLookupValue.Focus();
		}

		private void _cboQuestionType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			bool enableLookupData = false;

			switch( _cboQuestionType.Text )
			{
				case "Multi-Select":
					enableLookupData = true;
					break;
				case "Combobox":
					enableLookupData = true;
					break;
				case "Radio":
					enableLookupData = true;
					break;
			}

			
			_btnAdd.Enabled = enableLookupData;
			_btnRemove.Enabled = enableLookupData;
			_txtLookupValue.Enabled = enableLookupData;
			_lbLookups.Enabled = enableLookupData;
		}

		private void _btnRemove_Click(object sender, System.EventArgs e)
		{
			foreach(object item in _lbLookups.SelectedItems)
			{
				_lbLookups.Items.Remove(item);
			}
		}
	}
}
