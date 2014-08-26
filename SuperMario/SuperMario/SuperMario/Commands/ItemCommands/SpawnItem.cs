using Microsoft.Xna.Framework;
using SuperMario.Interfaces;

namespace SuperMario.Commands.ItemCommands
{
    class SpawnItem : ICommand 
    {
        private bool spawning;
        private Vector2 position;
        private Vector2 velocity;

        public SpawnItem(bool spawning, Vector2 position, Vector2 velocity)
        {
            this.spawning = spawning;
            this.position = position;
            this.velocity = velocity;
        }

        public void Execute()
        {
            if (spawning)
            {
                position = new Vector2(position.X, position.Y + velocity.Y);
            }
        }
    }
}
