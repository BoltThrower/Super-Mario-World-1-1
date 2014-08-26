using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario.Interfaces
{
    public interface IStaticObject
    {
        Vector2 Position { get; set; }

        Rectangle CollisionRectangle { get; set; }

        void Draw(SpriteBatch spriteBatch, GameTime gameTime);

    }
}
