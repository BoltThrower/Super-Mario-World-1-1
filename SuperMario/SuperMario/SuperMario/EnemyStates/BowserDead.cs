using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.EnemyStates
{
    class BowserDead: IEnemyState
    {
        private AnimatedSprite sprite;

        public Vector2 Velocity { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        private Enemy parent;
        private int timeout;

        public BowserDead(Vector2 position, Enemy parent)
        {
            this.parent = parent;
            ScoreValue = GameValues.BowserScoreValue;
            sprite = AnimatedSpriteFactory.Instance.BuildBowserDeadSprite(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
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
