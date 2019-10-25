using System;
using System.Collections;
using Afni.DataUtility;

namespace Afni.Applications.VLoop.VLoopBusinessObjects
{
	
	public class CustomerAnswerBSO : IManager
	{
		public CustomerAnswerBSO()
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
			return null;
		}

		ICollection IManager.Search(Hashtable parms)
		{
			return new ArrayList();
		}

		#endregion
	}
}
