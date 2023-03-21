using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace CrowEngine
{
    public class BasicTestScript : Script
    {
        private Transform transform;
        private Rigidbody rb;

        public BasicTestScript(GameObject owner) : base(owner)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void OnCollision(GameObject other)
        {
            if (other.ContainsComponent<Tile>())
            {
                gameObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().previousPosition;
                Debug.WriteLine("Colliding");
            }
        }


        public override void Start()
        {
            transform = gameObject.GetComponent<Transform>();
            rb = gameObject.GetComponent<Rigidbody>();
        }

    }
}
