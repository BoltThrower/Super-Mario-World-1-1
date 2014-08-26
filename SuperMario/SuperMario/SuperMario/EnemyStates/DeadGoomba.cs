using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.EnemyStates
{
    class DeadGoomba : IEnemyState
    {
        private AnimatedSprite sprite;

        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        private Enemy parent;

        public DeadGoomba(Vector2 position, Enemy parent)
        {
            this.parent = parent;
            ScoreValue = GameValues.GoombaScoreValue;
            sprite = AnimatedSpriteFactory.Instance.BuildEnemyDeadGoombaSprite(position);
            CollisionRectangle = GameValues.EmptyCollisionRectangle;
            ScoreSprite = new ScoreSprite(ScoreValue.ToString(), position, false);
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
