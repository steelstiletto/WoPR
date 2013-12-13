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
        public int currentPlayerIndex;
        public Player currentPlayer { get { return players[currentPlayerIndex]; } }
        public SimpleController currentPlayerController { get { return players[currentPlayerIndex].controller; } }
        public Map currentMap;
        public List<Tile> subSelection;
        public Tile activeTile;
        public Attack cAttack;
        

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
        public Texture2D overlay = null;
        public Texture2D uBorder1 = null;
        public Texture2D uBorder2 = null;
        public Texture2D trooper = null;
        public Texture2D demo = null;
        public Texture2D samT = null;
        public Texture2D uOverlay = null;
        public Texture2D hudBackplate = null;
        public Texture2D hpBackplate = null;

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
            players.Add(new Player(false, new SimpleController(this, PlayerIndex.One)));
            Components.Add(players[0].controller);
            Components.Add(players[1].controller);
            currentPlayerIndex = 0;
            currentMap = new Map(this);
            testStuff();
            initializeMenus();
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 834;
            graphics.PreferredBackBufferWidth = 1121;
            subSelection = null;
            activeTile = null;
            cAttack = null;

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
            overlay = Content.Load<Texture2D>("overlay");

            //Unit Borders
            uBorder1 = Content.Load<Texture2D>("borders_p1");
            uBorder2 = Content.Load<Texture2D>("borders_p2");
            uOverlay = Content.Load<Texture2D>("unitOverlay");

            //Unit overlays
            trooper = Content.Load<Texture2D>("trooper");
            demo = Content.Load<Texture2D>("Demoman");
            samT = Content.Load<Texture2D>("SamTroop");

            //Hud and hp backplates
            hudBackplate = Content.Load<Texture2D>("HudBackplate");
            hpBackplate = Content.Load<Texture2D>("HPBackplate");

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
                case "trooperMenu":
                case "demolitionSquadMenu":
                case "samInfantryMenu":
                    unitMenuSelection(itemSelected);
                    break;
                case "endTurnMenu":
                    endTurn();
                    break;
                case "barracksMenu":
                    barracksMenuSelection(itemSelected);
                    break;
                default:
                    break;
            }
        }

        public void MapSelection(Tile selectedTile)
        {
            ui.currentType = UI.InputType.menu;
            if (subSelection == null)
            {
                if (selectedTile.unit != null && selectedTile.unit.getOwner() == currentPlayer)
                {
                    switch (selectedTile.unit.t)
                    {
                        case Unit.unitType.trooper:
                            ui.activeMenu = "trooperMenu";
                            break;

                        case Unit.unitType.demolitionSquad:
                            ui.activeMenu = "demolitionSquadMenu";
                            break;

                        case Unit.unitType.samTrooper:
                            ui.activeMenu = "samInfantryMenu";
                            break;

                    }
                    return;
                }
                if (selectedTile.buildable())
                {
                    ui.activeMenu = "barracksMenu";
                    return;
                }
                ui.activeMenu = "endTurnMenu";
            }
            else
            {
                if (selectedTile.highlight == Tile.Highlight.blue)
                {
                    if (selectedTile.unit == null)
                    {
                        currentMap.move(activeTile.getPosition(), selectedTile.getPosition());
                        activeTile = null;
                        foreach (Tile t in subSelection)
                        {
                            t.highlight = Tile.Highlight.none;
                        }
                        subSelection = null;
                        selectedTile.unit.moved = true;
                    }
                }
                if (selectedTile.highlight == Tile.Highlight.orange)
                {
                    if (selectedTile.unit != null && selectedTile.unit.getOwner() != activeTile.unit.getOwner())
                    {
                        selectedTile.unit.damage(cAttack, selectedTile); activeTile = null;
                        foreach (Tile t in subSelection)
                        {
                            t.highlight = Tile.Highlight.none;
                        }
                        subSelection = null;
                        selectedTile.unit.moved = true;
                    }
                }
            }
        }

        private void initializeMenus()
        {
            Menu m = new Menu(new Vector2(200, 200), new Vector2(200, 200), 1);
            m.addItem("New Game");
            m.addItem("Exit");
            ui.addMenu(m, "mainMenu");
            ui.activeMenu = "mainMenu";

            //Action list for trooper
            m = new Menu(new Vector2(200, 200), new Vector2(200, 200), 1);
            m.addItem("Move");
            m.addItem("FlameThrower");
            m.addItem("Rifle");
            m.addItem("Capture");
            m.addItem("End Turn");
            ui.addMenu(m, "trooperMenu");

            //Action list for demolition squad
            m = new Menu(new Vector2(200, 200), new Vector2(200, 200), 1);
            m.addItem("Move");
            m.addItem("Mortar");
            m.addItem("Bazooka");
            m.addItem("Capture");
            m.addItem("End Turn");
            ui.addMenu(m, "demolitionSquadMenu");

            //Action list for SAM Infantry
            m = new Menu(new Vector2(200, 200), new Vector2(200, 200), 1);
            m.addItem("Move");
            m.addItem("SAM Launcher");
            m.addItem("Rifle");
            m.addItem("Capture");
            m.addItem("End Turn");
            ui.addMenu(m, "samInfantryMenu");

            //Barracks Build Menu unit
            m = new Menu(new Vector2(200, 200), new Vector2(200, 200), 1);
            m.addItem("Trooper - 100");
            m.addItem("Demolition Squad - 200");
            m.addItem("Sam Trooper - 200");
            m.addItem("End Turn");
            ui.addMenu(m, "barracksMenu");

            //Generic menu for an empty or enemy tile
            m = new Menu(new Vector2(200, 200), new Vector2(200, 200), 1);
            m.addItem("End Turn");
            ui.addMenu(m, "endTurnMenu");

        }

        private void mainMenuSelection(string itemSelected)
        {
            if (itemSelected == "Exit") this.Exit();
            if (itemSelected == "New Game")
            {
                ui.displayMap = true;
                startTurn();
            }
        }

        private void barracksMenuSelection(string itemSelected)
        {
            Tile temp;

            if (itemSelected == "Trooper - 100")
            {
                currentMap.tiles.TryGetValue(ui.currentSelection, out temp);
                {
                    if(temp.unit == null)
                    {
                        if(currentPlayer.spendResources(100))
                        {
                            temp.unit = new Unit(Unit.unitType.trooper, currentPlayer);
                            currentPlayer.unitList.Add(temp.unit);
                        }
                        else
                        {
                            //ERROR MSG NOT ENOUGH MONEY
                        }
                    }
                }
            }

            if (itemSelected == "Demolition Squad - 200")
            {
                currentMap.tiles.TryGetValue(ui.currentSelection, out temp);
                {
                    if (temp.unit == null)
                    {
                        if (currentPlayer.spendResources(200))
                        {
                            temp.unit = new Unit(Unit.unitType.demolitionSquad, currentPlayer);
                            currentPlayer.unitList.Add(temp.unit);
                        }
                        else
                        {
                            //ERROR MSG NOT ENOUGH MONEY
                        }
                    }
                }
            }
            if (itemSelected == "Sam Trooper - 200")
            {
                currentMap.tiles.TryGetValue(ui.currentSelection, out temp);
                {
                    if (temp.unit == null)
                    {
                        if (currentPlayer.spendResources(200))
                        {
                            temp.unit = new Unit(Unit.unitType.samTrooper, currentPlayer);
                            currentPlayer.unitList.Add(temp.unit);
                        }
                        else
                        {
                            //ERROR MSG NOT ENOUGH MONEY
                        }
                    }
                }
            }
            if (itemSelected == "End Turn")
            {
                endTurn();
            }
        }

        private void unitMenuSelection(string itemSelected)
        {
            List<Tile> tiles;
            Tile cTile;

            if (itemSelected == "Move")
            {
                currentMap.tiles.TryGetValue(ui.currentSelection, out cTile);
                tiles = currentMap.getLegalMoves(cTile);
                if (!cTile.unit.moved == true)
                {
                    foreach (Tile t in tiles)
                    {
                        t.highlight = Tile.Highlight.blue;
                    }
                    subSelection = tiles;
                    activeTile = cTile;
                }
            }
            if (itemSelected == "Rifle" || itemSelected == "Mortar" || itemSelected == "SAM Launcher")
            {
                currentMap.tiles.TryGetValue(ui.currentSelection, out cTile);
                tiles = currentMap.getLegalAttacks(cTile, true);
                foreach (Tile t in tiles)
                {
                    t.highlight = Tile.Highlight.orange;
                }
                subSelection = tiles;
                activeTile = cTile;
                cAttack = cTile.unit.getPrimaryAttack(); 
            }
            if (itemSelected == "Flamethrower" || itemSelected == "Bazooka" || itemSelected == "Rifle")
            {
                currentMap.tiles.TryGetValue(ui.currentSelection, out cTile);
                tiles = currentMap.getLegalAttacks(cTile, false);
                foreach (Tile t in tiles)
                {
                    t.highlight = Tile.Highlight.orange;
                }
                subSelection = tiles;
                activeTile = cTile;
                cAttack = cTile.unit.getSecondaryAttack();
            }
            if (itemSelected == "Capture")
            {
                endTurn();
            }
            if (itemSelected == "End Turn")
            {
                endTurn();
            }
        }

        private void TESTprintFunction()
        {
        }

        public void incrementPlayer()
        {
            currentPlayerIndex++;
            currentPlayerIndex %= players.Count;
        }

        public void endTurn()
        {
            foreach (Player p in players)
            {
                p.controller.clearQueue();
            }
            foreach (Unit u in currentPlayer.unitList)
            {
                u.acted = false;
                u.moved = false;
            }
            incrementPlayer();
            startTurn();
        }

        public void startTurn()
        {
            int resourcesToAdd = 0;
            foreach(KeyValuePair<HexCoord, Tile> pair in currentMap.tiles)
            {
                if (pair.Value.getBuilding() != null && pair.Value.owner == currentPlayer)
                    resourcesToAdd += 100;
            }
            currentPlayer.grantIncome(resourcesToAdd);
        }
    }
}
