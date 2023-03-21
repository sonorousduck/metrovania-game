using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngine
{
    public class ParticleRenderingSystem : System
    {
        private GameObject camera;

        public ParticleRenderingSystem(SystemManager systemManager, GameObject camera) : base(systemManager, typeof(ParticleGroup))
        {
            this.camera = camera;
            systemManager.UpdateSystem -= Update;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (uint id in gameObjects.Keys)
            {

                ParticleGroup particleGroup = gameObjects[id].GetComponent<ParticleGroup>();
                

                foreach (Particle particle in particleGroup.particles)
                {

                    Vector2 distanceFromCenter = particle.position - camera.GetComponent<Transform>().position;
                    Vector2 renderDistanceFromCenter = distanceFromCenter;
                    Vector2 trueRenderPosition = renderDistanceFromCenter;

                    spriteBatch.Draw(particleGroup.texture, trueRenderPosition, null,
                       Color.White, particle.rotation,
                       new Vector2(particleGroup.texture.Width / 2, particleGroup.texture.Height / 2), particle.scale,
                       SpriteEffects.None, particleGroup.renderDepth);
                }
            }
        }
        protected override void Update(GameTime gameTime)
        {
        }
    }
}
