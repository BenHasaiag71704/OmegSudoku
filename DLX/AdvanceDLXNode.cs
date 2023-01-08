using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega.DLX
{
    public class AdvanceDLXNode : BaseDLXNode
    {
        public int Size;

        public int IndexName;

        public AdvanceDLXNode(int Name) : base (null)
        {
            this.father = this;
            this.IndexName = Name;
            this.Size = 0;
        }

        public void Cover()
        {
            unlinkRight();
            BaseDLXNode CurrentRow =  Down;
            BaseDLXNode CurrentNode;
            while (CurrentRow != this)
            {
                CurrentNode = CurrentRow.Right; 
                while (CurrentNode != CurrentRow)
                {
                    CurrentNode.unlinkDown();
                    CurrentNode.father.Size--;
                    CurrentNode = CurrentNode.Right;
                }
                CurrentRow = CurrentRow.Down;
            }
        }


        public void unCover()
        {
            BaseDLXNode CurrentRow = Up;
            BaseDLXNode CurrentNode;

            while (CurrentRow != this)
            {
                CurrentNode = CurrentRow.Left;
                while (CurrentNode != CurrentRow)
                {
                    CurrentNode.father.Size++;
                    CurrentNode.relinkDown();
                    CurrentNode = CurrentNode.Left;
                }
                CurrentRow = CurrentRow.Up;
            }
            relinkRight();
        }
    }
}
