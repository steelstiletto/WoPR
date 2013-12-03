using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoPR
{
    class Tile
    {
        public enum TileType {plain, water, road, forest, headquarters, barracks, garage, supplyDepot};
        private const int MAXCHP = 10;

        public Player owner;
        public Unit unit;

        private int[] coords;   //x,y,z
        private int[] moveCosts;//foot, tread, air
        private int capturehp;
        private int hpHealed;
        private double dBonus;        
        private bool capturable;
        
        private String name;
        private Building build;
        private TileType t;

        public Tile(TileType Type, int[] xyz)
        {
            coords = xyz;
            unit = null;
            t = Type;

            switch (Type)
            {
                case TileType.plain:
                    name = "Plain";
                    moveCosts = new int[]{1, 1, 1};
                    hpHealed = 0;
                    capturehp = 10;
                    dBonus = .1;
                    capturable = false; 
                    owner = null;
                    build = null;
                    break;

                case TileType.water:
                    name = "Water";
                    moveCosts = new int[] { -1, -1, 1 };
                    hpHealed = 0;
                    capturehp = 10;
                    dBonus = 0;
                    capturable = false;
                    owner = null;
                    build = null;
                    break;

                case TileType.road:
                    name = "Road";
                    moveCosts = new int[] { 1, 1, 1 };
                    hpHealed = 0;
                    capturehp = 10;
                    dBonus = 0;
                    capturable = false;
                    owner = null;
                    build = null;
                    break;

                case TileType.forest:
                    name = "Forest";
                    moveCosts = new int[] { 1, 2, 1 };
                    hpHealed = 0;
                    capturehp = 10;
                    dBonus = .3;
                    capturable = false;
                    owner = null;
                    build = null;
                    break;

                case TileType.headquarters:
                    name = "Headquarters";
                    moveCosts = new int[] { 1, 1, 1 };
                    hpHealed = 20;
                    capturehp = 10;
                    dBonus = .4;
                    capturable = true;
                    owner = null;
                    build = new Building(Building.buildType.headquarters);
                    break;

                case TileType.barracks:
                    name = "Barracks";
                    moveCosts = new int[] { 1, 1, 1 };
                    hpHealed = 20;
                    capturehp = 10;
                    dBonus = .4;
                    capturable = true;
                    owner = null;
                    build = new Building(Building.buildType.barracks);
                    break;

                case TileType.garage:
                    name = "Garage";
                    moveCosts = new int[] { 1, 1, 1 };
                    hpHealed = 20;
                    capturehp = 10;
                    dBonus = 0.4;
                    capturable = true;
                    owner = null;
                    build = new Building(Building.buildType.garage);
                    break;

                case TileType.supplyDepot:
                    name = "Supply Depot";
                    moveCosts = new int[] { 1, 1, 1 };
                    hpHealed = 20;
                    capturehp = 10;
                    dBonus = 0.6;
                    capturable = true;
                    owner = null;
                    build = new Building(Building.buildType.supplyDepot);
                    break;
            }

        }

        public bool Capture(Unit u)
        {
            if (!capturable)
                {
                    return false;
                }
            capturehp = (int)(capturehp - ((double)u.getHp() / 10));
            if (capturehp <= 0)
            {
                owner = u.getOwner();
            }
            return true;
        }

        public void resetCaptureHP()
        {
            capturehp = MAXCHP;
        }

        public bool isCapturable()
        {
            return capturable;
        }
        public int getHPHealed()
        {
            return hpHealed;
        }
        public int getCaptureHp()
        {
            return capturehp;
        }
        public double getDBonus()
        {
            return dBonus;
        }
        public int[] getMoveCosts()
        {
            return moveCosts;
        }
        public String getName()
        {
            return name;
        }
        public Unit getUnit()
        {
            return unit;
        }
        public Building getBuilding()
        {
            return build;
        }
        private TileType getType()
        {
            return t;
        }
    }
}
