using SuperMario.Interfaces;
using SuperMario.MarioStates;

namespace SuperMario.Commands
{
    public class MarioCollidesWithEnemyCommand : ICommand
    {
        private IPlayableObject playableObject;

        public MarioCollidesWithEnemyCommand(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            if (!playableObject.StarPower && !playableObject.TakenDamageState && !playableObject.OnYoshi)
            {
                playableObject.IsBig = false;
                playableObject.IsFire = false;
                playableObject.PlayableObjectState.TakeDamage();
                Level.Instance.PowerUpState = false;

                if (playableObject.PlayableObjectState.ToString() != "SuperMario.MarioStates.DeadMario")
                {
                    SoundManager.Instance.PlayPowerDownSound();
                    playableObject.TakenDamageState = true;
                }
            }

            else if (!playableObject.StarPower && !playableObject.TakenDamageState)
            {
                playableObject.OnYoshi = false;
                playableObject.TakenDamageState = true;
                SoundManager.Instance.PlayPowerDownSound();

                if (playableObject.IsFire)
                {
                    playableObject.PlayableObjectState = new FireRightIdleMario(playableObject);
                }

                else if (Mario.Instance.IsBig)
                {
                    playableObject.PlayableObjectState = new BigRightIdleMario(playableObject);
                }

                else
                {
                    playableObject.PlayableObjectState = new SmallRightIdleMario(playableObject);
                }
            }
            else
            {
                // enemy takes damage
            }
        }
    }
}
