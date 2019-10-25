using System;
using Afni.Applications.VLoop.Viewing;
using Afni.Applications.VLoop.Commands;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;
using System.Collections;
using System.Windows.Forms;

namespace Afni.Applications.VLoop
{
	public abstract class AfniWizard
	{
		public virtual void Start()
		{
		}

		public virtual bool MoveNextStep()
		{
			return true;
		}

		public virtual bool MovePrevStep()
		{
			return true;
		}

		public virtual bool Commit()
		{
			return true;
		}

		public virtual bool Cancel()
		{
			return true;
		}
	}


	public class NewPlanManager : AfniWizard
	{
		private Afni.Applications.VLoop.Application _app;
		private Afni.Applications.VLoop.VLoopDataObjects.Product selProd;
		private NewPlanWizardSteps _currentStep;
		private NewPlanWiz1Ctl _plan_select_ctl;
		private NewPlanWizFinalCtl _plan_final;
		private CurrentPlan _plan;
		private ArrayList _plans;

		public NewPlanManager(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			
		}

		public override void Start()
		{
			_plan_select_ctl = (NewPlanWiz1Ctl)(((Viewing.View)_app.Views[ViewTypes.NewPlanStep1]).ViewedForm);
			_plan_final = (NewPlanWizFinalCtl)(((Viewing.View)_app.Views[ViewTypes.NewPlanFinal]).ViewedForm);
			_plan_select_ctl.WizMgr = this;
			_plan_final.PlanWiz = this;
			_currentStep = NewPlanWizardSteps.PlanSelection;
			_app.LoadView(ViewTypes.NewPlanStep1);

			//start with a new, empty plan
			_plan = new CurrentPlan();
		}

		public override bool MoveNextStep()
		{
			switch(_currentStep)
			{
				case NewPlanWizardSteps.PlanSelection:
					selProd =  _plan_select_ctl.SelectedProduct;
					_plan.ProductID = selProd.ProductID;
					_plan_final.NewPlan = _plan;
					_app.LoadView(ViewTypes.NewPlanFinal);
					break;
				case NewPlanWizardSteps.CCDetails:
					break;
				case NewPlanWizardSteps.TFNDetails:
					break;
		
			}
			return true;
		}

		public override bool MovePrevStep()
		{
			switch(_currentStep)
			{
				case NewPlanWizardSteps.CCDetails:
					break;
				case NewPlanWizardSteps.TFNDetails:
					break;
				case NewPlanWizardSteps.Summary:
					break;
			}
			return true;
		}

		public override bool Commit()
		{
			Viewing.View acctView;
			WTN planWTN;
			try 
			{
				acctView = (Viewing.View)_app.Views[ViewTypes.Account];
				planWTN = _plan_final.SelectedWTN;
				planWTN.CurrentPlans.Add(_plan);
				_plan.WTNID = planWTN.WtnID;
				_app.LoadView(acctView);
				acctView.ViewedForm.Refresh();
			}
			catch(System.Exception ex)
			{
				MessageBox.Show("An error has occured saving the new plan.",
								"VLoop Error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
			}
			
			return true;
		}

		public override bool Cancel()
		{
			
			return true;
		}
	}

	public enum NewPlanWizardSteps
	{
		PlanSelection,
		TFNDetails,
		CCDetails,
		Summary
	}

}
