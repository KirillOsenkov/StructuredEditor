using System;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Canvas.Shapes;
using GuiLabs.Utils.Delegates;

namespace GuiLabs.Canvas.Controls
{
	public enum StretchMode
	{
		None,
		Horizontal,
		Vertical,
		Both
	}

	public partial class Control : ShapeWithEvents
	{
		#region Stretch

		private StretchMode mStretch = StretchMode.None;
		public StretchMode Stretch
		{
			get
			{
				return mStretch;
			}
			set
			{
				mStretch = value;
			}
		}

		public bool ShouldStretchHorizontally
		{
			get
			{
				return Stretch == StretchMode.Horizontal || Stretch == StretchMode.Both;
			}
		}

		public bool ShouldStretchVertically
		{
			get
			{
				return Stretch == StretchMode.Vertical || Stretch == StretchMode.Both;
			}
		}

		#endregion
	}
}
