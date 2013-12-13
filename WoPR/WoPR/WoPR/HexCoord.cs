using System.Diagnostics;

namespace WoPR
{
    public class HexCoord
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

        public System.Collections.Generic.List<HexCoord> neighbors()
        {
            System.Collections.Generic.List<HexCoord> neighborsList = new System.Collections.Generic.List<HexCoord>();
            neighborsList.Add(this + HexCoord.Up);
            neighborsList.Add(this + HexCoord.UpLeft);
            neighborsList.Add(this + HexCoord.UpRight);
            neighborsList.Add(this + HexCoord.Down);
            neighborsList.Add(this + HexCoord.DownLeft);
            neighborsList.Add(this + HexCoord.DownRight);
            return neighborsList;
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

        public static HexCoord Zero { get { return new HexCoord(0, 0, 0); } }
        public static HexCoord Up { get { return new HexCoord(0, -1, 1); } }
        public static HexCoord UpLeft { get { return new HexCoord(-1, 0, 1); } }
        public static HexCoord UpRight { get { return new HexCoord(1, -1, 0); } }
        public static HexCoord Down { get { return new HexCoord(0, 1, -1); } }
        public static HexCoord DownLeft { get { return new HexCoord(-1, 1, 0); } }
        public static HexCoord DownRight { get { return new HexCoord(1, 0, -1); } }

        public static HexCoord operator +(HexCoord a, HexCoord b)
        {
            return new HexCoord(a.x + b.x, a.y + b.y, a.z + b.z);
        }

    }
}
