using System;
using System.Collections;
using Afni.Applications.VLoop.VLoopServer;
using System.Data;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.DataUtility;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	public class QueueBSO : IManager
	{
		public QueueBSO()
		{
			
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
			ArrayList queues = new ArrayList();
			DataSet ds = OrderQueueServer. (key);

			foreach(DataRow row in ds.Tables["tblQueue"].Rows)
			{
				queues.Add(new OrderQueue(row));
			}

			return queues;
		}

		ICollection IManager.GetByParentKey(int key)
		{
			ArrayList queues = new ArrayList();
			DataSet ds = OrderQueueServer.GetByCampaignID(key);

			foreach(DataRow row in ds.Tables["tblQueue"].Rows)
			{
				queues.Add(new OrderQueue(row));
			}

			return queues;
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
