using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("set")]
	public class PropertySetBlock : PropertyAccessorBlock
	{
		#region ctors

		public PropertySetBlock()
			: base("set")
		{
			Init();
		}

		public PropertySetBlock(BlockStatementBlock statementBlock)
			: base("set", statementBlock)
		{
			Init();
		}

		private void Init()
		{
			Parameters.Add(Parameter.CreateValueParameter());
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ((e.KeyCode == System.Windows.Forms.Keys.Return
				|| e.KeyCode == System.Windows.Forms.Keys.Insert)
				&& ParentProperty != null)
			{
				if (ParentProperty.GetAccessor == null)
				{
					ParentProperty.GetAccessor = new PropertyGetBlock();
					e.Handled = true;
				}
			}

			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		#endregion

		#region Parameters

		private ParameterList mParameters = new ParameterList();
		public ParameterList Parameters
		{
			get
			{
				if (this.ParentProperty != null)
				{
					mParameters["value"].Type = this.ParentProperty.TypeText;
				}
				return mParameters;
			}
		}

		#endregion

	}
}
