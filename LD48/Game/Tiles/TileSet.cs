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
        public Vector2 tileSize;
        int width;
        int height;
        public Tile[,] tiles;

        public TileSet(
            Vector2 tileSize,
            int width,
            int height)
        {
            this.tileSize = tileSize;
            this.width = width;
            this.height = height;

            tiles = new Tile[width, height];
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    //top left corner
                    var position = new Vector2(x * tileSize.X, y * tileSize.Y);
                    tiles[x, y] = new Tile(position: position);
                }
            }
        }

        enum TileSprite
        {
            BgGrayStone = 0,
            FgDarkBlue = 1
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
            var textures = new Texture2D[Enum.GetNames(typeof(TileSprite)).Length];

            for (int i = 0; i < textures.Length; ++i)
            {
                String path = GetTilePath(i);
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

        public Tile GetTile(float xFloat, float yFloat)
        {
            int x = (int) (xFloat / tileSize.X);
            int y = (int) (yFloat / tileSize.Y);

            //check the correct tile, even if wrapping around
            x = MathUtil.Mod(x, width);
            y = MathUtil.Mod(y, height);

            return tiles[x, y];
        }

        public string GetTilePath(int i)
        {
            string tileName = i switch
            {
                (int)TileSprite.BgGrayStone => "bg_tile",
                (int)TileSprite.FgDarkBlue => "tile1",
                _ => throw new NotSupportedException(String.Format("Unsupported tile sprite '{0}'", i)),
            };

            return String.Format("Tiles/{0}", tileName);
        }
    }
}