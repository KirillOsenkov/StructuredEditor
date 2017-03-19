namespace GuiLabs.Editor.Blocks
{
	public class TokenLineBlock : LineBlock
	{
		public TokenLineBlock()
			: base()
		{
			AddFirstChild();

			this.Children.ElementAdded += Children_ElementAdded;
			this.Children.ElementRemoved += Children_ElementRemoved;
			this.Children.ElementReplaced += Children_ElementReplaced;
			this.Children.CollectionChanged += Children_CollectionChanged;
		}

		private TokenList mTokens = new TokenList();
		public TokenList Tokens
		{
			get
			{
				return mTokens;
			}
			set
			{
				mTokens = value;
			}
		}

		public void UpdateTokenList()
		{
			foreach (Block child in this.Children)
			{
				IToken token = child as IToken;
				if (token != null && token.Text != "")
				{
					Tokens.Add(token);
				}
			}
		}

		void Children_CollectionChanged()
		{
			UpdateTokenList();
		}



		protected virtual void AddFirstChild()
		{
			this.Children.Add(new TokenSeparatorBlock());
		}

		void Children_ElementReplaced(Block oldElement, Block newElement)
		{
			UnSubscribeElement(oldElement);
			SubscribeElement(newElement);
		}

		void Children_ElementAdded(Block element)
		{
			SubscribeElement(element);
		}

		void Children_ElementRemoved(Block element)
		{
			UnSubscribeElement(element);
		}

		private void SubscribeElement(Block element)
		{
			//ITextToken token = element as ITextToken;
			//if (token != null)
			//{
			//    token.TextChanged += token_TextChanged;
			//}
		}

		private void UnSubscribeElement(Block element)
		{
			//ITextToken token = element as ITextToken;
			//if (token != null)
			//{
			//    token.TextChanged -= token_TextChanged;
			//}
		}

		//void token_TextChanged(TextBoxBlock textBox, string oldText)
		//{
		//    if (textBox.Text == "")
		//    {
		//        textBox.Delete();
		//    }
		//}
	}
}
