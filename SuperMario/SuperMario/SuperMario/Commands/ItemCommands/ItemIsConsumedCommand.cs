using Microsoft.Xna.Framework;
using SuperMario.Interfaces;
using SuperMario.ItemStates;

namespace SuperMario.Commands.ItemCommands
{
    class ItemIsConsumedCommand : ICommand
    {
        private IPlayableObject playableObject;
        private IDynamicObject dynamicObject;

        public ItemIsConsumedCommand(IPlayableObject playableObject, IDynamicObject dynamicObject)
        {
            this.playableObject = playableObject;
            this.dynamicObject = dynamicObject;
        }

        public void Execute()
        {
            Item item = dynamicObject as Item;

            if (!item.IsYoshi || (item.FinishedYoshiSpawning && playableObject.OnYoshi))
            {
                Item noItem = new Item(item.Position, "NoItem");

                // Mario consumes the item
                noItem.ItemState.ScoreSprite = new ScoreSprite(item.ItemState.ScoreSprite.ScoreValue(), item.Position, true);
                item.Position = Vector2.Zero;
                item.Velocity = Vector2.Zero;
                item.CollisionRectangle = GameValues.EmptyCollisionRectangle;
                item.ItemState = noItem.ItemState;
                item.FinishedYoshiSpawning = false;
            }
        }
    }
}
