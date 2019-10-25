using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;
using Afni.Applications.VLoop;
using Afni.Applications.VLoop.Commands;
using Afni.Applications.VLoop.Viewing;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for NewPlanWizPlanTypeCtl.
	/// </summary>
	public class NewPlanWizPlanTypeCtl : UserControl, IForm, ISkinnable
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox lstPlan;
		private System.Windows.Forms.Label lblInstructions;
		private System.Windows.Forms.Button btnNext;
		private FormStates _form_state = FormStates.Idle;
		private Afni.Applications.VLoop.Application _app;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.Button btnCancel;
		private Afni.Applications.VLoop.DisplayTheme _theme;

		public NewPlanWizPlanTypeCtl(Afni.Applications.VLoop.Application app)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			_app = app;

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

		#region IForm implementation
		bool IForm.Refresh()
		{
			return true;
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		string IForm.Name
		{
			get { return "New Plan Confirmation"; }
			set {}
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
			try
			{
				this.BackColor = theme.FormBackColor;

				if(!_app.IsOnXP)
				{
					if(theme.FlatControls)
					{
						lstPlan.BorderStyle = BorderStyle.FixedSingle;
						btnNext.FlatStyle = FlatStyle.Flat;
						btnBack.FlatStyle = FlatStyle.Flat;
						btnCancel.FlatStyle = FlatStyle.Flat;
					}
					else
					{
						lstPlan.BorderStyle = BorderStyle.Fixed3D;
						btnNext.FlatStyle = FlatStyle.Standard;
						btnBack.FlatStyle = FlatStyle.Standard;
						btnCancel.FlatStyle = FlatStyle.Standard;
					}
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
			this.label1 = new System.Windows.Forms.Label();
			this.lstPlan = new System.Windows.Forms.ListBox();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnBack = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblInstructions = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(488, 32);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select a Toll Free Plan";
			// 
			// lstPlan
			// 
			this.lstPlan.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lstPlan.Location = new System.Drawing.Point(8, 88);
			this.lstPlan.Name = "lstPlan";
			this.lstPlan.Size = new System.Drawing.Size(552, 394);
			this.lstPlan.TabIndex = 1;
			// 
			// btnNext
			// 
			this.btnNext.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnNext.Location = new System.Drawing.Point(392, 512);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(80, 24);
			this.btnNext.TabIndex = 2;
			this.btnNext.Text = "&Next >";
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// btnBack
			// 
			this.btnBack.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnBack.Location = new System.Drawing.Point(312, 512);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(80, 24);
			this.btnBack.TabIndex = 3;
			this.btnBack.Text = "< &Back";
			this.btnBack.Click += new System.EventHandler(this.button2_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(480, 512);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 24);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "&Cancel";
			// 
			// lblInstructions
			// 
			this.lblInstructions.Location = new System.Drawing.Point(16, 64);
			this.lblInstructions.Name = "lblInstructions";
			this.lblInstructions.Size = new System.Drawing.Size(352, 16);
			this.lblInstructions.TabIndex = 5;
			this.lblInstructions.Text = "Select a Toll Free Plan from the list and click \"Next\".";
			// 
			// NewPlanWizPlanTypeCtl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lblInstructions,
																		  this.btnCancel,
																		  this.btnBack,
																		  this.btnNext,
																		  this.lstPlan,
																		  this.label1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "NewPlanWizPlanTypeCtl";
			this.Size = new System.Drawing.Size(568, 552);
			this.ResumeLayout(false);

		}
		#endregion



		private void btnNext_Click(object sender, System.EventArgs e)
		{
			Viewing.View view;

			view = (Viewing.View)_app.Views[ViewTypes.NewPlanFinal];
			_app.LoadView(view);	
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			_app.MovePrevView();
		}
	}
}
