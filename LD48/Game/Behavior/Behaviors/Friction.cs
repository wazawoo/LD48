using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Friction: Behavior
    {
        public Friction(Entity owner, bool enabled)
            : base(owner, enabled, BehaviorType.Friction)
        {
        }

        //for now friction is only dependent on tile
        //entity has no friction value

        public override void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            var dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

            //disabling friction for a moment
            //i dont like how it feels
            //it causes movement to be slower on the ground than in the air
            //i dont know that it should work that way

            ////apply friction if entity is standing on a tile
            ////move opposite the velocity
            //if (owner.tileStandingOn != null)
            //{
            //    var oldSign = Math.Sign(owner.velocity.X);
            //    owner.velocity.X -= oldSign * owner.tileStandingOn.Friction() * dt;
            //    var newSign = Math.Sign(owner.velocity.X);

            //    if (oldSign != newSign)
            //    {
            //        //if friction causes velocity to cross zero, put it back to zero
            //        owner.velocity.X = 0f;
            //    }
            //}

            base.Update(gameTime, keyboardState);
        }
    }
}
