using SuperMario.Interfaces;
using SuperMario.MarioStates;

namespace SuperMario.Commands.MarioInputCommands
{
    class MarioRunCommand : ICommand
    {
        private IPlayableObject playableObject;

        public MarioRunCommand(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.PlayingState")
            {
                // tell mario he's now running
                playableObject.PlayableObjectState.RunButtonInput();
            }
        }
    }
}
