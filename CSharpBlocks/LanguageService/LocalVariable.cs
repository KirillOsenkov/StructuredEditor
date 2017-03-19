using System;

namespace GuiLabs.Editor.CSharp
{
	public class Variable
	{
		#region ctors

		public Variable()
		{

		}

		public Variable(string name, string type)
		{
			Name = name;
			Type = type;
		}

		#endregion

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

		private string mType;
		public string Type
		{
			get
			{
				return mType;
			}
			set
			{
				mType = value;
			}
		}
		
		private bool mIsConst = false;
		public bool IsConst
		{
			get
			{
				return mIsConst;
			}
			set
			{
				mIsConst = value;
			}
		}
	}
}
