using System;
using System.Collections;

namespace Afni.DataUtility
{
	/// <summary>
	/// Summary description for IManager.
	/// </summary>
	public interface IManager
	{
		IDataObject Validate(IDataObject dataObject);
		//private IDataObject BeforeSave(IDataObject dataObject);
		//private IDataObject AfterSave(IDataObject dataObject);
		IDataObject Save(IDataObject dataObject);
		bool Delete(IDataObject dataObject);
		ICollection GetAll();
		ICollection GetByParentKey(int key);
		IDataObject GetByPrimaryKey(int key);
		ICollection Search(Hashtable parms);
		
	}
}
