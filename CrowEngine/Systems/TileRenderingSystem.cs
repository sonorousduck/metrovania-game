using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledCS;

namespace CrowEngine
{

    public class TileRenderingSystem : System
    {
        private GameObject camera;

        public static Vector2 centerOfScreen;

        public bool debugMode = true;
        public static int width = 480;
        public static int height = 270;


        public TileRenderingSystem(SystemManager systemManager, GameObject camera, Vector2 screenSize) : base(systemManager, typeof(Transform), typeof(RenderedComponent))
        {
            this.camera = camera;
            centerOfScreen = screenSize / 2;
            systemManager.UpdateSystem -= Update;

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, TiledMap map, Dictionary<int, TiledTileset> tilesets, Texture2D tilesetTexture)
        {
            IEnumerable<TiledLayer> tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);

            foreach (TiledLayer layer in tileLayers)
            {
                for (var y = 0; y < layer.height; y++)
                {
                    for (var x = 0; x < layer.width; x++)
                    {
                        var index = (y * layer.width) + x;
                        var gid = layer.data[index];
                        var tileX = x * map.TileWidth - camera.GetComponent<Transform>().position.X;
                        var tileY = y * map.TileHeight - camera.GetComponent<Transform>().position.Y;

                        if (gid == 0)
                        {
                            continue;
                        }


                        // Helper method to fetch the right TieldMapTileset instance
                        // This is a connection object Tiled uses for linking the correct tileset to the gid value using the firstgid property
                        var mapTileset = map.GetTiledMapTileset(gid);

                        // Retrieve the actual tileset based on the firstgid property of the connection object we retrieved just now
                        var tileset = tilesets[mapTileset.firstgid];

                        var rect = map.GetSourceRect(mapTileset, tileset, gid);

                        // Create destination and source rectangles
                        var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);
                        var destination = new Rectangle((int)tileX, (int)tileY, map.TileWidth, map.TileHeight);

                        SpriteEffects effects = SpriteEffects.None;
                        float rotation = 0f;

                        spriteBatch.Draw(tilesetTexture, destination, source, Color.White, rotation, Vector2.Zero, effects, 0);
                    }
                }
            }

        }
        protected override void Update(GameTime gameTime)
        {
        }

    }

}
