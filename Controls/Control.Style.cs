using System;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Shapes;

namespace GuiLabs.Canvas.Controls
{
	public partial class Control : ShapeWithEvents
	{
		#region Style

		public IShapeStyle CurrentStyle
		{
			get
			{
				if (this.IsFocused)
				{
					return SelectedStyle;
				}
				else
				{
					return Style;
				}
			}
		}

		private IShapeStyle mStyle;
		public IShapeStyle Style
		{
			get
			{
				return mStyle;
			}
			set
			{
				mStyle = value;
			}
		}

		private IShapeStyle mSelectedStyle;
		public IShapeStyle SelectedStyle
		{
			get
			{
				return mSelectedStyle;
			}
			set
			{
				mSelectedStyle = value;
			}
		}

		internal void GetStylesFromFactory()
		{
			Style = StyleFactory.Instance.GetStyle(StyleName);
			SelectedStyle = StyleFactory.Instance.GetStyle(SelectedStyleName);
			if (Style == null || SelectedStyle == null)
			{
				InitStyleBase();
				StyleFactory.Instance.Add(StyleName, Style);
				StyleFactory.Instance.Add(SelectedStyleName, SelectedStyle);
			}
		}

		protected virtual string StyleName
		{
			get
			{
				return "Control";
			}
		}

		protected string SelectedStyleName
		{
			get
			{
				return StyleName + "_selected";
			}
		}

		/// <summary>
		/// Is a template method which calls virtual methods
		/// InitStyle and InitSelectedStyle
		/// Just override these methods in children classes
		/// to define a style for a block
		/// </summary>
		private void InitStyleBase()
		{
			Style = new ShapeStyle();
			SelectedStyle = new ShapeStyle();
			InitStyle();
			InitSelectedStyle();
			if (Style == null || SelectedStyle == null)
			{
				// TODO: Make a new RecoverableException class

				// <Kirill 2005-10-30>
				// what does this mean?
				// What do you mean by RecoverableException
				// and why would we need it here?
				// </Kirill>

				// most probably RecoverableException could be suppressed
				// at some outer stage, in contrast to other (unrecoverable)
				// exceptions which would lead to the 
				// unrecoverable exception / bug report dialog
				throw new Exception("InitStyle() or InitSelectedStyle() has set the style to a null value.");
			}
		}

		protected virtual void InitStyle()
		{
			Style.LineColor = System.Drawing.Color.Transparent;
			Style.FillColor = System.Drawing.Color.Transparent;
		}

		protected virtual void InitSelectedStyle()
		{
			SelectedStyle.LineColor = System.Drawing.Color.Transparent;
			SelectedStyle.FillColor = System.Drawing.Color.Transparent;
		}

		#endregion
	}
}
