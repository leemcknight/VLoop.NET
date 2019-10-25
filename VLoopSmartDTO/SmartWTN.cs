using System;
using System.Collections;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopBusinessObjects;

namespace Afni.Applications.VLoop.VLoopSmartDTO
{
	/// <summary>
	/// Summary description for WTN.
	/// </summary>
	public class WTN : WTNBase
	{
		private ArrayList _current_plans;

		public WTN()
		{
			
		}

		public WTN(WTNBase wtnBase)
		{
			SetData(wtnBase);
		}

		public ArrayList CurrentPlans
		{
			get 
			{
				if(_current_plans == null)
				{
					IManager mgr = new CurrentPlanBSO();
					ArrayList planBases = new ArrayList();
					_current_plans = new ArrayList();

					
					planBases = (ArrayList)mgr.GetByParentKey(_pk);

					foreach(CurrentPlanBase planBase in planBases)
					{
						_current_plans.Add(new CurrentPlan(planBase));
					}
				}
				return _current_plans; 
			}
		}
	}
}
