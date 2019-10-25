using System;
using System.Collections;
using System.Data;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	/// <summary>
	/// Summary description for Customer.
	/// </summary>
	public class CustomerBSO : IManager
	{
		public CustomerBSO()
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
			return new ArrayList();
		}

		IDataObject IManager.GetByPrimaryKey(int key)
		{
			DataRow row;
			DataSet ds = VLoopServer.CustomerServer.GetByID(key);
			row = ds.Tables["tblCustomer"].Rows[0];
			return new CustomerBase(row);
		}

		ICollection IManager.Search(Hashtable parms)
		{
			ArrayList customers = new ArrayList();
			
			DataSet ds = VLoopServer.CustomerServer.GetFromSearch(parms);

			foreach(DataRow row in ds.Tables["tblCustomer"].Rows)
			{
				customers.Add(new CustomerBase(row));
			}
			return customers;
		}

		#endregion
	}
}
