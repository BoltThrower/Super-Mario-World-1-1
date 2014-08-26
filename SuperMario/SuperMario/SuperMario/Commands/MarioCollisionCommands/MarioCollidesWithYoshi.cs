using Microsoft.Xna.Framework;
using SuperMario.Interfaces;
using SuperMario.MarioStates;

namespace SuperMario.Commands.MarioCollisionCommands
{
    public class MarioCollidesWithYoshi : ICommand
    {
        private IPlayableObject playableObject;

        public MarioCollidesWithYoshi(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            if (playableObject.InAir)
            {
                playableObject.InAir = false;
                playableObject.IsJumping = false;
                playableObject.Velocity = Vector2.Zero;
                playableObject.Acceleration = Vector2.Zero;

                SoundManager.Instance.PlayYoshiSound();
                playableObject.OnYoshi = true;

                if (playableObject.IsFire)
                {
                    // Fire MarioYoshi state.  Using big MarioYoshi for now.
                    playableObject.PlayableObjectState = new BigRightIdleMarioYoshi(playableObject);
                }

                else if (playableObject.IsBig)
                {
                    playableObject.PlayableObjectState = new BigRightIdleMarioYoshi(playableObject);
                }

                else
                {
                    playableObject.PlayableObjectState = new SmallRightIdleMarioYoshi(playableObject);
                }
            }
        }
    }
}
