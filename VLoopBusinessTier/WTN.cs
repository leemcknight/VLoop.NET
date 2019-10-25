using System;
using System.Collections;
using Afni.Applications.VLoop.VLoopServer;
using System.Data;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.DataUtility;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	/// <summary>
	/// Summary description for WTN.
	/// </summary>
	public class WTNBSO : IManager
	{
		public WTNBSO()
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
			ds = VLoopServer.WTNServer.GetByAccountID(key);
			ArrayList wtns = new ArrayList();
			foreach(DataRow row in ds.Tables["tblWTN"].Rows)
			{
				wtns.Add(new WTNBase(row));
			}

			return wtns;
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
