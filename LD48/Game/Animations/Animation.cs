using System;
using Microsoft.Xna.Framework.Graphics;

namespace LD48
{
    public class Animation
    {
        Texture2D texture;
        float frameTime;
        bool isLooping;

        public Texture2D Texture
        {
            get { return texture; }
        }

        public float FrameTime
        {
            get { return frameTime;  }
        }

        public bool IsLooping
        {
            get { return isLooping; }
        }

        // FrameCount is currently configured to be
        // calculated by width and height.
        public int FrameCount
        {
            get { return Texture.Width / FrameHeight; }
        }

        public int FrameWidth
        {
            get { return Texture.Height; }
        }

        public int FrameHeight
        {
            get { return Texture.Height; }
        }

        // PARAM@texture:   Entire sprite sheet
        // PARAM@frameTime: Time inbetween frames
        // PARAM@isLooping: Continue to loop through sheet.. or nah
        public Animation(Texture2D texture, float frameTime, bool isLooping)
        {
            this.texture = texture;
            this.frameTime = frameTime;
            this.isLooping = isLooping;
        }
    }
}
