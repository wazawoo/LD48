using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Dog : Entity
    {

        public Dog (
            Vector2 position,
            float speed)
            : base(position, speed)
        {
//
        }

        public void Update(GameTime gameTime, Player owner) {
            if (position.X < (owner.position.X - 10)) {
                position.X += (float).1;
            } else if (position.X > owner.position.X + 10)
            {
                position.X -= (float).1;
            }
        }    
    }
}

