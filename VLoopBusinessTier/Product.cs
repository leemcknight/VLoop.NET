using System;
using System.Collections;
using System.Data;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	public class ProductBSO : IManager
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
			ArrayList products = new ArrayList();
			ds= VLoopServer.ProductServer.GetByCampaignID(key);
			foreach(DataRow row in ds.Tables["tblProduct"].Rows)
			{
				products.Add(new Product(row));
			}

			return products;
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

	public class ProdTypeBSO : IManager
	{
		public ProdTypeBSO()
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
			DataSet ds = VLoopServer.ProdTypeServer.GetAll();
			ArrayList ProdTypes = new ArrayList();
			foreach(DataRow row in ds.Tables["tblProdType"].Rows)
			{
				ProdTypes.Add(new ProductType(row));
			}

			return ProdTypes;
		}

		ICollection IManager.GetByParentKey(int key)
		{
			return new ArrayList();
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
