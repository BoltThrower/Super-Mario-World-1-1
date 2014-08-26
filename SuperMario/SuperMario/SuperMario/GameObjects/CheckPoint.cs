using Microsoft.Xna.Framework;

namespace SuperMario
{
    public class CheckPoint
    {
        public Vector2 Position { get; set; }
        public bool CheckPointEnabled { get; set; }

        public CheckPoint(Vector2 position)
        {
            this.Position = position;
            this.CheckPointEnabled = false;
        }
    }
}
