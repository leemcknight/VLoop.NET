using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Afni.DataUtility;
using Afni.FormData;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopBusinessObjects;

namespace Afni.Applications.VLoop
{
	/// <summary>
	/// Summary description for ctlOrders.
	/// </summary>
	public class ctlOrders : System.Windows.Forms.UserControl , IForm
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TreeView _tvwOrders;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListView _lvwOrderEvent;
		private FormStates _form_state;
		private ArrayList _orders;
		private Afni.Applications.VLoop.Application _app;

		public ctlOrders(Afni.Applications.VLoop.Application app)
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

		#region Private Methods
		private void RebuildTVW()
		{
			TreeNode queueNode;
			TreeNode orderNode;
			TreeNode detailNode;
			OrderQueue queue;
			IManager bso = new QueueBSO();
			ArrayList queues;
			queues = (ArrayList)bso.GetByParentKey((int)_app.CurrentCampaign.CampaignID);

			//clear the tree
			_tvwOrders.Nodes.Clear();
			
			//add the root node
			TreeNode rootNode = new TreeNode();
			rootNode.Text = "Orders";
			_tvwOrders.Nodes.Add(rootNode);

			//add the orders
			foreach(Order order in _orders)
			{
				orderNode = new TreeNode();
				orderNode.Text = order.OrderDate.ToShortDateString();
				orderNode.Tag = order;
				queue = QueueFromID(order.CurrentQueueID);
				queueNode = TreeNodeFromKey(queue);
				if(queueNode == null)
				{
					queueNode = new TreeNode();
					queueNode.Text = queue.OrderQueueName;
					queueNode.Tag = queue;
					rootNode.Nodes.Add(queueNode);
				}

				queueNode.Nodes.Add(orderNode);

				foreach(OrderDetail detail in order.OrderDetails)
				{
					detailNode = new TreeNode();
					detailNode.Text = detail.Description;
					detailNode.Tag = detail;
					orderNode.Nodes.Add(detailNode);
				}
			}

		}

		private TreeNode TreeNodeFromKey(object key)
		{
			TreeNode found_node = null;
			foreach(TreeNode node in _tvwOrders.Nodes)
			{
				if(node.Tag == key)
				{
					found_node = node;
					break;
				}
			}

			return found_node;
		}

		private OrderQueue QueueFromID(long queueID)
		{
			OrderQueue foundQueue = null;
			foreach(OrderQueue queue in _app.CurrentCampaign.OrderQueues)
			{
				if(queue.OrderQueueID == queueID)
				{
					foundQueue  = queue;
					break;
				}
			}

			return foundQueue;
		}
		#endregion


		#region IForm implementation
		bool IForm.Refresh()
		{
			//reset the reference to the order collection
			_orders = _app.Call.CurrentCustCampaign.Customer.Account.Orders;

			RebuildTVW();

			return true;
		}

		bool IForm.ShowHelp()
		{
			return true;
		}

		string IForm.Name
		{
			get { return "VLoop Orders"; }
			set {}
		}

		FormStates IForm.FormState
		{
			get { return _form_state; }
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._tvwOrders = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this._lvwOrderEvent = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// _tvwOrders
			// 
			this._tvwOrders.Dock = System.Windows.Forms.DockStyle.Left;
			this._tvwOrders.ImageIndex = -1;
			this._tvwOrders.Location = new System.Drawing.Point(2, 2);
			this._tvwOrders.Name = "_tvwOrders";
			this._tvwOrders.SelectedImageIndex = -1;
			this._tvwOrders.Size = new System.Drawing.Size(160, 492);
			this._tvwOrders.TabIndex = 0;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(162, 2);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(5, 492);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(167, 2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(431, 32);
			this.panel1.TabIndex = 3;
			// 
			// _lvwOrderEvent
			// 
			this._lvwOrderEvent.Dock = System.Windows.Forms.DockStyle.Fill;
			this._lvwOrderEvent.Location = new System.Drawing.Point(167, 34);
			this._lvwOrderEvent.Name = "_lvwOrderEvent";
			this._lvwOrderEvent.Size = new System.Drawing.Size(431, 460);
			this._lvwOrderEvent.TabIndex = 4;
			// 
			// ctlOrders
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._lvwOrderEvent,
																		  this.panel1,
																		  this.splitter1,
																		  this._tvwOrders});
			this.DockPadding.All = 2;
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ctlOrders";
			this.Size = new System.Drawing.Size(600, 496);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
