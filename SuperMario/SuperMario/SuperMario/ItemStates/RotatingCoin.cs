using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.ItemStates
{
    class RotatingCoin : IItemState
    {
        private AnimatedSprite sprite;

        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        private Item parent;
        private float stoppingPosition;

        public RotatingCoin(Item parent)
        {
            this.parent = parent;
        }

        public void Initialize()
        {
            ScoreValue = GameValues.RotatingCoinScoreValue;
            sprite = AnimatedSpriteFactory.Instance.BuildRotatingCoinSprite(parent.Position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
            ScoreSprite = new ScoreSprite(ScoreValue.ToString(), parent.Position, false);
            stoppingPosition = parent.Position.Y - GameValues.BlockRotatingCoinStoppingYPosition;
        }

        public void Update(GameTime gameTime)
        {
            if (parent.Spawning)
            {
                parent.Velocity = new Vector2(parent.Velocity.X, parent.Velocity.Y + 1);
                /*
                if (parent.Position.Y + parent.Velocity.Y >= stoppingPosition)
                {
                    parent.Spawning = false;
                }
                */
                parent.Position = new Vector2(parent.Position.X, parent.Position.Y + parent.Velocity.Y);
            }

            if (parent.FinishedSpawning)
            {
                Physics.Move(parent);
            }

            if (gameTime.TotalGameTime.Milliseconds % GameValues.RotatingCoinUpdateDelay == 0)
            {
                sprite.AdvanceFrame();
            }
            sprite.UpdateSpritePosition(parent.Position);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (parent.Spawning)
            {
                if (ScoreSprite.ScoringOn)
                {
                    ScoreSprite.Draw(spriteBatch, gameTime);
                }

                sprite.Draw(spriteBatch, gameTime);
            }
        }
    }
}

