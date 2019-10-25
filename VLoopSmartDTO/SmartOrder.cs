using System;
using System.Collections;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopBusinessObjects;

namespace Afni.Applications.VLoop.VLoopSmartDTO
{
	/// <summary>
	/// Summary description for SmartOrder.
	/// </summary>
	public class Order: OrderBase
	{
		private ArrayList _details;
		private OrderQueue _queue;

		public Order()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ArrayList OrderDetails
		{
			get
			{
				if(_details == null)
				{
					DataSet ds;
					_details = new ArrayList();
					ds = OrderDetailServer.GetByOrderID(_pk);
					foreach(DataRow row in ds.Tables["tblOrderDetail"].Rows)
					{
						_details.Add(new OrderDetail(row));
					}
				}

				return _details;
			}
		}

		public OrderQueue CurrentQueue
		{
			get 
			{
				if(_queue == null)
				{
					DataSet ds = OrderQueueServer.GetByID(_currentQueueID);
					_queue = new OrderQueue( ds.Tables["tblQueue"].Rows[0] );
				}

				return _queue;
			}
		}
	}
}
