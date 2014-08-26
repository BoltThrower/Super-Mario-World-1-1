using SuperMario.GameStates;
using SuperMario.Interfaces;

namespace SuperMario.Commands.GameStateCommands
{
    public class StartButtonCommand : ICommand
    {        
        public void Execute()
        {
            if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.StartScreenState")
            {
                GameStateMachine.Instance.GameState = new MarioRespawnState();
            }

            else if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.PlayingState")
            {
                GameStateMachine.Instance.GameState = new PausedState();
                SoundManager.Instance.PlayPauseSound();
            }

            else if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.PausedState")
            {
                GameStateMachine.Instance.GameState = new PlayingState();
                SoundManager.Instance.PlayPauseSound();
            }
        }
    }
}
