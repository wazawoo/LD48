using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public enum BehaviorType
    {
        Follow = 0,
        Friction = 1,
        Gravity = 2,
        Jump = 3,
        Move = 4
    }

    public class Behavior
    {
        public Entity owner;
        public bool enabled;
        public readonly BehaviorType type;

        public Behavior(Entity owner, bool enabled, BehaviorType type)
        {
            this.owner = owner;
            this.enabled = enabled;
            this.type = type;
        }

        public virtual void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            //no base behavior
            //could potentially use an interface instead
            //not sure how properties work with that though
        }
    }
}
