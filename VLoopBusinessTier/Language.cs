using System;
using System.Collections;
using System.Data;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopServer;
using Afni.Applications.VLoop.VLoopDataObjects;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	/// <summary>
	/// Summary description for Language.
	/// </summary>
	public class LanguageBSO : IManager
	{
		public LanguageBSO()
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
			ArrayList langs = new ArrayList();
			DataSet ds = LanguageServer.GetByCampaignID(key);
			
			foreach(DataRow row in ds.Tables["tblLanguage"].Rows)
			{
				langs.Add(new LanguageBase(row));
			}

			return langs;
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
