using GuiLabs.Editor.Actions;
using GuiLabs.Editor.Blocks;
using GuiLabs.Utils;
using GuiLabs.Undo;

namespace GuiLabs.Editor.CSharp
{
	[BlockSerialization("interfaceMember")]
	public class InterfaceMemberDeclarationBlock : CodeLineBlock, ICSharpBlock
	{
		#region ctor

		public InterfaceMemberDeclarationBlock()
		{
			this.MyControl.Box.Margins.SetTopAndBottom(2);
			Text = new InterfaceMemberTextBlock();
			this.Children.CollectionChanged += Check;
		}

		#endregion

		#region OnEvents

		/// <summary>
		/// Inserts a new member on pressing Return
		/// </summary>
		protected override void Children_KeyDown(Block block, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Return)
			{
				if (!Text.MyTextBox.IsFocused || !Text.MyTextBox.CaretIsAtBeginning)
				{
					InsertNewMember();
				}
				else if (string.IsNullOrEmpty(Text.Text) || Text.MyTextBox.CaretIsAtBeginning)
				{
					PrependNewMember();
				}
				e.Handled = true;
			}

			if (!e.Handled)
			{
				base.Children_KeyDown(block, e);
			}
		}

		#endregion

		#region Check

		private void Check()
		{
			bool propertyAccessorsFound = false;
			bool parametersFound = false;
			bool thisParametersFound = false;

			foreach (Block b in this.Children)
			{
				InterfaceAccessorsBlock acc = b as InterfaceAccessorsBlock;
				if (acc != null)
				{
					if (PropertyAccessors != acc)
					{
						PropertyAccessors = acc;
					}
					propertyAccessorsFound = true;
				}
				ParameterListBlock par = b as ParameterListBlock;
				if (par != null)
				{
					if (par.TypeOfBraces == ParameterListBlock.TypeOfParentheses.Parentheses)
					{
						if (Parameters != par)
						{
							Parameters = par;
						}
						parametersFound = true;
					}
					else if (par.TypeOfBraces == ParameterListBlock.TypeOfParentheses.SquareBrackets)
					{
						if (ThisParameters != par)
						{
							ThisParameters = par;
						}
						thisParametersFound = true;
					}
				}
			}
			if (!propertyAccessorsFound && PropertyAccessors != null)
			{
				PropertyAccessors = null;
			}
			if (!parametersFound && Parameters != null)
			{
				Parameters = null;
			}
			if (!thisParametersFound && ThisParameters != null)
			{
				ThisParameters = null;
			}
		}

		#endregion

		#region Text

		private InterfaceMemberTextBlock mText;
		public InterfaceMemberTextBlock Text
		{
			get
			{
				return mText;
			}
			set
			{
				if (mText != null)
				{
					mText.KeyDown -= mText_KeyDown;
					mText.MyTextBox.PreviewKeyPress -= MyTextBox_PreviewKeyPress;
					mText.Delete();
				}
				mText = value;
				if (mText != null)
				{
					mText.KeyDown += mText_KeyDown;
					mText.MyTextBox.PreviewKeyPress += MyTextBox_PreviewKeyPress;
					this.Add(mText);
				}
			}
		}

		void MyTextBox_PreviewKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '(')
			{
				AppendParameters();
				e.Handled = true;
			}
			else if (e.KeyChar == '{')
			{
				AppendPropertyAccessors(true, true);
				e.Handled = true;
			}
			else if (e.KeyChar == '[')
			{
				AppendThisParameters("");
				e.Handled = true;
			}
		}

		void mText_KeyDown(Block block, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Delete)
			{
				if (Text.MyTextBox.CaretIsAtEnd && Parameters != null)
				{
					Parameters.Delete();
					e.Handled = true;
				}
				else if (string.IsNullOrEmpty(Text.Text) && (this.Prev != null || this.Next != null))
				{
					this.Delete();
					e.Handled = true;
				}
			}
			else if (e.KeyCode == System.Windows.Forms.Keys.Back)
			{
				if (string.IsNullOrEmpty(Text.Text) && this.Prev != null)
				{
					this.Delete();
					e.Handled = true;
				}
			}
		}

		#endregion

		#region Parameters

		public void AppendParameters()
		{
			if (!CanAppendParameters())
			{
				return;
			}
			Parameters = new ParameterListBlock();
			Text.AppendBlocks(Parameters);
		}

		public void AppendParameters(string parameters)
		{
			if (!CanAppendParameters())
			{
				return;
			}
			Parameters = new ParameterListBlock(parameters);
			Text.AppendBlocks(Parameters);
		}

		public string ParametersText
		{
			get
			{
				if (Parameters == null)
				{
					return string.Empty;
				}
				return Parameters.Text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					return;
				}
				AppendParameters(value);
			}
		}

		public void AppendParameters(ParameterListBlock block)
		{
			if (!CanAppendParameters())
			{
				return;
			}
			Parameters = block;
			Text.AppendBlocks(Parameters);
		}

		private bool CanAppendParameters()
		{
			return Parameters == null
				&& PropertyAccessors == null
				&& ThisParameters == null;
		}

		private ParameterListBlock mParameters;
		public ParameterListBlock Parameters
		{
			get
			{
				return mParameters;
			}
			set
			{
				mParameters = value;
			}
		}

		#endregion

		#region This Parameters

		public void AppendThisParameters(string parameters)
		{
			if (!CanAppendThisParameters())
			{
				return;
			}
			ThisParameters = new ParameterListBlock(parameters);
			ThisParameters.TypeOfBraces = ParameterListBlock.TypeOfParentheses.SquareBrackets;
			Text.AppendBlocks(ThisParameters);
		}

		private bool CanAppendThisParameters()
		{
			return ThisParameters == null && Parameters == null;
		}

		public string ThisParametersText
		{
			get
			{
				if (ThisParameters == null)
				{
					return string.Empty;
				}
				return ThisParameters.Text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					return;
				}
				AppendThisParameters(value);
			}
		}

		private ParameterListBlock mThisParameters;
		public ParameterListBlock ThisParameters
		{
			get
			{
				return mThisParameters;
			}
			set
			{
				mThisParameters = value;
			}
		}

		#endregion

		#region PropertyAccessors

		public void AppendPropertyAccessors(string accessorsString)
		{
			bool hasGetter = accessorsString.Contains("get");
			bool hasSetter = accessorsString.Contains("set");
			AppendPropertyAccessors(hasGetter, hasSetter);
		}

		public void AppendPropertyAccessors(bool hasGetter, bool hasSetter)
		{
			if (!CanAppendPropertyAccessors())
			{
				return;
			}

			PropertyAccessors = new InterfaceAccessorsBlock();
			if (hasGetter)
			{
				PropertyAccessors.AddGetter();
			}
			if (hasSetter)
			{
				PropertyAccessors.AddSetter();
			}

			using (Root.Transaction())
			{
				if (ThisParameters != null)
				{
					ThisParameters.AppendBlocks(PropertyAccessors);
				}
				else
				{
					Text.AppendBlocks(PropertyAccessors);
				}
				Text.Text = Text.Text.TrimEnd(null);
			}
		}

		private bool CanAppendPropertyAccessors()
		{
			return PropertyAccessors == null
				&& Parameters == null;
		}

		public string PropertyAccessorsText
		{
			get
			{
				string result = string.Empty;
				if (PropertyAccessors == null)
				{
					return string.Empty;
				}
				if (PropertyAccessors.Getter != null)
				{
					result = "get";
				}
				if (PropertyAccessors.Setter != null)
				{
					if (!string.IsNullOrEmpty(result))
					{
						result += " set";
					}
					else
					{
						result = "set";
					}
				}
				return result;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					return;
				}
				AppendPropertyAccessors(value);
			}
		}

		private InterfaceAccessorsBlock mPropertyAccessors;
		public InterfaceAccessorsBlock PropertyAccessors
		{
			get
			{
				return mPropertyAccessors;
			}
			set
			{
				mPropertyAccessors = value;
			}
		}

		#endregion

		#region Memento

		public override void ReadFromMemento(Memento storage)
		{
			this.Text.Text = storage["text"];
			PropertyAccessorsText = storage["accessors"];
			ParametersText = storage["parameters"];
			ThisParametersText = storage["indexerParameters"];
		}

		public override void WriteToMemento(Memento storage)
		{
			storage["text"] = this.Text.Text;
			storage["accessors"] = PropertyAccessorsText;
			storage["parameters"] = ParametersText;
			storage["indexerParameters"] = ThisParametersText;
		}

		public override System.Collections.Generic.IEnumerable<Block> GetChildrenToSerialize()
		{
			return null;
		}

		#endregion

		#region Insert new member

		public void PrependNewMember()
		{
			this.PrependBlocks(new InterfaceMemberDeclarationBlock());
		}

		public void InsertNewMember()
		{
			this.AppendBlocks(new InterfaceMemberDeclarationBlock());
			this.Next.SetFocus();
		}

		#endregion

		#region AcceptVisitor

		public void AcceptVisitor(IVisitor Visitor)
		{
			Visitor.Visit(this);
		}

		#endregion
	}
}
