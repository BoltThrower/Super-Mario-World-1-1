using SuperMario.Commands.MarioInputCommands;
using SuperMario.Interfaces;
using SuperMario.MarioStates;

namespace SuperMario.Commands
{
    class MarioCollidesWithFlagPoleCommand : ICommand
    {
        private IPlayableObject playableObject;

        public MarioCollidesWithFlagPoleCommand(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            playableObject.InAir = false;
            playableObject.IsJumping = false;
            playableObject.OnYoshi = false;

            if (playableObject.IsFire)
            {
                playableObject.PlayableObjectState = new FireMarioFlagSlide(playableObject);
            }

            else if (playableObject.IsBig)
            {
                playableObject.PlayableObjectState = new BigMarioFlagSlide(playableObject);
            }

            else
            {
                playableObject.PlayableObjectState = new SmallMarioFlagSlide(playableObject);
            }

            playableObject.IsSlidingOnPole = true;
            Level.Instance.Flag.IsMoving = true;
        }
    }
}
