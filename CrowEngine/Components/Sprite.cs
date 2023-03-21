using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngine
{
    public class Sprite : RenderedComponent
    {
        public Texture2D sprite;
        public Color color;
        public Vector2 center;
        public float renderDepth;

        /// <summary>
        /// Creates a Sprite
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="color"></param>
        /// <param name="center"></param>
        /// <param name="renderDepth"></param>
        /// <param name="isHUD"></param>
        public Sprite(Texture2D sprite, Color color, Vector2 center, float renderDepth=0, bool isHUD=false)
        {
            this.sprite = sprite;
            this.color = color;
            this.center = center;
            this.renderDepth = renderDepth;
            this.IsHUD= isHUD;
        }

        /// <summary>
        /// Computes the center automatically for you if you use this based on the sprite width and height
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="color"></param>
        /// <param name="renderDepth"></param>
        /// <param name="isHUD"></param>
        public Sprite(Texture2D sprite, Color color, float renderDepth=0, bool isHUD=false) : this(sprite, color, new Vector2((float)sprite.Width / 2, (float)sprite.Height / 2), renderDepth, isHUD)
        {
        }
    }
}
