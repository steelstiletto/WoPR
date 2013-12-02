using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WoPR
{

    public enum button : byte { up, down, left, right, A, B, X, Y, start, back};

    public class SimpleController : Microsoft.Xna.Framework.GameComponent
    {

        public Queue<button> buttonEvents;
        private PlayerIndex controllerSlot;
        private Dictionary<button, double> directionRepeatTimers;
        GamePadState previous;

        public SimpleController(Game game, PlayerIndex player)
            : base(game)
        {
            buttonEvents = new Queue<button>();
            controllerSlot = player;
            directionRepeatTimers = new Dictionary<button, double>();
            directionRepeatTimers.Add(button.up, 0);
            directionRepeatTimers.Add(button.down, 0);
            directionRepeatTimers.Add(button.left, 0);
            directionRepeatTimers.Add(button.right, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // First update is a special case. No events can be added, only initialization of the previous state
            if (previous == null)
            {
                previous = GamePad.GetState(controllerSlot);
                return;
            }
            GamePadState current = GamePad.GetState(controllerSlot);

            // Standard button presses happen once when initially pressed
            if (previous.Buttons.A == ButtonState.Released && current.Buttons.A == ButtonState.Pressed)
                buttonEvents.Enqueue(button.A);
            if (previous.Buttons.B == ButtonState.Released && current.Buttons.B == ButtonState.Pressed)
                buttonEvents.Enqueue(button.B);
            if (previous.Buttons.X == ButtonState.Released && current.Buttons.X == ButtonState.Pressed)
                buttonEvents.Enqueue(button.X);
            if (previous.Buttons.Y == ButtonState.Released && current.Buttons.Y == ButtonState.Pressed)
                buttonEvents.Enqueue(button.Y);
            if (previous.Buttons.Start == ButtonState.Released && current.Buttons.Start == ButtonState.Pressed)
                buttonEvents.Enqueue(button.start);
            if (previous.Buttons.Back == ButtonState.Released && current.Buttons.Back == ButtonState.Pressed)
                buttonEvents.Enqueue(button.back);

            // Direction presses add an event and start a timer when first pressed
            if (previous.DPad.Up == ButtonState.Released && current.DPad.Up == ButtonState.Pressed)
            {
                buttonEvents.Enqueue(button.up);
                directionRepeatTimers[button.up] = 0;
            }
            if (previous.DPad.Down == ButtonState.Released && current.DPad.Down == ButtonState.Pressed)
            {
                buttonEvents.Enqueue(button.down);
                directionRepeatTimers[button.down] = 0;
            }
            if (previous.DPad.Left == ButtonState.Released && current.DPad.Left == ButtonState.Pressed)
            {
                buttonEvents.Enqueue(button.left);
                directionRepeatTimers[button.left] = 0;
            }
            if (previous.DPad.Right == ButtonState.Released && current.DPad.Right == ButtonState.Pressed)
            {
                buttonEvents.Enqueue(button.right);
                directionRepeatTimers[button.right] = 0;
            }

            // Directions that are still down are checked vs the interval to add an additional event
            if (previous.DPad.Up == ButtonState.Pressed && current.DPad.Up == ButtonState.Pressed)
            {
                
                // If during the first second, check to see if the 1 second interval has passed to add the first repeat event
                if (directionRepeatTimers[button.up] < 1)
                    if (directionRepeatTimers[button.up] + gameTime.ElapsedGameTime.TotalSeconds >= 1)
                        buttonEvents.Enqueue(button.up);

                // Add the time since last update to the timer
                directionRepeatTimers[button.up] += gameTime.ElapsedGameTime.TotalSeconds;

                // Add events for every .2 seconds past 1.1
                while (directionRepeatTimers[button.up] >= 1.3)
                {
                    directionRepeatTimers[button.up] -= .2;
                    buttonEvents.Enqueue(button.up);
                }
            }
            if (previous.DPad.Down == ButtonState.Pressed && current.DPad.Down == ButtonState.Pressed)
            {
                // If during the first second, check to see if the 1 second interval has passed to add the first repeat event
                if (directionRepeatTimers[button.down] < 1)
                    if (directionRepeatTimers[button.down] + gameTime.ElapsedGameTime.TotalSeconds >= 1)
                        buttonEvents.Enqueue(button.down);

                // Add the time since last update to the timer
                directionRepeatTimers[button.down] += gameTime.ElapsedGameTime.TotalSeconds;

                // Add events for every .2 seconds past 1.1
                while (directionRepeatTimers[button.down] >= 1.3)
                {
                    directionRepeatTimers[button.down] -= .2;
                    buttonEvents.Enqueue(button.down);
                }
            }
            if (previous.DPad.Left == ButtonState.Pressed && current.DPad.Left == ButtonState.Pressed)
            {
                // If during the first second, check to see if the 1 second interval has passed to add the first repeat event
                if (directionRepeatTimers[button.left] < 1)
                    if (directionRepeatTimers[button.left] + gameTime.ElapsedGameTime.TotalSeconds >= 1)
                        buttonEvents.Enqueue(button.left);

                // Add the time since last update to the timer
                directionRepeatTimers[button.left] += gameTime.ElapsedGameTime.TotalSeconds;

                // Add events for every .2 seconds past 1.1
                while (directionRepeatTimers[button.left] >= 1.3)
                {
                    directionRepeatTimers[button.left] -= .2;
                    buttonEvents.Enqueue(button.left);
                }
            }
            if (previous.DPad.Right == ButtonState.Pressed && current.DPad.Right == ButtonState.Pressed)
            {
                // If during the first second, check to see if the 1 second interval has passed to add the first repeat event
                if (directionRepeatTimers[button.right] < 1)
                    if (directionRepeatTimers[button.right] + gameTime.ElapsedGameTime.TotalSeconds >= 1)
                        buttonEvents.Enqueue(button.right);

                // Add the time since last update to the timer
                directionRepeatTimers[button.right] += gameTime.ElapsedGameTime.TotalSeconds;

                // Add events for every .2 seconds past 1.1
                while (directionRepeatTimers[button.right] >= 1.3)
                {
                    directionRepeatTimers[button.right] -= .2;
                    buttonEvents.Enqueue(button.right);
                }
            }

            // If the direction buttons are not being held down, reset the timers.
            if (current.DPad.Up != ButtonState.Pressed)
                directionRepeatTimers[button.up] = 0;
            if (current.DPad.Down != ButtonState.Pressed)
                directionRepeatTimers[button.down] = 0;
            if (current.DPad.Left != ButtonState.Pressed)
                directionRepeatTimers[button.left] = 0;
            if (current.DPad.Right != ButtonState.Pressed)
                directionRepeatTimers[button.right] = 0;

            // Update the previous gamepad with the current
            previous = current;
        }

    }
}
