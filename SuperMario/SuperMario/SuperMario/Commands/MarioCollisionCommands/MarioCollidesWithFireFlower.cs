using SuperMario.Interfaces;

namespace SuperMario.Commands
{
    public class MarioCollidesWithFireFlower : ICommand
    {
        private IPlayableObject playableObject;

        public MarioCollidesWithFireFlower(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            playableObject.IsBig = true;
            playableObject.IsFire = true;
            playableObject.PlayableObjectState.PickUpPowerup();
        }
    }
}
