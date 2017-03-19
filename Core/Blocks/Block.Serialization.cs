using System;
using System.Collections.Generic;
using System.IO;
using GuiLabs.Editor.Actions;
using GuiLabs.Utils;

namespace GuiLabs.Editor.Blocks
{
	partial class Block
	{
		public virtual Block Clone()
		{
			Memento snapshot = this.CreateSnapshot();
			if (snapshot == null)
			{
				return Activator.CreateInstance(this.GetType()) as Block;
			}
			return BlockFactory.Instance.CreateBlock(snapshot);
		}

		public void CopyAfterBlock(Block afterBlock)
		{
			IEnumerable<Block> blocksToCopy = PrepareBlocksToMove();
			IEnumerable<Block> newBlocks = BlockActions.Clone(blocksToCopy);
			afterBlock.AppendBlocks(newBlocks);
		}

		public virtual Memento CreateSnapshot()
		{
			return CreateMemento();
		}

		protected internal Memento CreateMemento()
		{
			Memento m = new Memento();
			m.NodeType = SerializationName;
			WriteToMemento(m);
			return m;
		}

		private string mSerializationName;
		public string SerializationName
		{
			get
			{
				if (mSerializationName == null)
				{
					mSerializationName = GetSerializationName();
				}
				return mSerializationName;
			}
		}

		private string GetSerializationName()
		{
			BlockSerializationAttribute attr = Reflector.GetAttribute<BlockSerializationAttribute>(this.GetType());
			if (attr != null)
			{
				return attr.Name;
			}
			return this.GetType().Name;
		}

		public virtual void AddChildren(IEnumerable<Block> restoredChildren)
		{
		}

		public virtual void WriteToMemento(Memento storage)
		{
		}

		public virtual void ReadFromMemento(Memento storage)
		{
		}

		public void SaveToFile(string fileName)
		{
			Memento snapshot = this.CreateSnapshot();
			snapshot.SaveToFile(fileName);
		}

		public void LoadFromFile(string fileName)
		{
			string contents = File.ReadAllText(fileName);
			if (!string.IsNullOrEmpty(contents))
			{
				LoadFromString(contents);
			}
		}

		public Block LoadFromString(string contents)
		{
			Memento snapshot = Memento.ReadFromString(contents);
			return BlockFactory.Instance.CreateBlock(snapshot);
		}

		public string Serialize()
		{
			Memento snapshot = this.CreateSnapshot();
			if (snapshot == null)
			{
				return string.Empty;
			}
			return snapshot.WriteToString();
		}
	}
}
