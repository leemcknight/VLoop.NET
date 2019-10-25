using System;
using System.Collections;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopBusinessObjects;

namespace Afni.Applications.VLoop.VLoopSmartDTO
{

	public class Customer : CustomerBase
	{
		private Account _acct;
		private ArrayList _cust_answers;

		public Customer()
		{
			
		}

		public Customer(CustomerBase customerBase)
		{
			SetData(customerBase);
			
		}

		public Account Account
		{
			get 
			{
				if(_acct == null)
				{
					IManager mgr = new AccountBSO();
					ArrayList accounts = (ArrayList)mgr.GetByParentKey(_pk);
					_acct = new Account((AccountBase)accounts[0]);
				}

				return _acct;
			}
		}

		public ArrayList CustomerAnswers
		{
			get 
			{
				if(_cust_answers == null)
				{
					IManager mgr = new CustomerAnswerBSO();
					_cust_answers = (ArrayList)mgr.GetByParentKey(_pk);
				}
				
				return _cust_answers;
			}
		}
	}
}
