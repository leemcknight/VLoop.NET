using System;
using System.Collections;
using Afni.Applications.VLoop.VLoopDataObjects;
using Afni.DataUtility;
using Afni.Applications.VLoop.VLoopBusinessObjects;

namespace Afni.Applications.VLoop.VLoopSmartDTO
{
	/// <summary>
	/// Summary description for SmartCampaign.
	/// </summary>
	public class Campaign : CampaignBase
	{
		private ArrayList _langs;
		private ArrayList _dispositions;
		private ArrayList _products;
		private ArrayList _cust_qstns;
		private ArrayList _campgn_qstns;
		private Hashtable _settings;
		private ArrayList _files;
		private ArrayList _orderQueues;

		public Campaign()
		{
			
		}

		public Campaign(CampaignBase baseDataObject)
		{
			SetData(baseDataObject);
		}

		public ArrayList Languages
		{
			get 
			{ 
				if(_langs == null)
				{
					IManager mgr = new LanguageBSO();
					_langs = (ArrayList)mgr.GetByParentKey(_pk);
				}
				return _langs; 

			}
		}

		public ArrayList Dispositions
		{
			get 
			{
				if(_dispositions == null)
				{
					IManager mgr = new DispositionBSO();
					_dispositions = (ArrayList)mgr.GetByParentKey(_pk);
				}
				return _dispositions; 
			}
		}

		public ArrayList Files
		{
			get 
			{
				if(_files == null)
				{
					CampaignBSO bso = new CampaignBSO();
					_files = bso.GetFiles(this.CampaignID);
				}
				return _files;
			}
		}

		public ArrayList Products
		{
			get 
			{
				if(_products == null)
				{
					IManager mgr = new ProductBSO();
					_products = (ArrayList)mgr.GetByParentKey(_pk);
				}
				return _products;
			}
		}

		public ArrayList CustomerQuestions
		{
			get 
			{
				if(_cust_qstns == null)
				{
					IManager mgr = new CustomerQuestionBSO();
					_cust_qstns = (ArrayList)mgr.GetByParentKey(_pk);
				}
				return _cust_qstns;
			}
		}

		public ArrayList CampaignQuestions
		{
			get 
			{
				if(_campgn_qstns == null)
				{
					IManager mgr = new CampaignQuestionBSO();
					_campgn_qstns = (ArrayList)mgr.GetByParentKey(_pk);
				}
				return _campgn_qstns;
			}
		}

		public ArrayList OrderQueues
		{
			get
			{
				if(_orderQueues == null)
				{
					IManager mgr = new QueueBSO();
					_orderQueues = (ArrayList)mgr.GetByParentKey(_pk);
				}
				return _orderQueues;
			}
		}

		public Hashtable AppSettings
		{
			get 
			{
				if(_settings == null)
				{
					IManager mgr = new AppSettingBSO();
					_settings = (Hashtable)mgr.GetByParentKey(_pk);
				
				}
				return _settings; 
			}
		}
	}
}
