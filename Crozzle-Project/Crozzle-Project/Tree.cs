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
        public Tree(CrozzleGrid grid)
        {
            Root = new Node(grid);
        }

        //non recursive
        public void Add(CrozzleGrid grid)
        {
            int value = grid.GetScore();

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
                //traverse tree
                if(value < currentnode.Grid.GetScore())
                {
                    //go left
                    if(currentnode.Left == null)
                    {
                        //add the item
                        Node NewNode = new Node(grid);
                        currentnode.Left = NewNode;
                        added = true;
                    }
                    else
                    {
                        currentnode = currentnode.Left;
                    }
                }

                if(value >= currentnode.Grid.GetScore())
                {
                    if (currentnode.Right == null)
                    {
                        Node NewNode = new Node(grid);
                        currentnode.Right = NewNode;
                        added = true;
                    }
                    else
                    {
                        currentnode = currentnode.Right;
                    }
                }

            } while (!added);
        }

        //recursive
        public void AddRecursive(CrozzleGrid grid)
        {
            AddR(ref Root, grid);
        }

        //recursive search for where to add the new node
        private void AddR(ref Node N, CrozzleGrid grid)
        {
            int value = grid.GetScore();
            //recusive search on where to add the new node
            if (N == null)
            {
                //Node doesnt exist add it here
                Node NewNode = new Node(grid);
                N = NewNode; //set the old node ref to the newly create node and attach to tree
                return; //end the function call and fall back
            }

            if (value < N.Grid.GetScore())
            {
                AddR(ref N.Left, grid);
                return;
            }

            if(value >= N.Grid.GetScore())
            {
                AddR(ref N.Right, grid);
                return;
            }

        }

        //write out the tree in sorted order to the newstring
        //implement using recursive
        public void Print (ref string newstring)
        {

        }

    }
}
