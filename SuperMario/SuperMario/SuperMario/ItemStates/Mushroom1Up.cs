using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.ItemStates
{
    class Mushroom1Up : IItemState
    {
        private AnimatedSprite sprite;

        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        private Item parent;
        private float stoppingPosition;

        public Mushroom1Up(Item parent)
        {
            this.parent = parent;
        }

        public void Initialize()
        {
            ScoreValue = GameValues.Mushroom1UPScoreValue;
            sprite = AnimatedSpriteFactory.Instance.BuildMushroom1UpSprite(parent.Position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
            ScoreSprite = new ScoreSprite(GameValues.Mushroom1UPScoreSpriteName, parent.Position, false);
            stoppingPosition = parent.Position.Y - CollisionRectangle.Height;
        }

        public void Update(GameTime gameTime)
        {
            if (parent.Spawning)
            {
                if (parent.Position.Y <= stoppingPosition)
                {
                    parent.Spawning = false;
                    parent.FinishedSpawning = true;
                    parent.CollisionRectangle = CollisionRectangle;
                    parent.Velocity = new Vector2(parent.MaxVelocity.X, 0.0f);
                }

                parent.Position = new Vector2(parent.Position.X, parent.Position.Y + parent.Velocity.Y);
            }

            if (parent.FinishedSpawning)
            {
                Physics.Move(parent);
            }

            if (gameTime.TotalGameTime.Milliseconds % GameValues.Mushroom1UPUpdateDelay == 0)
            {
                sprite.AdvanceFrame();
            }
            sprite.UpdateSpritePosition(parent.Position);
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
