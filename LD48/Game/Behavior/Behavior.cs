using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Behavior
    {
        public Entity owner { get; set; }
        public bool enabled { get; set; }

        public Behavior(Entity owner, bool enabled)
        {
            this.owner = owner;
            this.enabled = enabled;
        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            //no base behavior
            //could potentially use an interface instead
            //not sure how properties work with that though
        }
    }
}
