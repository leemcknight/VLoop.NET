using System;
using System.Data;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	
	public class LanguageBase : VLoopDataObject
	{
		private string _lang;

		public LanguageBase()
		{
			
		}

		public LanguageBase(DataRow row)
		{
			_pk = Convert.ToInt32(row["iLanguageID"]);
			_lang = Convert.ToString(row["szLanguage"]);
		}

		public override string ToString()
		{
			return _lang;
		}

		public long LanguageID
		{
			get { return _pk; }
		}

		public string LanguageText
		{
			get { return _lang; }
		}
	}
}
