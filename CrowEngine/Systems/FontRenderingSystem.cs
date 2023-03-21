using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrowEngine
{
    public class FontRenderingSystem : System
    {
        private GameObject camera;

        public FontRenderingSystem(SystemManager systemManager, GameObject camera) : base(systemManager, typeof(Text), typeof(Transform))
        {

            systemManager.UpdateSystem -= Update;
            this.camera = camera;
        }

        private void DrawBackground(Text text, Transform transform, Vector2 trueRenderPosition, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(text.spriteFont, text.text, new Vector2(trueRenderPosition.X + 1, trueRenderPosition.Y), text.outlineColor, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth + 1);
            spriteBatch.DrawString(text.spriteFont, text.text, new Vector2(trueRenderPosition.X - 1, trueRenderPosition.Y), text.outlineColor, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth + 1);
            spriteBatch.DrawString(text.spriteFont, text.text, new Vector2(trueRenderPosition.X, trueRenderPosition.Y + 1), text.outlineColor, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth + 1);
            spriteBatch.DrawString(text.spriteFont, text.text, new Vector2(trueRenderPosition.X, trueRenderPosition.Y - 1), text.outlineColor, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth + 1);
        }

        protected override void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (uint id in gameObjects.Keys)
            {
                Text text = gameObjects[id].GetComponent<Text>();
                Transform transform = gameObjects[id].GetComponent<Transform>();

                Vector2 distanceFromCenter;
                if (!text.isHUDElement)
                {
                    distanceFromCenter = gameObjects[id].GetComponent<Transform>().position - camera.GetComponent<Transform>().position;
                }
                else
                {
                    distanceFromCenter = gameObjects[id].GetComponent<Transform>().position - new Vector2(PhysicsSystem.PHYSICS_DIMENSION_WIDTH, PhysicsSystem.PHYSICS_DIMENSION_HEIGHT) / 2f;
                }

                Vector2 renderDistanceFromCenter = distanceFromCenter;
                Vector2 trueRenderPosition = renderDistanceFromCenter;


                if (text.renderOutline) DrawBackground(text, transform, trueRenderPosition, spriteBatch);
                spriteBatch.DrawString(text.spriteFont, text.text, trueRenderPosition, text.color, transform.rotation, text.centerOfRotation, transform.scale, text.spriteEffect, text.layerDepth);

            }
        }
    }
}
