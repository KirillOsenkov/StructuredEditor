using GuiLabs.Canvas.Shapes;

namespace GuiLabs.Canvas.Controls
{
	public class Layouter
	{
		#region PutAt

		/// <summary>
		/// Moves shapeToMove so that its topleft corner
		/// corresponds with the topleft corner of reference.
		/// </summary>
		/// <param name="reference"></param>
		/// <param name="shapeToMove"></param>
		public static void PutAt(
			IDrawableRect reference,
			IDrawableRect shapeToMove)
		{
			PutAt(reference, shapeToMove, 0);
		}

		/// <summary>
		/// Moves shapeToMove so that its topleft corner
		/// corresponds with the topleft corner of reference.
		/// </summary>
		/// <param name="reference"></param>
		/// <param name="shapeToMove"></param>
		public static void PutAt(
			IDrawableRect reference,
			IDrawableRect shapeToMove,
			int Margin)
		{
			shapeToMove.MoveTo(
				reference.Bounds.Location.X + Margin,
				reference.Bounds.Location.Y + Margin);
		}

        /// <summary>
        /// Moves shapeToMove so that its topleft corner
        /// corresponds with the topleft corner of reference.
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="shapeToMove"></param>
        public static void PutAtRight(
			IDrawableRect reference,
			IDrawableRect shapeToMove,
            int Margin)
        {
            shapeToMove.MoveTo(
                reference.Bounds.Location.X + Margin,
                reference.Bounds.Location.Y);
        }

		#endregion

		#region PutRight

		/// <summary>
		/// Puts a shapeToMove to the right from reference.
		/// </summary>
		/// <param name="reference"></param>
		/// <param name="shapeToMove"></param>
		public static void PutRight(IDrawableRect reference, IDrawableRect shapeToMove)
		{
			PutRight(reference, shapeToMove, 0);
		}

		/// <summary>
		/// Puts a shapeToMove to the right from reference.
		/// </summary>
		/// <param name="reference"></param>
		/// <param name="shapeToMove"></param>
		public static void PutRight(
			IDrawableRect reference,
			IDrawableRect shapeToMove,
			int Margin)
		{
			shapeToMove.MoveTo(
				reference.Bounds.Right + 1 + Margin,
				reference.Bounds.Location.Y
			);
		}

        /// <summary>
        /// Puts a shapeToMove to the right from reference.
        /// </summary>
        /// <param name="xreference"></param>
        /// <param name="yreference"></param>
        /// <param name="shapeToMove"></param>
        public static void PutRight(
			IDrawableRect xreference,
			IDrawableRect yreference,
			IDrawableRect shapeToMove,
            int Margin)
        {
            shapeToMove.MoveTo(
                xreference.Bounds.Right + 1 + Margin,
                yreference.Bounds.Location.Y
            );
        }

		#endregion

		#region PutUnder

		/// <summary>
		/// Moves a shapeToMove so that it is left-aligned with reference
		/// and is below it.
		/// </summary>
		/// <param name="reference"></param>
		/// <param name="shapeToMove"></param>
		public static void PutUnder(
			IDrawableRect reference,
			IDrawableRect shapeToMove
		)
		{
			PutUnder(reference, shapeToMove, 0);
		}

		/// <summary>
		/// Moves a shapeToMove so that it is left-aligned with reference
		/// and is below it.
		/// </summary>
		/// <param name="reference"></param>
		/// <param name="shapeToMove"></param>
		public static void PutUnder(
			IDrawableRect reference,
			IDrawableRect shapeToMove,
			int Margin
		)
		{
			shapeToMove.MoveTo(
				reference.Bounds.Location.X,
				reference.Bounds.Bottom + 1 + Margin
			);
		}

        /// <summary>
        /// Moves a shapeToMove so that it is left-aligned with reference
        /// and is below it.
        /// </summary>
        /// <param name="xreference"></param>
        /// <param name="yreference"></param>
        /// <param name="shapeToMove"></param>
        public static void PutUnder(
			IDrawableRect xreference,
			IDrawableRect yreference,
			IDrawableRect shapeToMove,
            int Margin
        )
        {
            shapeToMove.MoveTo(
                xreference.Bounds.Location.X,
                yreference.Bounds.Bottom + 1 + Margin
            );
        }

		#endregion

		#region PutUnderAndIndent

		/// <summary>
		/// Puts the shapeToMove under reference
		/// and "indent" pixels to the right
		/// </summary>
		/// <param name="reference"></param>
		/// <param name="shapeToMove"></param>
		/// <param name="indent"></param>
		public static void PutUnderAndIndent(
			IDrawableRect reference,
			IDrawableRect shapeToMove,
			int indent
		)
		{
			PutUnderAndIndent(reference, shapeToMove, indent, 0);
		}
		
		/// <summary>
		/// Puts the shapeToMove under reference
		/// and "indent" pixels to the right
		/// </summary>
		/// <param name="reference"></param>
		/// <param name="shapeToMove"></param>
		/// <param name="indent"></param>
		public static void PutUnderAndIndent(
			IDrawableRect reference,
			IDrawableRect shapeToMove, 
			int indent,
			int margin
		)
		{
			shapeToMove.MoveTo(
				reference.Bounds.Location.X + indent,
				reference.Bounds.Bottom + 1 + margin
			);
		}

		public static void PutUnderAndIndentFrom(
			IDrawableRect xReference,
			IDrawableRect yReference,
			IDrawableRect shapeToMove,
			int indent,
			int margin
		)
		{
			shapeToMove.MoveTo(
				xReference.Bounds.Location.X + indent,
				yReference.Bounds.Bottom + margin
			);
		}

		#endregion

		#region PutAround

		/// <summary>
		/// Resizes a shapeToResize to be a minimal rectangle
		/// which fully contains leftTopShape in the top left corner
		/// and fully contains bottomRightShape in the bottom right corner.
		/// </summary>
		public static void PutAround(
			IDrawableRect leftTopShape,
			IDrawableRect bottomRightShape,
			IDrawableRect shapeToResize
		)
		{
			PutAround(leftTopShape, bottomRightShape, shapeToResize, 0, 0);
		}

		/// <summary>
		/// Resizes a shapeToResize to be a minimal rectangle
		/// which fully contains leftTopShape in the top left corner
		/// and fully contains bottomRightShape in the bottom right corner.
		/// </summary>
		public static void PutAround(
			IDrawableRect leftTopShape,
			IDrawableRect bottomRightShape,
			IDrawableRect shapeToResize,
			int xMargin,
			int yMargin
		)
		{
			shapeToResize.MoveTo(
				leftTopShape.Bounds.Location.X - xMargin,
				leftTopShape.Bounds.Location.Y - yMargin);
			
			shapeToResize.Bounds.Size.Set(
				bottomRightShape.Bounds.Right - leftTopShape.Bounds.Location.X + 2 * xMargin,
				bottomRightShape.Bounds.Bottom - leftTopShape.Bounds.Location.Y + 2 * yMargin
			);
		}

        /// <summary>
        /// Resizes a shapeToResize to be a minimal rectangle
        /// which fully contains leftTopShape in the top left corner
        /// and fully contains bottomRightShape in the bottom right corner.
        /// </summary>
        public static void PutAround(
			IDrawableRect leftTopShape,
			IDrawableRect RightShape,
			IDrawableRect bottomShape,
			IDrawableRect shapeToResize,
            int xMargin,
            int yMargin
        )
        {
            shapeToResize.MoveTo(
                leftTopShape.Bounds.Location.X - xMargin,
                leftTopShape.Bounds.Location.Y - yMargin);

            shapeToResize.Bounds.Size.Set(
                RightShape.Bounds.Right - leftTopShape.Bounds.Location.X + 2 * xMargin,
                bottomShape.Bounds.Bottom - leftTopShape.Bounds.Location.Y + 2 * yMargin
            );
        }

		#endregion

		#region GrowToInclude

		/// <summary>
		/// Increases size of shapeToResize,
		/// so that its right edge and bottom contain containedShape
		/// </summary>
		/// <param name="shapeToResize">The shape the size of which to increased.</param>
		/// <param name="containedShape">The shape to embrace.</param>
		public static void GrowToInclude(
			IDrawableRect shapeToResize,
			IDrawableRect containedShape
		)
		{
			GrowToInclude(shapeToResize, containedShape, 0, 0);
		}

		/// <summary>
		/// Increases size of shapeToResize,
		/// so that its right edge and bottom contain containedShape
		/// </summary>
		/// <param name="shapeToResize">The shape the size of which to increased.</param>
		/// <param name="containedShape">The shape to embrace.</param>
		/// <param name="xMargin">x-Distance between edges of shapes.</param>
		/// <param name="yMargin">y-Distance between edges of shapes.</param>
		public static void GrowToInclude(
			IDrawableRect shapeToResize,
			IDrawableRect containedShape,
			int xMargin,
			int yMargin
		)
		{
			if (containedShape.Bounds.Bottom + yMargin > shapeToResize.Bounds.Bottom)
			{
				shapeToResize.Bounds.Size.Y = 
					containedShape.Bounds.Bottom + yMargin - 
					shapeToResize.Bounds.Location.Y;
			}
			if (containedShape.Bounds.Right + xMargin > shapeToResize.Bounds.Right)
			{
				shapeToResize.Bounds.Size.X =
					containedShape.Bounds.Right + xMargin -
					shapeToResize.Bounds.Location.X;
			}
		}

		#endregion

		#region MaxChildSizes

		public static int MaxChildWidth(ContainerControl parent)
		{
			int max = 0;

			foreach (Control child in parent.Children)
			{
				if (child.Visible && child.Bounds.Size.X > max)
				{
					max = child.Bounds.Size.X;
				}
			}

			return max;
		}

		public static int MaxChildHeight(ContainerControl parent)
		{
			int max = 0;

			foreach (Control child in parent.Children)
			{
				if (child.Visible && child.Bounds.Size.Y > max)
				{
					max = child.Bounds.Size.Y;
				}
			}

			return max;
		}



		#endregion
	}
}
