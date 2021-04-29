using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Move: Behavior
    {
        public float lateralAcceleration;
        private float initialAcceleration;

        public Move(
            float lateralAcceleration,
            bool enabled = true)
            : base(BehaviorType.Move, enabled)
        {
            this.lateralAcceleration = lateralAcceleration;
            this.initialAcceleration = lateralAcceleration;
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
                //experiment: adjust acceleration with keys instead of velocity
                if (lateralAcceleration >= 0)
                {
                    lateralAcceleration = -initialAcceleration;
                }

                owner.velocity.X += lateralAcceleration * dt;

                //old:
                //owner.velocity.X -= lateralAcceleration * dt;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                if (lateralAcceleration <= 0)
                {
                    lateralAcceleration = initialAcceleration;
                }

                owner.velocity.X += lateralAcceleration * dt;
            }


            //alternative to friction:
            //if not pressing a movement key, and on the ground, stop immediately
            //this feels more like celeste/towerfall
            if (keyboardState.IsKeyUp(Keys.Right) && keyboardState.IsKeyUp(Keys.Left))
            {
                //if not trying to move and on ground, reset velocity
                if (owner.tileStandingOn != null && owner.tileStandingOn.type == TileType.Ground)
                {
                    lateralAcceleration = 0f;

                    //old:
                    //owner.velocity.X = 0f;
                }
            }

            base.Update(gameTime, keyboardState);
        }
    }
}
