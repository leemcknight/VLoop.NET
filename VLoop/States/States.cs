/******************************************************************************
 * 
 * Afni.Applications.VLoop.States
 * 
 * Application States define the current state of the application as a whole,
 * regardless of which form is loaded, or the current status of a particular
 * form (editing, adding, etc...).  
 * 
 * Application states can have a bearing on enabled/disabled actions, and 
 * enforce or relax constraints on GUI elements on the main application.  For 
 * example, the user should not be able to show the scripts screen while logging
 * in, because they haven't yet selected a campaign.  
 * 
 * By abstracting the application state away from views, we can have a current 
 * view be active under different application states.  For example, we might 
 * have the search screen viewed with a customer open or with no customer open.
 * The view itself shouldn't care about this.
 * 
 * ***************************************************************************/
using System;
using Afni.Applications.VLoop;
using Afni.Applications.VLoop.Commands;

namespace Afni.Applications.VLoop.States
{
	public abstract class ApplicationState
	{
		protected string _name;
		protected Afni.Applications.VLoop.Application _app;
		
		public string StateName
		{
			get { return _name; }
		}

		public virtual void SetActiveState()
		{
			
		}
	}

	public class CallActiveState : ApplicationState
	{
		public CallActiveState(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			_name = "Call Currently Active";
		}

		public override void SetActiveState()
		{
			ActionHelper.DisableAction(ActionTypes.CampaignSwitch, _app);
			ActionHelper.EnableAction(ActionTypes.Next, _app);
			ActionHelper.EnableAction(ActionTypes.Queue, _app);
			ActionHelper.EnableAction(ActionTypes.End, _app);
			ActionHelper.EnableAction(ActionTypes.Find, _app);
			ActionHelper.DisableAction(ActionTypes.Login, _app);
		}
	}

	public class DispositionState : ApplicationState
	{
		public DispositionState(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			_name = "Call Disposition";
		}

		public override void SetActiveState()
		{
			ActionHelper.DisableAction(ActionTypes.Login, _app);
			ActionHelper.DisableAction(ActionTypes.CampaignSwitch, _app);
			ActionHelper.DisableAction(ActionTypes.End, _app);
			ActionHelper.DisableAction(ActionTypes.Find, _app);
			ActionHelper.DisableAction(ActionTypes.Next, _app);
			ActionHelper.DisableAction(ActionTypes.Queue, _app);
		}
	}

	public class LoggingInState : ApplicationState
	{
		public LoggingInState(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			_name = "Logging In";
		}

		public override void SetActiveState()
		{
			ActionHelper.DisableAction(ActionTypes.Login, _app);
			ActionHelper.DisableAction(ActionTypes.CampaignSwitch, _app);
			ActionHelper.DisableAction(ActionTypes.End, _app);
			ActionHelper.DisableAction(ActionTypes.Find, _app);
			ActionHelper.DisableAction(ActionTypes.Next, _app);
			ActionHelper.DisableAction(ActionTypes.Queue, _app);
		}
	}

	public class IdleState : ApplicationState
	{
		public IdleState(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			_name = "Idle (No Customer open)";
		}

		public override void SetActiveState()
		{
			ActionHelper.EnableAction(ActionTypes.Login, _app);
			ActionHelper.EnableAction(ActionTypes.CampaignSwitch, _app);
			ActionHelper.DisableAction(ActionTypes.End, _app);
			ActionHelper.EnableAction(ActionTypes.Find, _app);
			ActionHelper.DisableAction(ActionTypes.Next, _app);
			ActionHelper.EnableAction(ActionTypes.Queue, _app);
		}
	}

	public class CallSetupState : ApplicationState
	{
		public CallSetupState(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			_name = "Queue Selection and Setup";
		}

		public override void SetActiveState()
		{
			ActionHelper.EnableAction(ActionTypes.Login, _app);
			ActionHelper.EnableAction(ActionTypes.CampaignSwitch, _app);
			ActionHelper.DisableAction(ActionTypes.End, _app);
			ActionHelper.DisableAction(ActionTypes.Find, _app);
			ActionHelper.DisableAction(ActionTypes.Next, _app);
			ActionHelper.DisableAction(ActionTypes.Queue, _app);
		}
	}

	public enum ApplicationStates
	{
		LogIn,
		CallSetup,
		CallActive,
		Idle,
		Disposition
	}
}
