using System;
using System.Collections;
using System.Data;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	/// <summary>
	/// Summary description for CurrentPlan.
	/// </summary>
	public class CurrentPlanBSO : IManager
	{
		public CurrentPlanBSO()
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
			DataSet ds = CurrentPlanServer.GetByWTNID(key);
			

			ArrayList current_plans = new ArrayList();
			foreach(DataRow row in ds.Tables["tblCurrentPlans"].Rows)
			{
				current_plans.Add(new CurrentPlanBase(row));
			}

			return current_plans;
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

	public class CurrentPlanDetailBSO : IManager
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
			ds= VLoopServer.CurrentPlanDetailServer.GetByCurrentPlanID(key);
			foreach(DataRow row in ds.Tables["tblCurrentPlanDetail"].Rows)
			{
				details.Add(new CurrentPlanDetail(row));
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
