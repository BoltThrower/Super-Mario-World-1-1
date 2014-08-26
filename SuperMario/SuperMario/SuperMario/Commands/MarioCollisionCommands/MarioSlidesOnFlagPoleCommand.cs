using Microsoft.Xna.Framework;
using SuperMario.Interfaces;
using SuperMario.MarioStates;

namespace SuperMario.Commands.MarioCollisionCommands
{
    class MarioSlidesOnFlagPoleCommand : ICommand
    {
        private IPlayableObject playableObject;
        private IDynamicObject dynamicObject;

        public MarioSlidesOnFlagPoleCommand(IPlayableObject playableObject, IDynamicObject dynamicObject)
        {
            this.dynamicObject = dynamicObject;
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            if (!playableObject.IsBig)
            {
                playableObject.PlayableObjectState = new SmallMarioFlagSlideEnd(playableObject);
                playableObject.Position = new Vector2(playableObject.Position.X + playableObject.CollisionRectangle.Width, dynamicObject.CollisionRectangle.Top - playableObject.CollisionRectangle.Height + 1);
            }

            else if (playableObject.PlayableObjectState.ToString() == "SuperMario.MarioStates.BigMarioFlagSlide")
            {
                playableObject.PlayableObjectState = new BigMarioFlagSlideEnd(playableObject);
                playableObject.Position = new Vector2(playableObject.Position.X + playableObject.CollisionRectangle.Width, dynamicObject.CollisionRectangle.Top - playableObject.CollisionRectangle.Height + 1);
            }

            else if (playableObject.PlayableObjectState.ToString() == "SuperMario.MarioStates.FireMarioFlagSlide")
            {
                playableObject.PlayableObjectState = new FireMarioFlagSlideEnd(playableObject);
                playableObject.Position = new Vector2(playableObject.Position.X + playableObject.CollisionRectangle.Width, dynamicObject.CollisionRectangle.Top - playableObject.CollisionRectangle.Height + 1);
            }

            playableObject.IsSlidingOnPole = false;
            playableObject.InAir = false;
            playableObject.Velocity = Vector2.Zero;
            playableObject.MaxVelocity = Vector2.Zero;
        }
    }
}
