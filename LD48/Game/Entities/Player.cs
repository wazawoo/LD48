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
            float speed)
            : base(position, speed)
        {
            //...
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
            if (position.X > width - texture.Width / 2)
            {
                position.X = width - texture.Width / 2;
            }
            else if (position.X < texture.Width / 2)
            {
                position.X = texture.Width / 2;
            }

            if (position.Y > height - texture.Height / 2)
            {
                position.Y = height - texture.Height / 2;
            }
            else if (position.Y < texture.Height / 2)
            {
                position.Y = texture.Height / 2;
            }
        public void ApplyPhysics(GameTime gameTime) {
            // TODO: Make better
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += 1;
        }
    }
}
