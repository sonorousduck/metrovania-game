using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace CrowEngine
{
    /// <summary>
    /// Represents the possible buttons on a mouse. Left middle and right are self explanatory, while x1 and x2 are the potential side mouse buttons
    /// </summary>
    public enum MouseButton
    {
        LeftButton,
        MiddleButton,
        RightButton,
        x1Button,
        x2Button,
        scrollWheelUp,
        scrollWheelDown,
    }
    public class MouseInput : Input
    {
        public Vector2 position;
        public Vector2 previousPosition;
        public int previousScrollWheelValue;
        public Dictionary<string, MouseButton> actionButtonPairs;
        public Dictionary<string, bool> actions;
        public Dictionary<string, bool> previousActions;

        public MouseInput()
        {
            position = new Vector2();
            previousPosition = new Vector2();
            actionButtonPairs = new Dictionary<string, MouseButton>();
            actions = new Dictionary<string, bool>();
            previousActions = new Dictionary<string, bool>();
            previousScrollWheelValue = 0;
        }

        /// <summary>
        /// Performs the conversion between the mouse's pixel position to the world's physics coordinates
        /// </summary>
        /// <returns></returns>
        public Vector2 GetCurrentPhysicsPosition()
        {
            Vector2 distanceFromCenterPixels = position - RenderingSystem.centerOfScreen;
            Vector2 physicsDistanceFromCenter = distanceFromCenterPixels;
            Vector2 truePosition = physicsDistanceFromCenter + new Vector2(PhysicsSystem.PHYSICS_DIMENSION_WIDTH, PhysicsSystem.PHYSICS_DIMENSION_HEIGHT) / 2;

            return truePosition;
        }

        /// <summary>
        /// Gets the location of the cursor relative to the camera's position
        /// </summary>
        /// <param name="cameraLocation"></param>
        /// <returns></returns>
        public Vector2 PhysicsPositionCamera(Transform cameraLocation)
        {
            Vector2 distanceFromCenterPixels = position - RenderingSystem.centerOfScreen;
            Vector2 physicsDistanceFromCenter = distanceFromCenterPixels;
            Vector2 truePosition = physicsDistanceFromCenter + cameraLocation.position;

            return truePosition;
        }
    }
}
