using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario
{
    public class Flag
    {
        public Vector2 Position { get; set; }
        public float YVelocity { get; set; }
        public bool IsMoving { get; set; }
        private AnimatedSprite sprite;

        public Flag(Vector2 position)
        {
            this.Position = position;
            sprite = AnimatedSpriteFactory.Instance.BuildFlagSprite(this.Position);
            this.YVelocity = 1.5f;
            this.IsMoving = false;
        }

        public void Update(GameTime gameTime)
        {
            if (IsMoving)
            {
                Position = new Vector2(Position.X, Position.Y + YVelocity);
            }
            sprite.UpdateSpritePosition(Position);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}
