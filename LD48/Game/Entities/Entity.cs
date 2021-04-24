using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Entity
    {
        public Texture2D texture;
        public Vector2 position;
        public float speed;

        //init
        public Entity(
            Vector2 position,
            float speed)
        {
            this.position = position;
            this.speed = speed;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (texture != null) {
                spriteBatch.Draw(
                    texture,
                    position,
                    null,
                    Color.White,
                    0f,
                    new Vector2(texture.Width / 2, texture.Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
