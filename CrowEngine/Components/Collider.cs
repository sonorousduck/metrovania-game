using System;
using Microsoft.Xna.Framework;
namespace CrowEngine
{
    /// <summary>
    /// Abstract class that colliders should extend in order to be recognized by the physics system
    /// </summary>
    public abstract class Collider : Component
    {
        public bool isStatic;
        public Vector2 offset;

    }
}
