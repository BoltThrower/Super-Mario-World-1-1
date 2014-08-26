using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.EnemyStates
{
    class BowserLeft : IEnemyState
    {
        private AnimatedSprite sprite;

        public Vector2 Velocity { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }

        private Enemy parent;
        private bool IsOpenMouth { get; set; }

        public BowserLeft(Vector2 position, Enemy parent)
        {
            this.parent = parent;
            ScoreValue = GameValues.BowserScoreValue;
            ScoreSprite = new ScoreSprite(ScoreValue.ToString(), position, false);
            Velocity = GameValues.BowserInitialVelocity;
            sprite = AnimatedSpriteFactory.Instance.BuildBowserLeftWalkingClosedSprite(new Vector2(position.X, position.Y));
            CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        private void ShootFireball()
        {
            // Shoot fireball
            if (!parent.Fireball.IsAlive)
            {
                parent.Fireball = new Fireball(new Vector2(parent.CollisionRectangle.Left - 8, parent.CollisionRectangle.Center.Y), false, true);
                parent.Fireball.IsAlive = true;
                SoundManager.Instance.PlayFireballSound();
            }
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            if (gameTime.TotalGameTime.Milliseconds % GameValues.BowserChangeStateDelay == 0)
            {
                if (!IsOpenMouth)
                {
                    IsOpenMouth = true;
                    sprite = AnimatedSpriteFactory.Instance.BuildBowserLeftWalkingOpenSprite(new Vector2(position.X, position.Y));
                    ShootFireball();
                }
                else
                {
                    IsOpenMouth = false;
                    sprite = AnimatedSpriteFactory.Instance.BuildBowserLeftWalkingClosedSprite(new Vector2(position.X, position.Y));
                }
            }

            else if (gameTime.TotalGameTime.Milliseconds % GameValues.BowserUpdateDelay == 0)
            {
                sprite.AdvanceFrame();
                sprite.UpdateSpritePosition(position);
            }
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
