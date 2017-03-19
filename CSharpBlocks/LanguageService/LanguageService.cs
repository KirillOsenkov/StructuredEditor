using System;
using System.Collections.Generic;
using GuiLabs.Editor.Blocks;
using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.CSharp
{
	public partial class LanguageService
	{
		#region ctor

		public LanguageService()
		{
			InitParser();
		}

		#endregion

		#region Events

		public delegate void ProvideCompletionEventHandler(
			TextBoxBlockWithCompletion codeBlock,
			ProvideCompletionEventArgs e);

		public class ProvideCompletionEventArgs : EventArgs
		{
			public ProvideCompletionEventArgs()
			{

			}

			public ProvideCompletionEventArgs(TextBoxBlockWithCompletion statementLine)
			{
				Statement = statementLine;
			}

			private TextBoxBlockWithCompletion mStatement;
			public TextBoxBlockWithCompletion Statement
			{
				get
				{
					return mStatement;
				}
				set
				{
					mStatement = value;
				}
			}

			public ICompletionListBuilder Items;

			public CompletionContext Context;

			//			public ProvideCompletionEventArgs(
			//				MethodBlock containingMethod, 
			//				ClassBlock containingClass)
			//			{
			//				Class = containingClass;
			//				Method = containingMethod;
			//			}
			//
			//			public ProvideCompletionEventArgs(
			//				PropertyAccessorBlock containingPropertyAccessor, 
			//				ClassBlock containingClass)
			//			{
			//				Class = containingClass;
			//				Method = containingMethod;
			//			}

			//			private ClassBlock mClass;
			//			public ClassBlock Class
			//			{
			//				get
			//				{
			//					return mClass;
			//				}
			//				set
			//				{
			//					mClass = value;
			//				}
			//			}
			//
			//			private MethodBlock mMethod;
			//			public MethodBlock Method
			//			{
			//				get
			//				{
			//					return mMethod;
			//				}
			//				set
			//				{
			//					mMethod = value;
			//				}
			//			}
			//			
			//			private PropertyAccessorBlock mPropertyAccessor;
			//			public PropertyAccessorBlock PropertyAccessor
			//			{
			//				get
			//				{
			//					return mPropertyAccessor;
			//				}
			//				set
			//				{
			//					mPropertyAccessor = value;
			//				}
			//			}
		}

		public event ProvideCompletionEventHandler ProvideCompletion;
		internal void RaiseProvideCompletion(
			TextBoxBlockWithCompletion statementBlock,
			ICompletionListBuilder items,
			CompletionContext context)
		{
			if (ProvideCompletion != null)
			{
				ProvideCompletionEventArgs e = new ProvideCompletionEventArgs(
					statementBlock);
				e.Items = items;
				e.Context = context;
				ProvideCompletion(statementBlock, e);
			}
		}

		#endregion

		#region API

		ReflectionNamespaceManager namespaceManager;

		public virtual IEnumerable<string> GetNamespaces(string namespaceName)
		{
			if (namespaceManager == null)
			{
				namespaceManager = new ReflectionNamespaceManager();
			}
			return namespaceManager.GetNamespaces(namespaceName);
		}

        public virtual IEnumerable<Type> GetAttributeTypes()
        {
            if (namespaceManager == null)
            {
                namespaceManager = new ReflectionNamespaceManager();
            }
            return namespaceManager.AttributeTypes;
        }

		public static ParameterList ParseParameters(TextBoxBlock parametersText)
		{
			LanguageService ls = Get(parametersText);
			if (ls != null && ls.Parser != null)
			{
				return ls.Parser.ParseParameters(parametersText.Text);
			}
			return null;
		}

		public static void GetCompletion(
			TextBoxBlockWithCompletion textBox,
			ICompletionListBuilder items,
			CompletionContext context)
		{
			LanguageService ls = Get(textBox);
			if (ls != null)
			{
				ls.RaiseProvideCompletion(textBox, items, context);
			}
		}

		#endregion

		#region Parser Service

		protected virtual void InitParser()
		{
			Parser = new ParserService();
		}

		private ParserService mParser;
		public ParserService Parser
		{
			get
			{
				return mParser;
			}
			set
			{
				mParser = value;
			}
		}

		#endregion

		#region Get

		public static LanguageService Get(Block blockFromTreeWithLanguageService)
		{
			if (blockFromTreeWithLanguageService != null
				&& blockFromTreeWithLanguageService.Root != null)
			{
				CodeUnitBlock codeUnit = blockFromTreeWithLanguageService.Root as CodeUnitBlock;
				if (codeUnit != null)
				{
					return codeUnit.LanguageService;
				}
			}
			return null;
		}

		#endregion
	}
}
