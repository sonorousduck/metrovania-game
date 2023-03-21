﻿using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CrowEngine
{
    public static class GenericTileCollider
    {
        public static GameObject Create(Vector2 position, Vector2 size)
        {
            GameObject gameObject = new();

            gameObject.Add(new Transform(position, 0, Vector2.One));
            gameObject.Add(new RectangleCollider(size, true));
            gameObject.Add(new Rigidbody());
            gameObject.Add(new RenderedComponent());
            gameObject.Add(new Tile());

            return gameObject;

        }
    }
}
