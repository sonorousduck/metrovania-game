using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using TiledCS;
using System.Linq;
using ninja;

namespace CrowEngine
{
    class TestScreen : Screen
    {

        private RenderingSystem renderingSystem;
        private AnimationSystem animationSystem;
        private PhysicsSystem physicsEngine;
        private ParticleRenderingSystem particleRenderer;
        private ParticleSystem particleSystem;
        private InputSystem inputSystem;
        private ScriptSystem scriptSystem;
        private LightRenderingSystem lightRenderer;
        RenderTarget2D mainRenderTarget;
        private GameObject camera;
        private FontRenderingSystem fontRenderingSystem;
        private TileRenderingSystem tileRenderingSystem;

        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        private Texture2D tilesetTexture;

        private RenderTarget2D renderTarget;
        private RenderTarget2D lightRenderTarget;
        private RenderTarget2D tileRenderTarget;
        private RenderTarget2D hudRenderTarget;

        private GameObject player;

        public TestScreen(ScreenEnum screenEnum) : base(screenEnum) { }

        public override void Initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameWindow window)
        {
            base.Initialize(graphicsDevice, graphics, window);

            physicsEngine = new PhysicsSystem(systemManager);
            inputSystem = new InputSystem(systemManager);
            scriptSystem = new ScriptSystem(systemManager);
            particleSystem = new ParticleSystem(systemManager);
            animationSystem = new AnimationSystem(systemManager);

            camera = new GameObject();
            camera.Add(new Transform(Vector2.Zero, 0, Vector2.One));
            camera.Add(new Rigidbody());
            camera.Add(new CircleCollider(1, false));
            mainRenderTarget = new RenderTarget2D(graphicsDevice, 480, 270, false, SurfaceFormat.Color, DepthFormat.None, graphics.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
            tileRenderTarget = new RenderTarget2D(graphicsDevice, 480, 270, false, SurfaceFormat.Color, DepthFormat.None, graphics.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
            hudRenderTarget = new RenderTarget2D(graphicsDevice, 480, 270, false, SurfaceFormat.Color, DepthFormat.None, graphics.GraphicsDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);
            systemManager.Add(camera);
        }

        private void LoadTileMapColliders()
        {
            TiledLayer collisionLayers = map.Layers.First(layer => layer.name == "Ground");

            foreach (TiledObject collider in collisionLayers.objects)
            {
                GameObject tileCollider = GenericTileCollider.Create(new Vector2(collider.x + collider.width / 2, collider.y + collider.height / 2), new Vector2(collider.width, collider.height));
                systemManager.Add(tileCollider);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.SetRenderTarget(tileRenderTarget);
            graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            tileRenderingSystem.Draw(gameTime, spriteBatch, map, tilesets, tilesetTexture);

            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);



            graphics.GraphicsDevice.SetRenderTarget(mainRenderTarget);
            graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            graphics.GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);
            renderingSystem.Draw(gameTime, spriteBatch);
            particleRenderer.Draw(gameTime, spriteBatch);
            fontRenderingSystem.Draw(gameTime, spriteBatch);


            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);

            graphics.GraphicsDevice.SetRenderTarget(hudRenderTarget);
            graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            graphics.GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);



            spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(tileRenderTarget, new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(mainRenderTarget, new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(hudRenderTarget, new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.End();

        }


        public override void LoadContent()
        {
            ResourceManager.RegisterTexture("Images/crow", "crow");
            ResourceManager.RegisterTexture("Images/fire", "fire");
            ResourceManager.RegisterTexture("Images/crow", "DefaultPlayer");

            ResourceManager.RegisterTexture("TileMaps/GenericTilemap", "DefaultMap");

            map = new TiledMap("Content/TileMaps/Ninja.tmx");
            // Loads in the TSX
            tilesets = map.GetTiledTilesets("Content/TileMaps/");

            tilesetTexture = ResourceManager.GetTexture("DefaultMap");

            LoadTileMapColliders();

            renderingSystem = new RenderingSystem(systemManager, window.ClientBounds.Height, camera, new Vector2(window.ClientBounds.Width, window.ClientBounds.Height));
            tileRenderingSystem = new TileRenderingSystem(systemManager, camera, new Vector2(window.ClientBounds.Width, window.ClientBounds.Height));
            particleRenderer = new ParticleRenderingSystem(systemManager, camera);
            fontRenderingSystem = new FontRenderingSystem(systemManager, camera);
            lightRenderer = new LightRenderingSystem(systemManager, camera, graphicsDevice);
            lightRenderer.globalLightLevel = 0f;

        }

        public override void OnScreenDefocus()
        {
            Debug.WriteLine("Default Screen was unloaded");
        }

        public override void OnScreenFocus()
        {
            Debug.WriteLine("Default Screen was loaded");
        }

        /// <summary>
        /// Note, while this one creates gameObjects manually inline, this should really be done in a separate file, in a static class.
        /// The reason this is done this way here, is so that any naming conventions you'd like to have don't conflict
        /// </summary>
        public override void SetupGameObjects()
        {

            player = PlayerPrefab.Create();
            systemManager.Add(player);



            GameObject testParticles = new GameObject();

            ParticleGroup particle = new(ResourceManager.GetTexture("fire"))
            {
                /*                maxSystemLifeSpan = TimeSpan.FromMilliseconds(2000),
                */
                maxSystemLifeSpan = TimeSpan.MaxValue,
                maxSpeed = 100,
                emissionArc = new Vector2(-45, 45),
                rate = TimeSpan.FromMilliseconds(200),
                minScale = 0.5f,
                maxScale = 1,
                emissionArea = Vector2.One * 10,
                maxLifeSpan = TimeSpan.FromMilliseconds(1000)
            };
            /*            particle.maxSystemLifeSpan = TimeSpan.MaxValue;
            *//*            particle.emissionArea = Vector2.One * 10;
                        particle.maxLifeSpan = TimeSpan.FromMilliseconds(1000);*/

            testParticles.Add(particle);
            testParticles.Add(new Transform(Vector2.Zero, 0, Vector2.One));

            systemManager.Add(testParticles);


            GameObject testText = new GameObject();

            testText.Add(new Text("Test Text", ResourceManager.GetFont("default"), Color.White, Color.Black, true));
            testText.Add(new Transform(new Vector2(0, 50), 0, Vector2.One));

            systemManager.Add(testText);




        }
    }
}
