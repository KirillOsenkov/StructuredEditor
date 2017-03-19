using System;
using System.Collections.Generic;
using System.Text;
using GuiLabs.Canvas.Events;
using GuiLabs.Canvas;
using GuiLabs.Canvas.Controls;
using GuiLabs.Canvas.DrawStyle;

namespace GuiLabs.Editor.Blocks
{
	public class ButtonBlock : Block
	{
		#region ctors

		public ButtonBlock(String text)
			: base()
		{
			MyButton = new Button(text);
			Init();
		}

		public ButtonBlock(String text, IPicture picture)
			: base()
		{
			MyButton = new Button(text, picture);
			Init();
		}

		public ButtonBlock(IPicture picture, String text)
			: base()
		{
			MyButton = new Button(picture, text);
			Init();
		}

		public ButtonBlock(IPicture picture)
			: base()
		{
			MyButton = new Button(picture);
			Init();
		}
		
		#endregion

		#region Events

		public event ButtonPushedHandler Pushed;
		public void RaisePushed()
		{
			if (Pushed != null)
			{
				Pushed();
			}
		}

		#endregion

		private void Init()
		{
			MyButton.Focusable = true;
		}

		#region OnEvents

		protected virtual void OnPushed(Button itemChanged)
		{
			RaisePushed();
		}

		#endregion

		#region Control

		public override Control MyControl
		{
			get { return MyButton; }
		}

		private Button mMyButton;
		public virtual Button MyButton
		{
			get { return mMyButton; }
			set
			{
				if (mMyButton != null)
				{
					UnSubscribeControl();
				}
				mMyButton = value;
				if (mMyButton != null)
				{
					SubscribeControl();
				}
			}
		}

		protected override void SubscribeControl()
		{
			base.SubscribeControl();
			mMyButton.Pushed += OnPushed;
		}

		protected override void UnSubscribeControl()
		{
			base.UnSubscribeControl();
			mMyButton.Pushed -= OnPushed;
		}

		#endregion

		private Point mSize;
		public Point Size
		{
			get { return MyButton.Bounds.Size; }
			set
			{
				mSize = value;
				MyButton.Bounds.Size = mSize;
			}
		}
	}
}
