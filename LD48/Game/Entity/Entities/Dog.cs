using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Dog : Entity
    {
        public Dog (
            Vector2 position,
            Vector2 size)
            : base(position, size)
        {
        }

        public override void Update(GameTime gameTime, Vector2 gameScreenSize, KeyboardState keyboardState, TileSet tileSet)
        {
            //hmm, nothing special about dog either?
            //is this the right approach?
            //where should behaviors be initiated?

            base.Update(gameTime, gameScreenSize, keyboardState, tileSet);
        }
    }
}

