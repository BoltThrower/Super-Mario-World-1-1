using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario.Interfaces
{
    public interface IBackgroundItem
    {
        Vector2 Position { get; set; }

        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
