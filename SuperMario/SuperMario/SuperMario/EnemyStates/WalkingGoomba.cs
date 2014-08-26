using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.EnemyStates
{
    class WalkingGoomba : IEnemyState
    {
        private AnimatedSprite sprite;

        public Vector2 Velocity { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        private Enemy parent;

        public WalkingGoomba(Vector2 position, Enemy parent)
        {
            this.parent = parent;
            ScoreValue = GameValues.GoombaScoreValue;
            Velocity = GameValues.GoombaWalkingInitialVelocity;
            sprite = AnimatedSpriteFactory.Instance.BuildEnemyWalkingGoombaSprite(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            if (gameTime.TotalGameTime.Milliseconds % GameValues.GoombaWalkingUpdateDelay == 0)
            {
                sprite.AdvanceFrame();
                sprite.UpdateSpritePosition(position);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}
