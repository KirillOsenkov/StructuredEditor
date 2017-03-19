using System;

namespace GuiLabs.Editor.Blocks
{
	public class BlockSerializationAttribute : Attribute
	{
		public BlockSerializationAttribute(string name)
		{
			Name = name;
		}

		private string mName;
		public string Name
		{
			get
			{
				return mName;
			}
			set
			{
				mName = value;
			}
		}
	}
}
