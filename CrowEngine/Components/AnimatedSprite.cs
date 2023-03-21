using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CrowEngine
{
    public class AnimatedSprite : RenderedComponent
    {
        // The texture from which to take the frame data
        public Texture2D spriteSheet { get; set; }
        public int currentFrame { get; set; }
        public int[] frameTiming { get; set; }
        public TimeSpan currentTime { get; set; }
        public Vector2 singleFrameSize { get; set; }
        public int layerDepth { get; set; }
        public int startFrame { get; set; }
        public int endFrame { get; set; }
        public bool playOnce { get; set; }
        public bool completedPlay { get; set; }
        public Dictionary<int, string> callbacks { get; set; }
        public Queue<string> callbackToRun { get; set; }


        public AnimatedSprite(Texture2D spriteSheet, int[] frameTiming, Vector2 singleFrameSize, int layerDepth = 0, bool isHUD = false, int startFrame = 0, int endFrame = 0, bool playOnce = false, Dictionary<int, string> callbacks=null)
        {
            this.spriteSheet = spriteSheet;
            this.currentFrame = startFrame;
            this.frameTiming = frameTiming;
            this.currentTime = new TimeSpan();
            this.singleFrameSize = singleFrameSize;
            this.layerDepth = layerDepth;
            this.IsHUD = isHUD;
            this.startFrame = startFrame;
            this.endFrame = endFrame;
            this.playOnce = playOnce;
            this.completedPlay = false;
            this.callbacks = callbacks;
            this.callbackToRun = new Queue<string>();
        }
    }
}
