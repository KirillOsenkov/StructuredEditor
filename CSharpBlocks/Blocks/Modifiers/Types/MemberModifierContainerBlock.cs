using System;
using System.Collections.Generic;
using System.Reflection;

namespace GuiLabs.Editor.CSharp
{
	public class MemberModifierContainerBlock : ModifierContainer
	{
		#region ctor

		public MemberModifierContainerBlock()
			: base()
		{
			this.AddModifierKinds(
				"MethodNew",
				"MemberAccess",
				"MethodInheritance",
				"MethodExtern"
			);
			AddTypeSelectionBlock();
		}

		#endregion

		public virtual void AddTypeSelectionBlock()
		{
			TypeBlock = new TypeSelectionBlock(this);
		}
		
		private TypeSelectionBlock mTypeBlock;
		public TypeSelectionBlock TypeBlock
		{
			get
			{
				return mTypeBlock;
			}
			set
			{
				if (mTypeBlock != null)
				{
					RemoveModifierBlock(mTypeBlock);
				}
				mTypeBlock = value;
				if (mTypeBlock != null)
				{
					AddModifierBlock(mTypeBlock);
				}
			}
		}
	}
}
