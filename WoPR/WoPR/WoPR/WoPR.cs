using System.Diagnostics;
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
        public SimpleController player1;
        public Map currentMap;

        //Textures
        public Texture2D tBorder0 = null;
        public Texture2D tBorder1 = null;
        public Texture2D tBorder2 = null;
        public Texture2D forest = null;
        public Texture2D plain = null;
        public Texture2D road = null;
        public Texture2D water = null;
        public Texture2D uBorder1 = null;
        public Texture2D uBorder2 = null;

        private double TESTprintTimer;

        public WoPR()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            currentMap = new Map(this);
            ui = new UI(this);
            Components.Add(ui);
            player1 = new SimpleController(this, PlayerIndex.One);
            Components.Add(player1);
            testStuff();
            initializeMenus();
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1000;

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

            //tiles
            forest = Content.Load<Texture2D>("forest");
            plain = Content.Load<Texture2D>("plain");
            road = Content.Load<Texture2D>("road");
            water = Content.Load<Texture2D>("water");

            //Unit Borders
            uBorder1 = Content.Load<Texture2D>("borders_p1");
            uBorder2 = Content.Load<Texture2D>("borders_p2");

            //Unit overlays


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

        private void initializeMenus()
        {
            Menu m = new Menu(new Vector2(200, 200), new Vector2(200, 200), 1);
            m.addItem("New Game");
            m.addItem("Exit");
            ui.addMenu(m, "mainMenu");
            ui.activeMenu = "mainMenu";
        }

        private void mainMenuSelection(string itemSelected)
        {
            if (itemSelected == "Exit") this.Exit();
            if (itemSelected == "New Game") ui.displayMap = true;
        }

        private void TESTprintFunction()
        {
            foreach (button b in player1.buttonEvents)
            {
                Debug.Print(b + " ");
            }
            Debug.Print("\n");
        }
    }
}
