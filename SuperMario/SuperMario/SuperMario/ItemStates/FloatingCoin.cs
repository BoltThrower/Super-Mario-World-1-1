using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.ItemStates
{
    class FloatingCoin : IItemState
    {
        private AnimatedSprite sprite;

        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        private Item parent;

        public FloatingCoin(Item parent)
        {
            this.parent = parent;
        }

        public void Initialize()
        {
            ScoreValue = GameValues.FloatingCoinScoreValue;
            sprite = AnimatedSpriteFactory.Instance.BuildFloatingCoinSprite(parent.Position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
            ScoreSprite = new ScoreSprite(ScoreValue.ToString(), parent.Position, false);
        }

        public void Update(GameTime gameTime)
        {
            parent.CollisionRectangle = CollisionRectangle;

            if (gameTime.TotalGameTime.Milliseconds % GameValues.FloatCoinUpdateDelay == 0)
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
