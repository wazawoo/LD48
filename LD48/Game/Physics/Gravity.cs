using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Gravity
    {
        public Vector2 value;
        public bool isActive;

        public Gravity(Vector2 value, bool isActive)
        {
            this.value = value;
            this.isActive = isActive;
        }
    }
}
