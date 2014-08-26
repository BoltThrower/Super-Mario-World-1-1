using SuperMario.Interfaces;

namespace SuperMario.Commands
{
    class MarioCollidesWithStarCommand : ICommand
    {
        private IPlayableObject playableObject;

        public MarioCollidesWithStarCommand(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            playableObject.PlayableObjectState.PickUpStar();
            playableObject.StarPower = true;
        }
    }
}
