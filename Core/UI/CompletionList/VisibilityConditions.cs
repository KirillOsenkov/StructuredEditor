using System;
using System.Collections.Generic;
using System.Text;
using GuiLabs.Utils;
using GuiLabs.Canvas.Controls;

namespace GuiLabs.Editor.UI
{
	public static class VisibilityConditions
	{
		
	}

	public class EmptyTextCondition : ICondition
	{
		public EmptyTextCondition(ITextProvider provider)
		{
			Provider = provider;
		}

		private ITextProvider mProvider;
		public ITextProvider Provider
		{
			get
			{
				return mProvider;
			}
			set
			{
				mProvider = value;
			}
		}

		public bool IsTrue()
		{
			return Provider.Text.Length <= 1;
		}
	}
}
