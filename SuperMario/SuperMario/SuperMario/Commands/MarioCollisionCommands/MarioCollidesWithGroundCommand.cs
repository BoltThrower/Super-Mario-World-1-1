using SuperMario.Commands.MarioInputCommands;
using SuperMario.Interfaces;

namespace SuperMario.Commands
{
    class MarioCollidesWithGroundCommand : ICommand
    {
        private IPlayableObject playableObject;

        public MarioCollidesWithGroundCommand(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            playableObject.InAir = false;
            playableObject.IsJumping = false;
        }
    }
}
