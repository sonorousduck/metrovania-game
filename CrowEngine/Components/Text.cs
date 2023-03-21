using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngine
{
    public class Text : Component
    {
        public string text;
        public Color color;
        public Color outlineColor;
        public Vector2 centerOfRotation;
        public SpriteFont spriteFont;
        public SpriteEffects spriteEffect;
        public float layerDepth;
        public bool renderOutline;
        public bool isHUDElement; // If this doesn't make sense down the line, this was originally called usesCameraPosition
    
    
        public Text(string text, SpriteFont spriteFont, Color color, Color outlineColor, bool renderOutline=false, float layerDepth=0f, bool isHUDElement=false)
        {
            this.text = text;
            this.spriteFont = spriteFont;
            this.color = color;
            this.outlineColor = outlineColor;
            this.renderOutline = renderOutline;
            this.layerDepth = layerDepth;
            this.isHUDElement = isHUDElement;
        }

    }
}
