using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.FormData;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for CCDetailsCtl.
	/// </summary>
	public class CCDetailsCtl : UserControl, IForm, ISkinnable
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button ctnCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private FormStates _form_state = FormStates.Idle;
		private Afni.Applications.VLoop.DisplayTheme _theme;
		private Afni.Applications.VLoop.Application _app;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CCDetailsCtl(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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
						btnBack.FlatStyle = FlatStyle.Flat;
						btnNext.FlatStyle = FlatStyle.Flat;
					}
					else
					{
						btnBack.FlatStyle = FlatStyle.Standard;
						btnNext.FlatStyle = FlatStyle.Standard;
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
			this.btnNext = new System.Windows.Forms.Button();
			this.btnBack = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.ctnCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(8, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(456, 80);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter the Calling Card Plan Details";
			// 
			// btnNext
			// 
			this.btnNext.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnNext.Location = new System.Drawing.Point(328, 368);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(72, 24);
			this.btnNext.TabIndex = 1;
			this.btnNext.Text = "&Next >";
			// 
			// btnBack
			// 
			this.btnBack.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnBack.Location = new System.Drawing.Point(264, 368);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(64, 24);
			this.btnBack.TabIndex = 2;
			this.btnBack.Text = "< &Back";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.label2.Location = new System.Drawing.Point(16, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(448, 64);
			this.label2.TabIndex = 3;
			this.label2.Text = "Enter the following details for the calling card plan and press \"Next\" to continu" +
				"e.";
			// 
			// ctnCancel
			// 
			this.ctnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.ctnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ctnCancel.Location = new System.Drawing.Point(408, 368);
			this.ctnCancel.Name = "ctnCancel";
			this.ctnCancel.Size = new System.Drawing.Size(64, 24);
			this.ctnCancel.TabIndex = 4;
			this.ctnCancel.Text = "&Cancel";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.groupBox1.Location = new System.Drawing.Point(8, 344);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(464, 8);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			// 
			// CCDetailsCtl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox1,
																		  this.ctnCancel,
																		  this.label2,
																		  this.btnBack,
																		  this.btnNext,
																		  this.label1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "CCDetailsCtl";
			this.Size = new System.Drawing.Size(480, 408);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
