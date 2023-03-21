using System;
using System.Collections.Generic;
using CrowEngine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngine
{
    /// <summary>
    /// Represents a single particle. Position is set using current transform location when created and then updated on its own after that
    /// </summary>
    public class Particle
    {
        public Vector2 position;
        public Vector2 velocity;
        public TimeSpan lifeSpan;
        public float rotation;
        public float scale;

        public Particle()
        {
            this.position = new Vector2();
            this.velocity = new Vector2();
            this.lifeSpan = new TimeSpan();
            this.scale = 1;
            this.rotation = 0;
        }
    }


    public class ParticleGroup : Component
    {
        /// <summary>
        /// List of Current Particles
        /// </summary>
        public List<Particle> particles;

        /// <summary>
        /// The texture for this group of particles
        /// </summary>
        public Texture2D texture;

        /// <summary>
        /// This is the possible bounds which a particle could spawn. It is relative to the CENTER of the particle
        /// component. If you want it all on one point emmision, this should be Vector2.Zero. For a square area, Vector2.One * size you want
        /// The center of the group is based on the transform of the gameObject is it attached to (you NEED a transform for this to render)
        /// </summary>
        public Vector2 emissionArea;
        /// <summary>
        /// The arc is defined in degrees with 0 being aligned with the gameobject's heading
        /// Defaults to 0, 360, which was the old behavior
        /// </summary>
        public Vector2 emissionArc;

        /// <summary>
        /// The lifetime of this particle group. Once this timer runs out, the group no longer spawns any new particles,
        /// and then will be removed from the system by the 
        /// </summary>
        public TimeSpan maxLifeSpan;
        /// <summary>
        /// TimeSpan between spawning a particle
        /// </summary>
        public TimeSpan rate;
        /// <summary>
        /// The relative current time of the group, used for spawn rate and removal
        /// </summary>
        public TimeSpan currentTime;
        /// <summary>
        /// The overall time that the system will emit particles. Leave null for unlimited time
        /// </summary>
        public TimeSpan maxSystemLifeSpan;
        
        public bool isEmitting;
        
        public float minScale;
        public float maxScale;
        public float minSpeed;
        public float maxSpeed;

        /// <summary>
        /// The speed which each particle should rotate
        /// </summary>
        public float rotationSpeed;
        public CrowRandom random;
       
        public int renderDepth;

        public ParticleGroup(Texture2D particleTexture)
        {
            this.texture = particleTexture;
            particles = new List<Particle>();
            currentTime = new TimeSpan();
            maxLifeSpan = new TimeSpan();
            rate = new TimeSpan();
            emissionArea = new Vector2();
            minScale = 1;
            maxScale = 2;
            minSpeed = 1;
            maxSpeed = 100;
            random = new CrowRandom();
            emissionArc = new Vector2(0, 360);
            isEmitting = true;
            renderDepth = 0;
        }



    }
}
