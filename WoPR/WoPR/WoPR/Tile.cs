using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoPR
{
    class Tile
    {
        public string type { get; private set; }
        public Tile(string Type)
        {
            type = Type;
        }
    }
}
