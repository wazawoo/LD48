using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Behavior
    {
        public Entity owner { get; set; }
        public bool enabled { get; set; }
        public String identifier {get; }
        public Behavior(Entity owner, bool enabled, String identifier)
        {
            this.owner = owner;
            this.enabled = enabled;
            this.identifier = identifier;
        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            //no base behavior
            //could potentially use an interface instead
            //not sure how properties work with that though
        }
    }
}
