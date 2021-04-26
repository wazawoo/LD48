using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Move: Behavior
    {
        public float lateralAcceleration;

        public Move(
            float lateralAcceleration,
            Entity owner,
            bool enabled)
            : base(owner, enabled, "move")
        {
            this.lateralAcceleration = lateralAcceleration;
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            //at the moment, this is just movement due to keyboard controls
            //bad name maybe

            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

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
                owner.velocity.X -= lateralAcceleration * dt;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                owner.velocity.X += lateralAcceleration * dt;
            }

            base.Update(gameTime, keyboardState);
        }
    }
}
