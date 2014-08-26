using SuperMario.GameStates;
using SuperMario.Interfaces;
using SuperMario.MarioStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMario.Commands.GameStateCommands
{
    public class TimeUpCommand : ICommand
    {
        private List<IPlayableObject> playableObjects;

        public TimeUpCommand(List<IPlayableObject> playableObjects)
        {
            this.playableObjects = playableObjects;
        }

        public void Execute()
        {
            GameStateMachine.Instance.GameState = new TimeUpState();
            foreach (IPlayableObject playableObject in playableObjects)
            {
                playableObject.PlayableObjectState = new DeadMario(playableObject);
                playableObject.Lives--;
            }
        }
    }
}
