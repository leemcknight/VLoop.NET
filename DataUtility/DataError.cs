using System;

namespace Afni.DataUtility.Errors
{

	/// <summary>
	/// Summary description for DataError.
	/// </summary>
	[Serializable()]
	public class DataError
	{
		private int _errorNumber;
		private string _errorMessage;
		private string _fieldInError;

		public DataError(string msg, int errorNumber,string fieldInError)
		{
			_errorNumber = errorNumber;
			_errorMessage = msg;
			_fieldInError = fieldInError;

		}

		#region properties
		public string ErrorMessage
		{
			get{return _errorMessage;}
			set{_errorMessage = value;}
		}

		public string FieldInError
		{
			get{return _fieldInError;}
			set{_fieldInError = value;}
		}

		int ErrorNumber
		{
			get{return _errorNumber;}
			set{_errorNumber = value;}
		}


		#endregion
	}
	
}
