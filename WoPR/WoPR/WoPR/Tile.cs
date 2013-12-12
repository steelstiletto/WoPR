using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WoPR
{
    class Tile : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public enum TileType {plain, water, road, forest, headquarters, barracks, garage, supplyDepot};
        private const int MAXCHP = 10;

        public Player owner;
        public Unit unit;
        new public WoPR Game;

        private int[] moveCosts;//foot, tread, air
        private int capturehp;
        private int hpHealed;
        private double dBonus;        
        private bool capturable;

        private String name;
        private HexCoord position;   //x,y,z
        private Building build;
        private TileType t;

        public string type
        {
            get { return t.ToString(); }
        }

        public Tile(Game game, TileType Type, HexCoord xyz) : base(game)
        {
            Game = (WoPR)game;
            position = xyz;
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

        public void Draw(SpriteBatch batch)
        {            
            drawBorder(batch);
            drawTile(batch);
            drawUnitBorder(batch);
            drawUnit(batch);
        }
                private void drawBorder(SpriteBatch batch)
                {
                    if(Game.ui.currentSelection == position)
                    {

                    }
                    else
                    {                        
                        batch.Draw(Game.tBorder0, convertToXY(), Color.White);
                    }
                }
                private void drawTile(SpriteBatch batch)
                {

                    Vector2 temp = convertToXY();
                    temp.X += 13;
                    temp.Y += 9;

                    switch (t)
                    {
                        case TileType.plain:
                            batch.Draw(Game.plain, temp, Color.White);
                            break;

                        case TileType.water:
                            batch.Draw(Game.water, temp, Color.White);
                            break;

                        case TileType.road:
                            batch.Draw(Game.road, temp, Color.White);
                            break;

                        case TileType.forest:
                            batch.Draw(Game.forest, temp, Color.White);
                            break;

                        case TileType.headquarters:
                            break;

                        case TileType.barracks:
                            break;

                        case TileType.garage:
                            break;

                        case TileType.supplyDepot:
                            break;
                    }
                }
                private void drawUnitBorder(SpriteBatch batch)
                {
                    if(unit != null)
                    {
                        batch.Draw(Game.uBorder1, convertToXY(), Color.White);
                    }
                }
                private void drawUnit(SpriteBatch batch)
                {
                    if (unit != null)
                    {
                        batch.Draw(Game.uBorder1, convertToXY(), Color.Red);
                    }
                }
        
        private Vector2 convertToXY()
        {
            float x = 0;
            float y = 0;

            x = (120 * (float)position.x * (3 / 2));
            y = (80 * (float)Math.Sqrt(3) * ((float)position.y + ((float)position.x / 2)));
            x += 370;
            y += 380;

            return new Vector2(x, y);
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
