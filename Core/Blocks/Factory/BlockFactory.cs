using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GuiLabs.Utils;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GuiLabs.Editor.Blocks
{
	using SerAttr = TypeAttributeInfo<BlockSerializationAttribute>;
	using GuiLabs.Editor.Actions;
	using System.Windows.Forms;
    using GuiLabs.Undo;

	public class BlockFactory
	{
		#region Singleton

		protected BlockFactory()
		{
			//StringBuilder s = new StringBuilder();

			//double time = Timer.ms;

			Assembly[] assemblies = Reflector.GetLoadedAssemblies();
			ReadOnlyCollection<SerAttr> types = Reflector.FindTypesWithAttribute
				<BlockSerializationAttribute>(assemblies);

			foreach (SerAttr t in types)
			{
				//s.AppendLine(t.TypeInfo.Name + " " + t.AttrInfo.Name);
				TypeNameMap.Add(t.AttrInfo.Name, t.TypeInfo);
			}

			//s.AppendLine("Elapsed time: " + (Timer.ms - time).ToString());
			//System.Windows.Forms.MessageBox.Show(s.ToString());
		}

		private static BlockFactory mInstance = null;
		public static BlockFactory Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new BlockFactory();
				}
				return mInstance;
			}
		}

		#endregion

		private static Dictionary<string, Type> TypeNameMap = new Dictionary<string, Type>();

		public Block CreateBlock(Memento storage)
		{
			Block b = InstantiateBlock(storage);
			if (b == null)
			{
				return null;
			}
			b.ReadFromMemento(storage);

			if (storage.Children.Count != 0)
			{
				if (b.Root != null)
				{
					using (b.Transaction())
					{
						b.AddChildren(CreateChildren(storage));
					}
				}
				else
				{
					b.AddChildren(CreateChildren(storage));
				}
			}
			return b;
		}

		private Type FindType(string typeAlias)
		{
			Type result = null;
			TypeNameMap.TryGetValue(typeAlias, out result);
			return result;
		}

		private Block InstantiateBlock(Memento storage)
		{
			if (storage == null)
			{
				return null;
			}
			try
			{
				Type t = FindType(storage.NodeType);
				if (t == null)
				{
					t = Type.GetType(storage.NodeType, false, false);
				}
				if (t == null)
				{
					return null;
				}

				Block b = Activator.CreateInstance(t) as Block;
				return b;
			}
			catch (Exception)
			{
				return null;
			}
		}

		private IEnumerable<Block> CreateChildren(Memento storage)
		{
			foreach (Memento childMemento in storage.Children)
			{
				yield return CreateBlock(childMemento);
			}
		}

		#region Clipboard

		public IEnumerable<Block> GetBlocksFromClipboard()
		{
			if (!Clipboard.ContainsData(Block.ClipboardFormat))
			{
				return Block.EmptyArray;
			}
			string clipBoard = Clipboard.GetData(Block.ClipboardFormat) as string;
			if (string.IsNullOrEmpty(clipBoard))
			{
				return Block.EmptyArray;
			}
			Memento snapshot = Memento.ReadFromString(clipBoard);
			if (snapshot == null)
			{
				return Block.EmptyArray;
			}
			Block toPaste = BlockFactory.Instance.CreateBlock(snapshot);
			return new Block[] { toPaste };
		}

		#endregion
	}
}
