using GuiLabs.Editor.Blocks;
using System.Collections.Generic;
using GuiLabs.Editor.UI;

namespace GuiLabs.Editor.CSharp
{
	public class ExpressionBlock : CodeLine
	{
		#region ctors

		public ExpressionBlock()
			: base()
		{
			Init();
		}

		public ExpressionBlock(string initialText)
			: base(initialText)
		{
			Init();
		}
		
		private void Init()
		{
			this.Multiline = false;
		}

		#endregion

		#region Completion
		
		protected override void FillItems(CustomItemsRequestEventArgs e)
		{
			LanguageService.GetCompletion(this, e.Items, this.Context);
		}
		
		private CompletionContext mContext;
		public CompletionContext Context
		{
			get
			{
				return mContext;
			}
			set
			{
				mContext = value;
			}
		}
		
		#endregion
		
		#region Style

		protected override string StyleName()
		{
			return "ExpressionBlock";
		}

		#endregion

		#region Help

		private static string[] mHelpStrings = new string[]
		{

		};
		public override IEnumerable<string> HelpStrings
		{
			get
			{
				foreach (string current in mHelpStrings)
				{
					yield return current;
				}
				foreach (string baseString in GetOldHelpStrings())
				{
					yield return baseString;
				}
			}
		}
		private IEnumerable<string> GetOldHelpStrings()
		{
			return base.HelpStrings;
		}

		#endregion
	}
}
