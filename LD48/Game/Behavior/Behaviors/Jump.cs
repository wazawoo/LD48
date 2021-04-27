using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LD48
{
    public class Jump: Behavior
    {
        //jump velocity
        //a jump just increases the velocity instantaneously for now
        public Vector2 velocity;

        public TimeSpan chargeStartTime;
        private JumpState state;

        enum JumpState
        {
            Ready = 0,
            Charging = 1,
            Used = 2
        }

        public Jump(
            Vector2 velocity,
            bool enabled = true)
            : base (BehaviorType.Jump, enabled)
        {
            this.velocity = velocity;
            chargeStartTime = TimeSpan.MinValue;
            state = JumpState.Ready;
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (state)
            {
                case JumpState.Ready:

                    if (keyboardState.IsKeyDown(Keys.Space))
                    {
                        if (owner.tileStandingOn != null)
                        {
                            //if not in the middle of jumping:
                            //start charging jump, record the time
                            //only allow charging while on the ground
                            chargeStartTime = gameTime.TotalGameTime;

                            state = JumpState.Charging;
                        }
                    }
                    break;

                case JumpState.Charging:

                    //I NEED A WAY TO ADJUST THESE CONSTANTS LIVE
                    var chargingTime = (float)(gameTime.TotalGameTime - chargeStartTime).TotalSeconds;
                    var maxChargeDuration = 0.105f;

                    if (keyboardState.IsKeyUp(Keys.Space) || chargingTime >= maxChargeDuration)
                    {
                        //if lift key or exceed maximum charge duration, jump
                        float jumpCharge = 15 * Math.Clamp(chargingTime, 0.07f, maxChargeDuration);

                        state = JumpState.Used;
                        owner.velocity +=  velocity * jumpCharge * dt;
                    }
                    break;

                case JumpState.Used:
                    //how do we get back to idle?
                    //we have the key lifted
                    if (keyboardState.IsKeyUp(Keys.Space))
                    {
                        state = JumpState.Ready;
                    }
                    break;
            }

            base.Update(gameTime, keyboardState);
        }
    }
}
