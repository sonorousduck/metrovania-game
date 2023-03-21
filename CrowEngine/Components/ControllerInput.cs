using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace CrowEngine
{

    /// <summary>
    /// For the controller, all possible values are read as floats. This makes it consistent that all input types for controller use it, although
    /// it may be inefficient. Something to look at potentially changing in the future
    /// </summary>
    /// 1/7/2023 - I may be wrong, but I don't know if the inefficiency from this is enough to warrant an overhaul of the system... If I need an increase in FPS and
    /// there are no other options, look at this I suppose
    public class ControllerInput : Input
    {

        public Dictionary<string, Buttons> actionButtonPairs; 
        public Dictionary<string, bool> actions;
        public Dictionary<string, bool> previousActions;
        public PlayerIndex controllerOwner;

        public ControllerInput(PlayerIndex controllerOwner = PlayerIndex.One)
        {
            actionButtonPairs = new Dictionary<string, Buttons>();
            actions = new();
            previousActions = new();
            this.controllerOwner = controllerOwner;
        }
    }
}
