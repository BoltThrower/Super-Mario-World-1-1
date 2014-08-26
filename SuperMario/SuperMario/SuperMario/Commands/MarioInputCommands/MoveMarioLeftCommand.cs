using SuperMario.Interfaces;

namespace SuperMario.Commands.MarioInputCommands
{
    class MoveMarioLeftCommand : ICommand
    {
        private IPlayableObject playableObject;

        public MoveMarioLeftCommand(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.PlayingState")
            {
                // tell mario to move to the left
                playableObject.PlayableObjectState.LeftInput();
            }
        }
    }
}
