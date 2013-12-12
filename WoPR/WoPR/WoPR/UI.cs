using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WoPR
{
    public class UI : Microsoft.Xna.Framework.DrawableGameComponent
    {

        public enum InputType {menu, map};

        private Dictionary<string, Menu> menus;
        public string activeMenu;
        private SpriteBatch batch;
        private SpriteFont font;
        public bool displayMap;
        public HexCoord currentSelection;
        public InputType currentType;
        new public WoPR Game;

        public UI(Game game)
            : base(game)
        {
            Game = (WoPR)game;
            initialize();
            teststuff();
        }

        private void initialize()
        {
            menus = new Dictionary<string, Menu>();
            currentSelection = HexCoord.Zero;
            currentType = InputType.menu;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            font = Game.Content.Load<SpriteFont>("gameFont");
        }

        public override void Initialize()
        {
            base.Initialize();
            batch = new SpriteBatch(Game.GraphicsDevice);
        }

        private void teststuff()
        {
            Menu temp = new Menu();
            temp.addItem("Item 1");
            temp.addItem("Item 3");
            temp.addItem("Item 2", 1);
            temp.addItem("More and longer text! Hooray!");
            menus.Add("Blah", temp);
            activeMenu = "Blah";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // If there are no events, there is no need to proceed
            if (Game.player1.buttonEvents.Count == 0) return;

            button pressedButton = Game.player1.buttonEvents.Dequeue();

            if (currentType == InputType.menu)
                menuInput(pressedButton);
            if (currentType == InputType.map)
                mapInput(pressedButton);

            
        }

        private void menuInput(button pressedButton)
        {
            // If the event is a direction up or down, or A or B, consume it for the active menu
            if (pressedButton == button.up ||
                pressedButton == button.down ||
                pressedButton == button.A ||
                pressedButton == button.B)
            {
                if (pressedButton == button.A)
                {
                    Game.MenuSelection(activeMenu, menus[activeMenu].getSelected().Value);
                }
                else if (pressedButton == button.B)
                {
                    // If it's a B button, implement this later.
                }
                // Else it must be a direction, pass it to the active menu
                else
                {
                    Debug.Print("It's a direction.");
                    menus[activeMenu].changeSelection(pressedButton);
                }
            }
        }

        private void mapInput(button pressedButton)
        {

        }

        // Draw the active menu
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            batch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            Debug.Assert(menus.ContainsKey(activeMenu), "Invalid menu selected as active: " + activeMenu);
            if (displayMap) Game.currentMap.draw(batch);
            menus[activeMenu].draw(batch, font);
            batch.End();
        }

        public void addMenu(Menu m, string label)
        {
            menus.Add(label, m);
        }
    }
}
