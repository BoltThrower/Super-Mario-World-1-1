using SuperMario.Interfaces;

namespace SuperMario.Commands.MarioInputCommands
{
    class MoveMarioRightCommand : ICommand
    {
        private IPlayableObject playableObject;

        public MoveMarioRightCommand(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.PlayingState")
            {
                // tell mario to move to the right
                playableObject.PlayableObjectState.RightInput();
            }
        }
    }
}
