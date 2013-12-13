using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WoPR
{
    public class HUD
    {
        private Vector2[] locations;
        private Vector2 textOffset;
        private WoPR Game;

        public HUD(WoPR game)
        {
            Game = game;
            locations = new Vector2[2];
            locations[0] = new Vector2(120, 0);
            locations[1] = new Vector2(600, 0);
            textOffset = new Vector2(20, 15);
        }
        public void draw(SpriteBatch batch, SpriteFont font)
        {
            batch.Draw(Game.hudBackplate, locations[0], Color.White);
            batch.Draw(Game.hudBackplate, locations[1], Color.White);
            batch.DrawString(font, Game.players[0].resources.ToString(), locations[0]+textOffset, Color.Blue);
            batch.DrawString(font, Game.players[1].resources.ToString(), locations[1]+textOffset, Color.Blue);
        }
    }
}
