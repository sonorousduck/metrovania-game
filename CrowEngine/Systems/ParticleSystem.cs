using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;


namespace CrowEngine
{
    public class ParticleSystem : System
    {
        public ParticleSystem(SystemManager systemManager) : base(systemManager, typeof(ParticleGroup), typeof(Transform))
        {
        }

        protected override void Update(GameTime gameTime)
        {

            // Update each particle group
            foreach (uint id in gameObjects.Keys)
            {
                ParticleGroup particleGroup = gameObjects[id].GetComponent<ParticleGroup>();

                if (particleGroup.maxSystemLifeSpan != null)
                {
                    // Update each particle of group
                    for (int i = 0; i < particleGroup.particles.Count; i++)
                    {
                        // Update time on particle
                        particleGroup.particles[i].lifeSpan -= gameTime.ElapsedGameTime;

                        // Remove it if it has expired
                        if (particleGroup.particles[i].lifeSpan <= TimeSpan.Zero)
                        {
                            particleGroup.particles.RemoveAt(i);
                        }
                        else
                        {
                            particleGroup.particles[i].position += (gameTime.ElapsedGameTime.Milliseconds / 1000f) * particleGroup.particles[i].velocity;
                            particleGroup.particles[i].rotation += (gameTime.ElapsedGameTime.Milliseconds / 1000f) * particleGroup.rotationSpeed;
                        }
                    }

                    if (particleGroup.maxSystemLifeSpan > TimeSpan.Zero)
                    {
                        particleGroup.maxSystemLifeSpan -= gameTime.ElapsedGameTime;


                        // Create new particles
                        particleGroup.currentTime += gameTime.ElapsedGameTime;

                        // number | time
                        // ------ | 
                        // second |
                        Transform particleTransform = gameObjects[id].GetComponent<Transform>();

                        while (particleGroup.currentTime > particleGroup.rate)
                        {
                            particleGroup.currentTime -= particleGroup.rate;

                            if (particleGroup.isEmitting)
                            {
                                Particle particle = new Particle
                                {
                                    lifeSpan = particleGroup.maxLifeSpan,
                                    position = new Vector2((float)particleGroup.random.NextGaussian(particleTransform.position.X, particleGroup.emissionArea.X), (float)particleGroup.random.NextGaussian(particleTransform.position.Y, particleGroup.emissionArea.Y)),
                                    rotation = particleGroup.random.NextRange(0, 360),
                                    scale = particleGroup.random.NextRange(particleGroup.minScale, particleGroup.maxScale)
                                };

                                float emittedRotation = particleTransform.rotation; // start with the transform's rotation

                                // Next we need to modify the rotation using the bounds

                                float rotationModification = particleGroup.random.NextRange(particleGroup.emissionArc.X, particleGroup.emissionArc.Y);

                                emittedRotation += (MathF.PI / 180) * rotationModification;

                                Vector2 particleDirection = new Vector2(MathF.Cos(emittedRotation), MathF.Sin(emittedRotation));

                                particle.velocity = particleDirection * particleGroup.random.NextRange(particleGroup.minSpeed, particleGroup.maxSpeed);

                                particleGroup.particles.Insert(0, particle);

                            }
                        }
                    }
                }
               /* else
                {
                    // Create new particles
                    particleGroup.currentTime += gameTime.ElapsedGameTime;

                    // number | time
                    // ------ | 
                    // second |
                    Transform particleTransform = gameObjects[id].GetComponent<Transform>();

                    while (particleGroup.currentTime > particleGroup.rate)
                    {
                        particleGroup.currentTime -= particleGroup.rate;

                        if (particleGroup.isEmitting)
                        {
                            Particle particle = new Particle
                            {
                                lifeSpan = particleGroup.maxLifeSpan,
                                position = new Vector2((float)particleGroup.random.NextGaussian(particleTransform.position.X, particleGroup.emissionArea.X), (float)particleGroup.random.NextGaussian(particleTransform.position.Y, particleGroup.emissionArea.Y)),
                                rotation = particleGroup.random.NextRange(0, 360),
                                scale = particleGroup.random.NextRange(particleGroup.minScale, particleGroup.maxScale)
                            };

                            float emittedRotation = particleTransform.rotation; // start with the transform's rotation

                            // Next we need to modify the rotation using the bounds

                            float rotationModification = particleGroup.random.NextRange(particleGroup.emissionArc.X, particleGroup.emissionArc.Y);

                            emittedRotation += (MathF.PI / 180) * rotationModification;

                            Vector2 particleDirection = new Vector2(MathF.Cos(emittedRotation), MathF.Sin(emittedRotation));

                            particle.velocity = particleDirection * particleGroup.random.NextRange(particleGroup.minSpeed, particleGroup.maxSpeed);

                            particleGroup.particles.Insert(0, particle);
                        }
                    }
                }*/
            }
        }
   }
}
