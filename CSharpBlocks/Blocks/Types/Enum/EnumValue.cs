using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;
using GuiLabs.Utils;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("value")]
	public class EnumValue : DraggableTextLine, ICSharpBlock
	{
		#region ctor

		public EnumValue()
			: base()
		{

		}

		public EnumValue(string text)
			: base(text)
		{

		}

		#endregion

		#region OnEvents

		protected override void OnKeyDownReturn(System.Windows.Forms.KeyEventArgs e)
		{
			if (this.Prev != null && this.Next == null && this.Text == "")
			{
				using (Redrawer r = new Redrawer(Root))
				{
					this.RemoveFocus(MoveFocusDirection.SelectNextInChain);
					this.Delete();
				}
				return;
			}

			if (e.Control
				&& ParentParent != null
				&& ParentParent.Next != null)
			{
				ParentParent.Next.SetFocus();
			}
			else
			{
				base.OnKeyDownReturn(e);
			}
		}

		#endregion

		public bool IsLastValue
		{
			get
			{
				return FindNextValue() == null;
			}
		}

		public EnumValue FindNextValue()
		{
			Block current = this.Next;
			while (current != null)
			{
				EnumValue suspect = current as EnumValue;
				if (suspect != null && suspect.IsValue)
				{
					return suspect;
				}
				current = current.Next;
			}
			return null;
		}

		public bool IsValue
		{
			get
			{
				return !string.IsNullOrEmpty(this.Text);
			}
		}

		#region AcceptVisitor

		public void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion

		#region Help

		protected override System.Collections.Generic.IEnumerable<string> GetCompletionHelp()
		{
			return Strings.EmptyArray;
		}

		#endregion

	}
}
