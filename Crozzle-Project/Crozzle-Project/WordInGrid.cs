using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crozzle_Project
{
    class WordInGrid
    {
        public string Name { get; set; }
        public Dictionary<Coordinate, char> NameCharArrayCords { get; set; }

        public WordInGrid(string name, Dictionary<Coordinate, char> nameCharArrayCords)
        {
            Name = name;
            NameCharArrayCords = nameCharArrayCords;
        }

    }
}
