using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.ItemStates
{
    class YoshiGreenEgg : IItemState
    {
        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        private Item parent;
        private float stoppingPosition;

        private AnimatedSprite sprite;

        public YoshiGreenEgg(Item parent)
        {
            this.parent = parent;
        }

        public void Initialize()
        {
            parent.IsYoshi = true;
            ScoreValue = GameValues.YoshiGreenEggScoreValue;
            sprite = AnimatedSpriteFactory.Instance.BuildYoshiGreenEggSprite(parent.Position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
            ScoreSprite = new ScoreSprite("", parent.Position, false);
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
                    parent.Velocity = Vector2.Zero;
                }

                CollisionRectangle = sprite.SpriteDestinationRectangle;
                parent.Position = new Vector2(parent.Position.X, parent.Position.Y + parent.Velocity.Y);
            }

            if (parent.FinishedSpawning)
            {
                parent.ItemState = new YoshiGreenEggCracked(parent);
            }

            sprite.UpdateSpritePosition(parent.Position);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}
