using System;
using System.Collections.Generic;
using System.Text;

namespace GuiLabs.Utils.Collections
{
	public class TreeNode<T> : ListSet<TreeNode<T>>
		where T : IComparable<T>, IEquatable<T>
	{
		#region ctor

		public TreeNode()
		{

		}

		public TreeNode(T value)
		{
			Value = value;
		}

		#endregion

		public TreeNode<T> AddChild(T value)
		{
			TreeNode<T> result = new TreeNode<T>(value);
			Add(result);
			return result;
		}

		public TreeNode<T> FindChild(T value)
		{
			foreach (TreeNode<T> child in this)
			{
				if (value.Equals(child.Value))
				{
					return child;
				}
			}
			return null;
		}

		public IEnumerable<T> GetValues()
		{
			foreach (TreeNode<T> child in this)
			{
				yield return child.Value;
			}
		}

		#region Value

		private T mValue;
		public T Value
		{
			get
			{
				return mValue;
			}
			set
			{
				mValue = value;
			}
		}

		#endregion
	}
}
