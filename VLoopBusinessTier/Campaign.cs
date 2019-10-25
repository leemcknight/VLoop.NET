using System;
using System.Collections;
using System.Data;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class CampaignBSO : IManager
	{
		public CampaignBSO()
		{
			
		}

		public ArrayList GetFiles(int campaignID)
		{
			DataSet ds;
			ArrayList files = new ArrayList();
			ds = VLoopServer.CampaignServer.GetFiles(campaignID);
			foreach(DataRow row in ds.Tables["tblCampaign"].Rows)
			{
				files.Add(Convert.ToString(row["szFileName"]));
			}

			return files;
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
			DataSet ds = VLoopServer.CampaignServer.GetAll();
			ArrayList Campaigns = new ArrayList();
			foreach(DataRow row in ds.Tables["tblCampaign"].Rows)
			{
				Campaigns.Add(new CampaignBase(row));
			}

			return Campaigns;
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
