using System;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace Afni.DataUtility
{
	/// <summary>
	/// A.K.A Casper the FriendlyException.
	/// </summary>
	[Serializable()]
	public class FriendlyException : Exception
	{
		private Exception _exception =null;
		private string _message;
		//private bool _isSql;

		public FriendlyException(Exception ex)
		{
			_exception = ex;
			_message = GetFriendlyMessage();
		}

		protected FriendlyException(SerializationInfo info, StreamingContext context):base(info, context)
		{
		}

		public override string Message
		{
			get
			{
				return _message;
			}
		}

		private string GetFriendlyMessage()
		{
			switch(GetErrorNumber())
			{
	
				default:
					return _exception.Message;
					//					_exception.					
			}

		}
		
		private int GetErrorNumber()
		{
			if(_exception.GetType()==typeof(SqlException))
			{
				SqlException ex = (SqlException)_exception;
				return ex.Number;
			}
			return 0;
		}

		public string DetailedMessage
		{
			get{return _exception.Message;}
		}

		public override System.Exception GetBaseException()
		{
			return null;
		}

		public override string StackTrace
		{
			get
			{
				return _exception.StackTrace;
			}
		}
	}
}
