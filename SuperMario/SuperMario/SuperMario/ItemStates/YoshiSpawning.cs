using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.ItemStates
{
    public class YoshiSpawning : IItemState
    {
        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }

        private AnimatedSprite sprite;
        private Item parent;
        private int yoshiSpawnBuffer;


        public YoshiSpawning(Item parent)
        {
            this.parent = parent;
            Initialize();
        }

        public void Initialize()
        {
            parent.IsYoshi = true;
            sprite = AnimatedSpriteFactory.Instance.BuildYoshiSpawningSprite(parent.Position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
            ScoreSprite = new ScoreSprite("", parent.Position, false);
            yoshiSpawnBuffer = 0;

            SoundManager.Instance.PlayYoshiSound();
        }

        public void Update(GameTime gameTime)
        {
            if (yoshiSpawnBuffer > 60)
            {
                yoshiSpawnBuffer = 0;
                parent.Position = new Vector2(parent.Position.X, parent.Position.Y - 16);
                parent.ItemState = new YoshiIdle(parent);
                parent.CollisionRectangle = parent.CollisionRectangle;
                parent.FinishedYoshiSpawning = true;
            }
            yoshiSpawnBuffer++;

            if (gameTime.TotalGameTime.Milliseconds % GameValues.YoshiSpawningUpdateDelay == 0)
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
