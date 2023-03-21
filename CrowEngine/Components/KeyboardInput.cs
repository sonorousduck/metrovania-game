using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

namespace CrowEngine
{
    public class KeyboardInput : Input
    {
        public Dictionary<string, Keys> actionKeyPairs;
        public Dictionary<string, bool> actions;
        public Dictionary<string, bool> previousActions;
        public KeyboardInput() 
        {
            actionKeyPairs = new Dictionary<string, Keys>();
            actions = new();
            previousActions = new();
        }
    }
}
