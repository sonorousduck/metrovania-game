using System;
using Microsoft.Xna.Framework;

namespace CrowEngine
{
    public class RectangleCollider : Collider
    {
        // Centered at object's position
        public Vector2 size;

        public RectangleCollider(Vector2 size, bool isStatic, float xOffset=0, float yOffset=0)
        {
            offset = new Vector2(xOffset, yOffset);
            this.size = size;
            this.isStatic = isStatic;
        }
    }
}
