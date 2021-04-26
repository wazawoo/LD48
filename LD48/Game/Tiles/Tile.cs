using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{

    public enum TileType
    {
        Air = 1,
        Ground = 2
    }

    public class Tile
    {
        public Texture2D texture;
        public Vector2 position;
        public TileType type;

        public Tile(
            Vector2 position)
        {
            this.position = position;
        }

        public float Friction() {
            //this is the amount of velocity removed from your movement when standing on this tile
            //currently it does not apply if you are floating though this tile
            return type switch
            {
                TileType.Air => 0f,
                TileType.Ground => 100f,
                _ => 0f,
            };
        }

        public void LoadTile(char c, Texture2D[] textures) {
            switch (c)
            {
                case '.':
                    texture = textures[0];
                    type = TileType.Air;
                    break;
                case 'x':
                    texture = textures[1];
                    type = TileType.Ground;
                    break;
                default:
                    // Unknown tile type character
                    throw new NotSupportedException(String.Format("Unsupported tile type character '{0}'", c));
            }
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
