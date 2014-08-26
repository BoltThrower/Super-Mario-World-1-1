using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.BlockStates
{
    class QuestionBlock : IBlockState
    {
        private AnimatedSprite sprite;
        public Rectangle CollisionRectangle { get; set; }
        public BlockStateTransitionMachine BlockStateTransitionMachine { get; set; }
        private Block parent;

        public QuestionBlock(Vector2 position, Block parent)
        {
            this.parent = parent;
            sprite = AnimatedSpriteFactory.Instance.BuildQuestionBlockSprite(position);
            sprite.UpdateSpritePosition(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
            BlockStateTransitionMachine = new BlockStateTransitionMachine();
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            if (gameTime.TotalGameTime.Milliseconds % GameValues.QuestionBlockUpdateDelay == 0)
            {
                sprite.AdvanceFrame();
            }
            sprite.UpdateSpritePosition(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}