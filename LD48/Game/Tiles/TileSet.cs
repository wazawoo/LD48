using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class TileSet
    {
        Vector2 tileSize;
        int width;
        int height;
        public Tile[,] tiles;

        public TileSet(Vector2 tileSize, int width, int height)
        {
            this.tileSize = tileSize;
            this.width = width;
            this.height = height;

            tiles = new Tile[width, height];
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    var position = new Vector2(x * tileSize.X, y * tileSize.Y);
                    tiles[x, y] = new Tile(position: position);
                }
            }
        }

        public void LoadTiles(Texture2D texture)
        {
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    tiles[x, y].texture = texture;
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    //checkerboard (just drawing half the tiles)
                    if ((x + y) % 2 == 0)
                    {
                        tiles[x, y].Draw(gameTime, spriteBatch);
                    }
                }
            }
        }
    }
}