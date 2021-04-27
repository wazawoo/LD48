using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Follow: Behavior
    {
        Entity target;
        public float lateralAcceleration;

        public Follow(
            Entity target,
            float lateralAcceleration,
            bool enabled = true)
            : base(BehaviorType.Follow, enabled)
        {
            this.target = target;
            this.lateralAcceleration = lateralAcceleration;
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //decide on dog movement
            //this only moves horizontally at the moment
            if (owner.position.X < (target.position.X))
            {
                owner.velocity.X += lateralAcceleration * dt;
            }
            else if (owner.position.X > target.position.X + target.size.X)
            {
                owner.velocity.X -= lateralAcceleration * dt;
            }

            base.Update(gameTime, keyboardState);
        }
    }
}
