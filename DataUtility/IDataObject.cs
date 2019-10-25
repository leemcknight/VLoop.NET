using System;

namespace Afni.DataUtility
{

	public interface IDataObject
	{
		bool Dirty{get;set;}
		bool IsInError{get;set;}
		System.Collections.ArrayList Errors{get;set;}

		/// <summary>
		/// A unique identifier for this object
		/// </summary>
		string GUID
		{
			get;
			set;
		}

	}
}
