using System;
using System.Data;
using Afni.Applications.VLoop.VLoopServer;

namespace Afni.Applications.VLoop.VLoopDataObjects
{
	/// <summary>
	/// Summary description for Product.
	/// </summary>
	public class Product : VLoopDataObject
	{
		private string _code;
		private string _desc;
		private int _type_id;
		private ProductType _type;

		public Product()
		{
			
		}

		public Product(DataRow row)
		{
			_pk = Convert.ToInt32(row["iProductID"]);
			_code = Convert.ToString(row["szProductCode"]);
			_desc = Convert.ToString(row["szDescription"]);
			_type_id = Convert.ToInt32(row["iProdTypeID"]);
		}

		public override string ToString()
		{
			return _desc;
		}

		public long ProductID
		{
			get { return _pk; }
		}

		public string ProductCode
		{
			get { return _code;  }
			set
			{
				if(_code != value)
				{
					_code = value;
					this.Dirty = true;
				}
			}
		}

		public string Description
		{
			get { return _desc;}
			set
			{
				if(_desc != value)
				{
					_desc = value;
					this.Dirty = true;
				}
			}
		}

		public int ProdTypeID
		{
			get { return _type_id; }
			set
			{
				_type_id = value;
				this.Dirty = true;
			}
		}
	}

	public class ProductType : VLoopDataObject
	{
		private long _id;
		private string _desc;

		public ProductType()
		{
		}

		public ProductType(DataRow row)
		{
			_id = Convert.ToInt64(row["iProdTypeID"]);
			_desc = Convert.ToString(row["szDescription"]);
		}

		public long ProductTypeID
		{
			get { return _id; }
		}

		public string ProductTypeDescription
		{
			get { return _desc; }
		}

	}
}
