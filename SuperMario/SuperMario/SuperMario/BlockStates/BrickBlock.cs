using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.BlockStates
{
    class BrickBlock : IBlockState
    {
        private AnimatedSprite sprite;
        private Vector2 position;
        public Rectangle CollisionRectangle { get; set; }
        public BlockStateTransitionMachine BlockStateTransitionMachine { get; set; }
        private Block parent;

        public BrickBlock(Vector2 position, Block parent, string color)
        {
            this.parent = parent;
            this.position = position;
            if (color == "Red")
            {
                sprite = AnimatedSpriteFactory.Instance.BuildBrickBlockSprite(position);
            }
            else
            {
                sprite = AnimatedSpriteFactory.Instance.BuildBrickBlockBlueSprite(position);
            }
            sprite.UpdateSpritePosition(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
            BlockStateTransitionMachine = new BlockStateTransitionMachine();
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            // brick block only has one animation frame and doesn't need to call AdvanceFrame()
            sprite.UpdateSpritePosition(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
    }
}