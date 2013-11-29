using System.Diagnostics;

namespace WoPR
{
    class HexCoord
    {
        //private int x, y;
        public int x { get; private set; }
        public int y { get; private set; }
        public int z
        {
            get { return 0 - x - y; }
        }

        public HexCoord(int X, int Y, int Z)
        {
            changeTo(X, Y, Z);
        }

        public void changeTo(int X, int Y, int Z)
        {
            Debug.Assert(checkCoords(X, Y, Z), "Invalid HexCoord: X = " + X + ", Y = " + Y + ", Z = " + Z);
            x = X;
            y = Y;
        }

        public bool checkCoords(int X, int Y, int Z)
        {
            return X + Y + Z == 0;
        }

        public void changeBy(int X, int Y, int Z)
        {
            Debug.Assert(checkCoords(X, Y, Z), "Invalid HexCoord movement: X = " + X + ", Y = " + Y + ", Z = " + Z);
            x += X;
            y += Y;
        }

        public override bool Equals(object obj)
        {
            HexCoord h = obj as HexCoord;
            if (h == null) return false;
            return x == h.x && y == h.y;
        }

        public override int GetHashCode()
        {
            return x * 3079 + y;
        }

        public override string ToString()
        {
            return "X=" + x + " Y=" + y + " Z=" + z;
        }

    }
}
