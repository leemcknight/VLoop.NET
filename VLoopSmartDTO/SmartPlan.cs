using System;
using System.Collections;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopBusinessObjects;

namespace Afni.Applications.VLoop.VLoopSmartDTO
{
	/// <summary>
	/// Summary description for SmartPlan.
	/// </summary>
	public class CurrentPlan : CurrentPlanBase
	{
		private ArrayList _details;

		public CurrentPlan()
		{
			
		}

		public CurrentPlan(CurrentPlanBase planBase)
		{
			SetData(planBase);
		}

		public ArrayList PlanDetails
		{
			get 
			{ 
				if(_details == null)
				{
					IManager mgr = new CurrentPlanDetailBSO();
					_details = (ArrayList)mgr.GetByParentKey(_pk);
				}
				return _details; 
			}
		}
	}
}
