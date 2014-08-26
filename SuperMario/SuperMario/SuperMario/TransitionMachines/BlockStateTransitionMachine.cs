using System;
using SuperMario.Commands.BlockCommands;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class BlockStateTransitionMachine
    {	
	    public BlockStateTransitionMachine()
	    {          
	    }

        public void DynamicStateChange(String collisionDirection, Block block, IDynamicObject dynamicObject)
        {
            if (block.BlockState.ToString() == "SuperMario.BlockStates.QuestionBlock")
            {
                new QuestionBlockCollisionCommand(block).Execute();
            }

            else if (block.BlockState.ToString() == "SuperMario.BlockStates.CoinBrickBlock")
            {
                new CoinBrickBlockCollisionCommand(block).Execute();
            }

            else if (block.BlockState.ToString() == "SuperMario.BlockStates.BrickBlock")
            {
                new BrickBlockCollisionCommand(block).Execute();
            }
        }
    }       
}