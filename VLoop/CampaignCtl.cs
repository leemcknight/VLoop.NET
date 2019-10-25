using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopBusinessObjects;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;
using Afni.Controls;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for CampaignCtl.
	/// </summary>
	public class CampaignCtl : System.Windows.Forms.UserControl, IForm, ISkinnable
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ImageList imglstCampaign;
		private System.ComponentModel.IContainer components;
		private FormStates _form_state = FormStates.Idle;
		private Afni.Applications.VLoop.DisplayTheme _theme;
		private Afni.Applications.VLoop.Application _app;
		private ArrayList _links;

		public CampaignCtl(Afni.Applications.VLoop.Application app)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			_app = app;
			_links = new ArrayList();
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

		private void OnCampaignClick(object sender, EventArgs e)
		{
			AfniLink link = (AfniLink)sender;
			_app.CurrentCampaign = (Campaign)link.Tag;
		}

		#region IForm implementation
		bool IForm.Refresh()
		{
			ArrayList campaigns;
			IManager bso = new CampaignBSO();
			DisplayTheme theme = _app.Theme;
			int row  = 1;
			int rows = 0;
			int col = 1;
			int y_offset = 50;
			int first_column_width = 0;
			Graphics g = Graphics.FromHwnd(this.Handle);

			//clear out the old links
			foreach(AfniLink link in _links)
			{
				this.Controls.Remove(link);
			}

			_links.Clear();
			
			campaigns = (ArrayList)bso.GetAll();
			rows = campaigns.Count / 2;
			foreach(Campaign campaign in campaigns)
			{
				AfniLink link = new AfniLink();
				link.Text = campaign.CampaignName;
				link.Height = 20;
				link.Width = (int)g.MeasureString(link.Text,link.Font).Width + 50;
				if(link.Width > first_column_width)
					first_column_width = link.Width;
				link.Tag = campaign;
				link.Icon = VLoopIcons.Next;
				link.ForeColor = theme.SpecialFormFontColor;
				link.LinkColor = theme.SpecialFormFontColor;
				link.ActiveLinkColor = theme.SpecialFormFontColor;
				link.Left = (col == 1 ? 20 : first_column_width + 50);
				link.Top = y_offset + (row * (link.Height + 10));
				link.LinkClicked += new EventHandler(this.OnCampaignClick);
				this.Controls.Add(link);
				_links.Add(link);
				
				if(row == rows)
				{
					col = 2;
					row = 1;
				}
				else
					row++;
			}
			

			return true;
		}

		string IForm.Name
		{
			get { return "Campaign"; }
			set {}
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		FormStates IForm.FormState
		{
			get { return _form_state; }
		}

		#endregion

		#region ISkinnable implementation
		bool ISkinnable.ApplyTheme(DisplayTheme theme)
		{
			bool theme_ok = true;
			label1.ForeColor = theme.SpecialFormHeaderColor;
			try
			{
				this.BackColor = theme.SpecialFormBackColor;
				foreach(AfniLink link in _links)
				{
					link.ForeColor = theme.SpecialFormFontColor;
					link.LinkColor = theme.SpecialFormFontColor;
					link.ActiveLinkColor = theme.SpecialFormFontColor;
				}
		
				_theme = theme;
			}
			catch
			{
				theme_ok = false;
			}

			return theme_ok;
		}

		DisplayTheme ISkinnable.CurrentTheme
		{
			get { return _theme; }
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CampaignCtl));
			this.label1 = new System.Windows.Forms.Label();
			this.imglstCampaign = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.label1.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(214)), ((System.Byte)(223)), ((System.Byte)(245)));
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(576, 40);
			this.label1.TabIndex = 0;
			this.label1.Text = "Choose a Campaign.";
			// 
			// imglstCampaign
			// 
			this.imglstCampaign.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imglstCampaign.ImageSize = new System.Drawing.Size(16, 16);
			this.imglstCampaign.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstCampaign.ImageStream")));
			this.imglstCampaign.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// CampaignCtl
			// 
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(99)), ((System.Byte)(117)), ((System.Byte)(214)));
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "CampaignCtl";
			this.Size = new System.Drawing.Size(600, 520);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
