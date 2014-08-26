using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.EnemyStates
{
    class CrawfisRight : IEnemyState
    {
        private AnimatedSprite sprite;

        public Vector2 Velocity { get; set; }

        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        private Enemy parent;

        public CrawfisRight(Vector2 position, Enemy parent)
        {
            this.parent = parent;
            ScoreValue = GameValues.CrawfisScoreValue;
            ScoreSprite = new ScoreSprite(ScoreValue.ToString(), position, false);
            Velocity = GameValues.CrawfisInitialVelocity;
            sprite = AnimatedSpriteFactory.Instance.BuildCrawfisRightSprite(new Vector2(position.X, position.Y));
            CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            sprite.UpdateSpritePosition(position);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (ScoreSprite.ScoringOn)
            {
                ScoreSprite.Draw(spriteBatch, gameTime);
            }

            sprite.Draw(spriteBatch, gameTime);
        }
    }
}
