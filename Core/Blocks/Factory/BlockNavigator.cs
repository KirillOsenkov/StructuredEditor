using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace GuiLabs.Editor.Blocks
{
	public delegate Block BlockNavigatorDelegate(Block source);

	public class BlockNavigator
	{
		public static Block ToParent(Block source)
		{
			if (source == null)
			{
				return null;
			}
			return source.Parent;
		}

		public static Block ToItself(Block source)
		{
			return source;
		}

		public static Block ToNext(Block source)
		{
			if (source == null)
			{
				return null;
			}
			return source.Next;
		}

		public static Block ToPrev(Block source)
		{
			if (source == null)
			{
				return null;
			}
			return source.Prev;
		}
	}

	//public class BlockNavigator
	//{
	//    protected BlockNavigator()
	//    {
	//        NavigationFunction = Identity;
	//    }

	//    private BlockNavigatorDelegate mNavigationFunction;
	//    public BlockNavigatorDelegate NavigationFunction
	//    {
	//        get
	//        {
	//            return mNavigationFunction;
	//        }
	//        set
	//        {
	//            mNavigationFunction = value;
	//        }
	//    }

	//    public ReadOnlyCollection<Block> FindStartingFromBlock(Block start)
	//    {
	//        if (NavigationFunction != null)
	//        {
	//            return NavigationFunction(start);
	//        }
	//    }

	//    protected ReadOnlyCollection<Block> Identity(Block start)
	//    {
	//        return new ReadOnlyCollection<Block>(new Block[] { start });
	//    }

	//    protected ReadOnlyCollection<Block> Parent(Block start)
	//    {

	//    }

	//    public BlockNavigator Parent
	//    {
	//        get
	//        {
	//            return new BlockNavigatorToParent(this);
	//        }
	//    }

	//    public class CustomBlockNavigator
	//    {

	//    }


	//    public class BlockNavigatorToParent : BlockNavigator
	//    {
	//        public BlockNavigatorToParent(BlockNavigator source)
	//        {
	//            Source = source;
	//        }

	//        private BlockNavigator Source;

	//        public override Block FindStartingFromBlock(Block start)
	//        {
	//            return Source.FindStartingFromBlock(start).Parent;
	//        }
	//    }

	//    public class BlockNavigatorToNext : BlockNavigator
	//    {
	//        public BlockNavigatorToNext(BlockNavigator source)
	//        {
	//            Source = source;
	//        }

	//        private BlockNavigator Source;

	//        public override Block FindStartingFromBlock(Block start)
	//        {
	//            return Source.FindStartingFromBlock(start).Next;
	//        }
	//    }

	//    public class BlockNavigatorToItself : BlockNavigator
	//    {
	//        public override Block FindStartingFromBlock(Block start)
	//        {
	//            return start;
	//        }
	//    }
	//}
}
