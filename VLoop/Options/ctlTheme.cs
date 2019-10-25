using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using Afni.FormData;
using System.Reflection;
using System.IO;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for ctlTheme.
	/// </summary>
	public class ctlTheme : System.Windows.Forms.UserControl , ISaveable
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboThemes;
		private ArrayList _themes;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private Afni.Applications.VLoop.Application _app;
		private Afni.FormData.FormStates _formstate = FormStates.Idle;
		private System.Windows.Forms.CheckBox chkfolderlist;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label lblCustOpen;
		private System.Windows.Forms.ComboBox cboStartupBehavior;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlTheme(Afni.Applications.VLoop.Application app)
		{
			// This call is required by the Windows.Forms Form Designer.
			_themes = new ArrayList();
			_app = app;
			InitializeComponent();
			ReadXML();
		}

		/// <summary>
		/// Reads the theme properties out of the 
		/// theme XML file and displays them on the 
		/// screen
		/// </summary>
		private void ReadXML()
		{
			string path;
			System.Xml.XmlDocument doc;
			System.Xml.XmlNodeList themes;
			System.Xml.XmlNode root;
		
			try
			{
				path = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
			
				path += @"\Def.xml";
				doc = new XmlDocument();
				doc.Load(path);
				root = doc.ChildNodes[1];
				themes = root.ChildNodes;
				
				foreach(XmlNode theme_node in themes)
				{
					_themes.Add(ThemeManager.ThemeFromNode(theme_node,_app));
				}

				cboThemes.DataSource = _themes;

			}
			catch(Exception ex)
			{
				MessageBox.Show("Unable to display themes: " + System.Environment.NewLine + ex.Message.ToString(),"VLoop Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
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

		bool ISaveable.Save()
		{
			DisplayTheme theme;
			bool save_ok = true;
			try
			{
				theme = (DisplayTheme)cboThemes.SelectedItem;
				_app.ApplyTheme(theme);
			}
			catch
			{
				MessageBox.Show("Unable to apply selected theme.",
								"VLoop Error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
				save_ok  = false;
			}

			return save_ok;
		}

		bool ISaveable.Undo()
		{
			return true;
		}

		string ISaveable.SaveButtonText
		{
			get { return "OK";}
			set {}
		}
		event EventHandler ISaveable.Dirtied
		{
			add	{((ISaveable)this).Dirtied += value;}
			remove{	((ISaveable)this).Dirtied -= value;}
		}

		event EventHandler ISaveable.SaveFailed
		{
			add { ((ISaveable)this).SaveFailed += value; }
			remove{ ((ISaveable)this).SaveFailed -= value; }
		}

		event EventHandler ISaveable.SaveSucceeded
		{
			add { ((ISaveable)this).SaveSucceeded += value; }
			remove { ((ISaveable)this).SaveSucceeded -= value; }
		}

		event EventHandler ISaveable.Undone
		{
			add { ((ISaveable)this).Undone += value; }
			remove { ((ISaveable)this).Undone -= value; }
		}

		Afni.FormData.FormStates IForm.FormState
		{
			get { return _formstate; }
		}

		bool IForm.Refresh()
		{
			ReadXML();
			return true;
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.cboThemes = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkfolderlist = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.lblCustOpen = new System.Windows.Forms.Label();
			this.cboStartupBehavior = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Display Theme:";
			// 
			// cboThemes
			// 
			this.cboThemes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboThemes.Location = new System.Drawing.Point(232, 56);
			this.cboThemes.Name = "cboThemes";
			this.cboThemes.Size = new System.Drawing.Size(128, 21);
			this.cboThemes.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Settings";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(72, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(448, 8);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			// 
			// chkfolderlist
			// 
			this.chkfolderlist.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkfolderlist.Location = new System.Drawing.Point(16, 120);
			this.chkfolderlist.Name = "chkfolderlist";
			this.chkfolderlist.Size = new System.Drawing.Size(192, 16);
			this.chkfolderlist.TabIndex = 4;
			this.chkfolderlist.Text = "Show Folder list at startup";
			// 
			// checkBox1
			// 
			this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox1.Location = new System.Drawing.Point(16, 144);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(256, 16);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "Expand scripts window at startup";
			// 
			// lblCustOpen
			// 
			this.lblCustOpen.Location = new System.Drawing.Point(16, 88);
			this.lblCustOpen.Name = "lblCustOpen";
			this.lblCustOpen.Size = new System.Drawing.Size(288, 16);
			this.lblCustOpen.TabIndex = 6;
			this.lblCustOpen.Text = "When a customer record is first opened:";
			// 
			// cboStartupBehavior
			// 
			this.cboStartupBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStartupBehavior.Items.AddRange(new object[] {
																	"Customer Screen",
																	"VLoop Home"});
			this.cboStartupBehavior.Location = new System.Drawing.Point(232, 88);
			this.cboStartupBehavior.Name = "cboStartupBehavior";
			this.cboStartupBehavior.Size = new System.Drawing.Size(128, 21);
			this.cboStartupBehavior.TabIndex = 7;
			// 
			// ctlTheme
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cboStartupBehavior,
																		  this.lblCustOpen,
																		  this.checkBox1,
																		  this.chkfolderlist,
																		  this.groupBox1,
																		  this.label2,
																		  this.cboThemes,
																		  this.label1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ctlTheme";
			this.Size = new System.Drawing.Size(528, 344);
			this.ResumeLayout(false);

		}
		#endregion
	}

	public interface ISkinnable
	{
		bool ApplyTheme(DisplayTheme theme);
		
		DisplayTheme CurrentTheme
		{
			get; 
		}
	}

	public class ThemeManager
	{
		private static Color ColorFromNode(XmlNode color_node)
		{
			int r;
			int g; 
			int b;

			r = Convert.ToInt32(color_node.Attributes["red"].Value);
			g = Convert.ToInt32(color_node.Attributes["green"].Value);
			b = Convert.ToInt32(color_node.Attributes["blue"].Value);

			return Color.FromArgb(r,g,b);
		}

		public static DisplayTheme ThemeFromNode(XmlNode theme_node, Afni.Applications.VLoop.Application app)
		{
			DisplayTheme theme;
			XmlNode item_node;
			theme = new DisplayTheme();

			//name
			theme.Name = theme_node.Attributes["name"].Value;
			theme.ID = theme_node.Attributes["id"].Value;		
			switch(theme.ID)
			{
				case "xpblue":
					theme.TaskBoxChevronDown = VLoopIcons.ChevronDown_XPBlue;
					theme.TaskBoxChevronDownHover = VLoopIcons.ChevronDownHover_XPBlue;
					theme.TaskBoxChevronUp = VLoopIcons.ChevronUp_XPBlue;
					theme.TaskBoxChevronUpHover = VLoopIcons.ChevronUpHover_XPBlue;
					break;
				case "win2k":
					theme.TaskBoxChevronDown = VLoopIcons.ChevronDown_Win2K;
					theme.TaskBoxChevronDownHover = VLoopIcons.ChevronDownHover_Win2K;
					theme.TaskBoxChevronUp = VLoopIcons.ChevronUp_Win2K;
					theme.TaskBoxChevronUpHover = VLoopIcons.ChevronUpHover_Win2K;
					break;
			}

			//title gradient
			item_node = theme_node.SelectSingleNode(".//titlebar_gradient/left");
			theme.TitleGradientLeftColor = ColorFromNode(item_node);
			item_node = theme_node.SelectSingleNode(".//titlebar_gradient/right");
			theme.TitleGradientRightColor = ColorFromNode(item_node);

			//navbar top gradient
			item_node = theme_node.SelectSingleNode(".//navbar_gradient/top");
			theme.NavBarTopGradient = ColorFromNode(item_node);

			//navbar bottom gradient
			item_node = theme_node.SelectSingleNode(".//navbar_gradient/bottom");
			theme.NavBarBottomGradient = ColorFromNode(item_node);

			//taskbox header gradient
			item_node = theme_node.SelectSingleNode(".//taskbox/header_gradient/left");
			theme.TaskBoxHeaderLeftGradient = ColorFromNode(item_node);
			item_node = theme_node.SelectSingleNode(".//taskbox/header_gradient/right");
			theme.TaskBoxHeaderRightGradient = ColorFromNode(item_node);

			//taskbox base
			item_node = theme_node.SelectSingleNode(".//taskbox/base_color");
			theme.TaskBoxInnerColor = ColorFromNode(item_node);

			//taskbox border
			item_node = theme_node.SelectSingleNode(".//taskbox/border_color");
			theme.TaskBoxBorderColor = ColorFromNode(item_node);

			//taskbox link color
			item_node = theme_node.SelectSingleNode(".//taskbox/link_color");
			theme.TaskNormalColor = ColorFromNode(item_node);

			//taskbox active link color
			item_node = theme_node.SelectSingleNode(".//taskbox/alink_color");
			theme.TaskActiveColor = ColorFromNode(item_node);

			//taskbox header text colors
			item_node = theme_node.SelectSingleNode(".//taskbox/header_font_color");
			theme.TaskBoxHeaderFontColor = ColorFromNode(item_node);
			item_node = theme_node.SelectSingleNode(".//taskbox/aheader_font_color");
			theme.TaskBoxHeaderActiveFontColor = ColorFromNode(item_node);

			//form
			item_node = theme_node.SelectSingleNode(".//form/back_color");
			if(item_node.Attributes["red"].Value != "sys_frm")
				theme.FormBackColor = ColorFromNode(item_node);
			else
				theme.FormBackColor = SystemColors.Control;

			item_node = theme_node.SelectSingleNode(".//form/controls");
			theme.FlatControls = 
				(Convert.ToString(item_node.Attributes["style"].Value) == "flat" ? true : false);
				
			//divider colors
			item_node = theme_node.SelectSingleNode(".//div_gradient/bright");
			theme.DividerLightColor = ColorFromNode(item_node);
			item_node = theme_node.SelectSingleNode(".//div_gradient/dark");
			theme.DividerDarkColor = ColorFromNode(item_node);

			//special page bg colors
			item_node = theme_node.SelectSingleNode(".//form/specialpage_back_color");
			theme.SpecialFormBackColor = ColorFromNode(item_node);
					
			//special page font color
			item_node = theme_node.SelectSingleNode(".//form/specialpage_link_color");
			theme.SpecialFormFontColor = ColorFromNode(item_node);

			item_node = theme_node.SelectSingleNode(".//form/specialpage_font_color");
			theme.SpecialFormHeaderColor = ColorFromNode(item_node);

			return theme;
		}


		public static DisplayTheme ThemeByName(string theme_name, Afni.Applications.VLoop.Application app)
		{
			string path;
			System.Xml.XmlDocument doc;
			System.Xml.XmlNodeList themes;
			System.Xml.XmlNode root;
			XmlNode item_node;
			DisplayTheme theme = null;

			try
			{
				path = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
			
				path += @"\Def.xml";
				doc = new XmlDocument();
				doc.Load(path);
				root = doc.ChildNodes[1];
				themes = root.ChildNodes;
				
				foreach(XmlNode theme_node in themes)
				{
					if(theme_node.Attributes["id"].Value == theme_name)
					{
						theme = ThemeManager.ThemeFromNode(theme_node, app);
						break;
					}
				}

			}
			catch(Exception ex)
			{
				MessageBox.Show("Unable to retrieve theme: " + System.Environment.NewLine + ex.Message.ToString(),"VLoop Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

			return theme;

		}
	}

	public class DisplayTheme
	{
		private string _name;
		private string _id;
		private bool _flat_ctls;
		private Color _title_grad_1;
		private Color _title_grad_2;
		private Color _title_font_clr;
		private Color _nav_grad_top;
		private Color _nav_grad_bottom;
		private Color _taskbox_border;
		private Color _taskbox_inner;
		private Color _taskbox_hdr_grad1;
		private Color _taskbox_hdr_grad2;
		private Color _taskbox_hdr_fontclr;
		private Color _taskbox_hdr_fontclr_a;
		private Color _task_active_clr;
		private Color _task_normal_clr;
		private Color _form_bgclr;
		private Color _div_clr_dark;
		private Color _div_clr_light;
		private Color _specialfrm_bgclr;
		private Color _specialfrm_fontclr;
		private Color _specialfrm_hdrclr;
		private Icon _chevron_down;
		private Icon _chevron_up;
		private Icon _chevron_down_hover;
		private Icon _chevron_up_hover;
		
		public DisplayTheme()
		{
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public override string ToString()
		{
			return _name;
		}

		public string ID
		{
			get { return _id; }
			set { _id = value; }
		}

		public Color FormBackColor
		{
			get { return _form_bgclr; }
			set { _form_bgclr = value; }
		}

		public bool FlatControls
		{
			get { return _flat_ctls; }
			set { _flat_ctls = value; }
		}

		public Color TitleGradientLeftColor
		{
			get { return _title_grad_1; }
			set { _title_grad_1 = value; }
		}

		public Color TitleGradientRightColor
		{
			get { return _title_grad_2; }
			set { _title_grad_2 = value; }
		}

		public Color TitleFontColor
		{
			get { return _title_font_clr; }
			set { _title_font_clr = value; }
		}

		public Color NavBarTopGradient
		{
			get { return _nav_grad_top; }
			set { _nav_grad_top = value; }
		}

		public Color NavBarBottomGradient
		{
			get { return _nav_grad_bottom; }
			set { _nav_grad_bottom = value; }
		}

		public Color TaskBoxBorderColor
		{
			get { return _taskbox_border; }
			set { _taskbox_border = value; }
		}

		public Color TaskBoxInnerColor
		{
			get { return _taskbox_inner; }
			set { _taskbox_inner = value; }
		}

		public Color TaskBoxHeaderLeftGradient
		{
			get { return _taskbox_hdr_grad1; }
			set { _taskbox_hdr_grad1 = value; }
		}

		public Color TaskBoxHeaderRightGradient
		{
			get { return _taskbox_hdr_grad2; }
			set { _taskbox_hdr_grad2 = value; }
		}
		
		public Color TaskBoxHeaderFontColor
		{
			get { return _taskbox_hdr_fontclr; }
			set { _taskbox_hdr_fontclr = value; }
		}

		public Color TaskBoxHeaderActiveFontColor
		{
			get { return _taskbox_hdr_fontclr_a;}
			set { _taskbox_hdr_fontclr_a = value; }
		}

		public Color TaskActiveColor
		{
			get { return _task_active_clr; }
			set { _task_active_clr = value; }
		}

		public Color TaskNormalColor
		{
			get { return _task_normal_clr; }
			set { _task_normal_clr = value; }
		}

		public Color DividerDarkColor
		{
			get { return _div_clr_dark; }
			set { _div_clr_dark = value; }
		}

		public Color DividerLightColor
		{
			get { return _div_clr_light; }
			set { _div_clr_light = value; }
		}

		public Color SpecialFormBackColor
		{
			get { return _specialfrm_bgclr; }
			set { _specialfrm_bgclr = value; }
		}

		public Color SpecialFormFontColor
		{
			get { return _specialfrm_fontclr; }
			set { _specialfrm_fontclr = value; }
		}

		public Color SpecialFormHeaderColor
		{
			get { return _specialfrm_hdrclr; }
			set { _specialfrm_hdrclr = value; }
		}

		public Icon TaskBoxChevronUp
		{
			get { return _chevron_up; }
			set { _chevron_up = value; }
		}

		public Icon TaskBoxChevronDown
		{
			get { return _chevron_down; }
			set { _chevron_down = value; }
		}

		public Icon TaskBoxChevronUpHover
		{
			get { return _chevron_up_hover;}
			set { _chevron_up_hover = value; }
		}

		public Icon TaskBoxChevronDownHover
		{
			get { return _chevron_down_hover; }
			set { _chevron_down_hover = value; }
		}
	}
}
