using System;
using System.Collections;
using System.Data;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	/// <summary>
	/// Summary description for AppSetting.
	/// </summary>
	public class AppSettingBSO : IManager
	{
		public AppSettingBSO()
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
			ApplicationSetting setting;

			ds = VLoopServer.AppSettingServer.GetByCampaignID(key);
			Hashtable settings = new Hashtable();

			foreach(DataRow row in ds.Tables["tblApplicationSettings"].Rows)
			{
				setting = new ApplicationSetting(row);
				settings.Add(setting.Key, setting);
			}
			return settings;
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
