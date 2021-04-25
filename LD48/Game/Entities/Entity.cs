using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Entity
    {
        public Texture2D texture;

        //size determines bounding box
        //this could differ from the texture size
        //for now they are the same
        public Vector2 size;

        //acceleration due to movement controls
        public float movementAcceleration;

        //these represent the current state
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 acceleration;
        public Tile tileStandingOn;

        //these are optional behaviors/modifiers
        //they may be null
        public Gravity gravity;

        public Entity(
            Vector2 position,
            Vector2 size,
            float movementAcceleration,
            Gravity gravity)
        {
            this.size = size;
            this.movementAcceleration = movementAcceleration;
            this.gravity = gravity;

            this.position = position;
            velocity = new Vector2(0, 0);
            acceleration = new Vector2(0, 0);
            tileStandingOn = null;
        }

        public virtual void Update(GameTime gameTime, Vector2 gameScreenSize, KeyboardState keyboardState, TileSet tileSet)
        {
            ApplyPhysics(gameTime, tileSet);

            MoveAndCollide(tileSet);

            StayInBounds(gameScreenSize);
        }

        void ApplyPhysics(GameTime gameTime, TileSet tileSet)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //apply gravity
            if (gravity != null && gravity.isActive)
            {
                //only apply vertical component of gravity for now
                acceleration.Y = gravity.value.Y;
            }

            //im not sure if this is in the right place
            velocity += acceleration;

            //apply friction if entity is standing on a tile
            //move opposite the velocity
            if (tileStandingOn != null)
            {
                velocity.X -= Math.Sign(velocity.X) * tileStandingOn.Friction() * dt;
            }

            //limit maximum vertical speed
            //(max jump speed, and terminal velocity for falling)
            velocity.Y = Math.Clamp(velocity.Y, -500, 5);

            //limit maximum horizontal speed
            velocity.X = Math.Clamp(velocity.X, -4, 4);

            //these could depend on the entity or the environment
        }

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

        void StayInBounds(Vector2 gameScreenSize)
        {
            var width = gameScreenSize.X;
            var height = gameScreenSize.Y;

            //allow continuing to the other side
            if (position.X > width)
            {
                //wrap to left side
                var dx = position.X - width;
                position.X = dx;
            }
            else if (position.X < 0)
            {
                //wrap to right side
                var dx = -position.X;
                position.X = width - dx;
            }

            if (position.Y > height)
            {
                //fall through
                var dy = position.Y - height;
                position.Y = dy;
            }
            else if (position.Y < 0)
            {
                //jump through the top
                var dy = -position.Y;
                position.Y = height - dy;
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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 gameScreenSize)
        {
            if (texture != null) {
                var origin = new Vector2(0, 0);

                spriteBatch.Draw(
                    texture,
                    position,
                    null,
                    Color.White,
                    0f,
                    origin,
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );

                //drawing extra copies so transitions appear seamless

                //draw another copy one width to the right
                spriteBatch.Draw(
                    texture,
                    new Vector2(position.X + gameScreenSize.X, position.Y),
                    null,
                    Color.White,
                    0f,
                    origin,
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );

                //draw another copy one width to the left
                spriteBatch.Draw(
                    texture,
                    new Vector2(position.X - gameScreenSize.X, position.Y),
                    null,
                    Color.White,
                    0f,
                    origin,
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );

                //draw another copy one height down
                spriteBatch.Draw(
                    texture,
                    new Vector2(position.X, position.Y + gameScreenSize.Y),
                    null,
                    Color.White,
                    0f,
                    origin,
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );

                //draw another copy one height up
                spriteBatch.Draw(
                    texture,
                    new Vector2(position.X, position.Y - gameScreenSize.Y),
                    null,
                    Color.White,
                    0f,
                    origin,
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );

            }
        }
    }
}
