using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Player: Entity
    {
        public JumpPhysics jump;
        public bool isOnGround;

        public Player(
            Vector2 position,
            float speed)
            : base(position, speed)
        {
            isOnGround = false;
            jump = new JumpPhysics();
        }

        public enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, Vector2 gameScreenSize, TileSet tileSet) {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;


            ApplyPhysics(gameTime, tileSet);
            

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                //position.Y -= speed * dt;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                
                //position.Y += speed * dt;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                var collidable = checkIfCollidable(tileSet, position, Direction.Left);
                if (!collidable)
                {
                    position.X -= speed * dt;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                var collidable = checkIfCollidable(tileSet, position, Direction.Right);
                if (!collidable)
                {
                    position.X += speed * dt;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (isOnGround) {
                    var collidable = checkIfCollidable(tileSet, position, Direction.Up);
                    if (!collidable)
                    {
                        position.Y -= jump.getJumpY();
                        jump.start();
                    }
                }
            }

            var width = gameScreenSize.X;
            var height = gameScreenSize.Y;

            // restrict to bounds

            //allow continuing to the other side
            if (position.X > width)
            {
                //amount we are off screen to the right
                var dx = position.X - width;

                //wrap to left side
                position.X = dx;
            }
            else if (position.X < 0)
            {
                //amount we are off screen to the left
                var dx = -position.X;

                //wrap to right side
                position.X = width - dx;
            }

            if (position.Y >= height - texture.Height / 2)
            {
                //bottom
                position.Y = height - texture.Height / 2;
                // right now this will always be true until we add collision
                // checking.
                isOnGround = true;
            }
            else if (position.Y < texture.Height / 2)
            {
                //top
                position.Y = texture.Height / 2;
            } 

        }
        public bool checkIfCollidable(TileSet tileSet, Vector2 position, Direction direction)
        {

            var allTiles = tileSet.tiles;

            // Get players position
            var playerPositionX = position.X;
            var playerPositionY = position.Y;

            // Get coordinate of next direction
            if (direction == Direction.Up)      { playerPositionY = position.Y - 5;}
            if (direction == Direction.Down)    { playerPositionY = position.Y + 5; }
            if (direction == Direction.Left)    { playerPositionX = position.X - 5; }
            if (direction == Direction.Right)   { playerPositionX = position.X + 5; }

            // Get the tile type of the next coordinate
            var x = (playerPositionX);
            var y = (playerPositionY);
            var type = tileSet.getType(x, y);

            if (type == TileType.Ground)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public void ApplyPhysics(GameTime gameTime, TileSet tileSet) {
            // TODO: Make better
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var collidable = checkIfCollidable(tileSet, position, Direction.Down);
            if (!collidable) {
                position.Y += 1;
            } else
            {
                isOnGround = true;
            }
        }
    }
}
