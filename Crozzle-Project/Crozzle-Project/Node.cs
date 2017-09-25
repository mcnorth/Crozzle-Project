﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle_Project
{
    class Node
    {
        public char[] Grid;
        public Node Left;
        public Node Right;

        public Node(char[] grid)
        {
            Grid = grid;
            Left = null;
            Right = null;
        }
    }
}
