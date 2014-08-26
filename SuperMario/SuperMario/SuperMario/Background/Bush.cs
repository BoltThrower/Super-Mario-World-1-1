using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class Bush : IBackgroundItem
    {
        public Vector2 Position { get; set; }
        private AnimatedSprite sprite;

        public Bush(Vector2 position, int bushFrame)
        {
            Position = position;
            sprite = AnimatedSpriteFactory.Instance.BuildBackgroundBushSprite(position, bushFrame);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}
