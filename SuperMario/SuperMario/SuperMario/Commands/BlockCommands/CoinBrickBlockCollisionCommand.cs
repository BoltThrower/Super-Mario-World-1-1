using Microsoft.Xna.Framework;
using SuperMario.BlockStates;
using SuperMario.Interfaces;
using System;

namespace SuperMario.Commands.BlockCommands
{
    class CoinBrickBlockCollisionCommand : ICommand
    {
        private IDynamicObject dynamicObject;

        public CoinBrickBlockCollisionCommand(IDynamicObject dynamicObject)
        {
            this.dynamicObject = dynamicObject;
        }
 
        public void Execute()
        {
            Block block = dynamicObject as Block;

            if (block.Item.ItemState.ToString() != "SuperMario.ItemStates.NoItem")
            {
                if (block.CoinCount <= 1)
                {
                    block.BlockState = new UsedBlock(block.Position, block);
                }

                block.Item.ItemState.ScoreSprite.ScoringOn = true;
                HUD.Instance.CoinHUDCounter += 1;
                HUD.Instance.ScoreHUDCounter += block.Item.ItemState.ScoreValue;
                SoundManager.Instance.Coin.Play();
                block.CoinCount--;
            }

            block.IsMoving = true;
            block.Velocity = new Vector2(block.Velocity.X, GameValues.BlockCollisionCommandYVelocity);
        }
    }
}
