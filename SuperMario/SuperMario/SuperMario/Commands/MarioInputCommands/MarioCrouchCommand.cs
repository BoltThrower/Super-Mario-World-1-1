using SuperMario.Interfaces;

namespace SuperMario.Commands.MarioInputCommands
{
    class MarioCrouchCommand : ICommand
    {
        private IPlayableObject playableObject;

        public MarioCrouchCommand(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.PlayingState")
            {
                playableObject.PlayableObjectState.DownInput();
            }
        }
    }
}
