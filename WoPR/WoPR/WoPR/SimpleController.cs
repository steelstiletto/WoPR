using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WoPR
{

    public enum button : byte { up, down, left, right, A, B, X, Y, start, select};

    public class SimpleController : Microsoft.Xna.Framework.GameComponent
    {

        private Queue<button> buttonEvents;
        private PlayerIndex controllerSlot;

        public SimpleController(Game game, PlayerIndex player)
            : base(game)
        {
            buttonEvents = new Queue<button>();
            controllerSlot = player;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
