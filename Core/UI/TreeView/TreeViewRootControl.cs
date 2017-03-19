using System.Collections.Generic;
using GuiLabs.Canvas;
using GuiLabs.Canvas.Controls;
using GuiLabs.Canvas.DrawOperations;
using GuiLabs.Canvas.DrawStyle;
using GuiLabs.Canvas.Renderer;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.UI
{
	public class TreeViewRootControl : RootControl
	{
		public TreeViewRootControl(TreeViewRootBlock root)
			: base(root.Children.Controls)
		{
			TreeView = root;
			this.Box.Padding.Left = 0;
			this.Box.Padding.Top = 0;
			this.StretchToWindow = false;
		}

		private TreeViewRootBlock mTreeView;
		public TreeViewRootBlock TreeView
		{
			get { return mTreeView; }
			set { mTreeView = value; }
		}

		public override void DrawCore(IRenderer Renderer)
		{
			base.DrawCore(Renderer);
			DrawRelationship(Renderer);
		}

		private const int ArrowRightMargin = 40;

		public void DrawRelationship(IRenderer Renderer)
		{
			NodeRelationship rel = TreeView.Relationship;
			if (rel == null)
			{
				return;

			}
			Control sender = ExtractCoreControl(rel.Sender);

			List<Control> receivers = new List<Control>();
			foreach (Block receiver in rel.Receivers)
			{
				receivers.Add(ExtractCoreControl(receiver));
			}

			List<int> Ys = new List<int>();
			List<int> Xs = new List<int>();
			Ys.Add(sender.Bounds.CenterY);
			Xs.Add(sender.Bounds.Right);
			foreach (Control c in receivers)
			{
				Ys.Add(c.Bounds.CenterY);
				Xs.Add(c.Bounds.Right);
			}

			int yMin = 0;
			int yMax = 0;

			FindMinMaxY(Ys, out yMin, out yMax);
			int x = FindMaxWidth() + ArrowRightMargin;

			ILineStyleInfo style;
			if (rel.Direction)
			{
				style = DefaultLineStyle;
			}
			else
			{
				style = DefaultReverseLineStyle;
			}

			IDrawOperations op = Renderer.DrawOperations;

			op.DrawLine(x, yMin, x, yMax, style);
			op.DrawLine(Xs[0] + 2, Ys[0], x, Ys[0], style);

			for (int i = 1; i < Xs.Count; i++)
			{
				op.DrawLine(Xs[i] + 3, Ys[i], x, Ys[i], style);

				if (rel.Direction)
				{
					DrawArrow(Renderer, Xs[i] + 2, Ys[i]);
				}
				else
				{
					op.DrawLine(Xs[i] + 3, Ys[i] - 4, Xs[i] + 3, Ys[i] + 4, style);
				}
			}
			if (rel.Direction)
			{
				op.DrawLine(Xs[0] + 1, Ys[0] - 4, Xs[0] + 1, Ys[0] + 4, style);
			}
			else
			{
				DrawArrow(Renderer, Xs[0] + 2, Ys[0]);
			}
		}

		public override void LayoutCore()
		{
			base.LayoutCore();

			if (TreeView != null && TreeView.Relationship != null)
			{
				this.Bounds.Size.X += ArrowRightMargin + 10;
			}
		}

		private void DrawArrow(IRenderer Renderer, int x, int y)
		{
			const int mx = 8;
			const int my = 3;
			List<Point> pts = new List<Point>();
			pts.Add(new Point(x, y));
			pts.Add(new Point(x + mx, y - my));
			pts.Add(new Point(x + mx, y + my));
			if (TreeView.Relationship.Direction)
			{
				Renderer.DrawOperations.FillPolygon(pts, DefaultLineStyleThin, DefaultFillStyle);
			}
			else
			{
				Renderer.DrawOperations.FillPolygon(pts, DefaultReverseLineStyleThin, DefaultReverseFillStyle);
			}
		}

		private Control ExtractCoreControl(Block block)
		{
			Control result = null;

			TreeViewNode specific = block as TreeViewNode;
			if (specific != null)
			{
				result = specific.HMembers.MyControl.FindFirstVisibleParent();
			}
			else
			{
				result = block.MyControl.FindFirstVisibleParent();
			}

			return result;
		}

		private int FindMaxWidth()
		{
			return this.Bounds.Size.X - ArrowRightMargin - 10;

			//int max = 0;
			//foreach (Block block in TreeView.FindChildrenRecursive<Block>())
			//{
			//    int current = block.MyControl.Bounds.Right;
			//    if (max < current)
			//    {
			//        max = current;
			//    }
			//}

			//return max;
		}

		private void FindMinMaxY(IEnumerable<int> Numbers, out int Min, out int Max)
		{
			Min = 100000;
			Max = 0;
			foreach (int n in Numbers)
			{
				if (Min > n)
				{
					Min = n;
				}
				if (Max < n)
				{
					Max = n;
				}
			}
		}

		private static ILineStyleInfo mDefaultLineStyle;
		private ILineStyleInfo DefaultLineStyle
		{
			get
			{
				if (mDefaultLineStyle == null)
				{
					mDefaultLineStyle = RendererSingleton.StyleFactory.ProduceNewLineStyleInfo(
						System.Drawing.Color.Red, 2);
				}
				return mDefaultLineStyle;
			}
		}

		private static ILineStyleInfo mDefaultReverseLineStyle;
		private ILineStyleInfo DefaultReverseLineStyle
		{
			get
			{
				if (mDefaultReverseLineStyle == null)
				{
					mDefaultReverseLineStyle = RendererSingleton.StyleFactory.ProduceNewLineStyleInfo(
						System.Drawing.Color.LimeGreen, 2);
				}
				return mDefaultReverseLineStyle;
			}
		}

		private static ILineStyleInfo mDefaultReverseLineStyleThin;
		private ILineStyleInfo DefaultReverseLineStyleThin
		{
			get
			{
				if (mDefaultReverseLineStyleThin == null)
				{
					mDefaultReverseLineStyleThin = RendererSingleton.StyleFactory.ProduceNewLineStyleInfo(
						System.Drawing.Color.LimeGreen, 1);
				}
				return mDefaultReverseLineStyleThin;
			}
		}

		private static ILineStyleInfo mDefaultLineStyleThin;
		private ILineStyleInfo DefaultLineStyleThin
		{
			get
			{
				if (mDefaultLineStyleThin == null)
				{
					mDefaultLineStyleThin = RendererSingleton.StyleFactory.ProduceNewLineStyleInfo(
						System.Drawing.Color.Red, 1);
				}
				return mDefaultLineStyleThin;
			}
		}

		private static IFillStyleInfo mDefaultFillStyle;
		private IFillStyleInfo DefaultFillStyle
		{
			get
			{
				if (mDefaultFillStyle == null)
				{
					mDefaultFillStyle = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
						System.Drawing.Color.Red);
				}
				return mDefaultFillStyle;
			}
		}

		private static IFillStyleInfo mDefaultReverseFillStyle;
		private IFillStyleInfo DefaultReverseFillStyle
		{
			get
			{
				if (mDefaultReverseFillStyle == null)
				{
					mDefaultReverseFillStyle = RendererSingleton.StyleFactory.ProduceNewFillStyleInfo(
						System.Drawing.Color.LimeGreen);
				}
				return mDefaultReverseFillStyle;
			}
		}
	}
}
