using Microsoft.Xna.Framework;
using SuperMario.BlockStates;
using SuperMario.Interfaces;
using System;

namespace SuperMario.Commands.BlockCommands
{
    class QuestionBlockCollisionCommand : ICommand
    {
        private IDynamicObject dynamicObject;

        public QuestionBlockCollisionCommand(IDynamicObject dynamicObject)
        {
            this.dynamicObject = dynamicObject;
        }

        public void Execute()
        {
            Block block = dynamicObject as Block;

            if (block.Item.ItemState.ToString() == "SuperMario.ItemStates.RotatingCoin")
            {
                SoundManager.Instance.Coin.Play();
                block.Item.ItemState.ScoreSprite.ScoringOn = true;
                HUD.Instance.CoinHUDCounter += 1;
                HUD.Instance.ScoreHUDCounter += block.Item.ItemState.ScoreValue;
            }
            else
            {
                SoundManager.Instance.PowerUpAppears.Play();
            }

            block.BlockState = new UsedBlock(block.Position, block);
            block.IsMoving = true;
            block.Velocity = new Vector2(block.Velocity.X, GameValues.BlockCollisionCommandYVelocity);
        }
    }
}
