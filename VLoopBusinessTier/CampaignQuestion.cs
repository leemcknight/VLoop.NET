using System;
using System.Collections;
using System.Data;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	/// <summary>
	/// Summary description for CampaignQuestion.
	/// </summary>
	public class CampaignQuestionBSO : IManager
	{
		public CampaignQuestionBSO()
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
