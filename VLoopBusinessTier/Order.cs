using System;
using System.Collections;
using Afni.Applications.VLoop.VLoopServer;
using System.Data;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.DataUtility;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	/// <summary>
	/// Summary description for Order.
	/// </summary>
	public class OrderBSO : IManager
	{
		public OrderBSO()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region IManager Implementation
		bool IManager.Delete(IDataObject dataObject)
		{
			return true;
		}

		IDataObject IManager.Save(IDataObject dataObject)
		{
			return dataObject;
		}

		IDataObject IManager.Validate(IDataObject dataObject)
		{
			return dataObject;
		}

		ICollection IManager.GetAll()
		{
			return new ArrayList();
		}

		ICollection IManager.GetByParentKey(int key)
		{
			DataSet ds;
			ArrayList orders = new ArrayList();
			ds = VLoopServer.OrderServer.GetByAccountID(key);
			foreach(DataRow row in ds.Tables["tblOrder"].Rows)
			{
				orders.Add(new Order(row));
			}

			return orders;
		}

		IDataObject IManager.GetByPrimaryKey(int key)
		{
			return null;
		}

		ICollection IManager.Search(Hashtable parms)
		{
			return new ArrayList();
		}

		#endregion
	}

	public class OrderDetailBSO : IManager
	{
		#region IManager Implementation
		bool IManager.Delete(IDataObject dataObject)
		{
			return true;
		}

		IDataObject IManager.Save(IDataObject dataObject)
		{
			return dataObject;
		}

		IDataObject IManager.Validate(IDataObject dataObject)
		{
			return dataObject;
		}

		ICollection IManager.GetAll()
		{
			return new ArrayList();
		}

		ICollection IManager.GetByParentKey(int key)
		{
			DataSet ds;
			ArrayList details = new ArrayList();
			ds = OrderDetailServer.GetByOrderID(key);
			foreach(DataRow row in ds.Tables["tblOrderDetail"].Rows)
			{
				details.Add(new OrderDetail(row));
			}

			return details;
		}

		IDataObject IManager.GetByPrimaryKey(int key)
		{
			return null;
		}

		ICollection IManager.Search(Hashtable parms)
		{
			return new ArrayList();
		}

		#endregion
	}
}
