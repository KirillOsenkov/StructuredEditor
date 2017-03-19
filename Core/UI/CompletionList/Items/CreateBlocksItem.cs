using System;
using System.Collections.Generic;
using GuiLabs.Editor.Blocks;
using System.Linq;

namespace GuiLabs.Editor.UI
{
	/// <summary>
	/// Represents a menu item in the drop-down CompletionList,
	/// which creates predefined blocks when user clicks on it.
	/// </summary>
	public class CreateBlocksItem : CompletionListItemWithVisibilityConditions
	{
		#region ctors

		public CreateBlocksItem(
			string text,
			IEnumerable<Type> blockTypes)
			: base(text)
		{
			Factory = new BlockActivatorFactory(blockTypes);
			Init();
		}

		public CreateBlocksItem(
			string text,
			IBlockFactory factory)
			: base(text)
		{
			Factory = factory;
			Init();
		}

        public CreateBlocksItem(
            string text,
            Func<Block> blockCreator)
            : base(text)
        {
            BlockCreator = blockCreator;
            Init();
        }

        public Func<Block> BlockCreator { get; set; }

		private void Init()
		{
			ReferenceBlock = GetSameBlock;
		}

		private static Block GetSameBlock(Block source)
		{
			return source;
		}

		#endregion

		public static CreateBlocksItem Create<T>(string text)
		{
			return new CreateBlocksItem(text, BlockActivatorFactory.Types<T>());
		}

		public static CreateBlocksItem Create<T1, T2>(string text)
		{
			return new CreateBlocksItem(text, BlockActivatorFactory.Types<T1, T2>());
		}

		public BlockNavigatorDelegate ReferenceBlock { get; set; }

		protected IBlockFactory Factory;

		/// <summary>
		/// Happens when user selects this CreateBlocksItem
		/// from the drop-down CompletionList.
		/// </summary>
		public override void Click(CompletionFunctionality hostItemList)
		{
			Block reference = hostItemList.HostBlock;
			if (ReferenceBlock != null)
			{
				reference = ReferenceBlock(reference);
			}
			if (reference != null)
			{
                IEnumerable<Block> blocks = null;
                if (BlockCreator != null)
                {
                    blocks = Enumerable.Repeat(BlockCreator(), 1);
                }
                else
                {
                    blocks = Factory.CreateBlocks();
                }
				reference.AppendBlocks(blocks);
			}
		}

		/// <summary>
		/// Prepare an AddBlocksAction
		/// </summary>
		/// <remarks>
		/// For each Block Type from the Types list,
		/// an instance of that type will be created by this action
		/// </remarks>
		//protected virtual IAction CreateAddBlocksAction()
		//{
		//    AddBlocksAction Action = new AddBlocksAction(Block.Parent, Block);

		//    Action.PrepareBlocks(GetBlocksToAdd());

		//    return Action;
		//}

		//protected virtual IEnumerable<Block> GetBlocksToAdd()
		//{
		//    return Factory.CreateBlocks();

		//    //LinkedList<Block> resultBlocks = new LinkedList<Block>();

		//    //// Add each block from the Types list to the Action
		//    //foreach (Type CurrentType in Types)
		//    //{
		//    //    Block NewBlock = null;
		//    //    //try
		//    //    //{
		//    //        NewBlock = System.Activator.CreateInstance(CurrentType) as Block;
		//    //    //}
		//    //    //catch (Exception ex)
		//    //    //{
		//    //    //    System.Diagnostics.Debug.Assert(false, 
		//    //    //        "Exception when trying to create an instance of type " + CurrentType.ToString() + Environment.NewLine + "Exception: " + ex.InnerException.ToString());
		//    //    //} 
		//    //    if (NewBlock != null)
		//    //    {
		//    //        resultBlocks.AddLast(NewBlock);
		//    //    }
		//    //}

		//    // Fill the surrounding Empty Blocks Items with the content from this one
		//    //if (shouldFillSurroundingEmptyBlocks)
		//    //{
		//    //    EmptyBlock parent = (Block as EmptyBlock);
		//    //    EmptyBlock first = (resultBlocks.First.Value as EmptyBlock);
		//    //    EmptyBlock last = (resultBlocks.Last.Value as EmptyBlock);

		//    //    foreach (ReplaceBlocksItem i in parent.Items)
		//    //    {
		//    //         System.Type[] myTypes = new System.Type[i.Types.Count];
		//    //         i.Types.CopyTo(myTypes, 0);
		//    //         if (first != null)
		//    //         {
		//    //             first.Items.Add(new ReplaceBlocksItem(i.Text, myTypes, first, true));
		//    //         }
		//    //         if (first != null)
		//    //         {
		//    //             last.Items.Add(new ReplaceBlocksItem(i.Text, myTypes, last, true));
		//    //         }
		//    //    }
		//    //}

		//    //return resultBlocks;
		//}
	}
}
