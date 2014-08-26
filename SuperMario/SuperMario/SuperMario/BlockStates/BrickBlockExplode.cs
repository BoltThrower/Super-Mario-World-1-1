using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.BlockStates
{
    class BrickBlockExplode : IBlockState
    {
        public Rectangle CollisionRectangle { get; set; }

        private Vector2 velocity;
        private Vector2 stoppingPosition;
        private bool particlesDead;
        private int scoreValue;

        private Vector2 topLeftPosition, topRightPosition, bottomLeftPosition, bottomRightPosition;
        private AnimatedSprite topLeftSprite, topRightSprite, bottomLeftSprite, bottomRightSprite;
        private Block parent;

        public BrickBlockExplode(Vector2 position, Block parent)
        {
            this.parent = parent;
            velocity = GameValues.ExplodingBlockInitialVelocity;
            scoreValue = GameValues.ExplodingBlockScoreValue;
            HUD.Instance.ScoreHUDCounter += scoreValue;
            particlesDead = false;
            stoppingPosition = position - (GameValues.ExplodingBlockStoppingPositionOffset * velocity);

            topLeftPosition = position - velocity;
            topRightPosition = position + new Vector2(1, -1) * velocity;
            bottomLeftPosition = position + new Vector2(-1, 1) * velocity;
            bottomRightPosition = position + velocity;

            topLeftSprite = AnimatedSpriteFactory.Instance.BuildBrickBlockParticleSprite(topLeftPosition);
            topLeftSprite.UpdateSpritePosition(topLeftPosition);
            topRightSprite = AnimatedSpriteFactory.Instance.BuildBrickBlockParticleSprite(topRightPosition);
            topRightSprite.UpdateSpritePosition(topRightPosition);
            bottomLeftSprite = AnimatedSpriteFactory.Instance.BuildBrickBlockParticleSprite(bottomLeftPosition);
            bottomLeftSprite.UpdateSpritePosition(bottomLeftPosition);
            bottomRightSprite = AnimatedSpriteFactory.Instance.BuildBrickBlockParticleSprite(bottomRightPosition);
            bottomRightSprite.UpdateSpritePosition(bottomRightPosition);
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            float elapsed = gameTime.ElapsedGameTime.Seconds;

            if (topLeftPosition != stoppingPosition)
            {
                if (gameTime.TotalGameTime.Milliseconds % GameValues.ExplodingBlockUpdateDelay == 0)
                {
                    topLeftPosition = topLeftPosition + new Vector2(-1, -1) * velocity;
                    topRightPosition = topRightPosition + new Vector2(1, -1) * velocity;
                    bottomLeftPosition = bottomLeftPosition + new Vector2(-1, 1) * velocity;
                    bottomRightPosition = bottomRightPosition + new Vector2(1, 1) * velocity;

                    topLeftSprite.UpdateSpritePosition(topLeftPosition);
                    topRightSprite.UpdateSpritePosition(topRightPosition);
                    bottomLeftSprite.UpdateSpritePosition(bottomLeftPosition);
                    bottomRightSprite.UpdateSpritePosition(bottomRightPosition);
                }
            }
            else
            {
                particlesDead = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!particlesDead)
            {
                topLeftSprite.Draw(spriteBatch, gameTime);
                topRightSprite.Draw(spriteBatch, gameTime);
                bottomLeftSprite.Draw(spriteBatch, gameTime);
                bottomRightSprite.Draw(spriteBatch, gameTime);
            }
        }
    }
}
