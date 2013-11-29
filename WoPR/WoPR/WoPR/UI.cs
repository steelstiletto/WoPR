using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WoPR
{
    public class UI : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Dictionary<string, Menu> menus;
        public string activeMenu;
        private SpriteBatch batch;
        private SpriteFont font;
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

            // Peek to view the top button event in player1's queue
            button currentEvent = Game.player1.buttonEvents.Peek();

            // If the event is a direction up or down, or A or B, consume it for the active menu
            if (currentEvent == button.up ||
                currentEvent == button.down ||
                currentEvent == button.A ||
                currentEvent == button.B)
            {
                Game.player1.buttonEvents.Dequeue();
                if (currentEvent == button.A)
                {
                    Game.MenuSelection(activeMenu, menus[activeMenu].getSelected().Value);
                }
                else if (currentEvent == button.B)
                {
                    // If it's a B button, implement this later.
                }
                // Else it must be a direction, pass it to the active menu
                else
                {
                    menus[activeMenu].changeSelection(currentEvent);
                }
            }
        }

        // Draw the active menu
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            batch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            Debug.Assert(menus.ContainsKey(activeMenu), "Invalid menu selected as active: " + activeMenu);
            menus[activeMenu].draw(batch, font);
            batch.End();
        }
    }
}
