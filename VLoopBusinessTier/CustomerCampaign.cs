using System;
using System.Collections;
using System.Data;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	
	public class CustomerCampaignBSO : IManager
	{
		public CustomerCampaignBSO()
		{
			
		}

		public CustomerCampaignBase GetByID(long cust_camp_id)
		{
			DataRow row;
			DataSet ds = VLoopServer.CustomerCampaignServer.GetByID(cust_camp_id);
			row = ds.Tables["tblCustomerCampaign"].Rows[0];
			return new CustomerCampaignBase(row);
		}

		public CustomerCampaignBase GetNextCustomer(long campaignID,string fileName,long dispositionID,string state,string tzidList,string user,long langID)
		{
			DataRow row;
			DataSet ds = CustomerCampaignServer.GetNextCustomer(campaignID,
																fileName,
																dispositionID,
																state,
																tzidList,
																user,
																langID);

			row = ds.Tables["tblCustomerCampaign"].Rows[0];
			return new CustomerCampaignBase(row);
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
			DataSet ds = VLoopServer.CustomerCampaignServer.GetByID(key);
			row = ds.Tables["tblCustomerCampaign"].Rows[0];
			return new CustomerCampaignBase(row);
		}

		ICollection IManager.Search(Hashtable parms)
		{
			return new ArrayList();
		}

		#endregion

		
	}
}
