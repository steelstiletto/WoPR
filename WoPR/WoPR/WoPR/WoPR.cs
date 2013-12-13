using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WoPR
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class WoPR : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public UI ui;
        public List<Player> players;
        public Map currentMap;

        //Textures
        public Texture2D tBorder0 = null;
        public Texture2D tBorder1 = null;
        public Texture2D tBorder2 = null;
        public Texture2D tBorderS = null;
        public Texture2D forest = null;
        public Texture2D plain = null;
        public Texture2D road = null;
        public Texture2D water = null;
        public Texture2D barracks = null;
        public Texture2D hq = null;
        public Texture2D supply = null;
        public Texture2D uBorder1 = null;
        public Texture2D uBorder2 = null;
        public Texture2D trooper = null;
        public Texture2D demo = null;
        public Texture2D samT = null;

        private double TESTprintTimer;

        public WoPR()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            ui = new UI(this);
            Components.Add(ui);
            players = new List<Player>();
            players.Add(new Player(true, new SimpleController(this, PlayerIndex.One)));
            players.Add(new Player(false, new SimpleController(this, PlayerIndex.Two)));
            Components.Add(players[0].controller);
            Components.Add(players[1].controller);
            currentMap = new Map(this);
            testStuff();
            initializeMenus();
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 834;
            graphics.PreferredBackBufferWidth = 1121;

            TESTprintTimer = 0;
        }

        private void testStuff()
        {

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            //Tile borders
            tBorder0 = Content.Load<Texture2D>("Tborders_p0");
            tBorder1 = Content.Load<Texture2D>("Tborders_p1");
            tBorder2 = Content.Load<Texture2D>("Tborders_p2");
            tBorderS = Content.Load<Texture2D>("TborderS");

            //tiles
            forest = Content.Load<Texture2D>("forest");
            plain = Content.Load<Texture2D>("plain");
            road = Content.Load<Texture2D>("road");
            water = Content.Load<Texture2D>("water");
            barracks = Content.Load<Texture2D>("Barracks");
            hq = Content.Load<Texture2D>("HQ");
            supply = Content.Load<Texture2D>("Supply");

            //Unit Borders
            uBorder1 = Content.Load<Texture2D>("borders_p1");
            uBorder2 = Content.Load<Texture2D>("borders_p2");

            //Unit overlays
            trooper = Content.Load<Texture2D>("trooper");
            demo = Content.Load<Texture2D>("Demoman");
            samT = Content.Load<Texture2D>("SamTroop");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            TESTprintTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (TESTprintTimer >= 1)
            {
                TESTprintFunction();
                TESTprintTimer -= 1;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void MenuSelection(string menuName, string itemSelected)
        {
            switch (menuName)
            {
                case "mainMenu":
                    mainMenuSelection(itemSelected);
                    break;
                default:
                    break;
            }
        }

        public void MapSelection(Tile selectedTile)
        {
        }

        private void initializeMenus()
        {
            Menu m = new Menu(new Vector2(200, 200), new Vector2(200, 200), 1);
            m.addItem("New Game");
            m.addItem("Exit");
            ui.addMenu(m, "mainMenu");
            ui.activeMenu = "mainMenu";

            //Action list for infantry
            m = new Menu(new Vector2(200, 200), new Vector2(200, 200), 1);
            m.addItem("Move");
            m.addItem("Attack");
            m.addItem("Capture");
            ui.addMenu(m, "InfantryMenu");

            //Barracks Build Menu unit
            m = new Menu(new Vector2(200, 200), new Vector2(200, 200), 1);
            m.addItem("Trooper - 100");
            m.addItem("Demolition Squad - 200");
            m.addItem("Sam Trooper - 200");
            ui.addMenu(m, "BarracksBuild");

        }

        private void mainMenuSelection(string itemSelected)
        {
            if (itemSelected == "Exit") this.Exit();
            if (itemSelected == "New Game") ui.displayMap = true;
        }

        private void TESTprintFunction()
        {
        }
    }
}
