using SuperMario.Interfaces;

namespace SuperMario.Commands.MarioInputCommands
{
    class MarioNoInputCommand : ICommand
    {
        private IPlayableObject playableObject;

        public MarioNoInputCommand(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            playableObject.PlayableObjectState.NoInput();
        }
    }
}
