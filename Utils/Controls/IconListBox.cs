using System.Drawing;
using System.Windows.Forms;

namespace GuiLabs.Utils
{
	using ImageType = System.Drawing.Image;
	using System;

	public class IconListBox : ListBox
	{
		public IconListBox()
			: base()
		{
			this.DrawMode = DrawMode.OwnerDrawFixed;
			this.DrawItem += PaintItem;
			EnableDoubleBuffering();
			// this.MeasureItem += new MeasureItemEventHandler(CalcItemSize);
		}

		//protected override void OnPaintBackground(PaintEventArgs pevent)
		//{

		//}

		//protected override void WndProc(ref Message m)
		//{
		//    if (m.Msg == API.WM_VSCROLL)
		//    {
		//        API.SetRedraw(this.Handle, false);
		//        base.WndProc(ref m);
		//        API.SetRedraw(this.Handle, true);

		//        return;
		//    }

		//    base.WndProc(ref m);
		//}

		public void EnableDoubleBuffering()
		{
			//this.SetStyle(
			//    ControlStyles.DoubleBuffer
			//    | ControlStyles.UserPaint
			//    | ControlStyles.AllPaintingInWmPaint,
			//    true);
			//this.UpdateStyles();
			//this.DoubleBuffered = true;
		}

		protected virtual void CalcItemSize(object sender, MeasureItemEventArgs e)
		{
			ImageType image = GetIconForItem(e.Index);
			if (image != null)
			{
				e.ItemHeight = Common.Max(e.ItemHeight, image.Height + 2);
			}
		}

		protected SolidBrush TextBrush = new SolidBrush(Color.Black);

		protected const int SpacingBetweenImageAndText = 4;

		protected virtual void PaintItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index < 0 || e.Index >= this.Items.Count)
			{
				return;
			}
			e.DrawBackground();

			ImageType image = GetIconForItem(e.Index);

			Rectangle textRect = e.Bounds;

			if (image != null)
			{
				int margin = image.Width + SpacingBetweenImageAndText;
				textRect.X += margin;
				textRect.Width -= margin;

				//e.Graphics.DrawImageUnscaled(
				//    image,
				//    e.Bounds.Left + 1,
				//    e.Bounds.Top + 1);

				e.Graphics.FillRectangle(
					Brushes.White,
					e.Bounds.Left,
					e.Bounds.Top,
					image.Width + 1,
					this.ItemHeight);

				e.Graphics.DrawImage(
					image,
					e.Bounds.Left + 1,
					e.Bounds.Top + 1,
					image.Width,
					image.Height);
			}

			TextBrush.Color = e.ForeColor;

			e.Graphics.DrawString(
				this.Items[e.Index].ToString(),
				e.Font,
				TextBrush,
				textRect.Left,
				textRect.Top);

			//if
			//(
			//    (
			//           Common.BitSet(e.State, DrawItemState.Focus)
			//        || Common.BitSet(e.State, DrawItemState.Selected)
			//    )
			//    &&
			//        !Common.BitSet(e.State, DrawItemState.NoFocusRect)
			//)
			//{
			//    e.DrawFocusRectangle();
			//}
		}

		protected virtual ImageType GetIconForItem(int index)
		{
			return null;
		}

		#region Width

		public virtual int GetRequiredListBoxWidth(int contentWidth, int contentHeight)
		{
			int result = contentWidth;

			if (contentHeight > this.Height)
			{
				result += SystemInformation.VerticalScrollBarWidth;
			}

			if (result > MaxWidth)
			{
				result = MaxWidth;
			}

			return result;
		}

		public int GetRequiredContentHeight()
		{
			int result = 0;

			for (int i = 0; i < this.Items.Count; ++i)
			{
				result += this.GetItemHeight(i);
			}

			return result;
		}

		public virtual int GetRequiredContentWidth()
		{
			int result = 0;

			using (Graphics g = this.CreateGraphics())
			{
				for (int i = 0; i < this.Items.Count; ++i)
				{
					float w = g.MeasureString(
						this.Items[i].ToString(), 
						this.Font).Width;

					w += 10;

					ImageType image = GetIconForItem(i);
					if (image != null)
					{
						w += image.Width + SpacingBetweenImageAndText;
					}

					if (w > result)
						result = (int) w;
				}
			}

			return result;
		}

		private int mMaxWidth = 800;
		public int MaxWidth
		{
			get
			{
				return mMaxWidth;
			}
			set
			{
				mMaxWidth = value;
			}
		}

		#endregion


	}
}
