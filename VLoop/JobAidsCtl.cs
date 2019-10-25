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
	/// Summary description for JobAidsCtl.
	/// </summary>
	public class JobAidsCtl : System.Windows.Forms.UserControl , IForm
	{
		private FormStates _form_state= FormStates.Idle;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public JobAidsCtl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//axWebBrowser1.Navigate2("http://www.afninet.com",null,null,null,null);

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

		bool IForm.Refresh()
		{
			return true;
		}

		string IForm.Name
		{
			get { return "Job Aids"; }
			set {}
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		FormStates IForm.FormState
		{
			get {return _form_state; }
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// JobAidsCtl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Name = "JobAidsCtl";
			this.Size = new System.Drawing.Size(488, 488);

		}
		#endregion
	}
}
