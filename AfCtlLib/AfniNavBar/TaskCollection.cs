using System;
using System.Collections;
using System.Windows.Forms;

namespace Afni.Controls
{
	/// <summary>
	/// Summary description for MenuItemExCollection.
	/// </summary>
	public class TaskCollection : System.Collections.CollectionBase, IEnumerable
	{
		
		#region Events
		public event EventHandler Changed;
		#endregion
		
		#region Constructors
		public TaskCollection()
		{
			
		}
		#endregion

		#region Methods
		public int Add(AfniLink item)
		{
			if (Contains(item)) return -1;
			int index = InnerList.Add(item);
			RaiseChanged();
			return index;
		}

		public bool Contains(AfniLink item)
		{
			return InnerList.Contains(item);
		}
	
		public int IndexOf(AfniLink item)
		{
			return InnerList.IndexOf(item);
		}
	
		public void Remove(AfniLink item)
		{
			InnerList.Remove(item);
			RaiseChanged();		
		}

		public void Insert(int index, AfniLink item)
		{
			InnerList.Insert(index, item);
			RaiseChanged();
		}

		public AfniLink this[int index]
		{
			get { return (AfniLink) InnerList[index]; }
			set {  InnerList[index] = value; }
		}
		#endregion
	
		#region Implementation
		void RaiseChanged()
		{
			if (Changed != null) Changed(this, null);
		}
		#endregion

	}
}
