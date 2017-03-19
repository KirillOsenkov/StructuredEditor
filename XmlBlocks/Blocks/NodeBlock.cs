using GuiLabs.Editor.Blocks;
using System.Collections.Generic;

namespace GuiLabs.Editor.Xml
{
	[BlockSerialization("node")]
	public class NodeBlock : UniversalBlock, IXmlBlock
	{
		#region ctor

		public NodeBlock()
		{
			NameBlock = new TextBoxBlock();
			Attributes = new AttributeList();

			this.HMembers.Add(NameBlock);
			this.HMembers.Add(Attributes);

			this.VMembers.Add(new EmptyNodeBlock());
			this.VMembers.AddAcceptableBlockTypes<NodeBlock>();
			this.VMembers.AddAcceptableBlockTypes<EmptyNodeBlock>();
			
			this.Draggable = true;
			this.CanMoveUpDown = true;
		}

		#endregion

		#region Name

		private TextBoxBlock mNameBlock;
		public TextBoxBlock NameBlock
		{
			get
			{
				return mNameBlock;
			}
			set
			{
				mNameBlock = value;
				if (mNameBlock != null)
				{
					mNameBlock.MyTextBox.MinWidth = 10;
				}
			}
		}

		#endregion

		#region AttributeList

		private AttributeList mAttributes;
		public AttributeList Attributes
		{
			get
			{
				return mAttributes;
			}
			set
			{
				mAttributes = value;
			}
		}

		#endregion

		#region OnEvents

		protected override void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Up)
			{
				NodeBlock parent = ParentParent as NodeBlock;
				if (Prev == null && parent != null && parent.Parent is XMLRootBlock)
				{
					parent.HMembers.SetDefaultFocus();
					e.Handled = true;
				}
			}
			if (e.KeyCode == System.Windows.Forms.Keys.Return && !e.Control)
			{
				this.AppendBlocks(new EmptyNodeBlock());
				e.Handled = true;
			}
			else if (e.KeyCode == System.Windows.Forms.Keys.Insert
				|| e.KeyCode == System.Windows.Forms.Keys.Return)
			{
				this.PrependBlocks(new EmptyNodeBlock());
				e.Handled = true;
			}
			if (!e.Handled)
			{
				base.OnKeyDown(sender, e);
			}
		}

		#endregion

		public XMLRootBlock RootNode
		{
			get
			{
				return Root as XMLRootBlock;
			}
		}

		#region Memento

		public override void ReadFromMemento(GuiLabs.Utils.Memento storage)
		{
			NameBlock.Text = storage["name"];
		}

		public override void WriteToMemento(GuiLabs.Utils.Memento storage)
		{
			storage["name"] = NameBlock.Text;
		}

		public override void AddChildren(IEnumerable<Block> restoredChildren)
		{
			EmptyNodeBlock firstText = this.VMembers.Children.Head as EmptyNodeBlock;
			if (this.VMembers.Children.Count == 1
				&& firstText != null && firstText.Text == "")
			{
				this.VMembers.Children.Delete(firstText);
			}
			foreach (Block child in restoredChildren)
			{
				this.VMembers.Children.Add(child);
			}
		}

		#endregion

		#region DefaultFocusableControl

		public override GuiLabs.Canvas.Controls.Control DefaultFocusableControl()
		{
			if (string.IsNullOrEmpty(NameBlock.Text))
			{
				return NameBlock.DefaultFocusableControl();
			}
			//return Attributes.DefaultFocusableControl();
			return VMembers.DefaultFocusableControl();
		}

		#endregion

		#region Delete

		public override void Delete()
		{
			if (this.Prev == null && this.Next == null)
			{
				this.Replace(new EmptyNodeBlock());
			}
			else
			{
				base.Delete();
			}
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "NodeBlock";
		}

		#endregion
	}
}