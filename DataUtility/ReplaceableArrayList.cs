using System;
using System.Collections;

namespace Afni.DataUtility
{
	/// <summary>
	/// Summary description for ReplaceableArrayList.
	/// </summary>
	[Serializable()]
	public class ReplaceableArrayList:ArrayList
	{
		public ReplaceableArrayList():base()
		{
		}

		public ReplaceableArrayList(ICollection collection):base(collection)
		{
		}
		
		public ReplaceableArrayList(int capacity):base(capacity)
		{
		}

		public int Replace(Object oldObject, Object newObject)
		{
			int index;
			index = this.IndexOf(oldObject);
			this[index] = newObject;
			return index;
		}

		public new static  ReplaceableArrayList Adapter(IList List)
		{
			return new ReplaceableArrayListWrapper(ArrayList.Adapter(List));
		}

		public static ReplaceableArrayList Adapter(ArrayList List)
		{
			return new ReplaceableArrayListWrapper(List);
		}

		public new static ReplaceableArrayList FixedSize(ArrayList List)
		{
			return new ReplaceableArrayListWrapper(ArrayList.FixedSize(List));
		}

		public new static ReplaceableArrayList FixedSize(IList List)
		{
			return new ReplaceableArrayListWrapper(ArrayList.Adapter(ArrayList.FixedSize(List)));
		}

		public new static ReplaceableArrayList ReadOnly(ArrayList List)
		{
			return new ReplaceableArrayListWrapper(ArrayList.ReadOnly(List));
		}

		public new static ReplaceableArrayList ReadOnly(IList List)
		{
			return new ReplaceableArrayListWrapper(ArrayList.Adapter(ArrayList.ReadOnly(List)));
		}

		public new static ReplaceableArrayList Repeat(Object repeatVal, int count)
		{
			return new ReplaceableArrayListWrapper(ArrayList.Repeat(repeatVal,count));
		}

		public new static ReplaceableArrayList Synchronized(ArrayList List)
		{
			return new ReplaceableArrayListWrapper(ArrayList.Synchronized(List));
		}

		public new static ReplaceableArrayList Synchronized(IList List)
		{
			return new ReplaceableArrayListWrapper(ArrayList.Adapter(ArrayList.Synchronized(List)));
		}

		public new  ReplaceableArrayList GetRange(int index, int count)
		{
			return new ReplaceableArrayListWrapper(base.GetRange(index, count));
		}

		private class ReplaceableArrayListWrapper:ReplaceableArrayList
		{
			ArrayList _List;
			public ReplaceableArrayListWrapper(ArrayList list)
			{
				_List = list;
			}

			public new int Capacity
			{
				get{return _List.Capacity;}
				set{_List.Capacity = value;}
			}

			public new int Count
			{
				get{return _List.Count;}
			}

			public new bool IsFixedSize
			{
				get{return _List.IsFixedSize;}
			}

			public new virtual bool IsReadOnly 
			{
				get{return _List.IsReadOnly;}
			}

			public new virtual bool IsSynchronized
			{
				get{return _List.IsSynchronized;}
			}

			public new object this[int index]
			{
				get{return _List[index];}
				set{_List[index] = value;}
			}

			public new object SyncRoot
			{
				get{return _List.SyncRoot;}
			}

			public new int Add(object newvalue)
			{
				return _List.Add(newvalue);
			}

			public new void AddRange(ICollection c)
			{
				_List.AddRange(c);
			}

			public new int BinarySearch(object o)
			{
				return _List.BinarySearch(o);
			}

			public new int BinarySearch(object o, IComparer c)
			{
				return _List.BinarySearch(o, c);
			}

			public new int BinarySearch(int index, int count, object o, IComparer c)
			{
				return _List.BinarySearch(index, count, o, c);
			}

			public new void Clear()
			{
				_List.Clear();
			}

			public new object Clone()
			{
				return _List.Clone();
			}

			public new bool Contains(object item)
			{
				return _List.Contains(item);
			}

			public new void CopyTo(Array array)
			{
				_List.CopyTo(array);
			}

			public new void CopyTo(Array array, int arrayindex)
			{
				_List.CopyTo(array, arrayindex);
			}

			public new void CopyTo(int index, Array array, int arrayindex, int count)
			{
				_List.CopyTo(index, array, arrayindex, count);
			}

			public new IEnumerator GetEnumerator()
			{
				return _List.GetEnumerator();
			}

			public new IEnumerator GetEnumerator(int index, int count)
			{
				return _List.GetEnumerator(index, count);
			}

			public new  ReplaceableArrayListWrapper GetRange(int index, int count)
			{
				return new ReplaceableArrayListWrapper(_List.GetRange(index, count));
			}

			public new int IndexOf(object o)
			{
				return _List.IndexOf(o);
			}

			public new int IndexOf(object o, int startIndex)
			{
				return _List.IndexOf(o, startIndex);
			}

			public new int IndexOf(object o, int startIndex, int count)
			{
				return _List.IndexOf(o, startIndex, count);
			}

			public new void Insert(int index, object o)
			{ 
				_List.Insert(index, o);
			}

			public new void InsertRange(int index, ICollection c)
			{
				_List.InsertRange(index, c);
			}

			public new int LastIndexOf(object o)
			{
				return _List.LastIndexOf(o);
			}

			public new int LastIndexOf(object o , int startIndex)
			{
				return _List.LastIndexOf(o, startIndex);
			}

			public new int LastIndexOf(object o, int startIndex, int count)
			{
				return _List.LastIndexOf(o, startIndex, count);
			}

			public new void Remove(object o)
			{
				_List.Remove(o);
			}

			public new void RemoveAt(int index)
			{
				_List.RemoveAt(index);
			}

			public new void RemoveRange(int index, int count)
			{
				_List.RemoveRange(index, count);
			}

			public new void Reverse()
			{
				_List.Reverse();
			}

			public new void Reverse(int index, int count)
			{
				_List.Reverse(index, count);
			}

			public new void SetRange(int index, ICollection c)
			{
				_List.SetRange(index, c);
			}

			public new void Sort()
			{
				_List.Sort();
			}

			public new void Sort(IComparer comparer)
			{
				_List.Sort(comparer);
			}

			public new void Sort(int index, int count, IComparer comparer)
			{
				_List.Sort(index, count, comparer);
			}

			public new object[] ToArray()
			{
				return _List.ToArray();
			}

			public new Array ToArray(Type type)
			{
				return _List.ToArray(type);
			}

			public new void TrimToSize()
			{
				_List.TrimToSize();
			}


		}
	}

	
	

}
