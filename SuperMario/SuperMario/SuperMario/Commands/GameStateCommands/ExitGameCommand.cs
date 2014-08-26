using SuperMario.Interfaces;
using Microsoft.Xna.Framework;

namespace SuperMario.Commands.MarioInputCommands
{
    class ExitGameCommand : ICommand
    {
        private Game game;

        public ExitGameCommand(Game game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Exit();
        }
    }
}
