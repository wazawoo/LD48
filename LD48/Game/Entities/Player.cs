using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Player: Entity
    {
        public JumpPhysics jump;

        public Player(
            Vector2 position,
            Vector2 size,
            float movementAcceleration,
            Gravity gravity)
            : base(position, size, movementAcceleration, gravity)
        {
            jump = new JumpPhysics(new Vector2(0, -5 * movementAcceleration));

            velocity = new Vector2(0, 0);
            acceleration = new Vector2(0, 0);            
        }

        void ProcessInput(GameTime gameTime, KeyboardState keyboardState)
        {
            var dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                //...
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                //...
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                velocity.X -= movementAcceleration * dt;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                velocity.X += movementAcceleration * dt;
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (tileStandingOn != null & !jump.isCharging)
                {
                    //if not in the middle of jumping:
                    //start charging jump, record the time
                    //only allow charging while on the ground
                    jump.isCharging = true;
                    jump.chargeStartTime = gameTime.TotalGameTime;
                }
            }

            if (keyboardState.IsKeyUp(Keys.Space))
            {
                //if lift up jump key during charging, start the jump
                if (jump.isCharging)
                {
                    jump.isCharging = false;
                    velocity += jump.ActualVelocity(gameTime) * dt;
                }
            }
        }

        public override void Update(GameTime gameTime, Vector2 gameScreenSize, KeyboardState keyboardState, TileSet tileSet)
        {
            //process input before doing standard entity stuff
            //could allow entities to respond to input as well

            ProcessInput(gameTime, keyboardState);

            base.Update(gameTime, gameScreenSize, keyboardState, tileSet);
        }
    }
}
