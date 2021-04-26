using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public partial class Entity
    {
        public Texture2D texture;

        //size determines bounding box
        //this could differ from the texture size
        //for now they are the same
        public Vector2 size;

        //these represent the current state
        public Vector2 position;
        public Vector2 velocity;
        public Tile tileStandingOn;

        public List<Behavior> behaviors = new List<Behavior>();

        public Entity(
            Vector2 position,
            Vector2 size)
        {
            this.size = size;
            this.position = position;
            velocity = new Vector2(0, 0);

            //this may not always be true though...
            tileStandingOn = null;
        }

        public virtual void Update(
            GameTime gameTime,
            Vector2 gameScreenSize,
            KeyboardState keyboardState,
            TileSet tileSet)
        {
            //order matters for these
            //what is the precedence for behaviors?
            //input should happen first, right?
            //i dont want the order of these to be inconsistent between entities
            //they can have a precedence thing
            //but really, which ones are dependent on each other?

            foreach (Behavior behavior in behaviors)
            {
                if (behavior.enabled)
                {
                    behavior.Update(gameTime, keyboardState);
                }
            }

            ClampVelocity();

            MoveAndCollide(tileSet);

            StayInBounds(gameScreenSize);
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
                //this is a ~hack~

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
