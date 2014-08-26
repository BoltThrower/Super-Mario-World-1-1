using SuperMario.Interfaces;

namespace SuperMario.Commands
{
    public class MarioCollidesWithMushroomCommand : ICommand
    {
        private IPlayableObject playableObject;

        public MarioCollidesWithMushroomCommand(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            playableObject.IsBig = true;
            Level.Instance.PowerUpState = true;
            playableObject.PlayableObjectState.PickUpPowerup();
        }
    }
}
