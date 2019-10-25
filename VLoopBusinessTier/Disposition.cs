using System;
using System.Collections;
using System.Data;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	/// <summary>
	/// Summary description for Disposition.
	/// </summary>
	public class DispositionBSO : IManager
	{
		public DispositionBSO()
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
			ArrayList dispositions = new ArrayList();
			DataSet ds = DispositionServer.GetByCampaignID(key);
			
			foreach(DataRow row in ds.Tables["tblDisposition"].Rows)
			{
				dispositions.Add(new Disposition(row));
			}

			return dispositions;
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
