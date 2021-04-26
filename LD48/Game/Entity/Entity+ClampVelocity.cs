using System;
using Microsoft.Xna.Framework;

namespace LD48
{
    public partial class Entity
    {
        void ClampVelocity()
        {
            //for now, this is the same for all entities
            //these could depend on the entity or the environment

            //limit maximum vertical speed 
            //only limit downward velocity (terminal velocity)
            velocity.Y = Math.Min(velocity.Y, 5);

            //limit maximum horizontal speed    
            velocity.X = Math.Clamp(velocity.X, -2, 2);
        }
    }
}
