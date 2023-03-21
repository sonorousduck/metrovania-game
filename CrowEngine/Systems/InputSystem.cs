using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;


namespace CrowEngine
{
    /// <summary>
    /// Updates Input components in the game (does not do anything with it, just updates state)
    /// </summary>
    public class InputSystem : System
    {
        public InputSystem(SystemManager systemManager) : base(systemManager, typeof(Input))
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (uint id in gameObjects.Keys)
            {

                if (gameObjects[id].ContainsComponent<KeyboardInput>())
                {
                    KeyboardInput keyboardInput = gameObjects[id].GetComponent<KeyboardInput>();
                    KeyboardState keyboardState = Keyboard.GetState();
                    keyboardInput.previousActions = new Dictionary<string, bool>(keyboardInput.actions);

                    foreach (string action in keyboardInput.actionKeyPairs.Keys)
                    {
                        if (!keyboardInput.actions.ContainsKey(action))
                        {
                            keyboardInput.actions.Add(action, false);
                            keyboardInput.previousActions.Add(action, false);
                        }          
                        keyboardInput.actions[action] = keyboardState.IsKeyDown(keyboardInput.actionKeyPairs[action]);
                    }
                }

                if (gameObjects[id].ContainsComponent<MouseInput>())
                {
                    MouseInput mouseInput = gameObjects[id].GetComponent<MouseInput>();
                    MouseState mouseState = Mouse.GetState();
                    mouseInput.previousPosition = new Vector2(mouseInput.position.X, mouseInput.position.Y);
                    mouseInput.previousActions = new Dictionary<string, bool>(mouseInput.actions);

                    mouseInput.position = new Vector2(mouseState.X, mouseState.Y);

                    foreach (string action in mouseInput.actionButtonPairs.Keys)
                    {
                        if (!mouseInput.actions.ContainsKey(action))
                        {
                            mouseInput.actions.Add(action, false);
                            mouseInput.previousActions.Add(action, false);
                        }

                        mouseInput.actions[action] = GetMouseState(mouseState, mouseInput, action);
                    }
                }


                if (gameObjects[id].ContainsComponent<ControllerInput>())
                {
                    ControllerInput controllerInput = gameObjects[id].GetComponent<ControllerInput>();
                    GamePadState gamePadState = GamePad.GetState(controllerInput.controllerOwner);

                    controllerInput.previousActions = new Dictionary<string, bool>(controllerInput.actions);

                    foreach (string action in controllerInput.actionButtonPairs.Keys)
                    {
                        if (!controllerInput.actions.ContainsKey(action))
                        {
                            controllerInput.actions.Add(action, false);
                            controllerInput.previousActions.Add(action, false);
                        }

                        controllerInput.actions[action] = GetControllerState(controllerInput.actionButtonPairs[action], gamePadState) > 0.5f;
                    }
                    

                }
            }
        }

        private bool GetMouseState(MouseState mouseState, MouseInput mouseInput, string action)
        {
            switch (mouseInput.actionButtonPairs[action])
            {
                case MouseButton.LeftButton:
                    return mouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.MiddleButton:
                    return mouseState.MiddleButton == ButtonState.Pressed;
                case MouseButton.RightButton:
                    return mouseState.RightButton == ButtonState.Pressed;
                case MouseButton.x1Button:
                    return mouseState.XButton1 == ButtonState.Pressed;
                case MouseButton.x2Button:
                    return mouseState.XButton2 == ButtonState.Pressed;
                case MouseButton.scrollWheelUp:
                    bool changedScrollUp = mouseState.ScrollWheelValue > mouseInput.previousScrollWheelValue;
                    if (changedScrollUp)
                    {
                        mouseInput.previousScrollWheelValue = mouseState.ScrollWheelValue;
                    }
                    return changedScrollUp;
                case MouseButton.scrollWheelDown:
                    bool changedScrollDown = mouseState.ScrollWheelValue < mouseInput.previousScrollWheelValue;
                    if (changedScrollDown)
                    {
                        mouseInput.previousScrollWheelValue = mouseState.ScrollWheelValue;
                    }
                    return changedScrollDown;
                default:
                    break;
            }


            return false;
        }



        /// <summary>
        /// Very ugly way of reading controller input. Should be improved upon, but allows all input types to be treated as floats
        /// </summary>
        /// <param name="type"></param>
        /// <param name="gamePadState"></param>
        /// <returns></returns>
        private float GetControllerState(Buttons type, GamePadState gamePadState)
        {
            if (gamePadState.IsConnected)
            {
                switch (type)
                {
                    case (Buttons.A):
                        return gamePadState.IsButtonDown(Buttons.A) ? 1 : 0;
                    case (Buttons.B):
                        return gamePadState.IsButtonDown(Buttons.B) ? 1 : 0;
                    case (Buttons.Back):
                        return gamePadState.IsButtonDown(Buttons.Back) ? 1 : 0;
                    case (Buttons.DPadDown):
                        return gamePadState.DPad.Down == ButtonState.Pressed ? 1 : 0;
                    case (Buttons.DPadLeft):
                        return gamePadState.DPad.Left == ButtonState.Pressed ? 1 : 0;
                    case (Buttons.DPadRight):
                        return gamePadState.DPad.Right == ButtonState.Pressed ? 1 : 0;
                    case (Buttons.DPadUp):
                        return gamePadState.DPad.Up == ButtonState.Pressed ? 1 : 0;
                    case (Buttons.LeftShoulder):
                        return gamePadState.IsButtonDown(Buttons.LeftShoulder) ? 1 : 0;
                    case (Buttons.LeftThumbstickLeft):
                        return -gamePadState.ThumbSticks.Left.X;
                    case (Buttons.LeftThumbstickRight):
                        return gamePadState.ThumbSticks.Left.X;
                    case (Buttons.LeftStick):
                        return gamePadState.IsButtonDown(Buttons.LeftStick) ? 1 : 0;
                    case (Buttons.LeftThumbstickUp):
                        return gamePadState.ThumbSticks.Left.Y;
                    case (Buttons.LeftThumbstickDown):
                        return -gamePadState.ThumbSticks.Left.Y;
                    case (Buttons.LeftTrigger):
                        return gamePadState.Triggers.Left;
                    case (Buttons.RightShoulder):
                        return gamePadState.IsButtonDown(Buttons.RightShoulder) ? 1 : 0;
                    case (Buttons.RightThumbstickRight):
                        return gamePadState.ThumbSticks.Right.X;
                    case (Buttons.RightThumbstickDown):
                        return gamePadState.IsButtonDown(Buttons.RightStick) ? 1 : 0;
                    case (Buttons.RightThumbstickUp):
                        return gamePadState.ThumbSticks.Right.Y;
                    case (Buttons.RightTrigger):
                        return gamePadState.Triggers.Right;
                    case (Buttons.Start):
                        return gamePadState.IsButtonDown(Buttons.Start) ? 1 : 0;
                    case (Buttons.X):
                        return gamePadState.IsButtonDown(Buttons.X) ? 1 : 0;
                    case (Buttons.Y):
                        return gamePadState.IsButtonDown(Buttons.Y) ? 1 : 0;
                    default:
                        Debug.WriteLine("Found a controller input type that was non-existant, returning 0");
                        return 0;
                }

            }
            return 0;
        }
    }
}
