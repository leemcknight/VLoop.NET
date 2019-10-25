using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Afni.Applications.VLoopMaintenance
{
	public enum PropertyGroups
	{
		General,
		Dispositions,
		TransferTypes,
		ContactTypes,
		CallResults,
		Products,
		Dialer,
		Sales
	}
	
	public class frmCampProperties : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TreeView _tvwProperties;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Panel _pnlProp;
		private UserControl _currentControl = null;
		private ctlGeneral _generalControl = new ctlGeneral();
		private ctlDialer _dialerControl = new ctlDialer();
		private ctlOrders _ordersControl = new ctlOrders();

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmCampProperties()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			InitializeUserControls();
			BuildTVW();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmCampProperties));
			this._tvwProperties = new System.Windows.Forms.TreeView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this._pnlProp = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// _tvwProperties
			// 
			this._tvwProperties.ImageIndex = -1;
			this._tvwProperties.Location = new System.Drawing.Point(8, 8);
			this._tvwProperties.Name = "_tvwProperties";
			this._tvwProperties.Scrollable = false;
			this._tvwProperties.SelectedImageIndex = -1;
			this._tvwProperties.ShowLines = false;
			this._tvwProperties.ShowPlusMinus = false;
			this._tvwProperties.ShowRootLines = false;
			this._tvwProperties.Size = new System.Drawing.Size(160, 296);
			this._tvwProperties.TabIndex = 0;
			this._tvwProperties.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._tvwProperties_AfterSelect);
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(176, 300);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(408, 5);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(496, 312);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 24);
			this.button1.TabIndex = 2;
			this.button1.Text = "&Cancel";
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button2.Location = new System.Drawing.Point(408, 312);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(80, 24);
			this.button2.TabIndex = 3;
			this.button2.Text = "&OK";
			// 
			// groupBox2
			// 
			this.groupBox2.Location = new System.Drawing.Point(176, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(408, 8);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			// 
			// _pnlProp
			// 
			this._pnlProp.Location = new System.Drawing.Point(176, 16);
			this._pnlProp.Name = "_pnlProp";
			this._pnlProp.Size = new System.Drawing.Size(408, 280);
			this._pnlProp.TabIndex = 5;
			// 
			// frmCampProperties
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(592, 349);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._pnlProp,
																		  this.groupBox2,
																		  this.button2,
																		  this.button1,
																		  this.groupBox1,
																		  this._tvwProperties});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmCampProperties";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "frmCampProperties";
			this.ResumeLayout(false);

		}
		#endregion

		private void BuildTVW()
		{
			AddTreeNode("General",PropertyGroups.General);
			AddTreeNode("Products",PropertyGroups.Products);
			AddTreeNode("Transfer Types", PropertyGroups.TransferTypes);
			AddTreeNode("Contact Types", PropertyGroups.ContactTypes);
			AddTreeNode("Call Results", PropertyGroups.CallResults);
			AddTreeNode("Dispositions", PropertyGroups.Dispositions);
			AddTreeNode("Dialer", PropertyGroups.Dialer);
			AddTreeNode("Sales", PropertyGroups.Sales);
		}

		private void InitializeUserControls()
		{
			//default control
			_generalControl.Parent = _pnlProp;
			_generalControl.Dock = DockStyle.Fill;
			_generalControl.Show();
			_currentControl = _generalControl;

			//dialer
			_dialerControl.Parent = _pnlProp;
			_dialerControl.Dock = DockStyle.Fill;

			//orders
			_ordersControl.Parent = _pnlProp;
			_ordersControl.Dock = DockStyle.Fill;

		}

		private void AddTreeNode(string Text, PropertyGroups Group)
		{
			TreeNode node;
			node = _tvwProperties.Nodes.Add(Text);
			node.Tag = Group;
			
		}

		private void SwapToGroup(PropertyGroups newGroup)
		{
			UserControl ctl;

			switch(newGroup)
			{
				case PropertyGroups.General:
					if(_generalControl != _currentControl)
					{
						_currentControl.Hide();
						_generalControl.Show();
						_currentControl = _generalControl;
					}
					break;
				case PropertyGroups.Dialer:
					if(_dialerControl != _currentControl)
					{
						_currentControl.Hide();
						_dialerControl.Show();
						_currentControl = _dialerControl;
					}
					break;
				case PropertyGroups.Sales:
					if(_ordersControl != _currentControl)
					{
						_currentControl.Hide();
						_ordersControl.Show();
						_currentControl = _ordersControl;
					}
					break;
			}
		}


		private void _tvwProperties_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			PropertyGroups selectedGroup;

			selectedGroup = (PropertyGroups)e.Node.Tag;
			SwapToGroup(selectedGroup);
			
		}

		
	}
}
