using System;
using Microsoft.Xna.Framework;
using SuperMario.Commands.GenericCollisionCommands;
using SuperMario.Commands.ItemCommands;
using SuperMario.Interfaces;

namespace SuperMario
{
    class ItemStateTransitionMachine
    {
        public ItemStateTransitionMachine()
        {
        }

        public void DynamicStateChange(String collisionDirection, Item item, IDynamicObject dynamicObject)
        {
            IPlayableObject playableObject = dynamicObject as IPlayableObject;

            if (dynamicObject.ToString() == "SuperMario.Mario")
            {
                new ItemIsConsumedCommand(playableObject, item).Execute();
            }

            else if (dynamicObject.ToString() == "SuperMario.Block" || dynamicObject.ToString() == "SuperMario.Pipe")
            {
                // Stars are supposed to ignore collisions with blocks
                bool starBlockCollision = false;

                if (dynamicObject.ToString() == "SuperMario.Block")
                {
                    Block block = dynamicObject as Block;

                    if (block.BlockState.ToString() != "SuperMario.BlockStates.MetalBlock" && item.ItemState.ToString() == "SuperMario.ItemStates.Star")
                    {
                        starBlockCollision = true;
                    }

                    if (block.IsMoving)
                    {
                        item.Velocity = new Vector2(-item.Velocity.X, item.Velocity.Y);
                    }
                }

                if (!starBlockCollision)
                {
                    if (collisionDirection == GameValues.CollisionDirectionTop)
                    {
                        new GenericDynamicGroundCollisionCommand(item, dynamicObject).Execute();
                    }

                    else if (collisionDirection == GameValues.CollisionDirectionBottom || item.CollisionRectangle.Center.Y < dynamicObject.CollisionRectangle.Top)
                    {
                        if (item.ItemState.ToString() == "SuperMario.ItemStates.Star")
                        {
                            item.Velocity = new Vector2(item.Velocity.X, Vector2.Zero.Y);
                            item.InAir = false;
                        }

                        new GenericDynamicBottomCollisionCommand(item, dynamicObject).Execute();
                    }

                    else if (collisionDirection == GameValues.CollisionDirectionLeft || collisionDirection == GameValues.CollisionDirectionRight)
                    {
                        new GenericSideCollisionCommand(item).Execute();
                    }
                }
            }
        }

        public void StaticStateChange(String location, Item item, IStaticObject staticObject)
        {
            if (location == GameValues.CollisionDirectionTop)
            {
                new GenericStaticGroundCollisionCommand(item, staticObject).Execute();
            }

            else if (location == GameValues.CollisionDirectionBottom)
            {
                if (item.ItemState.ToString() == "SuperMario.ItemStates.Star")
                {
                    item.Velocity = new Vector2(item.Velocity.X, Vector2.Zero.Y);
                    item.InAir = false;
                }

                new GenericStaticBottomCollisionCommand(item, staticObject).Execute();
            }

            else if ((location == GameValues.CollisionDirectionRight || location == GameValues.CollisionDirectionLeft) && staticObject.ToString() != "SuperMario.FloorTile")
            {
                new GenericSideCollisionCommand(item).Execute();
            }

            else
            {
                new GenericStaticBottomCollisionCommand(item, staticObject).Execute();
                item.InAir = false;
            }
        }
    }
}
