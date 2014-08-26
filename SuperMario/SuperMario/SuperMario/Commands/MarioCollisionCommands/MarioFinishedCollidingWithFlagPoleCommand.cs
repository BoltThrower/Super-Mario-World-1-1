using Microsoft.Xna.Framework;
using SuperMario.GameStates;
using SuperMario.Interfaces;
using SuperMario.MarioStates;

namespace SuperMario.Commands.MarioCollisionCommands
{
    class MarioFinishedCollidingWithFlagPoleCommand : IPlayableCommand
    {
        public void Execute(IPlayableObject playableObject)
        {
            if (playableObject.IsFire)
            {
                playableObject.PlayableObjectState = new FireRightWalkingMario(playableObject);
            }

            else if (playableObject.IsBig)
            {
                playableObject.PlayableObjectState = new BigRightWalkingMario(playableObject);
            }

            else
            {
                playableObject.PlayableObjectState = new SmallRightWalkingMario(playableObject);
            }

            playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - GameValues.FinishedCollisingWithFlagPoleYPositionOffset);
            playableObject.Velocity = new Vector2(1f, -1f);
            playableObject.MaxVelocity = new Vector2(1f, 1f);
            GameStateMachine.Instance.GameState = new EndLevelState();
        }
    }
}
