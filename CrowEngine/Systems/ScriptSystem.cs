using System;
using Microsoft.Xna.Framework;
using System.Diagnostics;



namespace CrowEngine
{
    public class ScriptSystem : System
    {
        public ScriptSystem(SystemManager systemManager) : base(systemManager, typeof(Script)) 
        { 
            foreach (uint id in gameObjects.Keys)
            {
                gameObjects[id].GetComponent<Script>().Start();
            }
        }

        protected override void Add(GameObject gameObject)
        {
            if (IsInterested(gameObject))
            {
                gameObject.GetComponent<Script>().Start();
            }
            base.Add(gameObject);
        }

        protected override void Remove(uint id)
        {
            if (gameObjects.ContainsKey(id))
            {
                gameObjects[id].GetComponent<Script>().Destroyed();
            }
            base.Remove(id);
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (uint id in gameObjects.Keys)
            {
                CallInputScripts(id);
                gameObjects[id].GetComponent<Script>().Update(gameTime);
            }
        }

        private void CallInputScripts(uint id)
        {

            /*if (gameObjects[id].ContainsComponent<ControllerInput>())
            {
                ControllerInput controllerInput = gameObjects[id].GetComponent<ControllerInput>();
                foreach (string action in controllerInput.actionStatePairs.Keys)
                {
                    if (controllerInput.actionStatePairs[action] != controllerInput.previousActionStatePairs[action])
                    {
                        gameObjects[id].GetComponent<Script>().SendMessage($"On{action}", controllerInput.actionStatePairs[action]);
                    }
                }
            }*/
           /* if (gameObjects[id].ContainsComponent<MouseInput>())
            {
                MouseInput mouseInput = gameObjects[id].GetComponent<MouseInput>();
                foreach (string action in mouseInput.actionStatePairs.Keys)
                {
                    if (mouseInput.actionStatePairs[action] != mouseInput.previousActionStatePairs[action])
                    {
                        gameObjects[id].GetComponent<Script>().SendMessage($"On{action}", mouseInput.actionStatePairs[action] ? 1f : 0f);
                    }
                }
                if (mouseInput.position != mouseInput.previousPosition)
                {
                    gameObjects[id].GetComponent<Script>().SendMessage($"OnMouseMove", mouseInput.position);
                }
            }*/
        }


    }
}
