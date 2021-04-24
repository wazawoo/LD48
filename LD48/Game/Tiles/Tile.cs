using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Tile
    {
        public Texture2D texture;
        public Vector2 position;

        public Tile(
            Vector2 position)
        {
            this.position = position;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(
                    texture,
                    position,
                    null,
                    Color.White,
                    0f,
                    new Vector2(0, 0),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
