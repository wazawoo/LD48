using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Diagnostics;

namespace LD48
{
    public class JumpPhysics
    {
        public bool isCharging;
        public TimeSpan chargeStartTime;

        //jump velocity
        //a jump just increases the velocity instantaneously for now
        public Vector2 velocity;

        public bool isActive;
        public bool isFalling;

        private int height;
        private int maxJumpHeight;
        private int heightIncrement;

        public JumpPhysics(Vector2 velocity)
        {
            isCharging = false;
            chargeStartTime = TimeSpan.MinValue;
            this.velocity = velocity;

            isActive = false;
            isFalling = false;
            height = 0;
            maxJumpHeight = 50;
            heightIncrement = 6;
        }

        public Vector2 ActualVelocity(GameTime gameTime)
        {
            //change power of jump based on how long it has been charging
            double dt_seconds = (gameTime.TotalGameTime - chargeStartTime).TotalMilliseconds / 1000f;

            float jumpCharge = Math.Clamp(13f * (float) dt_seconds, 2f, 3f);
            return velocity * jumpCharge;
        }

        //public void Stop()
        //{
        //    height = 0;
        //    isActive = false;
        //}

        //public int GetJumpY()
        //{
        //    if (height < maxJumpHeight * jumpCharge)
        //    {

        //        //this is an interesting curve
        //        //how hard would it be to do constant accelleration

        //        if (height > maxJumpHeight / 2)
        //        {
        //            heightIncrement = 2 * jumpCharge;
        //        } else if (height > maxJumpHeight / 5)
        //        {
        //            heightIncrement = 4 * jumpCharge;
        //        } else
        //        {
        //            heightIncrement = 6 * jumpCharge;
        //        }

        //        height += heightIncrement;
        //        return heightIncrement;
        //    }
        //    else
        //    {
        //        Stop();
        //        isFalling = true;
        //        return 0;
        //    }
        //}
    }
}