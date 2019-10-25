using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;
using System.Drawing.Drawing2D;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.ControlPlacer;
using Afni.ControlPlacer.Questions;

namespace Afni.Applications.VLoop 
{
	/// <summary>
	/// Summary description for DispositionCtl.
	/// </summary>
	public class DispositionCtl : UserControl, ISaveable, ISkinnable
	{
		private System.Windows.Forms.Label lblRepName;
		private System.Windows.Forms.TextBox txtRepName;
		private System.Windows.Forms.Label lblContactType;
		private System.Windows.Forms.ComboBox cboCallStatus;
		private System.Windows.Forms.Label lblCallResult;
		private System.Windows.Forms.ComboBox cboCallResult;
		private System.Windows.Forms.Label lblTransferType;
		private System.Windows.Forms.ComboBox cboTransferType;
		private System.Windows.Forms.Label lblDisposition;
		private System.Windows.Forms.ComboBox cboDisposition;
		private System.Windows.Forms.Label lblAddlInfo;
		private System.Windows.Forms.Label lblComments;
		private System.Windows.Forms.TextBox txtComments;
		private Afni.FormData.FormStates _form_state;
		private System.Windows.Forms.Label lblCampaign;
		private System.Windows.Forms.TextBox txtCampaign;
		private System.Windows.Forms.Label lblCallDate;
		private System.Windows.Forms.TextBox txtCallDate;
		private System.Timers.Timer _disp_timer;
		private int _disp_seconds;
		private int _disp_ticks;
		private int _ticks = 0;
		private int _seconds = 0;
		private Afni.Controls.AfniDivider afniDivider1;
		private Afni.Applications.VLoop.DisplayTheme _theme;
		private System.Windows.Forms.CheckBox chkNoAction;
		private Afni.Applications.VLoop.Application _app;
		private Afni.ControlPlacer.Area _dyn_area;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DispositionCtl(Afni.Applications.VLoop.Application app)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			_disp_timer = new System.Timers.Timer();
			_disp_timer.Interval = 100;
			_disp_timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
			_disp_seconds = 120;
			_disp_ticks = _disp_seconds * 10;
			_app = app;
			_app.CampaignSwitched += new EventHandler(app_CampaignChanged);
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

		#region Private methods
		private void PaintTimer()
		{
			int ctl_width;
			int rect_width;
			double disp_perc;
			double rect_width_d;

			ctl_width = this.Width;
			
			disp_perc = (double)_ticks/(double)_disp_ticks;

			rect_width_d = disp_perc * ctl_width;

			rect_width = (int)rect_width_d;
			rect_width++;

			Rectangle rect = new Rectangle(new Point(0,0),
									new Size(rect_width, 20));


			Graphics g = Graphics.FromHwnd(this.Handle);
			PointF text_loc = new PointF(this.Width / 2, 3);
			System.Drawing.SolidBrush brush = new SolidBrush(Color.CornflowerBlue);
			g.FillRectangle(brush,rect);
			g.DrawString(_seconds.ToString(),this.Font,Brushes.White,text_loc);
		}

		private void RefreshDynCtls()
		{
			_dyn_area = new Area(2,this,new Point(10,300));
			Afni.ControlPlacer.Questions.Question dyn_q;
			QuestionTypes type;
			ILookupQuestion lookup;

			foreach(CampaignQuestion cq in _app.CurrentCampaign.CampaignQuestions)
			{
				//convert the vloop control type ID to a control placer
				//question type enum
				type = VLoopDynControlTypes.FromControlTypeID(cq.QuestionTypeID);

				//create the question
				dyn_q = _dyn_area.AddQuestion(cq.QuestionID,
											cq.QuestionText,
											type,
											cq.Sequence);

				foreach(AvailableLookupInfo ali in cq.AvailableLookups)
				{
					//cast to an ILookup
					lookup = (ILookupQuestion)dyn_q;

					//add the lookup
					lookup.AddQuestionData(ali.AvailableLookupInfoID,
											ali.LookupValue);
				}
			}

			_dyn_area.Draw();
	
		}
		#endregion

		#region IForm Implementation
		bool IForm.Refresh()
		{
			_disp_timer.Enabled = true;
			return true;
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		string IForm.Name
		{
			get { return "Call Disposition";}
			set {}
		}

		FormStates IForm.FormState
		{
			get { return _form_state; }
		}
		#endregion

		#region ISaveable Implementation
		bool ISaveable.Save()
		{
			_form_state = FormStates.Idle;
			if(SaveSucceeded != null)
				SaveSucceeded(this,null);
			return true;
		}

		bool ISaveable.Undo()
		{
			_form_state = FormStates.Idle;
			if(Undone != null)
				Undone(this,null);
			return true;
		}

		string ISaveable.SaveButtonText
		{
			get { return "Save Disposition"; }
			set {}
		}

		public event EventHandler Dirtied;
		public event EventHandler SaveFailed;
		public event EventHandler SaveSucceeded;
		public event EventHandler Undone;
		#endregion

		#region ISkinnable implementation
		bool ISkinnable.ApplyTheme(DisplayTheme theme)
		{
			bool theme_ok = true;
			afniDivider1.FirstColor = theme.DividerLightColor;
			afniDivider1.SecondColor = theme.DividerDarkColor;
			try
			{
				this.BackColor = theme.FormBackColor;
				if(theme.FlatControls)
				{
					txtComments.BorderStyle = BorderStyle.FixedSingle;
					chkNoAction.FlatStyle = FlatStyle.Flat;
				}
				else
				{
					txtComments.BorderStyle = BorderStyle.Fixed3D;
					chkNoAction.FlatStyle = FlatStyle.Standard;
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
			this.lblRepName = new System.Windows.Forms.Label();
			this.txtRepName = new System.Windows.Forms.TextBox();
			this.lblContactType = new System.Windows.Forms.Label();
			this.cboCallStatus = new System.Windows.Forms.ComboBox();
			this.lblCallResult = new System.Windows.Forms.Label();
			this.cboCallResult = new System.Windows.Forms.ComboBox();
			this.lblTransferType = new System.Windows.Forms.Label();
			this.cboTransferType = new System.Windows.Forms.ComboBox();
			this.lblDisposition = new System.Windows.Forms.Label();
			this.cboDisposition = new System.Windows.Forms.ComboBox();
			this.lblAddlInfo = new System.Windows.Forms.Label();
			this.chkNoAction = new System.Windows.Forms.CheckBox();
			this.lblComments = new System.Windows.Forms.Label();
			this.txtComments = new System.Windows.Forms.TextBox();
			this.lblCampaign = new System.Windows.Forms.Label();
			this.txtCampaign = new System.Windows.Forms.TextBox();
			this.lblCallDate = new System.Windows.Forms.Label();
			this.txtCallDate = new System.Windows.Forms.TextBox();
			this.afniDivider1 = new Afni.Controls.AfniDivider();
			this.SuspendLayout();
			// 
			// lblRepName
			// 
			this.lblRepName.Location = new System.Drawing.Point(8, 128);
			this.lblRepName.Name = "lblRepName";
			this.lblRepName.Size = new System.Drawing.Size(64, 16);
			this.lblRepName.TabIndex = 0;
			this.lblRepName.Text = "Rep Name:";
			// 
			// txtRepName
			// 
			this.txtRepName.BackColor = System.Drawing.Color.White;
			this.txtRepName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtRepName.Enabled = false;
			this.txtRepName.Location = new System.Drawing.Point(80, 128);
			this.txtRepName.Name = "txtRepName";
			this.txtRepName.Size = new System.Drawing.Size(120, 14);
			this.txtRepName.TabIndex = 1;
			this.txtRepName.Text = "";
			// 
			// lblContactType
			// 
			this.lblContactType.Location = new System.Drawing.Point(232, 32);
			this.lblContactType.Name = "lblContactType";
			this.lblContactType.Size = new System.Drawing.Size(80, 16);
			this.lblContactType.TabIndex = 2;
			this.lblContactType.Text = "Contact Type:";
			// 
			// cboCallStatus
			// 
			this.cboCallStatus.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.cboCallStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCallStatus.Location = new System.Drawing.Point(328, 32);
			this.cboCallStatus.Name = "cboCallStatus";
			this.cboCallStatus.Size = new System.Drawing.Size(112, 21);
			this.cboCallStatus.TabIndex = 3;
			// 
			// lblCallResult
			// 
			this.lblCallResult.Location = new System.Drawing.Point(232, 64);
			this.lblCallResult.Name = "lblCallResult";
			this.lblCallResult.Size = new System.Drawing.Size(72, 16);
			this.lblCallResult.TabIndex = 4;
			this.lblCallResult.Text = "Call Result:";
			// 
			// cboCallResult
			// 
			this.cboCallResult.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.cboCallResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCallResult.Location = new System.Drawing.Point(328, 64);
			this.cboCallResult.Name = "cboCallResult";
			this.cboCallResult.Size = new System.Drawing.Size(112, 21);
			this.cboCallResult.TabIndex = 5;
			// 
			// lblTransferType
			// 
			this.lblTransferType.Location = new System.Drawing.Point(232, 96);
			this.lblTransferType.Name = "lblTransferType";
			this.lblTransferType.Size = new System.Drawing.Size(80, 16);
			this.lblTransferType.TabIndex = 6;
			this.lblTransferType.Text = "Transfer Type:";
			// 
			// cboTransferType
			// 
			this.cboTransferType.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.cboTransferType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTransferType.Location = new System.Drawing.Point(328, 96);
			this.cboTransferType.Name = "cboTransferType";
			this.cboTransferType.Size = new System.Drawing.Size(112, 21);
			this.cboTransferType.TabIndex = 7;
			// 
			// lblDisposition
			// 
			this.lblDisposition.Location = new System.Drawing.Point(232, 128);
			this.lblDisposition.Name = "lblDisposition";
			this.lblDisposition.Size = new System.Drawing.Size(80, 16);
			this.lblDisposition.TabIndex = 8;
			this.lblDisposition.Text = "Disposition:";
			// 
			// cboDisposition
			// 
			this.cboDisposition.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.cboDisposition.Location = new System.Drawing.Point(328, 128);
			this.cboDisposition.Name = "cboDisposition";
			this.cboDisposition.Size = new System.Drawing.Size(112, 21);
			this.cboDisposition.TabIndex = 9;
			// 
			// lblAddlInfo
			// 
			this.lblAddlInfo.Location = new System.Drawing.Point(8, 280);
			this.lblAddlInfo.Name = "lblAddlInfo";
			this.lblAddlInfo.Size = new System.Drawing.Size(128, 16);
			this.lblAddlInfo.TabIndex = 10;
			this.lblAddlInfo.Text = "Additional Information";
			// 
			// chkNoAction
			// 
			this.chkNoAction.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkNoAction.Location = new System.Drawing.Point(16, 32);
			this.chkNoAction.Name = "chkNoAction";
			this.chkNoAction.Size = new System.Drawing.Size(192, 16);
			this.chkNoAction.TabIndex = 12;
			this.chkNoAction.Text = "&No Action on Contact";
			// 
			// lblComments
			// 
			this.lblComments.Location = new System.Drawing.Point(8, 160);
			this.lblComments.Name = "lblComments";
			this.lblComments.Size = new System.Drawing.Size(72, 16);
			this.lblComments.TabIndex = 13;
			this.lblComments.Text = "Comments:";
			// 
			// txtComments
			// 
			this.txtComments.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtComments.Location = new System.Drawing.Point(80, 160);
			this.txtComments.Multiline = true;
			this.txtComments.Name = "txtComments";
			this.txtComments.Size = new System.Drawing.Size(360, 104);
			this.txtComments.TabIndex = 14;
			this.txtComments.Text = "";
			this.txtComments.TextChanged += new System.EventHandler(this.txtComments_TextChanged);
			// 
			// lblCampaign
			// 
			this.lblCampaign.Location = new System.Drawing.Point(8, 64);
			this.lblCampaign.Name = "lblCampaign";
			this.lblCampaign.Size = new System.Drawing.Size(64, 16);
			this.lblCampaign.TabIndex = 15;
			this.lblCampaign.Text = "Campaign:";
			// 
			// txtCampaign
			// 
			this.txtCampaign.BackColor = System.Drawing.Color.White;
			this.txtCampaign.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtCampaign.Enabled = false;
			this.txtCampaign.Location = new System.Drawing.Point(80, 64);
			this.txtCampaign.Name = "txtCampaign";
			this.txtCampaign.Size = new System.Drawing.Size(120, 14);
			this.txtCampaign.TabIndex = 16;
			this.txtCampaign.Text = "";
			// 
			// lblCallDate
			// 
			this.lblCallDate.Location = new System.Drawing.Point(8, 96);
			this.lblCallDate.Name = "lblCallDate";
			this.lblCallDate.Size = new System.Drawing.Size(64, 16);
			this.lblCallDate.TabIndex = 17;
			this.lblCallDate.Text = "Call Date:";
			// 
			// txtCallDate
			// 
			this.txtCallDate.BackColor = System.Drawing.Color.White;
			this.txtCallDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtCallDate.Enabled = false;
			this.txtCallDate.Location = new System.Drawing.Point(80, 96);
			this.txtCallDate.Name = "txtCallDate";
			this.txtCallDate.Size = new System.Drawing.Size(120, 14);
			this.txtCallDate.TabIndex = 18;
			this.txtCallDate.Text = "";
			// 
			// afniDivider1
			// 
			this.afniDivider1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.afniDivider1.DividerStyle = Afni.Controls.DividerStyles.Horizontal;
			this.afniDivider1.FirstColor = System.Drawing.Color.FromArgb(((System.Byte)(1)), ((System.Byte)(72)), ((System.Byte)(178)));
			this.afniDivider1.Location = new System.Drawing.Point(144, 288);
			this.afniDivider1.Name = "afniDivider1";
			this.afniDivider1.SecondColor = System.Drawing.Color.White;
			this.afniDivider1.Size = new System.Drawing.Size(296, 1);
			this.afniDivider1.TabIndex = 19;
			this.afniDivider1.Text = "afniDivider1";
			// 
			// DispositionCtl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.afniDivider1,
																		  this.txtCallDate,
																		  this.lblCallDate,
																		  this.txtCampaign,
																		  this.lblCampaign,
																		  this.txtComments,
																		  this.lblComments,
																		  this.chkNoAction,
																		  this.lblAddlInfo,
																		  this.cboDisposition,
																		  this.lblDisposition,
																		  this.cboTransferType,
																		  this.lblTransferType,
																		  this.cboCallResult,
																		  this.lblCallResult,
																		  this.cboCallStatus,
																		  this.lblContactType,
																		  this.txtRepName,
																		  this.lblRepName});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "DispositionCtl";
			this.Size = new System.Drawing.Size(448, 496);
			this.ResumeLayout(false);

		}
		#endregion

		#region Events
		private void txtComments_TextChanged(object sender, System.EventArgs e)
		{
			if(_form_state != FormStates.EditInProgress)
			{
				_form_state = FormStates.EditInProgress;
				if(Dirtied != null)
					Dirtied(this,null);
			}
		}

		private void OnTimer(object sender, System.Timers.ElapsedEventArgs e)
		{
			_ticks++;
			_seconds = _ticks / 10;
			PaintTimer();
		}

		private void app_CampaignChanged(object sender, System.EventArgs e)
		{
			RefreshDynCtls();
		}
		#endregion
	}
}
