namespace GuiLabs.Canvas.Shapes
{
	public interface IMovable
	{
		void Move(int deltaX, int deltaY);
		void MoveTo(int x, int y);
		void MoveTo(Point point);
	}
}
