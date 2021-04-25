using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LD48
{
    public class TileSet
    {
        int width;
        int height;
        public Tile[,] tiles;

        public TileSet(
            Vector2 tileSize,
            int width,
            int height)
        {
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

        public void LoadTiles(
            IServiceProvider serviceProvider,
            Stream fileStream)
        {
            var content = new ContentManager(serviceProvider, "Content");

            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                        throw new Exception(String.Format("The length of line {0} is incorrect.", lines.Count));
                    line = reader.ReadLine();
                }
            }

            if (lines.Count != height)
                throw new Exception(String.Format("Got {0} lines, expected {1}", lines.Count, height));

            //only load tile textures once
            var textures = new Texture2D[2];
            for (int i = 0; i < textures.Length; ++i)
            {
                string path = String.Format("Tiles/tile{0}", i + 1);
                textures[i] = content.Load<Texture2D>(path);
            }

            // Loop over every tile position,
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    char c = lines[y][x];
                    tiles[x, y].LoadTile(c, textures);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    tiles[x, y].Draw(gameTime, spriteBatch);
                }
            }
        }

        public TileType getType(float xFloat, float yFloat)
        {
            var x = (xFloat / 10);
            var y = (yFloat / 10);
            if (y > 23) { return TileType.Air;  }
            if (x > 31) { return TileType.Air; }
            var tile = tiles[(int)x, (int)y];
            return tile.type;
        }
    }
}