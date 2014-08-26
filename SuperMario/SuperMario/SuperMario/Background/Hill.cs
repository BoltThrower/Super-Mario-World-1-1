using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class Hill : IBackgroundItem
    {
        public Vector2 Position { get; set; }
        private AnimatedSprite sprite;

        public Hill(Vector2 position, int hillFrame)
        {
            Position = position;
            sprite = AnimatedSpriteFactory.Instance.BuildBackgroundHillSprite(position, hillFrame);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}
