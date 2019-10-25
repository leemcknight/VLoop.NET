using System;
using System.Collections;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopBusinessObjects;

namespace Afni.Applications.VLoop.VLoopSmartDTO
{
	/// <summary>
	/// Summary description for SmartCustCampaign.
	/// </summary>
	public class CustomerCampaign : CustomerCampaignBase
	{
		private Customer _customer;

		public CustomerCampaign()
		{
			
		}

		public CustomerCampaign(CustomerCampaignBase ccBase)
		{
			SetData(ccBase);
		}

		public Customer Customer
		{
			get 
			{
				if(_customer == null)
				{
					IManager mgr = new CustomerBSO();
					_customer = new Customer((CustomerBase)mgr.GetByPrimaryKey((int)this.CustomerID));
				}

				return _customer;
			}
		}
	}
}
