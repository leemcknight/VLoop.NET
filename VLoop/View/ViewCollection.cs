using System;
using System.Collections;

namespace Afni.Applications.VLoop.Viewing
{
	/// <summary>
	/// Summary description for ViewCollection.
	/// </summary>
	public class ViewCollection : CollectionBase, IEnumerable
	{
		#region Events
		public event EventHandler Changed;
		#endregion

		public ViewCollection()
		{
			
		}

		#region Methods
		public int Add(Viewing.View item)
		{
			if (Contains(item)) return -1;
			int index = InnerList.Add(item);
			RaiseChanged();
			return index;
		}

		public bool Contains(Viewing.View item)
		{
			return InnerList.Contains(item);
		}
	
		public int IndexOf(Viewing.View item)
		{
			return InnerList.IndexOf(item);
		}
	
		public void Remove(Viewing.View item)
		{
			InnerList.Remove(item);
			RaiseChanged();		
		}

		public void Insert(int index, Viewing.View item)
		{
			InnerList.Insert(index, item);
			RaiseChanged();
		}

		public Viewing.View this[int index]
		{
			get { return (Viewing.View) InnerList[index]; }
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
