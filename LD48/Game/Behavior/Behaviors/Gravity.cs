using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Gravity: Behavior
    {
        public Vector2 acceleration;

        public Gravity(
            Vector2 acceleration,
            bool enabled = true)
            : base(BehaviorType.Gravity, enabled)
        {
            this.acceleration = acceleration;
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //apply gravity
            owner.velocity += acceleration * dt;

            base.Update(gameTime, keyboardState);
        }
    }
}
