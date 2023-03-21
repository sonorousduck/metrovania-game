using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace CrowEngine
{
    public class SystemManager
    {
        public event Action<GameObject> AddGameObject;
        public event Action<uint> RemoveGameObject;
        public event Action<GameTime> UpdateSystem;
        private Queue<GameObject> safeToAddObjects = new Queue<GameObject>();
        private Queue<uint> safeToRemoveObjects = new Queue<uint>();

        public Dictionary<uint, GameObject> gameObjectsDictionary = new Dictionary<uint, GameObject>();

        /// <summary>
        /// Adds a new gameobject to all systems. This is the old delayed add. Reference CrowEngineBase if you need to 
        /// add in the old form of adding
        /// </summary>
        /// <param name="gameObject"></param>
        public void Add(GameObject gameObject)
        {
            safeToAddObjects.Enqueue(gameObject);
        }
        
        /// <summary>
        /// Delay removes a gameobject at the END of an update frame.
        /// </summary>
        public void Remove(GameObject gameObject)
        {
            safeToRemoveObjects.Enqueue(gameObject.id);
        }

        public void Update(GameTime gameTime)
        {
            UpdateSystem?.Invoke(gameTime);

            while (safeToAddObjects.Count > 0)
            {
                AddObject(safeToAddObjects.Dequeue());
            }
            while (safeToRemoveObjects.Count > 0)
            {
                uint idToRemove = safeToRemoveObjects.Dequeue();
                gameObjectsDictionary.Remove(idToRemove);
                RemoveObject(idToRemove);
            }
        }

        private void AddObject(GameObject gameObject)
        {
            gameObjectsDictionary.Add(gameObject.id, gameObject);
            AddGameObject?.Invoke(gameObject);
        }

        private void RemoveObject(uint id)
        {
            RemoveGameObject?.Invoke(id);
            gameObjectsDictionary.Remove(id);
        }

    }
}
