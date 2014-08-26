using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario.Interfaces
{
    public interface IBlockState
    {
        Rectangle CollisionRectangle { get; set; }

        void Update(GameTime gameTime, Vector2 position);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}