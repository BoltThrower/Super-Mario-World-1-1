using Microsoft.Xna.Framework;
using SuperMario.Interfaces;

namespace SuperMario.Commands.MarioInputCommands
{
    class MarioJumpCommand : ICommand
    {
        private IPlayableObject playableObject;

        public MarioJumpCommand(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            playableObject.PlayableObjectState.JumpButtonInput();
        }
    }
}
