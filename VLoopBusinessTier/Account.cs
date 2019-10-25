using System;
using System.Data;
using System.Collections;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	
	public class AccountBSO : IManager
	{
		public AccountBSO()
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
			return new ArrayList();
		}

		ICollection IManager.GetByParentKey(int key)
		{
			ArrayList accounts = new ArrayList();
			
			DataSet ds = AccountServer.GetByCustomerID(key);
			
			foreach(DataRow row in ds.Tables["tblAccount"].Rows)
			{
				accounts.Add(new AccountBase(row));
			}
			
			return accounts;
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
