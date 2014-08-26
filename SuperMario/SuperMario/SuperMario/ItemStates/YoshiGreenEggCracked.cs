using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.ItemStates
{
    public class YoshiGreenEggCracked : IItemState
    {
        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }

        private AnimatedSprite sprite;
        private Item parent;
        private int yoshiCrackedEggBuffer;

        public YoshiGreenEggCracked(Item parent)
        {
            this.parent = parent;
            Initialize();
        }

        public void Initialize()
        {
            parent.IsYoshi = true;
            sprite = AnimatedSpriteFactory.Instance.BuildYoshiGreenEggCrackedSprite(parent.Position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
            ScoreSprite = new ScoreSprite("", parent.Position, false);
            yoshiCrackedEggBuffer = 0;

            SoundManager.Instance.PlayEggHatchingSound();
        }

        public void Update(GameTime gameTime)
        {
            if (yoshiCrackedEggBuffer > 30)
            {
                yoshiCrackedEggBuffer = 0;
                parent.ItemState = new YoshiSpawning(parent);
            }
            yoshiCrackedEggBuffer++;

            sprite.UpdateSpritePosition(parent.Position);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}
