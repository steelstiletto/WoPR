using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WoPR
{
    public class Tile
    {
        public enum TileType {plain, water, road, forest, headquarters, barracks, garage, supplyDepot};
        public enum Highlight {none, blue, orange};
        private const int MAXCHP = 10;

        public Player owner;
        public Unit unit;
        public WoPR Game;

        private int[] moveCosts;//foot, tread, air
        private int capturehp;
        private int hpHealed;
        private double dBonus;        
        private bool capturable;

        private String name;
        private HexCoord position;   //x,y,z
        private Building build;
        private TileType t;
        public Highlight highlight;

        public Tile(Game game, TileType Type, HexCoord xyz)
        {
            initialize(game, Type, xyz, null);
        }
        public Tile(Game game, TileType Type, HexCoord xyz, Player owner)
        {
            initialize(game, Type, xyz, owner);
        }

        private void initialize(Game game, TileType Type, HexCoord xyz, Player owner)
        {
            Game = (WoPR)game;
            position = xyz;
            this.owner = owner;
            unit = null;
            t = Type;
            highlight = Highlight.none;

            switch (Type)
            {
                case TileType.plain:
                    name = "Plain";
                    moveCosts = new int[]{1, 1, 1};
                    hpHealed = 0;
                    capturehp = 10;
                    dBonus = .1;
                    capturable = false;
                    build = null;
                    break;

                case TileType.water:
                    name = "Water";
                    moveCosts = new int[] { -1, -1, 1 };
                    hpHealed = 0;
                    capturehp = 10;
                    dBonus = 0;
                    capturable = false;
                    build = null;
                    break;

                case TileType.road:
                    name = "Road";
                    moveCosts = new int[] { 1, 1, 1 };
                    hpHealed = 0;
                    capturehp = 10;
                    dBonus = 0;
                    capturable = false;
                    build = null;
                    break;

                case TileType.forest:
                    name = "Forest";
                    moveCosts = new int[] { 1, 2, 1 };
                    hpHealed = 0;
                    capturehp = 10;
                    dBonus = .3;
                    capturable = false;
                    build = null;
                    break;

                case TileType.headquarters:
                    name = "Headquarters";
                    moveCosts = new int[] { 1, 1, 1 };
                    hpHealed = 20;
                    capturehp = 10;
                    dBonus = .4;
                    capturable = true;
                    build = new Building(Building.buildType.headquarters);
                    break;

                case TileType.barracks:
                    name = "Barracks";
                    moveCosts = new int[] { 1, 1, 1 };
                    hpHealed = 20;
                    capturehp = 10;
                    dBonus = .4;
                    capturable = true;
                    build = new Building(Building.buildType.barracks);
                    break;

                case TileType.garage:
                    name = "Garage";
                    moveCosts = new int[] { 1, 1, 1 };
                    hpHealed = 20;
                    capturehp = 10;
                    dBonus = 0.4;
                    capturable = true;
                    build = new Building(Building.buildType.garage);
                    break;

                case TileType.supplyDepot:
                    name = "Supply Depot";
                    moveCosts = new int[] { 1, 1, 1 };
                    hpHealed = 20;
                    capturehp = 10;
                    dBonus = 0.6;
                    capturable = true;
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
            drawUnitOverlay(batch);
            drawOverlay(batch);
            drawHealthOverlay(batch);
        }
                private void drawBorder(SpriteBatch batch)
                {
                    if (position.Equals(Game.ui.currentSelection)) //if tile is selected us the highlight sprite
                    {
                        batch.Draw(Game.tBorderS, convertToXY(), Color.White);
                    }
                    else
                    {
                        if (owner != null && owner.isPlayer1)//check if owned by player 1
                        {
                            batch.Draw(Game.tBorder1, convertToXY(), Color.White);
                        }
                        else if (owner != null && !(owner.isPlayer1))//check if owned by player 1
                        {
                            batch.Draw(Game.tBorder2, convertToXY(), Color.White);
                        }
                        else //not owned
                        {
                            batch.Draw(Game.tBorder0, convertToXY(), Color.White);
                        }
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
                            batch.Draw(Game.hq, temp, Color.White);
                            break;

                        case TileType.barracks:
                            batch.Draw(Game.barracks, temp, Color.White);
                            break;

                        case TileType.garage:
                            break;

                        case TileType.supplyDepot:
                            batch.Draw(Game.supply, temp, Color.White);
                            break;
                    }
                }
                private void drawUnitBorder(SpriteBatch batch)
                {
                    Vector2 temp = convertToXY();
                    temp.X += 45;
                    temp.Y += 9;

                    if(unit != null)
                    {
                        if (unit.getOwner().isPlayer1)//check if unit belongs to player 1
                        {
                            batch.Draw(Game.uBorder1, temp, Color.White);
                        }
                        else
                        {
                            batch.Draw(Game.uBorder2, temp, Color.White);
                        }
                    }
                }
                private void drawUnit(SpriteBatch batch)
                {
                    Vector2 temp = convertToXY();
                    temp.X += 49;
                    temp.Y += 13;

                    if (unit != null)
                    {
                        Unit.unitType t = unit.t;

                        if (unit.getOwner().isPlayer1)//check if unit belongs to player 1
                        {
                            switch (t)
                            {
                                case Unit.unitType.trooper:
                                    batch.Draw(Game.trooper, temp, Color.Red);
                                    break;

                                case Unit.unitType.demolitionSquad:
                                    batch.Draw(Game.demo, temp, Color.Red);
                                    break;

                                case Unit.unitType.samTrooper:
                                    batch.Draw(Game.samT, temp, Color.Red);
                                    break;
                            }
                        }
                        else
                        {
                            switch (t)
                            {
                                case Unit.unitType.trooper:
                                    batch.Draw(Game.trooper, temp, Color.Blue);
                                    break;

                                case Unit.unitType.demolitionSquad:
                                    batch.Draw(Game.demo, temp, Color.Blue);
                                    break;

                                case Unit.unitType.samTrooper:
                                    batch.Draw(Game.samT, temp, Color.Blue);
                                    break;
                            }
                        }
                    }
                }

                private void drawUnitOverlay(SpriteBatch batch)
                {
                    Vector2 temp = convertToXY();
                    temp.X += 45;
                    temp.Y += 9;

                    if (unit != null && unit.acted)
                    {
                        batch.Draw(Game.uOverlay, temp, Color.Black * 0.5f);
                    }
                    if (unit != null && unit.moved)
                    {
                        batch.Draw(Game.uOverlay, temp, Color.Black * 0.5f);
                    }
                }
        private void drawOverlay(SpriteBatch batch)
        {
            Vector2 temp = convertToXY();
            temp.X += 13;
            temp.Y += 9;

            switch (highlight)
            {
                case Highlight.blue:
                    batch.Draw(Game.overlay, temp, Color.CornflowerBlue * 0.7f);
                    break;

                case Highlight.orange:
                    batch.Draw(Game.overlay, temp, Color.Orange * 0.5f);
                    break;
            }
        }

        private void drawHealthOverlay(SpriteBatch batch)
        {
            if (unit != null && unit.getHp() != 100)
            {
                Vector2 temp = convertToXY();
                temp.X += 15;
                temp.Y += 55;
                batch.Draw(Game.hpBackplate, temp, Color.White);
                temp.X += 8;
                batch.DrawString(Game.ui.font, ((unit.getHp() + 9) / 10).ToString(), temp, Color.Black);
            }
            if (capturable && capturehp != 10)
            {
                Vector2 temp = convertToXY();
                temp.X += 115;
                temp.Y += 55;
                batch.Draw(Game.hpBackplate, temp, Color.White);
                temp.X += 8;
                batch.DrawString(Game.ui.font, capturehp.ToString(), temp, Color.Black);
            }
        }
        
        private Vector2 convertToXY()
        {
            float x = 0;
            float y = 0;

            x = (120 * (float)position.x * (3 / 2));
            y = (80 * (float)Math.Sqrt(3) * ((float)position.y + ((float)position.x / 2)));

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
        public HexCoord getPosition()
        {
            return position;
        }

        public bool buildable()
        {
            // If the active player is not in control, or the tile is occupied, return
            if (owner != Game.currentPlayer || unit != null) return false;

            // If this is a barracks, it can be built on
            if (this.getType() == TileType.barracks) return true;

            // Check neighbors for friendly barracks
            foreach(HexCoord h in position.neighbors())
            {
                if(Game.currentMap.tiles.ContainsKey(h))
                {
                    Tile target = Game.currentMap.tiles[h];
                    if (target.owner == owner)
                        if (target.getType() == TileType.barracks)
                            return true;
                }
            }

            // If all else has failed, this tile cannot be built on.
            return false;
        }
    }
}
