using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LD48
{
    public class Player: Entity
    {
        private AnimationPlayer sprite;
        private Animation test;
        public Player(
            Vector2 position,
            Vector2 size)
            : base(position, size)
        {

        }
        public void LoadContent(ContentManager content)
        {
            test = new Animation(content.Load<Texture2D>("player-standing-forward-sheet"), 0.5f, true);
        }

        public override void Update(GameTime gameTime, Vector2 gameScreenSize, KeyboardState keyboardState, TileSet tileSet)
        {
            //nothing special about the player for now!

            base.Update(gameTime, gameScreenSize, keyboardState, tileSet);
        }

        public void drawAnimation(GameTime gameTime, SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            sprite.PlayAnimation(test);
            sprite.Draw(gameTime, spriteBatch, position, spriteEffects);
        }
    }
}
