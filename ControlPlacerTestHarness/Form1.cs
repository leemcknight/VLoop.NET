using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Afni.ControlPlacer;
using Afni.ControlPlacer.Questions;
using Afni.ControlPlacer.Lookups;

namespace ControlPlacerTestHarness
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu _mnuMain;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton tbNew;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.ComponentModel.IContainer components;
		private ArrayList _questions;
		private System.Windows.Forms.StatusBar _sbMain;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.TreeView _tvwMain;
		private int _seq;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox _cboColumns;
		private System.Windows.Forms.ComboBox _cboView;
		private System.Windows.Forms.CheckBox _chkAllowSmallResize;
		private System.Windows.Forms.Label _lblView;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem _ctxNewText;
		private System.Windows.Forms.MenuItem _ctxNewCheckbox;
		private System.Windows.Forms.MenuItem _ctxNewComboBox;
		private System.Windows.Forms.MenuItem _ctxNewMultiSelect;
		private System.Windows.Forms.MenuItem _ctxNewRadio;
		private System.Windows.Forms.MenuItem _ctxNewMultiLine;
		private System.Windows.Forms.MenuItem _ctxNewPhone;
		private System.Windows.Forms.MenuItem _ctxNewNumber;
		private System.Windows.Forms.MenuItem _ctxNewCurrency;
		private System.Windows.Forms.MenuItem menuItem25;
		private System.Windows.Forms.MenuItem menuItem26;
		private System.Windows.Forms.MenuItem menuItem27;
		private System.Windows.Forms.CheckBox _chkGroup;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
	
			_questions = new ArrayList();
			_seq = 1;
			_cboColumns.Text = "1";
			_cboView.Text = "ListView";
			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this._mnuMain = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.tbNew = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this._sbMain = new System.Windows.Forms.StatusBar();
			this._tvwMain = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this._cboView = new System.Windows.Forms.ComboBox();
			this._lblView = new System.Windows.Forms.Label();
			this._cboColumns = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this._chkAllowSmallResize = new System.Windows.Forms.CheckBox();
			this._chkGroup = new System.Windows.Forms.CheckBox();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this._ctxNewText = new System.Windows.Forms.MenuItem();
			this._ctxNewCheckbox = new System.Windows.Forms.MenuItem();
			this._ctxNewComboBox = new System.Windows.Forms.MenuItem();
			this._ctxNewMultiSelect = new System.Windows.Forms.MenuItem();
			this._ctxNewRadio = new System.Windows.Forms.MenuItem();
			this._ctxNewMultiLine = new System.Windows.Forms.MenuItem();
			this._ctxNewPhone = new System.Windows.Forms.MenuItem();
			this._ctxNewNumber = new System.Windows.Forms.MenuItem();
			this._ctxNewCurrency = new System.Windows.Forms.MenuItem();
			this.menuItem25 = new System.Windows.Forms.MenuItem();
			this.menuItem26 = new System.Windows.Forms.MenuItem();
			this.menuItem27 = new System.Windows.Forms.MenuItem();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _mnuMain
			// 
			this._mnuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItem1,
																					 this.menuItem13});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem12});
			this.menuItem1.Text = "&File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem3,
																					  this.menuItem4,
																					  this.menuItem5,
																					  this.menuItem6,
																					  this.menuItem7,
																					  this.menuItem8,
																					  this.menuItem9,
																					  this.menuItem10,
																					  this.menuItem11});
			this.menuItem2.Text = "&New";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Text = "&Text Question";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.Text = "&Multiline Text Question";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 2;
			this.menuItem5.Text = "&Combo Question";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 3;
			this.menuItem6.Text = "C&heckbox Question";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 4;
			this.menuItem7.Text = "&Date Question";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 5;
			this.menuItem8.Text = "C&urrency Question";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 6;
			this.menuItem9.Text = "N&umeric Question";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 7;
			this.menuItem10.Text = "Multi&select Question";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 8;
			this.menuItem11.Text = "&Radio Question";
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 1;
			this.menuItem12.Text = "E&xit";
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 1;
			this.menuItem13.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem14});
			this.menuItem13.Text = "Actions";
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 0;
			this.menuItem14.Text = "&Build Controls";
			// 
			// toolBar1
			// 
			this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.tbNew,
																						this.toolBarButton5,
																						this.toolBarButton1});
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(800, 25);
			this.toolBar1.TabIndex = 0;
			this.toolBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// tbNew
			// 
			this.tbNew.ImageIndex = 0;
			this.tbNew.Tag = "NEW";
			this.tbNew.Text = "&New Question...";
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.ImageIndex = 2;
			this.toolBarButton1.Tag = "BUILD";
			this.toolBarButton1.Text = "&Build Controls";
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// _sbMain
			// 
			this._sbMain.Location = new System.Drawing.Point(0, 529);
			this._sbMain.Name = "_sbMain";
			this._sbMain.ShowPanels = true;
			this._sbMain.Size = new System.Drawing.Size(800, 24);
			this._sbMain.TabIndex = 6;
			this._sbMain.Text = "statusBar1";
			// 
			// _tvwMain
			// 
			this._tvwMain.ContextMenu = this.contextMenu1;
			this._tvwMain.Dock = System.Windows.Forms.DockStyle.Left;
			this._tvwMain.ImageList = this.imageList1;
			this._tvwMain.Location = new System.Drawing.Point(0, 25);
			this._tvwMain.Name = "_tvwMain";
			this._tvwMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																				 new System.Windows.Forms.TreeNode("Questions", 3, 3)});
			this._tvwMain.Size = new System.Drawing.Size(152, 504);
			this._tvwMain.TabIndex = 7;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(152, 25);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(5, 504);
			this.splitter1.TabIndex = 8;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._cboView,
																				 this._lblView,
																				 this._cboColumns,
																				 this.label1,
																				 this._chkAllowSmallResize,
																				 this._chkGroup});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(157, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(643, 504);
			this.panel1.TabIndex = 9;
			// 
			// _cboView
			// 
			this._cboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboView.Enabled = false;
			this._cboView.Items.AddRange(new object[] {
														  "ListView",
														  "ListBox",
														  "Navigation Buttons"});
			this._cboView.Location = new System.Drawing.Point(208, 40);
			this._cboView.Name = "_cboView";
			this._cboView.Size = new System.Drawing.Size(152, 21);
			this._cboView.TabIndex = 5;
			// 
			// _lblView
			// 
			this._lblView.Enabled = false;
			this._lblView.Location = new System.Drawing.Point(176, 40);
			this._lblView.Name = "_lblView";
			this._lblView.Size = new System.Drawing.Size(128, 16);
			this._lblView.TabIndex = 4;
			this._lblView.Text = "View:";
			// 
			// _cboColumns
			// 
			this._cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cboColumns.Items.AddRange(new object[] {
															 "1",
															 "2",
															 "3",
															 "4",
															 "5",
															 "6"});
			this._cboColumns.Location = new System.Drawing.Point(120, 72);
			this._cboColumns.Name = "_cboColumns";
			this._cboColumns.Size = new System.Drawing.Size(152, 21);
			this._cboColumns.Sorted = true;
			this._cboColumns.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Max # of columns:";
			// 
			// _chkAllowSmallResize
			// 
			this._chkAllowSmallResize.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._chkAllowSmallResize.Location = new System.Drawing.Point(16, 16);
			this._chkAllowSmallResize.Name = "_chkAllowSmallResize";
			this._chkAllowSmallResize.Size = new System.Drawing.Size(144, 16);
			this._chkAllowSmallResize.TabIndex = 1;
			this._chkAllowSmallResize.Text = "Allow Small Resize";
			// 
			// _chkGroup
			// 
			this._chkGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._chkGroup.Location = new System.Drawing.Point(16, 40);
			this._chkGroup.Name = "_chkGroup";
			this._chkGroup.Size = new System.Drawing.Size(152, 16);
			this._chkGroup.TabIndex = 0;
			this._chkGroup.Text = "Use Group Answers";
			this._chkGroup.CheckedChanged += new System.EventHandler(this._chkGroup_CheckedChanged);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem15,
																						 this.menuItem25,
																						 this.menuItem26,
																						 this.menuItem27});
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 0;
			this.menuItem15.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this._ctxNewText,
																					   this._ctxNewCheckbox,
																					   this._ctxNewComboBox,
																					   this._ctxNewMultiSelect,
																					   this._ctxNewRadio,
																					   this._ctxNewMultiLine,
																					   this._ctxNewPhone,
																					   this._ctxNewNumber,
																					   this._ctxNewCurrency});
			this.menuItem15.Text = "New";
			// 
			// _ctxNewText
			// 
			this._ctxNewText.Index = 0;
			this._ctxNewText.Text = "Text Question";
			// 
			// _ctxNewCheckbox
			// 
			this._ctxNewCheckbox.Index = 1;
			this._ctxNewCheckbox.Text = "CheckBox Question";
			// 
			// _ctxNewComboBox
			// 
			this._ctxNewComboBox.Index = 2;
			this._ctxNewComboBox.Text = "ComboBox Question";
			// 
			// _ctxNewMultiSelect
			// 
			this._ctxNewMultiSelect.Index = 3;
			this._ctxNewMultiSelect.Text = "Multi-Select Question";
			// 
			// _ctxNewRadio
			// 
			this._ctxNewRadio.Index = 4;
			this._ctxNewRadio.Text = "Radio Question";
			// 
			// _ctxNewMultiLine
			// 
			this._ctxNewMultiLine.Index = 5;
			this._ctxNewMultiLine.Text = "Multi-line text Question";
			// 
			// _ctxNewPhone
			// 
			this._ctxNewPhone.Index = 6;
			this._ctxNewPhone.Text = "Phone Question";
			// 
			// _ctxNewNumber
			// 
			this._ctxNewNumber.Index = 7;
			this._ctxNewNumber.Text = "Number Question";
			// 
			// _ctxNewCurrency
			// 
			this._ctxNewCurrency.Index = 8;
			this._ctxNewCurrency.Text = "Currency Question";
			// 
			// menuItem25
			// 
			this.menuItem25.Index = 1;
			this.menuItem25.Text = "&Edit";
			// 
			// menuItem26
			// 
			this.menuItem26.Index = 2;
			this.menuItem26.Text = "&Delete";
			// 
			// menuItem27
			// 
			this.menuItem27.Index = 3;
			this.menuItem27.Text = "&Help";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(800, 553);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1,
																		  this.splitter1,
																		  this._tvwMain,
																		  this._sbMain,
																		  this.toolBar1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this._mnuMain;
			this.Name = "Form1";
			this.Text = "Dynamic Control Test Harness";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button.Tag.ToString() == "BUILD")
			{
				BuildDynCtls();
			}
			else
			{
				frmQuestion form = new frmQuestion(panel1, _seq++, QuestionTypes.Text);
				if( form.ShowDialog(this) == DialogResult.OK )
				{
					AddQuestionToTVW( form.Question );
				}
				
			}
		}

		private void AddQuestionToTVW(Question question)
		{
			TreeNode tvwNode;
			TreeNode lookupNode;

			_questions.Add(question);
			tvwNode = _tvwMain.Nodes[0].Nodes.Add(question.Text);
			tvwNode.ImageIndex = 3;
			tvwNode.Tag = question;
			tvwNode.ImageIndex = 4;
			tvwNode.SelectedImageIndex = 4;
				
			ILookupQuestion lookup = question as ILookupQuestion;
			if ( lookup != null )
			{
				foreach(LookupData lookupItem in lookup.Lookups)
				{
					lookupNode = tvwNode.Nodes.Add(lookupItem.Text);
					lookupNode.ImageIndex = 5;
					lookupNode.SelectedImageIndex = 5;
				}
			}
		}

		private void BuildDynCtls()
		{
			frmDynamicQuestions frm; 
			RecordDisplayStyles style = RecordDisplayStyles.ListBox;
			switch(_cboView.Text)
			{
				case "ListView":
					style = RecordDisplayStyles.ListView;
					break;
				case "ListBox":
					style = RecordDisplayStyles.ListBox;
					break;
				case "NavButtons":
					style = RecordDisplayStyles.NavButtons;
					break;
			}
			
			frm = new frmDynamicQuestions(_questions, _chkGroup.Checked, Convert.ToInt32( _cboColumns.Text ),  _chkAllowSmallResize.Checked, style);
				
			frm.ShowDialog();
		}

		
		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			//text questions
			frmQuestion frmQ = new frmQuestion(panel1,_seq++,QuestionTypes.Text);
			if( frmQ.ShowDialog(this) == DialogResult.OK )
			{
				AddQuestionToTVW( frmQ.Question );
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			//multiline text
			frmQuestion frmQ = new frmQuestion(panel1,_seq++,QuestionTypes.Text);
			if( frmQ.ShowDialog(this) == DialogResult.OK )
			{
				AddQuestionToTVW( frmQ.Question );
			}
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			//combo
			frmQuestion frmQ = new frmQuestion(panel1,_seq++,QuestionTypes.Text);
			if( frmQ.ShowDialog(this) == DialogResult.OK )
			{
				AddQuestionToTVW( frmQ.Question );
			}
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			//checkbox
			frmQuestion frmQ = new frmQuestion(panel1,_seq++,QuestionTypes.Text);
			if( frmQ.ShowDialog(this) == DialogResult.OK )
			{
				AddQuestionToTVW( frmQ.Question );
			}
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			//date
			frmQuestion frmQ = new frmQuestion(panel1,_seq++,QuestionTypes.Text);
			if( frmQ.ShowDialog(this) == DialogResult.OK )
			{
				AddQuestionToTVW( frmQ.Question );
			}
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			//currency
			frmQuestion frmQ = new frmQuestion(panel1,_seq++,QuestionTypes.Text);
			if( frmQ.ShowDialog(this) == DialogResult.OK )
			{
				AddQuestionToTVW( frmQ.Question );
			}
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			//numeric
			frmQuestion frmQ = new frmQuestion(panel1,_seq++,QuestionTypes.Text);
			if( frmQ.ShowDialog(this) == DialogResult.OK )
			{
				AddQuestionToTVW( frmQ.Question );
			}
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			//multiselect
			frmQuestion frmQ = new frmQuestion(panel1,_seq++,QuestionTypes.Text);
			if( frmQ.ShowDialog(this) == DialogResult.OK )
			{
				AddQuestionToTVW( frmQ.Question );
			}
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			//radio
			frmQuestion frmQ = new frmQuestion(panel1,_seq++,QuestionTypes.Text);
			if( frmQ.ShowDialog(this) == DialogResult.OK )
			{
				AddQuestionToTVW( frmQ.Question );
			}
		}

		private void _chkGroup_CheckedChanged(object sender, System.EventArgs e)
		{
			_cboView.Enabled = _chkGroup.Checked;
			_lblView.Enabled = _chkGroup.Checked;
		}

		
	}
}
