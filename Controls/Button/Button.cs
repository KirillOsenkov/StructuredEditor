using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Events;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Utils.Delegates;
using GuiLabs.Utils;

namespace GuiLabs.Canvas.Controls
{
	public class Button : LinearContainerControl
	{
		#region ctors

		public Button()
			: base()
		{
			this.Bounds.Size.Set(16, 16);
		}

		public Button(string caption)
			: base()
		{
			TextLabel = new Label(caption);
			this.Add(TextLabel);
		}

		public Button(IPicture picture)
			: base()
		{
			MyPicture = new PictureBox(picture);
			this.Add(MyPicture);
		}

		public Button(string caption, IPicture picture)
			: base()
		{
			TextLabel = new Label(caption);
			MyPicture = new PictureBox(picture);
			this.Add(TextLabel);
			this.Add(MyPicture);
		}

		public Button(IPicture picture, string caption)
			: base()
		{
			TextLabel = new Label(caption);
			MyPicture = new PictureBox(picture);
			this.Add(MyPicture);
			this.Add(TextLabel);
		}

		protected override void InitLayoutStrategy()
		{
			base.InitLayoutStrategy();
			this.LinearLayoutStrategy.Orientation = OrientationType.Horizontal;
			this.LinearLayoutStrategy.Alignment = AlignmentType.Center;
			this.LinearLayoutStrategy.XSpacing = 8;
		}

		#endregion

		#region Events

		public event ChangeHandler<Button> Pushed;

		protected void RaisePushed()
		{
			if (Pushed != null)
			{
				Pushed(this);
			}
		}

		#endregion

		public void ChangeOrder()
		{
			if (this.ChildrenCollection.Count == 2)
			{
				Control t = this.ChildrenCollection[0];
				this.ChildrenCollection[0] = this.ChildrenCollection[1];
				this.ChildrenCollection[1] = t;
			}
		}

		#region Keyboard

		public override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			bool shouldRedraw = ShouldPushByKeydown(e);
			using (RedrawAccumulator a = new RedrawAccumulator(this.Root, shouldRedraw))
			{
				if (shouldRedraw)
				{
					this.State = ButtonState.Pushed;
					RaisePushed();
				}
				base.OnKeyDown(e);
			}
		}

		private bool ShouldPushByKeydown(System.Windows.Forms.KeyEventArgs e)
		{
			return e.KeyCode == System.Windows.Forms.Keys.Return
				|| e.KeyCode == System.Windows.Forms.Keys.Space;
		}

		public override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			bool shouldRedraw = ShouldPushByKeydown(e);
			using (RedrawAccumulator a = new RedrawAccumulator(this.Root, shouldRedraw))
			{
				if (shouldRedraw)
				{
					this.State = ButtonState.Normal;
				}
				base.OnKeyUp(e);
			}
		}

		#endregion

		#region Style

		protected override string StyleName
		{
			get
			{
				return "Button";
			}
		}

		protected override void InitStyle()
		{
			Style.LineColor = System.Drawing.Color.DarkGray;
			Style.FillColor = System.Drawing.Color.Transparent;
		}

		protected override void InitSelectedStyle()
		{
			SelectedStyle.LineColor = System.Drawing.Color.Black;
			SelectedStyle.FillColor = System.Drawing.Color.Transparent;
		}

		#endregion

		#region Text and Picture

		private PictureBox mMyPicture;
		public PictureBox MyPicture
		{
			get { return mMyPicture; }
			set
			{
				mMyPicture = value;
				mMyPicture.Box.Margins.SetAll(2);
			}
		}

		private Label mTextLabel;
		public Label TextLabel
		{
			get
			{
				return mTextLabel;
			}
			set
			{
				mTextLabel = value;
				mTextLabel.Box.Margins.SetLeftAndRight(2);
			}
		}

		public string Text
		{
			get
			{
				return TextLabel.Text;
			}
			set
			{
				TextLabel.Text = value;
			}
		}

		#endregion

		#region Draw

		private ILineStyleInfo LightBorder = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewLineStyleInfo(System.Drawing.Color.GhostWhite, 1);
		//private ILineStyleInfo DarkBorder  = RendererSingleton.Instance.DrawOperations.Factory.ProduceNewLineStyleInfo(System.Drawing.Color.Black, 1);

		private ILineStyleInfo TopLeft;
		private ILineStyleInfo BottomRight;

		protected override void DrawBorder(IRenderer Renderer)
		{
			if (this.State == ButtonState.Normal)
			{
				TopLeft = LightBorder;
				BottomRight = this.CurrentStyle.LineStyleInfo;
			}
			else
			{
				TopLeft = this.CurrentStyle.LineStyleInfo;
				BottomRight = LightBorder;
			}

			Rect R = this.Bounds;
			int x1 = R.Location.X;
			int y1 = R.Location.Y;
			int x2 = R.Right - 1;
			int y2 = R.Bottom - 1;

			Renderer.DrawOperations.DrawLine(x1, y1, x2, y1, TopLeft);
			Renderer.DrawOperations.DrawLine(x1, y1, x1, y2, TopLeft);
			Renderer.DrawOperations.DrawLine(x2, y1, x2, y2, BottomRight);
			Renderer.DrawOperations.DrawLine(x1, y2, x2 + 1, y2, BottomRight);
		}

		public override void DrawChildren(IRenderer Renderer)
		{
			if (this.State == ButtonState.Pushed)
			{
				if (TextLabel != null && TextLabel.Visible)
				{
					TextLabel.Bounds.Location.Add(1);
					TextLabel.Draw(Renderer);
					TextLabel.Bounds.Location.Add(-1);
				}
				if (MyPicture != null && MyPicture.Visible)
				{
					MyPicture.Bounds.Location.Add(1);
					MyPicture.Draw(Renderer);
					MyPicture.Bounds.Location.Add(-1);
				}
			}
			else
			{
				if (TextLabel != null && TextLabel.Visible)
				{
					TextLabel.Draw(Renderer);
				}
				if (MyPicture != null && MyPicture.Visible)
				{
					MyPicture.Draw(Renderer);
				}
			}
		}

		protected override void DrawRectangleLike(IRenderer Renderer)
		{
			Renderer.DrawOperations.FillRectangle(this.Bounds, CurrentStyle.FillStyleInfo);
		}

		#endregion

		#region State

		public enum ButtonState
		{
			Normal,
			Pushed
		}

		private ButtonState mState = ButtonState.Normal;
		public ButtonState State
		{
			get
			{
				return mState;
			}
			set
			{
				mState = value;
			}
		}

		#endregion

		#region MouseDown & MouseUp

		public override void OnMouseDown(MouseWithKeysEventArgs e)
		{
			using (RedrawAccumulator a = new RedrawAccumulator(this.Root))
			{
				this.State = ButtonState.Pushed;
				base.OnMouseDown(e);
			}
		}

		public override void OnMouseUp(MouseWithKeysEventArgs e)
		{
			if (this.State == ButtonState.Normal)
			{
				return;
			}
			using (RedrawAccumulator a = new RedrawAccumulator(this.Root))
			{
				this.State = ButtonState.Normal;
				base.OnMouseUp(e);
				RaisePushed();
			}
		}

		#endregion
	}
}