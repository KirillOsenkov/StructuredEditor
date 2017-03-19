using System.Windows.Forms;
using GuiLabs.Canvas.Controls;
using GuiLabs.Canvas.Events;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Editor.Actions;

namespace GuiLabs.Editor.Blocks
{
	public class SpaceBlock : Block
	{
		public SpaceBlock() : base()
		{
			InitControl();
			this.MyShapeControl.Focusable = true;
			this.MyShapeControl.Stretch = StretchMode.None;
		}

		#region Control

		void InitControl()
		{
			MyShapeControl = new RectShape();
			MyShapeControl.Size.Set(
				MyShapeControl.Style.FontStyleInfo.Font.SpaceCharSize.X,
				MyShapeControl.Style.FontStyleInfo.Font.SpaceCharSize.Y);
			MyShapeControl.Layout();
		}

		public override GuiLabs.Canvas.Controls.Control MyControl
		{
			get 
			{ 
				return MyShapeControl; 
			}
		}

		private RectShape mMyShapeControl;
		public virtual RectShape MyShapeControl
		{
			get
			{
				return mMyShapeControl;
			}
			set
			{
				if (mMyShapeControl != null)
				{
					UnSubscribeControl();
				}
				mMyShapeControl = value;
				if (mMyShapeControl != null)
				{
					SubscribeControl();
				}
			}
		}

		#endregion

		#region OnEvents

		/// <summary>
		/// ignore the Delete key - we don't want to delete ourselves
		/// </summary>
		protected override void OnKeyDownDelete(KeyEventArgs e)
		{
			
		}

		#endregion

		#region Style

		protected override string StyleName()
		{
			return "SpaceBlock";
		}

		#endregion
	}
}
