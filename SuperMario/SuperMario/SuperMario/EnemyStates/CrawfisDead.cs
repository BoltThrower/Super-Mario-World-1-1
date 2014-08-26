using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.EnemyStates
{
    class CrawfisDead : IEnemyState
    {
        private AnimatedSprite sprite;

        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        private Enemy parent;
        private int timeout;

        public CrawfisDead(Vector2 position, Enemy parent)
        {
            this.parent = parent;
            ScoreValue = GameValues.CrawfisScoreValue;
            sprite = AnimatedSpriteFactory.Instance.BuildCrawfisDeadSprite(position);
            CollisionRectangle = GameValues.EmptyCollisionRectangle;
            ScoreSprite = new ScoreSprite(ScoreValue.ToString(), position, true);
            timeout = GameValues.EnemyDeadEnemyTimeout;
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            timeout--;
            if (timeout <= 0)
            {
                parent.EnemyState = new NoEnemy(parent);
            }
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
