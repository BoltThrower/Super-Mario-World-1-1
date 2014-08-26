using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.ItemStates
{
    public class YoshiIdle : IItemState
    {
        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }

        private AnimatedSprite sprite;
        private Item parent;

        public YoshiIdle(Item parent)
        {
            this.parent = parent;
            Initialize();
        }

        public void Initialize()
        {
            parent.IsYoshi = true;
            sprite = AnimatedSpriteFactory.Instance.BuildYoshiIdleSprite(parent.Position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
            ScoreSprite = new ScoreSprite("", parent.Position, false);

        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Milliseconds % GameValues.YoshiIdleUpdateDelay == 0)
            {
                sprite.AdvanceFrame();
            }
            sprite.UpdateSpritePosition(parent.Position);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}
