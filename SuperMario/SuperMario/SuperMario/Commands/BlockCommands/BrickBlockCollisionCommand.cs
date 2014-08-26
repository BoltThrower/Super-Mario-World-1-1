using Microsoft.Xna.Framework;
using SuperMario.BlockStates;
using SuperMario.Interfaces;
using System;

namespace SuperMario.Commands.BlockCommands
{
    class BrickBlockCollisionCommand : ICommand
    {
        private IDynamicObject dynamicObject;

        public BrickBlockCollisionCommand(IDynamicObject dynamicObject)
        {
            this.dynamicObject = dynamicObject;
        }

        public void Execute()
        {
            Block block = dynamicObject as Block;

            if (block.Item.ItemState.ToString() == "SuperMario.ItemStates.NoItem" && block.CoinCount == 0 && Mario.Instance.IsBig)
            {
                SoundManager.Instance.BlockBreaking.Play();
                block.BlockState = new BrickBlockExplode(block.Position, block);
                block.CollisionRectangle = new Rectangle();
            }
            else if (block.Item.ItemState.ToString() != "SuperMario.ItemStates.NoItem" && block.CoinCount == 0)
            {
                SoundManager.Instance.PowerUpAppears.Play();
                block.BlockState = new UsedBlock(block.Position, block);
            }
            else
            {
                SoundManager.Instance.BlockBump.Play();
            }

            block.IsMoving = true;
            block.Velocity = new Vector2(block.Velocity.X, GameValues.BlockCollisionCommandYVelocity);
        }
    }
}
