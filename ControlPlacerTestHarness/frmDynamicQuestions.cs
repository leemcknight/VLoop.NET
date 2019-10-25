using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace ControlPlacerTestHarness
{
	/// <summary>
	/// Summary description for frmDynamicQuestions.
	/// </summary>
	public class frmDynamicQuestions : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.StatusBar _sbMain;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolBarButton _btnNew;
		private System.Windows.Forms.ToolBarButton _btnSave;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton _btnEdit;
		private System.Windows.Forms.ToolBarButton _btnUndo;
		private System.Windows.Forms.ToolBarButton _btnDelete;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBarButton _btnXML;
		private Afni.ControlPlacer.Area _area;
		private bool _grouped;
		private System.Windows.Forms.MenuItem _mnuNew;
		private System.Windows.Forms.MenuItem _mnuSave;
		private System.Windows.Forms.MenuItem _mnuExit;
		private System.Windows.Forms.MenuItem _mnuEdit;
		private System.Windows.Forms.MenuItem _mnuUndo;
		private System.Windows.Forms.MenuItem _mnuDelete;
		private System.Windows.Forms.MenuItem _mnuHelp;
		private System.Windows.Forms.MenuItem _mnuAbout;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private EditMode _mode;
		private Afni.ControlPlacer.RecordDisplayStyles _style;

		private enum EditMode { Add, EditNonDirty, EditDirty, View }

		public frmDynamicQuestions(ArrayList questions, bool grouped, int columns, bool allowSmallResize, Afni.ControlPlacer.RecordDisplayStyles style)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_style=style;

			if(grouped)
			{
				_area = new Afni.ControlPlacer.RecordArea(columns, this, new Point(0,0),_style, panel1);

			}
			else
			{
				_area = new Afni.ControlPlacer.Area(columns,this,new Point(3,3),panel1);
			}
			_area.AllowSmallResize = allowSmallResize;

			foreach(Afni.ControlPlacer.Questions.Question question in questions)
			{
				question.Parent = panel1;
				_area.AddQuestion(question);
			}

			_area.Changed += new System.EventHandler(this.AreaChanged);
			_area.Draw();
			_grouped = grouped;

			_btnSave.Enabled = false;
			_btnNew.Enabled = grouped;
			_btnUndo.Enabled = false;
			_btnEdit.Enabled = false;
			_btnDelete.Enabled = false;

		}

		private void AreaChanged(object sender, System.EventArgs e)
		{
			_btnSave.Enabled = true;
			_btnNew.Enabled = false;
			_btnUndo.Enabled = true;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmDynamicQuestions));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this._mnuNew = new System.Windows.Forms.MenuItem();
			this._mnuSave = new System.Windows.Forms.MenuItem();
			this._mnuExit = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this._mnuEdit = new System.Windows.Forms.MenuItem();
			this._mnuUndo = new System.Windows.Forms.MenuItem();
			this._mnuDelete = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this._mnuHelp = new System.Windows.Forms.MenuItem();
			this._mnuAbout = new System.Windows.Forms.MenuItem();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this._btnNew = new System.Windows.Forms.ToolBarButton();
			this._btnSave = new System.Windows.Forms.ToolBarButton();
			this._btnXML = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this._btnEdit = new System.Windows.Forms.ToolBarButton();
			this._btnUndo = new System.Windows.Forms.ToolBarButton();
			this._btnDelete = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this._sbMain = new System.Windows.Forms.StatusBar();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem5,
																					  this.menuItem9});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this._mnuNew,
																					  this._mnuSave,
																					  this._mnuExit});
			this.menuItem1.Text = "&File";
			// 
			// _mnuNew
			// 
			this._mnuNew.Index = 0;
			this._mnuNew.Text = "&New";
			// 
			// _mnuSave
			// 
			this._mnuSave.Index = 1;
			this._mnuSave.Text = "&Save";
			// 
			// _mnuExit
			// 
			this._mnuExit.Index = 2;
			this._mnuExit.Text = "E&xit";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this._mnuEdit,
																					  this._mnuUndo,
																					  this._mnuDelete});
			this.menuItem5.Text = "&Edit";
			// 
			// _mnuEdit
			// 
			this._mnuEdit.Index = 0;
			this._mnuEdit.Text = "&Edit";
			// 
			// _mnuUndo
			// 
			this._mnuUndo.Index = 1;
			this._mnuUndo.Text = "&Undo";
			// 
			// _mnuDelete
			// 
			this._mnuDelete.Index = 2;
			this._mnuDelete.Text = "&Delete";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 2;
			this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this._mnuHelp,
																					  this._mnuAbout});
			this.menuItem9.Text = "&Help";
			// 
			// _mnuHelp
			// 
			this._mnuHelp.Index = 0;
			this._mnuHelp.Text = "&Help";
			// 
			// _mnuAbout
			// 
			this._mnuAbout.Index = 1;
			this._mnuAbout.Text = "&About";
			// 
			// toolBar1
			// 
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this._btnNew,
																						this._btnSave,
																						this._btnXML,
																						this.toolBarButton1,
																						this._btnEdit,
																						this._btnUndo,
																						this._btnDelete,
																						this.toolBarButton2,
																						this.toolBarButton3});
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(912, 25);
			this.toolBar1.TabIndex = 0;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// _btnNew
			// 
			this._btnNew.ImageIndex = 0;
			this._btnNew.Tag = "NEW";
			this._btnNew.ToolTipText = "Add a new record";
			// 
			// _btnSave
			// 
			this._btnSave.ImageIndex = 1;
			this._btnSave.Tag = "SAVE";
			this._btnSave.ToolTipText = "Save this record";
			// 
			// _btnXML
			// 
			this._btnXML.ImageIndex = 5;
			this._btnXML.Tag = "EXPORT";
			this._btnXML.ToolTipText = "Export to XML file";
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// _btnEdit
			// 
			this._btnEdit.ImageIndex = 4;
			this._btnEdit.Tag = "EDIT";
			this._btnEdit.ToolTipText = "Edit this record";
			// 
			// _btnUndo
			// 
			this._btnUndo.ImageIndex = 3;
			this._btnUndo.Tag = "UNDO";
			this._btnUndo.ToolTipText = "Undo Changes";
			// 
			// _btnDelete
			// 
			this._btnDelete.ImageIndex = 2;
			this._btnDelete.Tag = "DELETE";
			this._btnDelete.ToolTipText = "Delete Record";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.ImageIndex = 7;
			this.toolBarButton3.Tag = "HELP";
			this.toolBarButton3.ToolTipText = "Help";
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// _sbMain
			// 
			this._sbMain.Location = new System.Drawing.Point(0, 489);
			this._sbMain.Name = "_sbMain";
			this._sbMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																					   this.statusBarPanel1});
			this._sbMain.ShowPanels = true;
			this._sbMain.Size = new System.Drawing.Size(912, 24);
			this._sbMain.TabIndex = 1;
			this._sbMain.Text = "statusBar1";
			// 
			// statusBarPanel1
			// 
			this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarPanel1.Width = 896;
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(912, 464);
			this.panel1.TabIndex = 2;
			// 
			// frmDynamicQuestions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(912, 513);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1,
																		  this._sbMain,
																		  this.toolBar1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "frmDynamicQuestions";
			this.Text = "AFNI";
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch(e.Button.Tag.ToString())
			{
				case "SAVE":
					if(_area.Validate())
					{
						_area.RegisterSave();
						LoadEditMode( EditMode.View );
					}
					else
					{
						MessageBox.Show("Validation Failed!");
					}
					break;
				case "EXPORT":
					System.IO.StreamWriter writer;
					SaveFileDialog dlg = new SaveFileDialog();
					dlg.OverwritePrompt = true;
					dlg.ValidateNames = true;
					dlg.CreatePrompt = true;
					dlg.AddExtension = true;
					dlg.Filter = "XML files(*.xml)|*.xml";
					dlg.DefaultExt = "*.xml";
					dlg.Title = "Save XML to file...";
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						writer = File.AppendText(dlg.FileName);
						writer.Write(_area.ToXML());
						writer.Close();
					}
					break;
				case "UNDO":
					_area.Undo();
					LoadEditMode( EditMode.View );
					break;
				case "NEW":
					((Afni.ControlPlacer.RecordArea)_area).New();
					LoadEditMode( EditMode.Add );
					break;
				case "DELETE":
					((Afni.ControlPlacer.RecordArea)_area).Delete();
					LoadEditMode( EditMode.View );
					break;
				case "EDIT":
					((Afni.ControlPlacer.RecordArea)_area).Edit();
					LoadEditMode( EditMode.EditNonDirty );
					break;
			}
		}

		private void LoadEditMode( EditMode em)
		{
			_mode = em;

			switch(em)
			{
				case EditMode.Add:
					_btnNew.Enabled = false;
					_btnSave.Enabled = true;
					_btnUndo.Enabled = true;
					_btnDelete.Enabled = false;
					_btnEdit.Enabled = false;
					_btnXML.Enabled = false;
					break;
				case EditMode.EditDirty:
					_btnSave.Enabled = true;
					_btnUndo.Enabled = true;
					_btnNew.Enabled = false;
					_btnDelete.Enabled = false;
					_btnEdit.Enabled = false;
					_btnXML.Enabled = false;
					break;
				case EditMode.View:
					_btnNew.Enabled = true;
					_btnSave.Enabled = false;
					_btnDelete.Enabled = true;
					_btnEdit.Enabled = true;
					_btnXML.Enabled = true;
					_btnUndo.Enabled = false;
					break;
			}
		}
	}
}
