﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.EnemyStates
{
    class LeftWalkingKoopa : IEnemyState
    {
        private AnimatedSprite sprite;

        public Vector2 Velocity { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        private Enemy parent;

        public LeftWalkingKoopa(Vector2 position, Enemy parent)
        {
            this.parent = parent;
            ScoreValue = GameValues.KoopaScoreValue;
            ScoreSprite = new ScoreSprite(ScoreValue.ToString(), position, false);
            Velocity = GameValues.LeftWalkingKoopaInitialVelocity;
            sprite = AnimatedSpriteFactory.Instance.BuildEnemyLeftWalkingKoopaSprite(new Vector2(position.X, position.Y));
            CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            if (gameTime.TotalGameTime.Milliseconds % GameValues.KoopaUpdateDelay == 0)
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