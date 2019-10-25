using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Afni.Applications.VLoop.VLoopServer
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class CampaignServer
	{
		public static DataSet GetAll()
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblCampaign","proctblCampaignsSELALL",conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}

		public static DataSet GetFiles(int campaignID)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblCampaign","proctblCustomerCampaignSELFileLoadByCampaignID " + campaignID.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;

		}
	}


	public class AppSettingServer
	{
		public static DataSet GetByCampaignID(long camp_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblApplicationSettings","proctblApplicationSettingsSELByCampaignID " + camp_id.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	public class CustomerCampaignServer
	{
		public static DataSet GetByID(long cust_camp_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblCustomerCampaign","proctblCustomerCampaignSEL " + cust_camp_id.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}

		public static DataSet GetNextCustomer(long campaignID,string fileName,long dispositionID,string state,string tzidList,string user,long langID)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			string query;
			try
			{
				query = "proctblCustomerSELNextCustomer ";
				query += campaignID.ToString() + ",";
				query += "'" + fileName + "',";
				query += dispositionID.ToString() + ",";
				query += "'" + state + "',";
				query += "'" + tzidList + "',";
				query += "'" + user +  "',";
				query += langID.ToString();
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblCustomerCampaign",query,conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}

		
	}

	public class OrderQueueServer
	{
		public static DataSet GetByCampaignID(long campaignID)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblQueue","proctblCampaignQueueSELByCampaignID " + campaignID.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}

		public static DataSet GetByID(long queueID)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblQueue","proctblQueueSELByQueueID " + queueID.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;

		}
	}

	public class OrderServer
	{
		public static DataSet GetByAccountID(long accountID)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblOrder","proctblOrderSELByAccountID " + accountID.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	public class OrderDetailServer
	{
		public static DataSet GetByOrderID(long orderID)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblOrderDetail","proctblOrderDetailSELByOrderID " + orderID.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}


	public class CustomerServer
	{

		public static DataSet GetByID(long cust_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblCustomer","proctblCustomerSEL " + cust_id.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}

		public static DataSet GetFromSearch(Hashtable searchParams)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			string query;
			try
			{
				query = "procSearchCustomers ";
				query += "'" + searchParams["firstName"] + "',";
				query += "'" + searchParams["lastName"] + "',";
				query += "'" + searchParams["billingAddr"] + "',";
				query += "'" + searchParams["billingState"] + "',";
				query += "'" + searchParams["billingCity"] + "',";
				query += "'" + searchParams["billingZip"] + "',";
				query += "'" + searchParams["servAddr"] + "',";
				query += "'" + searchParams["servCity"] + "',";
				query += "'" + searchParams["servZip"] + "',";
				query += "'" + searchParams["servState"] + "',";
				query += "'" + searchParams["wtn"] + "',";
				query += "'" + searchParams["company"] + "',";
				query += searchParams["campaignID"];
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblCustomer",query,conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	public class CustomerQuestionServer
	{
		public static DataSet GetByCampaignID(long camp_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblCustomerQuestion","proctblCustomerQuestionsLookupSELBYCampaignID " + camp_id.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	public class CustomerAnswerServer
	{
		public static DataSet GetByCustomerID(long customerID)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblCustomerAnswers","proctblCustomerAnswersSELByCustomerID " + customerID.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	public class CampaignQuestionServer
	{
		public static DataSet GetByCampaignID(long camp_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblCampaignQuestion","proctblCampaignQuestionsLookupSELBYCampaignID " + camp_id.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	public class AccountServer
	{
		public static DataSet GetByCustomerID(long cust_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblAccount","proctblAccountSELBYCustomerID " + cust_id.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
		
	}

	public class WTNServer
	{
		public static DataSet GetByAccountID(long acct_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblWTN","proctblWTNSELByAccountID " + acct_id.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
			
		}
	}

	public class CurrentPlanServer
	{
		public static DataSet GetByWTNID(long wtn_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblCurrentPlans","proctblCurrentPlansSELByWTNID " + wtn_id.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	public class CurrentPlanDetailServer
	{
		public static DataSet GetByCurrentPlanID(long plan_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblCurrentPlanDetail","proctblCurrentPlanDetailSELBYCurrentPlanID " + plan_id.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	public class ProductServer
	{
		public static DataSet GetByCampaignID(long campaign_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblProduct","proctblProductCampaignSELBYCampaignID " + campaign_id.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	public class ProdTypeServer
	{
		public static DataSet GetByID(long type_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblProdType","proctblProdTypeSEL " + type_id.ToString(),conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}

		public static DataSet GetAll()
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,"tblProdType","proctblProdTypeSELALL",conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	public class DispositionServer
	{
		public static DataSet GetByCampaignID(long campaign_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{

				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,
												"tblDisposition",
												"proctblCampaignDispositionSELByCampaignID " + campaign_id.ToString(),
												conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	public class LanguageServer
	{
		public static DataSet GetByCampaignID(long campaign_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,
												"tblLanguage",
												"proctblLanguageCampaignSELByCampaignID " + campaign_id.ToString(),
												conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	public class AvailableLookupInfoServer
	{
		public static DataSet GetByQuestionID(long question_id)
		{
			DataSet ds = new DataSet();
			string connect_string = DataTierHelpers.GetConnectionString();
			try
			{
				SqlConnection conn = new SqlConnection(connect_string);
				conn.Open();
				DataTierHelpers.CreateDataTable(ds,
					"tblAvailableLookupInfo",
					"proctblAvailableLookupInfoSELBYQuestionID " + question_id.ToString(),
					conn);
				conn.Close();
			}
			catch(System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
			return ds;
		}
	}

	internal class DataTierHelpers
	{
		internal static void CreateDataTable(DataSet dataset, string table, string select,SqlConnection conn)
		{
			try
			{
				SqlCommand cmd;
				SqlDataAdapter adapter = new SqlDataAdapter();
				cmd = new SqlCommand(select,conn);
				adapter.SelectCommand = cmd;
				adapter.TableMappings.Add("Table",table);
				adapter.Fill(dataset,table);
			}
			catch(SqlException ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.ToString());
			}
		}

		internal static string GetConnectionString()
		{
			return "Initial Catalog=VLoop;Data Source=localhost;Trusted_Connection=Yes";
			//return "Server=AFNBLDDEV2; Database=VLOOP; trusted_connection=yes";
		}
	}
}
