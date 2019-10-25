using System;
using System.Collections;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopBusinessObjects;

namespace Afni.Applications.VLoop.VLoopSmartDTO
{

	public class Account : AccountBase
	{
		private WTN _btn;
		private ArrayList _wtns;
		private ArrayList _orders;

		public Account()
		{
		}

		public Account(AccountBase account)
		{
			SetData(account);
		}

		public ArrayList WTNs
		{
			get 
			{
				if(_wtns == null)
				{
					IManager mgr = new WTNBSO();
					_wtns = new ArrayList();
					
					//copy the base class wtns into "smart"
					//wtn objects.
					ArrayList baseWTNs = new ArrayList();
					baseWTNs = (ArrayList)mgr.GetByParentKey((int)_pk);
					
					foreach(WTNBase wtn_base in baseWTNs)
					{
						_wtns.Add(new WTN(wtn_base));
					}
				}
				return _wtns; 
			}
		}

		public ArrayList Orders
		{
			get 
			{
				if(_orders == null)
				{
					IManager mgr = new OrderBSO();
					_orders = (ArrayList)mgr.GetByParentKey(_pk);
				}
				return _orders;
			}
		}

		public WTN BTN
		{
			get 
			{ 
				if(_btn == null)
				{
					foreach(WTN wtn in this.WTNs)
					{
						if(wtn.WtnID == this.BTNID)
						{
							_btn = wtn;
							break;
						}
					}
				}
				return _btn; 
			}
			set { _btn = value; }
		}
	}
}
