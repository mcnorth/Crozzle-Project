using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle_Project
{
    class Tree
    {
        Node Root;

        //constructor empty tree
        public Tree()
        {
            Root = null;
        }

        //constructor assign the node to the root of tree
        public Tree(char[] grid)
        {
            Root = new Node(grid);
        }

        //non recursive
        public void Add(char[] grid)
        {
            if (Root == null)
            {
                Node NewNode = new Node(grid);
                Root = NewNode;
                return;
            }
            Node currentnode = Root;
            bool added = false;
            do
            {


            } while (!added);
        }

        //recursive
        public void AddRecursive(char[] grid)
        {
            AddR(ref Root, grid);
        }

        //recursive search for where to add the new node
        private void AddR(ref Node N, char[] grid)
        {

        }

        //write out the tree in sorted order to the newstring
        //implement using recursive
        public void Print (ref string newstring)
        {

        }

    }
}
