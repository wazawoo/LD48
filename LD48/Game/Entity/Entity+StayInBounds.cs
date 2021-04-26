using System;
using Microsoft.Xna.Framework;

namespace LD48
{
    public partial class Entity
    {
        void StayInBounds(Vector2 gameScreenSize)
        {
            var width = gameScreenSize.X;
            var height = gameScreenSize.Y;

            //allow continuing to the other side
            if (position.X > width)
            {
                //wrap to left side
                var dx = position.X - width;
                position.X = dx;
            }
            else if (position.X < 0)
            {
                //wrap to right side
                var dx = -position.X;
                position.X = width - dx;
            }

            if (position.Y > height)
            {
                //fall through
                var dy = position.Y - height;
                position.Y = dy;
            }
            else if (position.Y < 0)
            {
                //jump through the top
                var dy = -position.Y;
                position.Y = height - dy;
            }
        }
    }
}
