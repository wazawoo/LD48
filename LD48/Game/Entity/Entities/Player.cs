using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Player: Entity
    {
        public Player(
            Vector2 position,
            Vector2 size)
            : base(position, size)
        {
        }

        public override void Update(GameTime gameTime, Vector2 gameScreenSize, KeyboardState keyboardState, TileSet tileSet)
        {
            //nothing special about the player for now!

            base.Update(gameTime, gameScreenSize, keyboardState, tileSet);
        }
    }
}
