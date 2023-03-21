using System;
using Microsoft.Xna.Framework;

namespace CrowEngine
{
    public class AnimationSystem : System
    {
        public AnimationSystem(SystemManager systemManager) : base(systemManager, typeof(AnimatedSprite)) 
        {
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (uint id in gameObjects.Keys)
            {
                AnimatedSprite animatedSprite = gameObjects[id].GetComponent<AnimatedSprite>();
                animatedSprite.currentTime += gameTime.ElapsedGameTime;

                while (animatedSprite.currentTime.Milliseconds > animatedSprite.frameTiming[animatedSprite.currentFrame])
                {
                    animatedSprite.currentTime = animatedSprite.currentTime.Subtract(TimeSpan.FromMilliseconds(animatedSprite.frameTiming[animatedSprite.currentFrame]));

/*                    if (animatedSprite.playOnce && animatedSprite.currentFrame == animatedSprite.endFrame)
                    {
                        animatedSprite.completedPlay = true;
                    }*/

                    if (animatedSprite.currentFrame != animatedSprite.endFrame || !animatedSprite.playOnce)
                    {
                        if (animatedSprite.callbacks != null && animatedSprite.callbacks.ContainsKey(animatedSprite.currentFrame))
                        {
                            // Run the callback
                            animatedSprite.callbackToRun.Enqueue(animatedSprite.callbacks[animatedSprite.currentFrame]);
                        }

                        animatedSprite.currentFrame += 1;
                    }
                    else if (animatedSprite.currentFrame == animatedSprite.endFrame)
                    {
                        if (animatedSprite.callbacks != null && animatedSprite.callbacks.ContainsKey(animatedSprite.currentFrame))
                        {
                            // Run the callback
                            animatedSprite.callbackToRun.Enqueue(animatedSprite.callbacks[animatedSprite.currentFrame]);
                        }
                    }

                    if (animatedSprite.endFrame != 0 && !animatedSprite.playOnce)
                    {
                        if (animatedSprite.currentFrame > animatedSprite.endFrame)
                        {
                            animatedSprite.currentFrame = animatedSprite.startFrame;
                        }
                    }
                    else if (animatedSprite.endFrame == 0)
                    {
                        animatedSprite.currentFrame %= animatedSprite.frameTiming.Length;
                    }
/*                    else if (animatedSprite.playOnce)
                    {
                        if (animatedSprite.currentFrame == animatedSprite.endFrame)
                        {
                            animatedSprite.completedPlay = true;
                        }
                    }*/

                }
            }
        }
    }
}
