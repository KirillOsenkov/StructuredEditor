using GuiLabs.Editor.Blocks;
namespace GuiLabs.Editor.Clipboard
{
	public class DragQueryResult
	{
		public DragQueryResult(Block target)
		{
			DropTarget = target;
		}

		public DragQueryResult(Block target, DropTargetInfo howToDrop)
		{
			DropTarget = target;
			HowToDrop = howToDrop;
		}

		private Block mDropTarget;
		public Block DropTarget
		{
			get
			{
				return mDropTarget;
			}
			set
			{
				mDropTarget = value;
			}
		}

		private IPotentialDropTarget mDropTargetContainer;
		public IPotentialDropTarget DropTargetContainer
		{
			get
			{
				return mDropTargetContainer;
			}
			set
			{
				mDropTargetContainer = value;
			}
		}

		private DropTargetInfo mHowToDrop;
		public DropTargetInfo HowToDrop
		{
			get
			{
				return mHowToDrop;
			}
			set
			{
				mHowToDrop = value;
			}
		}

		private bool mIsDropPointInTheUpperHalfOfDropTarget = false;
		public bool IsDropPointInTheUpperHalfOfDropTarget
		{
			get
			{
				return mIsDropPointInTheUpperHalfOfDropTarget;
			}
			set
			{
				mIsDropPointInTheUpperHalfOfDropTarget = value;
			}
		}

		public bool DropTargetValid
		{
			get
			{
				if (DropTarget == null)
				{
					return false;
				}
				if (HowToDrop == DropTargetInfo.DropBeforeTarget)
				{
					return DropTarget.CanPrependBlocks;
				}
				if (HowToDrop == DropTargetInfo.DropAfterTarget
					|| HowToDrop == DropTargetInfo.DropAfterSeparator)
				{
					return DropTarget.CanAppendBlocks;
				}
				return false;
			}
		}

		public void AdjustDropOnSeparator()
		{
			if (DropTarget == null)
			{
				return;
			}
			if (HowToDrop == DropTargetInfo.DropBeforeTarget)
			{
				if (DropTarget.Prev is ISeparatorBlock)
				{
					DropTarget = DropTarget.Prev;
					HowToDrop = DropTargetInfo.DropAfterSeparator;
				}
			}
			else if (HowToDrop == DropTargetInfo.DropAfterTarget)
			{
				if (DropTarget.Next is ISeparatorBlock)
				{
					DropTarget = DropTarget.Next;
					HowToDrop = DropTargetInfo.DropAfterSeparator;
				}
			}
		}
	}

	public enum DropTargetInfo
	{
		None,
		DropAfterSeparator,
		DropBeforeTarget,
		DropAfterTarget,
		ReplaceTarget
	}
}
