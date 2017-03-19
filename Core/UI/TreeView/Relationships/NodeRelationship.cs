using System.Collections.Generic;
using GuiLabs.Editor.Blocks;

namespace GuiLabs.Editor.UI
{
    public class NodeRelationship : INodeRelationship
    {
		private bool mDirection = true;
		public bool Direction
		{
			get { return mDirection; }
			set { mDirection = value; }
		}
	

        private Block mSender;
        public Block Sender
        {
            get { return mSender; }
            set { mSender = value; }
        }

        private List<Block> mReceivers;
        public List<Block> Receivers
        {
            get { return mReceivers; }
            set { mReceivers = value; }
        }
    }
}
