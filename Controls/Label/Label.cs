using GuiLabs.Canvas.Renderer;
using GuiLabs.Utils;
using System;

namespace GuiLabs.Canvas.Controls
{
	public class Label : Control, IHasText, ITextProvider
	{
		#region ctor

		public Label()
			: base()
		{

		}

		public Label(ITextProvider bindToText)
			: base()
		{
			Param.CheckNotNull(bindToText, "bindToText");
			TextProvider = bindToText;
		}

		public Label(string text)
			: base()
		{
			this.Text = text;
		}

		#endregion

		#region Binding

		private ITextProvider mTextProvider;
		public ITextProvider TextProvider
		{
			get
			{
				return mTextProvider;
			}
			set
			{
				if (mTextProvider != null)
				{
					mTextProvider.TextChanged -= TextProvider_TextChanged;
				}
				mTextProvider = value;
				if (mTextProvider != null)
				{
					mTextProvider.TextChanged += TextProvider_TextChanged;
				}
				Layout();
			}
		}

		#endregion

		#region Events

		public event TextChangedEventHandler TextChanged;
		protected void RaiseTextChanged(string oldText, string newText)
		{
			if (TextChanged != null)
			{
				TextChanged(this, oldText, newText);
			}
		}

		#endregion

		#region Draw & Layout

		public override void DrawCore(IRenderer Renderer)
		{
			if (this.Visible)
			{
				//if (this.IsFocused)
				//{
				//    Renderer.DrawOperations.DrawShadow(this.Bounds);
				//}
				base.DrawCore(Renderer);
				Renderer.DrawOperations.DrawString(
					this.Text, 
					this.Bounds,
					this.CurrentStyle.FontStyleInfo);
			}
		}

		public override void LayoutCore()
		{
			Bounds.Size.Set(
				RendererSingleton.DrawOperations.StringSize(
				Text,
				CurrentStyle.FontStyleInfo.Font)
			);
		}

		public Point StringSize(string s)
		{
			return RendererSingleton.DrawOperations.StringSize(
				s,
				CurrentStyle.FontStyleInfo.Font);
		}

		#endregion

		#region Text

		private string mText;
		public string Text
		{
			get
			{
				if (TextProvider != null)
				{
					return TextProvider.Text;
				}
				return mText;
			}
			set
			{
				if (mText != value)
				{
					mText = value;
					Layout();
					Redraw();
				}
			}
		}

		void TextProvider_TextChanged(ITextProvider sender, string oldText, string newText)
		{
			this.Layout();
			RaiseTextChanged(oldText, newText);
			Redraw();
		}

		public override string ToString()
		{
			return Text;
		}

		public void SetText(string newValue)
		{
			if (TextProvider != null)
			{
				throw new InvalidOperationException(
					"Cannot directly set the .Text property of a Label"
					+ " when the Label is bound to a TextProvider."
					+ " If you want to change the text, change the text"
					+ " in the TextProvider and the Label will update itself automatically.");
			}
			if (mText != newValue)
			{
				string oldText = mText;
				mText = newValue;
				Layout();
				RaiseTextChanged(oldText, newValue);
				Redraw();
			}
		}

		#endregion
	}
}
