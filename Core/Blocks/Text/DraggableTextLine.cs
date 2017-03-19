namespace GuiLabs.Editor.Blocks
{
	public class DraggableTextLine : TextLine
	{
		public DraggableTextLine()
			: base()
		{
            this.Draggable = true;
		}

        public DraggableTextLine(string initialText)
			: base(initialText)
		{
            this.Draggable = true;
        }

        public override bool Draggable
        {
            get
            {
                if (this.Prev == null && this.Next == null)
                {
                    return false;
                }
                return base.Draggable;
            }
            set
            {
                base.Draggable = value;
            }
        }
	}
}
