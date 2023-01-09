using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega.DLX
{
    public class AdvanceDLXNode : BaseDLXNode
    {
        // this class with represent the "fathers" , whome we construct the linked list on and whome we use to save node which will point to the start of the matrix


        //number of nodes in its column
        public int Size;

        // name of the father node , to be able to identify
        public int IndexName;

        public AdvanceDLXNode(int Name) : base (null)
        {
            this.father = this;
            this.IndexName = Name;
            this.Size = 0;
        }


        // "cover" - "removing" the father (unlinked it)
        // and all the rows that are at the same rows as the noded the father point at
        public void Cover()
        {
            unlinkRight();
            BaseDLXNode CurrentRow =  Down;
            BaseDLXNode CurrentNode;

            // while there are still rows to cover
            while (CurrentRow != this)
            {
                // unlink all node in the row
                CurrentNode = CurrentRow.Right; 
                while (CurrentNode != CurrentRow)
                {
                    CurrentNode.unlinkDown();

                    // fixing the size after we removed something
                    CurrentNode.father.Size--;
                    CurrentNode = CurrentNode.Right;
                }

                //using this to pass down and keep moving all over all the rows
                CurrentRow = CurrentRow.Down;
            }
        }


        //an undo function to the cover

        // we do the exact oposite of cover , we go up and up , relinking the noded which the father holds and all the noded which were disconected in the effected rows
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
