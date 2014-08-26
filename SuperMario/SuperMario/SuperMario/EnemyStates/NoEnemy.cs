using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.EnemyStates
{
    class NoEnemy : IEnemyState
    {
        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        private Enemy parent;

        public NoEnemy(Enemy parent)
        {
            this.parent = parent;
            CollisionRectangle = GameValues.EmptyCollisionRectangle;
            ScoreSprite = new ScoreSprite("", Vector2.Zero, true);
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (ScoreSprite.ScoringOn)
            {
                ScoreSprite.Draw(spriteBatch, gameTime);
            }
        }
    }
}
