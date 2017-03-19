using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Lists;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Blocks
{
	public abstract partial class ContainerBlock : Block
	{
		#region ctor

		public ContainerBlock()
			: base()
		{
			Children = new ChildrenList(this);
		}

		#endregion

		#region Root

		public override RootBlock Root
		{
			get
			{
				return base.Root;
			}
			internal set
			{
				base.Root = value;
				foreach (Block child in Children)
				{
					RootBlock oldRoot = child.Root;
					child.Root = value;
					child.NotifyRootChanged(oldRoot);
				}
				if (value != null)
				{
					MyControl.Root = value.MyRootControl;
				}
				else
				{
					MyControl.Root = null;
				}
			}
		}

		#endregion

		#region Children

		private IChildrenList mChildren;
		public virtual IChildrenList Children
		{
			get
			{
				return mChildren;
			}
			set
			{
				mChildren = value;
			}
		}

		#endregion

		#region API

		public virtual void Clear()
		{
			if (this.Children.Count == 0)
			{
				return;
			}
			BlockActions.Delete(this.Children.ToArray());
		}

		#region Add

		public void Add(string plainText)
		{
			Add(new LabelBlock(plainText));
		}

		public virtual void Add(IEnumerable<Block> blocksToAdd)
		{
			if (this.ActionManager != null)
			{
				AddBlocksAction action = ActionFactory.AddBlocks(this, blocksToAdd);
				this.ActionManager.RecordAction(action);
			}
			else
			{
				this.Children.Add(blocksToAdd);
			}
		}

		public void Add(params Block[] blocksToAdd)
		{
			Add((IEnumerable<Block>)blocksToAdd);
		}

		#endregion

		#region AddToBeginning

		public virtual void AddToBeginning(IEnumerable<Block> blocksToAdd)
		{
			Block head = this.Children.Head;
			if (head != null)
			{
				head.PrependBlocks(blocksToAdd);
			}
			else
			{
				this.Add(blocksToAdd);
			}
		}

		public void AddToBeginning(params Block[] blocksToAdd)
		{
			AddToBeginning((IEnumerable<Block>)blocksToAdd);
		}

		#endregion

		#region FindChildren

		/// <summary>
		/// Searches for and returns all children of a given ContainerBlock
		/// that have a specified type T
		/// </summary>
		/// <typeparam name="T">Type of blocks to look for</typeparam>
		/// <returns>An enumerable list of children, which have the required type T</returns>
		public virtual ReadOnlyCollection<T> FindChildren<T>()
			where T : class
		{
			List<T> result = new List<T>();

			foreach (Block child in this.Children)
			{
				// see, if a found child is of type T
				T found = child as T;

				// if yes, include it in the resulting list
				if (found != null)
				{
					result.Add(found);
				}
			}

			return result.AsReadOnly();
		}

		/// <summary>
		/// Searches for all children and sub-children of a given ContainerBlock
		/// recursively, that have a specified type T, and adds them to the result list
		/// </summary>
		/// <param name="resultList">List where found blocks should be added</param>
		/// <typeparam name="T">Type of blocks to look for</typeparam>
		public virtual void FindChildrenRecursive<T>(ICollection<T> result)
			where T : class
		{
			foreach (Block child in this.Children)
			{
				T found = child as T; // see, if a found child is of type T

				if (found != null) // if yes, include it in the resulting list
				{
					result.Add(found);
				}

				// try to see, if the found child itself is a ContainerBlock:
				// if yes, call FindChildrenResursive for it, and include
				// all the results in the results of the current method call
				ContainerBlock foundAsContainer = child as ContainerBlock;
				if (foundAsContainer != null)
				{
					foundAsContainer.FindChildrenRecursive<T>(result);
					//foreach (T foundRecursively in foundAsContainer.FindChildrenRecursive<T>())
					//{
					//    yield return foundRecursively;
					//}
				}
			}
		}

		/// <summary>
		/// Searches for all children and sub-children of a given ContainerBlock
		/// recursively, that have a specified type T, and returns them in a ReadOnlyCollection
		/// </summary>
		/// <typeparam name="T">Type of blocks to look for</typeparam>
		/// <returns>A collection of found blocks of type T</returns>
		public ReadOnlyCollection<T> FindChildrenRecursive<T>()
			where T : class
		{
			List<T> result = new List<T>();
			FindChildrenRecursive<T>(result);
			return result.AsReadOnly();
		}

		#endregion

		public IEnumerable<Block> EnumAllBlocks()
		{
			yield return this;
			foreach (Block b in EnumAllBlocks(this))
			{
				yield return b;
			}
		}

		public static IEnumerable<Block> EnumAllBlocks(ContainerBlock parent)
		{
			foreach (Block child in parent.Children)
			{
				yield return child;
				ContainerBlock embedded = child as ContainerBlock;
				if (embedded != null)
				{
					foreach (Block b in EnumAllBlocks(embedded))
					{
						yield return b;
					}
				}
			}
		}

		#endregion

		#region Memento

		public sealed override Memento CreateSnapshot()
		{
			Memento storage = base.CreateSnapshot();
			AddChildrenToSnapshot(storage);
			return storage;
		}

		public void AddChildrenToSnapshot(Memento storage)
		{
			IEnumerable<Block> children = GetChildrenToSerialize();
			if (children != null)
			{
				foreach (Block b in children)
				{
					storage.Add(b.CreateSnapshot());
				}
			}
		}

		public virtual IEnumerable<Block> GetChildrenToSerialize()
		{
			return this.Children;
		}

		#endregion

		#region Visibility

		public override bool CheckVisibility(Predicate<Block> checker)
		{
			bool needLayout = base.CheckVisibility(checker);

			foreach (Block block in this.Children)
			{
				needLayout = block.CheckVisibility(checker) || needLayout;
			}

			if (needLayout)
			{
				this.MyControl.Layout();
			}
			return needLayout;
		}

		#endregion

		#region HitTest

		public override Block FindBlockAtPoint(int x, int y)
		{
			Block childAtPoint = FindDirectChildAtPoint(x, y);

			if (childAtPoint != null)
			{
				return childAtPoint.FindBlockAtPoint(x, y);
			}

			if (this.MyControl.HitTest(x, y))
			{
				return this;
			}

			return null;
		}

		public virtual Block FindDirectChildAtPoint(int x, int y)
		{
			foreach (Block child in this.Children.Reversed)
			{
				if (child.MyControl.HitTest(x, y))
				{
					return child;
				}
			}

			return null;
		}

		#endregion
	}
}
