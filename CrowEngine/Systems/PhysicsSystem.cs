using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CrowEngine
{
    public class PhysicsSystem : System
    {
        public static int PHYSICS_DIMENSION_WIDTH = 2000;
        public static int PHYSICS_DIMENSION_HEIGHT = 2000; // These should match up with rendering
        public static float GRAVITY = -9.81f;

        private Quadtree quadtree;
        private Quadtree staticTree;

        public PhysicsSystem(SystemManager systemManager) : base(systemManager, typeof(Transform), typeof(Rigidbody), typeof(Collider)) 
        { 
            staticTree = new Quadtree(PHYSICS_DIMENSION_WIDTH, PHYSICS_DIMENSION_HEIGHT);
        }


        protected override void Add(GameObject gameObject)
        {
            base.Add(gameObject);

            if (gameObject.ContainsComponentOfParentType<Collider>() && gameObject.GetComponent<Collider>().isStatic)
            {
                staticTree.Insert(gameObject);
            }
        }


        /// <summary>
        /// This section updates all the rigidbody positions, and calls the Collision events from a component's script, if it has one
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime) 
        {
            quadtree = new Quadtree(PHYSICS_DIMENSION_WIDTH, PHYSICS_DIMENSION_HEIGHT);

            // add everything into the quadtree
            foreach ((uint id, GameObject gameObject) in gameObjects)
            {
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();

                if (rb.usesGravity)
                {
                    // Update velocity from gravity
                    rb.velocity += new Vector2(0, GRAVITY * (gameTime.ElapsedGameTime.Milliseconds / 1000f) * rb.gravityScale);

                }
                Transform transform = gameObjects[id].GetComponent<Transform>();

                // Update velocity from accleration as well
                rb.velocity += new Vector2(rb.acceleration.X * gameTime.ElapsedGameTime.Milliseconds / 1000f, rb.acceleration.Y * MathF.Pow(gameTime.ElapsedGameTime.Milliseconds / 1000f, 2f));

                transform.previousPosition = transform.position;
                // Update position from velocity
                transform.position += rb.velocity * (gameTime.ElapsedGameTime.Milliseconds / 1000f);

                quadtree.Insert(gameObject);
            }


            foreach (uint id in gameObjects.Keys)
            {
                Rigidbody rb = gameObjects[id].GetComponent<Rigidbody>(); // No need for null check here, by nature of being in physics engine, there is one

                List<GameObject> possibleCollisions = quadtree.GetPossibleCollisions(gameObjects[id]);
                List<GameObject> possibleStaticCollisions = staticTree.GetPossibleCollisions(gameObjects[id]);
                List<uint> currentCollisions = new List<uint>();
                
                possibleCollisions.AddRange(possibleStaticCollisions);


                foreach (GameObject gameObject in possibleCollisions)
                {
                    if (HasCollision(gameObjects[id], gameObject))
                    {
                        if (!rb.currentlyCollidingWith.Contains(gameObject.id)) // First frame of colliding
                        {
                            if (gameObjects[id].ContainsComponentOfParentType<Script>())
                            {
                                gameObjects[id].GetComponent<Script>().OnCollisionStart(gameObject);
                            }

                        }
                        if (gameObjects[id].ContainsComponentOfParentType<Script>())
                        {
                            gameObjects[id].GetComponent<Script>().OnCollision(gameObject);
                        }
                    }
                    else
                    {
                        if (rb.currentlyCollidingWith.Contains(gameObject.id)) // We used to be colliding with this
                        {
                            if (gameObjects[id].ContainsComponentOfParentType<Script>())
                            {
                                gameObjects[id].GetComponent<Script>().OnCollisionEnd(gameObject);
                            }
                        }
                    }
                }
                rb.currentlyCollidingWith = currentCollisions;
            }
        }

        private bool HasCollision(GameObject one, GameObject two)
        {
            if (one == two)
            {
                return false;
            }
            if (one.ContainsComponent<CircleCollider>())
            {
                if (two.ContainsComponent<CircleCollider>())
                {
                    return CircleOnCircle(one, two);
                }
                else
                {
                    return CircleOnSquare(one, two);
                }
            }
            else
            {
                if (two.ContainsComponent<CircleCollider>())
                {
                    return CircleOnSquare(two, one);
                }
                else
                {
                    return SquareOnSquare(one, two);
                }
            }
        }


        private bool CircleOnCircle(GameObject circle1, GameObject circle2)
        {
            // Squared distance is less than the summed squared radius
            // TODO: IS THIS RIGHT? I DON'T THINK SO
            return (Vector2.DistanceSquared(circle1.GetComponent<Transform>().position + circle1.GetComponent<Collider>().offset, circle2.GetComponent<Transform>().position + circle2.GetComponent<Collider>().offset) < MathF.Pow(circle1.GetComponent<CircleCollider>().radius + circle2.GetComponent<CircleCollider>().radius, 2));
        }

        // Used http://jeffreythompson.org/collision-detection/circle-rect.php
        private bool CircleOnSquare(GameObject circle, GameObject square)
        {


            Transform squareTransform = square.GetComponent<Transform>();
            Transform circleTransform = circle.GetComponent<Transform>();
            RectangleCollider squareCollider = square.GetComponent<RectangleCollider>();

            Vector2 testLocation = circleTransform.position;

            if (circleTransform.position.X < squareTransform.position.X - squareCollider.size.X / 2)
            {
                testLocation.X = squareTransform.position.X - squareCollider.size.X / 2;
            }
            else if (circleTransform.position.X > squareTransform.position.X + squareCollider.size.X / 2)
            {
                testLocation.X = squareTransform.position.X + squareCollider.size.X / 2;
            }

            if (circleTransform.position.Y < squareTransform.position.Y - squareCollider.size.Y / 2)
            {
                testLocation.Y = squareTransform.position.Y - squareCollider.size.Y / 2;
            }
            else if (circleTransform.position.Y > squareTransform.position.Y + squareCollider.size.Y / 2)
            {
                testLocation.Y = squareTransform.position.Y + squareCollider.size.Y / 2;
            }

            float squaredDistance = Vector2.DistanceSquared(circleTransform.position, testLocation);

            return (squaredDistance <= MathF.Pow(circle.GetComponent<CircleCollider>().radius, 2));


        }

        private bool SquareOnSquare(GameObject square1, GameObject square2)
        {


            RectangleCollider square1Collider = square1.GetComponent<RectangleCollider>();
            RectangleCollider square2Collider = square2.GetComponent<RectangleCollider>();

            Transform square1Transform = new Transform(square1.GetComponent<Transform>().position + square1Collider.offset, square1.GetComponent<Transform>().rotation, square1.GetComponent<Transform>().scale);
            Transform square2Transform = new Transform(square2.GetComponent<Transform>().position + square2Collider.offset, square2.GetComponent<Transform>().rotation, square2.GetComponent<Transform>().scale);

            return !(
                    square1Transform.position.X - square1Collider.size.X / 2f > square2Transform.position.X + square2Collider.size.X / 2f || // sq1 left is greater than sq2 right
                    square1Transform.position.X + square1Collider.size.X / 2f < square2Transform.position.X - square2Collider.size.X / 2f || // sq1 right is less than sq2 left
                    square1Transform.position.Y - square1Collider.size.Y / 2f > square2Transform.position.Y + square2Collider.size.Y / 2f || // sq1 top is below sq2 bottom
                    square1Transform.position.Y + square1Collider.size.Y / 2f < square2Transform.position.Y - square2Collider.size.Y / 2f // sq1 bottom is above sq1 top
                    );
        }


    }
}
