using System;

namespace Afni.Formatting
{
	/// <summary>
	/// Summary description for Helpers.
	/// </summary>
	public class AfFormat
	{
		public static string ToMaskedPhoneNumber(string phone_num)
		{
			string masked;

			masked = "(" + phone_num.Substring(0,3) +
				")" + phone_num.Substring(3,3) + 
				"-" + phone_num.Substring(5,4);

			return masked;

		}

		public static string ToUnmaskedPhoneNumber(string formatted_phone_num)
		{
			string unmasked;
			unmasked = formatted_phone_num.Replace("(","");
			unmasked = unmasked.Replace(")","");
			unmasked = unmasked.Replace("-","");
			return unmasked;
		}
	}
}
