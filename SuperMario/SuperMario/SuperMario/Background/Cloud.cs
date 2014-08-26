using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class Cloud : IBackgroundItem
    {
        public Vector2 Position { get; set; }
        private AnimatedSprite sprite;

        public Cloud(Vector2 position, int cloudFrame)
        {
            Position = position;
            sprite = AnimatedSpriteFactory.Instance.BuildBackgroundCloudSprite(position, cloudFrame);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}
