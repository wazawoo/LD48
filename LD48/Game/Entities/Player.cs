using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Player: Entity
    {
        public JumpPhysics jump;
        public bool isOnGround;

        public Player(
            Vector2 position,
            float speed)
            : base(position, speed)
        {
            isOnGround = false;
            jump = new JumpPhysics();
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, Vector2 gameScreenSize) {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            ApplyPhysics(gameTime);

            if (jump.isActive)
            {
                position.Y -= jump.getJumpY();
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                position.Y -= speed * dt;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                position.Y += speed * dt;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                position.X -= speed * dt;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                position.X += speed * dt;
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (isOnGround) { jump.start(); }
            }

            var width = gameScreenSize.X;
            var height = gameScreenSize.Y;

            // restrict to bounds

            //allow continuing to the other side
            if (position.X > width - texture.Width / 2)
            {
                //amount we are off screen
                var dx = position.X - (width - texture.Width / 2);

                //right
                position.X = dx;
            }
            else if (position.X < texture.Width / 2)
            {
                //amount we are off screen
                var dx = texture.Width / 2 - position.X;

                //left
                position.X = width - dx;
            }

            if (position.Y >= height - texture.Height / 2)
            {
                //bottom
                position.Y = height - texture.Height / 2;
                // right now this will always be true until we add collision
                // checking.
                isOnGround = true;
            }
            else if (position.Y < texture.Height / 2)
            {
                //top
                position.Y = texture.Height / 2;
            }

            //test: round to nearest pixel
            position.X = (float) Math.Round(position.X);
            position.Y = (float) Math.Round(position.Y);
        }

        public void ApplyPhysics(GameTime gameTime) {
            // TODO: Make better
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += 1;
        }
    }
}
