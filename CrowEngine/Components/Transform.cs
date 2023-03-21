using System;
using Microsoft.Xna.Framework;

namespace CrowEngine
{
    public class Transform : Component
    {
        public Vector2 position;
        public float rotation;
        public Vector2 scale;
        public Vector2 previousPosition;

        public Transform(Vector2 position, float rotation, Vector2 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.previousPosition = position;
        }
    }
}
