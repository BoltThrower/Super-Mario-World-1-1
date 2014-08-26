using SuperMario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMario.Commands.MarioInputCommands
{
    class MarioDownInputCommand : ICommand
    {
        private IPlayableObject playableObject;

        public MarioDownInputCommand(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
        }

        public void Execute()
        {
            playableObject.PlayableObjectState.DownInput();
        }
    }
}
