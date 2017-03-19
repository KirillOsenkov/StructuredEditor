using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("get")]
	public class PropertyGetBlock : PropertyAccessorBlock
	{
		#region ctors

		public PropertyGetBlock()
			: base("get")
		{

		}

		public PropertyGetBlock(BlockStatementBlock statementBlock)
			: base("get", statementBlock)
		{

		}

		#endregion

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ((e.KeyCode == System.Windows.Forms.Keys.Return
				|| e.KeyCode == System.Windows.Forms.Keys.Insert)
				&& ParentProperty != null)
			{
				if (ParentProperty.SetAccessor == null)
				{
					ParentProperty.SetAccessor = new PropertySetBlock();
					e.Handled = true;
				}
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		#endregion
	}
}