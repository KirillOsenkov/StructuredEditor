using System;
using System.Collections.Generic;

namespace GuiLabs.Editor.Blocks
{
	public abstract partial class Block
	{
		public bool Visible
		{
			get
			{
				return MyControl.Visible;
			}
		}
		
		public bool IsVisible
		{
			get
			{
				return Visible && !Hidden;
			}
		}

		/// <summary>
		/// Sets the new value for this.Visible
		/// </summary>
		/// <param name="newValue">the new visibility value to set</param>
		/// <returns>true if the value has actually been changed, 
		/// false if the value was the same</returns>
		private bool SetVisible(bool newValue)
		{
			if (MyControl.Visible != newValue)
			{
				MyControl.Visible = newValue;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Apply a given checker function to recalculate Visible of this block.
		/// </summary>
		/// <param name="checker">A delegate to a function that sets this.Visible to true or false</param>
		/// <remarks>This method is overriden in ContainerBlock</remarks>
		/// <returns>true if this.Visible has changed, false otherwise</returns>
		public virtual bool CheckVisibility(Predicate<Block> checker)
		{
			bool blockShouldBeVisible = CalculateVisibility(checker);
			return SetVisible(blockShouldBeVisible);
		}

		protected virtual bool CalculateVisibility(Predicate<Block> checker)
		{
			// default is visible
			bool visible = true;

			// Apply the delegate and note to ourselves,
			// if the delegate wants us to be visible or not.
			if (checker != null)
			{
				visible = checker(this);
			}

			visible = visible && !this.Hidden;
			return visible;
		}

		public virtual void CheckVisibility()
		{
			if (this.Root != null && this.Root.VisibilityManager != null)
			{
				CheckVisibility(this.Root.VisibilityManager.ShouldBeVisible);
			}
			else
			{
				CheckVisibility(null);
			}
		}

		private bool mHidden = false;
		public bool Hidden
		{
			get
			{
				return mHidden;
			}
			set
			{
				mHidden = value;
				CheckVisibility();
			}
		}
	}

	public delegate void QueryVisibilityEventHandler(Block block, ref bool shouldBeVisible);

	public class VisibilityFilter
	{
		#region ctors

		public VisibilityFilter()
		{

		}

		public VisibilityFilter(Predicate<Block> visibilityPredicate)
		{
			predicate = visibilityPredicate;
		}

		#endregion

		private Predicate<Block> predicate;

		/// <summary>
		/// Subscribe to this event to add a custom handler for this filter
		/// </summary>
		public event QueryVisibilityEventHandler QueryVisibility;
		protected bool RaiseQueryVisibility(Block block)
		{
			bool shouldBeVisible = true;
			if (QueryVisibility != null)
			{
				QueryVisibility(block, ref shouldBeVisible);
			}
			return shouldBeVisible;
		}

		public virtual bool ShouldBeVisible(Block block)
		{
			foreach (VisibilityFilter filter in ChildFilters)
			{
				if (!filter.ShouldBeVisible(block))
				{
					return false;
				}
			}
			if (predicate != null)
			{
				return predicate(block);
			}
			return RaiseQueryVisibility(block);
		}

		public void Add(VisibilityFilter filter)
		{
			mChildFilters.Add(filter);
		}

		private List<VisibilityFilter> mChildFilters = new List<VisibilityFilter>();
		public ICollection<VisibilityFilter> ChildFilters
		{
			get
			{
				return mChildFilters;
			}
		}
	}
}
