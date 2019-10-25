using System;
using System.Collections;

namespace Afni.Controls
{
	
	public class ButtonCollection : CollectionBase, IEnumerable
	{
		#region Events
		public event EventHandler Changed;
		#endregion

		public ButtonCollection()
		{
			
		}

		#region Methods
		public int Add(ButtonBarItem item)
		{
			if (Contains(item)) return -1;
			int index = InnerList.Add(item);
			RaiseChanged();
			return index;
		}

		public bool Contains(ButtonBarItem item)
		{
			return InnerList.Contains(item);
		}
	
		public int IndexOf(ButtonBarItem item)
		{
			return InnerList.IndexOf(item);
		}
	
		public void Remove(ButtonBarItem item)
		{
			InnerList.Remove(item);
			RaiseChanged();		
		}

		public void Insert(int index, ButtonBarItem item)
		{
			InnerList.Insert(index, item);
			RaiseChanged();
		}

		public ButtonBarItem this[int index]
		{
			get { return (ButtonBarItem) InnerList[index]; }
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
