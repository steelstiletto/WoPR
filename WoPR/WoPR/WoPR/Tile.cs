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
        //coord conversion
        //x' = size * 3/2 * x
        //y' = size * sqrt(3) * (y + x/2)

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

        SpriteBatch spriteBatch;
        Texture2D tBorder0 = null;
        Texture2D tBorder1 = null;
        Texture2D tBorder2 = null;
        Texture2D forest = null;
        Texture2D plain = null;
        Texture2D road = null;
        Texture2D water = null;
        Texture2D uBorder1 = null;
        Texture2D uBorder2 = null;

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

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            //Tile borders
            tBorder0 = Game.Content.Load<Texture2D>("Tborders_p0");
            tBorder1 = Game.Content.Load<Texture2D>("Tborders_p1");
            tBorder2 = Game.Content.Load<Texture2D>("Tborders_p2");

            //tiles
            forest = Game.Content.Load<Texture2D>("forest");
            plain = Game.Content.Load<Texture2D>("plain");
            road = Game.Content.Load<Texture2D>("road");
            water = Game.Content.Load<Texture2D>("water");

            //Unit Borders
            uBorder1 = Game.Content.Load<Texture2D>("borders_p1");
            uBorder2 = Game.Content.Load<Texture2D>("borders_p2");

            //Unit overlays

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


        }


        public override void Draw(SpriteBatch batch, GameTime gt)
        {
            base.Draw(gt);
            
            drawBorder(batch);
            drawTile(batch);
            drawUnitBorder(batch);
            drawUnit(batch);
        }
                private void drawBorder(SpriteBatch batch)
                {
                    batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                    batch.Draw(tBorder0, convertToXY(), Color.White);

                    batch.End();

                }
                private void drawTile(SpriteBatch batch)
                {
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                    spriteBatch.Draw(plain, convertToXY(), Color.White);

                    spriteBatch.End();
                }
                private void drawUnitBorder(SpriteBatch batch)
                {
                    if(unit != null)
                    {
                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                        spriteBatch.Draw(uBorder1, convertToXY(), Color.White);

                        spriteBatch.End();
                    }
                }
                private void drawUnit(SpriteBatch batch)
                {
                    if (unit != null)
                    {
                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                        spriteBatch.Draw(uBorder1, convertToXY(), Color.Red);

                        spriteBatch.End();
                    }
                }

        private Vector2 convertToXY()
        {
            int[] temp = new int[2];
            int x = 0;
            int y = 0;
            int size = 10;

            x = (int)(size * position.x * 3 / 2);
            y = (int)(size * Math.Sqrt(3) * (position.y + (position.x / 2)));

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
