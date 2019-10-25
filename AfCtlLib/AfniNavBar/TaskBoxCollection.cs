using System;
using System.Collections;
using System.Windows.Forms;

namespace Afni.Controls
{
	/// <summary>
	/// Summary description for MenuItemExCollection.
	/// </summary>
	public class TaskBoxCollection : System.Collections.CollectionBase, IEnumerable
	{
	
		#region Events
		public event EventHandler Changed;
		#endregion
		
		#region Constructors
		public TaskBoxCollection()
		{
			
		}
		#endregion

		#region Methods
		public int Add(TaskBox item)
		{
			if (Contains(item)) return -1;
			int index = InnerList.Add(item);
			RaiseChanged();
			return index;
		}

		public bool Contains(TaskBox item)
		{
			return InnerList.Contains(item);
		}
	
		public int IndexOf(TaskBox item)
		{
			return InnerList.IndexOf(item);
		}
	
		public void Remove(TaskBox item)
		{
			InnerList.Remove(item);
			RaiseChanged();		
		}

		public void Insert(int index, TaskBox item)
		{
			InnerList.Insert(index, item);
			RaiseChanged();
		}

		public TaskBox this[int index]
		{
			get { return (TaskBox) InnerList[index]; }
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
