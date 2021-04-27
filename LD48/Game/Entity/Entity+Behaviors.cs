using System;
using Microsoft.Xna.Framework;

namespace LD48
{
    public partial class Entity
    {

        //this will return null if this entity does not have this behavior
        public Behavior GetBehavior(BehaviorType type)
        {
            foreach(Behavior behavior in behaviors)
            {
                if (behavior.type == type)
                {
                    return behavior;
                }
            }

            return null;
        }

        public void AddBehavior(Behavior behavior)
        {
            //need to assign owner to the behavior, or it will not work
            //for this reason, behaviors do not have owner in their init
            //this means it is not safe to add a behavior other than through this method
            //this is not ideal, so maybe there's a better way

            //for now, not allowing multiple behaviors of the same type
            if (GetBehavior(behavior.type) == null)
            {
                behavior.owner = this;
                behaviors.Add(behavior);
            } else
            {
                throw new NotSupportedException(String.Format("Attempted to add duplicate behavior {0}", behavior.type));
            }
        }
    }
}
