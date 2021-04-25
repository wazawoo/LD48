using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Dog : Entity
    {
        Player owner;

        public Dog (
            Vector2 position,
            Vector2 size,
            float movementAcceleration,
            Gravity gravity,
            Player owner)
            : base(position, size, movementAcceleration, gravity)
        {
            //dog do not exist without owner
            this.owner = owner;
        }

        public override void Update(GameTime gameTime, Vector2 gameScreenSize, KeyboardState keyboardState, TileSet tileSet)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //decide on dog movement
            if (position.X < (owner.position.X - owner.size.X))
            {
                acceleration.X = 3 * movementAcceleration * dt;
            }
            else if (position.X > owner.position.X + owner.size.X)
            {
                acceleration.X = -3 * movementAcceleration * dt;
            }

            base.Update(gameTime, gameScreenSize, keyboardState, tileSet);
        }
    }
}

