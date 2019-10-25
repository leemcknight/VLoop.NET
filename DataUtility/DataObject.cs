using System;
using System.Reflection;
using System.Collections;

namespace Afni.DataUtility
{
	/// <summary>
	/// Implementation of the IDataObject interface
	/// </summary>
	[Serializable()]
	public class DataObject : Afni.DataUtility.IDataObject
	{
		private bool _dirty = false;
		//private Afni.DataUtility.Errors.DataError _error = null;
		private ArrayList _errors = new ArrayList();
		private bool _isInError = false;
		private string _guid = "";

		#region Implementation of IDataObject
		public virtual bool IsInError
		{
			get{return _isInError;}
			set{_isInError = value;}
		}

		public virtual bool Dirty
		{
			get{return _dirty;}
			set{_dirty=value;}
		}
/*
		public virtual Afni.DataUtility.Errors.DataError Error
		{
			get{return _error;}
			set{_error = value;}
		}

*/
		public virtual ArrayList Errors
		{
			get{return _errors;}
			set{_errors = value;}
		}		
		public virtual string GUID
		{
			get
			{
				if (_guid=="")
				{
					_guid = Guid.NewGuid().ToString();
				}

				return _guid;
			}
			set{_guid = value;}
		}

		/// <summary>
		/// Sets the properties of this class to match the one passed in (reverse shallow-copy)
		/// </summary>

		public virtual void SetData(DataObject dataObject)
		{
			//Sanity check - make sure the type passed in is the same type as me.
			if(!(dataObject.GetType()==this.GetType()))
			{
				//throw new Exception("Invalid Type for SetData");
			}

			PropertyInfo[] itemTypeProperties =dataObject.GetType().GetProperties();
			PropertyInfo property;         
			
			for(int i  = 0; i < itemTypeProperties.Length; i++ )    
			{    
				property = itemTypeProperties[i]; 
				System.Console.WriteLine(property.Name);
				PropertyInfo myProp = this.GetType().GetProperty(property.Name);
				if(myProp.CanWrite)
				{
					myProp.SetValue(this,property.GetValue(dataObject,null),null);
				}
			}
			//TODO: Do we need to copy the errors over?
		}

		#endregion
	}
}
