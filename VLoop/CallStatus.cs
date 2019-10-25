using System;
using Afni.Applications.VLoop.States;
using Afni.Applications.VLoop.Viewing;
using Afni.FormData;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;

namespace Afni.Applications.VLoop
{
	public class CallStatus
	{
		private System.Timers.Timer _timer;
		private StepAfterDisposition _next_step;
		private Afni.Applications.VLoop.Application _app;
		private QueueManager _mgr;
		private CustomerCampaign _cust_campaign;

		public event EventHandler NewCallStarted;
		
		public CallStatus(Afni.Applications.VLoop.Application app)
		{
			_app = app;
			_mgr = app.QueueManager;
			_timer = new System.Timers.Timer();
			
		}

		private void RegisterForDispEvent()
		{
			ISaveable ctl;
			Viewing.View disp_view;

			disp_view = (Viewing.View)_app.Views[ViewTypes.Disposition];
			ctl = (ISaveable)disp_view.ViewedForm;
			ctl.SaveSucceeded += new EventHandler(this.OnDispositionSaved);
		}

		private void OnDispositionSaved(object sender, System.EventArgs e)
		{

			switch(_next_step)
			{
				case StepAfterDisposition.ExitApp:
				{
					_app.Close();
					break;
				}
				case StepAfterDisposition.NextCall:
				{
					_mgr.ShowNextCustomer();
					_app.ClearViewStacks();
					break;
				}
				case StepAfterDisposition.NoCall:
				{
					_app.LoadView(ViewTypes.Search);
					_app.SwitchAppState(ApplicationStates.Idle);
					_app.ClearViewStacks();
					break;
				}
			}
		}

		public CustomerCampaign CurrentCustCampaign
		{
			get { return _cust_campaign; }
		}

		public QueueManager CallQueue
		{
			get { return _mgr; }
		}

		public bool StartCall(CustomerCampaign cust_campaign)
		{
			_cust_campaign = cust_campaign;
			_app.LoadView(ViewTypes.Customer);
			_timer.Start();
			_app.SwitchAppState(ApplicationStates.CallActive);
			if(NewCallStarted != null)
				NewCallStarted(this,null);
			return true;
		}

		public bool StartCall(CustomerCampaignBase custCampaign)
		{
			return StartCall(new CustomerCampaign(custCampaign));
		}

		public StepAfterDisposition StepAfterDisposition
		{
			get { return _next_step; }
			set { _next_step = value; }
		}

		public bool EndCall()
		{
			_timer.Stop();
			return true;
		}
	}
}
