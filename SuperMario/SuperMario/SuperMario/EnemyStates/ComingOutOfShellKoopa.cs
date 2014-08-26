using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.EnemyStates
{
    class ComingOutOfShellKoopa : IEnemyState
    {
        private AnimatedSprite sprite;

        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        private Enemy parent;

        public ComingOutOfShellKoopa(Vector2 position, Enemy parent)
        {
            this.parent = parent;
            ScoreValue = GameValues.ComingOutOfShellKoopaScoreValue;
            sprite = AnimatedSpriteFactory.Instance.BuildEnemyComingOutOfShellKoopaSprite(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
            ScoreSprite = new ScoreSprite(ScoreValue.ToString(), position, false);
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            sprite.AdvanceFrame();
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