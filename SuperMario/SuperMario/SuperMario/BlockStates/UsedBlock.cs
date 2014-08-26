using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.BlockStates
{
    class UsedBlock : IBlockState
    {
        private AnimatedSprite sprite;
        public Rectangle CollisionRectangle { get; set; }
        public BlockStateTransitionMachine BlockStateTransitionMachine { get; set; }
        private Block parent;

        public UsedBlock(Vector2 position, Block parent)
        {
            this.parent = parent;
            sprite = AnimatedSpriteFactory.Instance.BuildUsedBlockSprite(position);
            sprite.UpdateSpritePosition(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
            BlockStateTransitionMachine = new BlockStateTransitionMachine();
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            // used block only has one animation frame and doesn't need to call AdvanceFrame()
            sprite.UpdateSpritePosition(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}