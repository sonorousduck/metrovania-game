using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace CrowEngine
{
    public class Rigidbody : Component
    {
        public float mass;
        public float gravityScale;
        public Vector2 velocity;
        public Vector2 acceleration;
        public bool usesGravity;

        /// <summary>
        /// List of ids which this rigidbody is colliding with
        /// </summary>
        public List<uint> currentlyCollidingWith;

        // Convenience calls. Calls to the last function in this file
        public Rigidbody(float mass = 0, float gravityScale = 0) : this(new Vector2(0, 0), new Vector2(0, 0), mass, gravityScale)
        {
        }

        // Convenience calls. Calls to the last function in this file
        public Rigidbody(Vector2 startVelocity, float mass = 0, float gravityScale = 0) : this(new Vector2(0, 0), startVelocity, mass, gravityScale)
        {
        }

        public Rigidbody(Vector2 acceleration, Vector2 startVelocity, float mass = 0, float gravityScale = 0, bool usesGravity = false)
        {
            this.mass = mass;
            this.gravityScale = gravityScale;
            this.velocity = startVelocity;
            this.currentlyCollidingWith = new List<uint>();
            this.acceleration = acceleration;
            this.usesGravity = usesGravity;
        }
    }
}
