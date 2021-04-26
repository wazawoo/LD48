using System;
using Microsoft.Xna.Framework;

namespace LD48
{
    public partial class Entity
    {
        void MoveAndCollide(TileSet tileSet)
        {
            //right now this does not handle all level boundaries

            //set that we are not on the ground initially
            //only say we are on the ground if we collide from the bottom
            tileStandingOn = null;

            //do X movement + collision
            if (velocity.X != 0)
            {
                int move = MathUtil.Round(velocity.X);

                if (move != 0)
                {
                    int sign = Math.Sign(move);

                    while (move != 0)
                    {
                        //check our location, moving one pixel at a time
                        //check the collision one pixel in the x direction
                        var collidedTile = collidesWithTile(tileSet, position + new Vector2(sign, 0));
                        if (collidedTile != null)
                        {
                            //collision occurred
                            //do not move
                            //break out of this loop
                            velocity.X = 0;
                            break;
                        }
                        else
                        {
                            //did not collide
                            //do one pixel of the move
                            position.X += sign;
                            move -= sign;
                        }
                    }
                }
            }

            //then do Y
            if (velocity.Y != 0)
            {
                int move = MathUtil.Round(velocity.Y);

                if (move != 0)
                {
                    int sign = Math.Sign(move);

                    while (move != 0)
                    {
                        var collidedTile = collidesWithTile(tileSet, position + new Vector2(0, sign));

                        if (collidedTile != null)
                        {
                            //collision occurred

                            //we know we are on the ground if the sign of the move is 1 (moving down, collided)
                            //is that true?
                            if (sign == 1)
                            {
                                tileStandingOn = collidedTile;
                            }

                            //do not move
                            //break out of this loop
                            velocity.Y = 0;
                            break;
                        }
                        else
                        {
                            //did not collide
                            //do one pixel of the move
                            position.Y += sign;
                            move -= sign;
                        }
                    }
                }
            }
        }

        public Tile collidesWithTile(TileSet tileSet, Vector2 position)
        {
            for (int i = 0; i <= 1; ++i)
            {
                for (int j = 0; j <= 1; ++j)
                {
                    //check all four corners for collisions
                    //return true if any corner collides with the ground
                    //origin is top left for now
                    var x = (position.X + i * ((int)size.X - 1));
                    var y = (position.Y + j * ((int)size.Y - 1));

                    var tile = tileSet.GetTile(x, y);
                    if (tile.type == TileType.Ground)
                    {
                        return tile;
                    }
                }
            }

            return null;
        }
    }
}
