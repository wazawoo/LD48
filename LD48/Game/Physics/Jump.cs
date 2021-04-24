using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace LD48
{
    public class JumpPhysics
    {
        public bool isActive;
        public bool isFalling;

        private int height;
        private int maxJumpHeight;
        private int heightIncrement;

        public JumpPhysics()
        {
            height = 0;
            maxJumpHeight = 90;
            heightIncrement = 10;
            isActive = false;
            isFalling = false;
        }

        public void start()
        {
            isActive = true;
            isFalling = false;
        }

        public void stop()
        {
            height = 0;
            isActive = false;
        }

        public int getJumpY()
        {
            if (height < maxJumpHeight)
            {
                height += heightIncrement;
                return heightIncrement;
            }
            else
            {
                stop();
                isFalling = true;
                return 0;
            }
        }
    }
}