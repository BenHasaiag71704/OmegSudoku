using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega.DLX
{
    public class BaseDLXNode
    {

        // this is the base node class which would later on used to
        // creat the doubly linked list , which is needed for the algorithm
        // this class will fill the matrix


        public BaseDLXNode Right, Left, Up, Down;


        // the "father" of the node , can be seen as another pointer
        public AdvanceDLXNode father { get; set; }


        //we are working with a doubly linked list
        // when we create a node we need to connect it to its father
        public BaseDLXNode(AdvanceDLXNode father)
        {
            this.Right = this;
            this.Left = this;
            this.Up = this;
            this.Down = this;
            this.father = father;
        }

        // unlinking the node 
        public void UnlinkRight()
        {
            Left.Right = Right;
            Right.Left= Left;
        }

        public void UnlinkDown()
        {
            Up.Down = Down;
            Down.Up = Up;
        }

        
        //link the nodes together with the pointers the disconnected node have
        public void RelinkRight()
        {
            Left.Right = this;
            Right.Left = this;
        }

        public void RelinkDown()
        {
            Up.Down = this;
            Down.Up = this;
        }

        // linking a new node from the down

        public void LinkDown(BaseDLXNode n)
        {
            n.Down = Down;
            n.Down.Up = n;
            n.Up = this;
            Down = n;
        }

        public void LinkRight(BaseDLXNode n)
        {
            n.Right = Right;
            n.Right.Left = n;
            n.Left = this;
            Right = n;
        }



    }
}
