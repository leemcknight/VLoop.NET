using System;
using Afni.Applications.VLoop.VLoopBusinessObjects;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.Applications.VLoop.VLoopSmartDTO;

namespace Afni.Applications.VLoop
{
	public enum QueueTypes
	{
		CallQueue,
		OrderQueue
	}

	public abstract class QueueManager
	{
		protected Afni.Applications.VLoop.Application _app;
		public virtual bool ShowNextCustomer()
		{
			return true;
		}
	}

	public class OrderQueueManager : QueueManager
	{
		private long _queue_id;
		
		public OrderQueueManager(Afni.Applications.VLoop.Application app)
		{
			_app = app;
		}

		public override bool ShowNextCustomer()
		{
			//get the next customer campaign
			CustomerCampaignBSO bso = new CustomerCampaignBSO();

			//start the call
			_app.Call.StartCall((CustomerCampaign)bso.GetByID(1));
			return true;
		}

		public long QueueID
		{
			get { return _queue_id; }
			set { _queue_id = value; }
		}

	}

	public class CallQueueManager : QueueManager
	{
		private string _filename;
		private string _state;
		private long _disp_id; 
		private long _lang_id; 
		private long[] _timeZones;

		public CallQueueManager(Afni.Applications.VLoop.Application app)
		{
			_app = app;
		}

		public override bool ShowNextCustomer()
		{
			//get the next customer campaign
			CustomerCampaignBSO bso = new CustomerCampaignBSO();
			CustomerCampaignBase cc;
			string tzList = "";

			/*
			for(int i = 0; i<= _timeZones.GetUpperBound(1); i++)
			{
				tzList = tzList + _timeZones[i].ToString() + ",";
			}
			*/

			//chop off end ","
			tzList = tzList.TrimEnd(new char[]{ ',' });
		

			//start the call
			cc = bso.GetNextCustomer(_app.CurrentCampaign.CampaignID,
								_filename,
								_disp_id,
								_state,
								tzList,
								_app.User.Name,
								_lang_id);
			_app.Call.StartCall(cc);
			return true;
		}

		public string FileName
		{
			get { return _filename; }
			set { _filename = value; }
		}

		public string State
		{
			get { return _state; }
			set { _state = value; }
		}

		public long DispositionID
		{
			get { return _disp_id; }
			set { _disp_id = value; }
		}

		public long LanguageID
		{
			get { return _lang_id; }
			set { _lang_id = value; }
		}

		public long[] TimeZones
		{
			get { return _timeZones; }
			set { _timeZones = value; }
		}
	}
}
