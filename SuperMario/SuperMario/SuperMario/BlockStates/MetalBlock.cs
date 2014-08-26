using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.BlockStates
{
    class MetalBlock : IBlockState
    {
        private AnimatedSprite sprite;
        public Rectangle CollisionRectangle { get; set; }
        public BlockStateTransitionMachine BlockStateTransitionMachine { get; set; }
        private Block parent;

        public MetalBlock(Vector2 position, Block parent)
        {
            this.parent = parent;
            sprite = AnimatedSpriteFactory.Instance.BuildMetalBlockSprite(position);
            sprite.UpdateSpritePosition(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
            BlockStateTransitionMachine = new BlockStateTransitionMachine();
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            // metal block only has one animation frame and doesn't need to call AdvanceFrame().
            sprite.UpdateSpritePosition(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}
