using System;
using System.Collections.Generic;
using GuiLabs.Utils;
using System.Collections.ObjectModel;

namespace GuiLabs.Editor.Blocks
{
	/// <summary>
	/// Stores a list of block types: Type[]
	/// BlockActivatorFactory.CreateBlocks() creates instances of those types
	/// and returns the instances 
	/// </summary>
	public class BlockActivatorFactory : IBlockFactory
	{
		public BlockActivatorFactory(IEnumerable<Type> typeList)
		{
			types = typeList;
		}

		private IEnumerable<Type> types;

		//public bool Contains(Type type)
		//{
		//    foreach (Type t in types)
		//    {
		//        if (type == t)
		//            return true;
		//    }

		//    return false;
		//}

		public static BlockActivatorFactory CreateFactory<T1>()
		{
			return new BlockActivatorFactory(Types<T1>());
		}

		public static BlockActivatorFactory CreateFactory<T1, T2>()
		{
			return new BlockActivatorFactory(Types<T1, T2>());
		}

		public static BlockActivatorFactory CreateFactory<T1, T2, T3>()
		{
			return new BlockActivatorFactory(Types<T1, T2, T3>());
		}

		public static IEnumerable<Type> Types<T1>()
		{
			yield return typeof(T1);
		}

		public static IEnumerable<Type> Types<T1, T2>()
		{
			yield return typeof(T1);
			yield return typeof(T2);
		}

		public static IEnumerable<Type> Types<T1, T2, T3>()
		{
			yield return typeof(T1);
			yield return typeof(T2);
			yield return typeof(T3);
		}

		/// <summary>
		/// For every Type in types, creates an instance of that type
		/// and returns 
		/// </summary>
		/// <returns>A list of created instances for all types</returns>
		public ReadOnlyCollection<Block> CreateBlocks()
		{
			List<Block> result = new List<Block>();

			if (types == null)
			{
				return result.AsReadOnly();
			}

			foreach (Type type in types)
			{
				Block block = null;

				try
				{
					block = System.Activator.CreateInstance(type) as Block;
				}
				catch (Exception ex)
				{
					Log.NonCriticalException(
						ex,
						"BlockActivatorFactory.CreateBlocks: Couldn't instantiate: " + type.ToString());
				}

				if (block != null)
				{
					result.Add(block);
				}
			}

			return result.AsReadOnly();
		}

		public Block CreateBlock()
		{
			ReadOnlyCollection<Block> results = CreateBlocks();
			if (results != null && results.Count > 0)
			{
				return results[0];
			}
			return null;
		}
	}
}
