using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario
{
    interface IAnimatedSprite
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        void AdvanceFrame();
    }
}