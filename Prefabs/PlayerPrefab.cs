using Microsoft.Xna.Framework;
using CrowEngine;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ninja
{
    public static class PlayerPrefab
    {
        static PlayerPrefab()
        {
        }

        public static GameObject Create()
        {

            GameObject player = new GameObject();
            KeyboardInput keyboardInput = new();
            keyboardInput.actionKeyPairs.Add("MoveUp", Keys.W);
            keyboardInput.actionKeyPairs.Add("MoveDown", Keys.S);
            keyboardInput.actionKeyPairs.Add("MoveLeft", Keys.A);
            keyboardInput.actionKeyPairs.Add("MoveRight", Keys.D);
            keyboardInput.actionKeyPairs.Add("Interact", Keys.E);
            



            ControllerInput controllerInput = new();
            controllerInput.actionButtonPairs.Add("MoveUp", Buttons.LeftThumbstickUp);
            controllerInput.actionButtonPairs.Add("MoveLeft", Buttons.LeftThumbstickLeft);
            controllerInput.actionButtonPairs.Add("MoveRight", Buttons.LeftThumbstickRight);
            controllerInput.actionButtonPairs.Add("MoveDown", Buttons.LeftThumbstickDown);
            controllerInput.actionButtonPairs.Add("Interact", Buttons.A);

            MouseInput mouseInput = new();
            mouseInput.actionButtonPairs.Add("Interact", MouseButton.RightButton);


            player.Add(keyboardInput);
            player.Add(controllerInput);
            player.Add(mouseInput);
            player.Add(new BasicTestScript(player));
            player.Add(new Transform(new Vector2(240, 150), 0, Vector2.One));
            player.Add(new Sprite(ResourceManager.GetTexture("DefaultPlayer"), Color.White, 0));
            player.Add(new Rigidbody());
            player.Add(new RectangleCollider(new Vector2(15, 15), false, yOffset: 10));
            player.Add(new Light(Color.White, 1f, 100));


            return player;
        }
    }
}
