using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;
using SuperMario.ItemStates;
using System.Collections.Generic;

namespace SuperMario
{
    public class Item : IDynamicObject
    {
        public IItemState ItemState { get; set; }
        public IItemState PreviousItemState { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 MaxVelocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        public bool Spawning { get; set; }
        public bool FinishedSpawning { get; set; }
        public bool InAir { get; set; }
        public bool IsYoshi { get; set; }
        public bool FinishedYoshiSpawning { get; set; }

        private ItemStateTransitionMachine itemStateTransitionMachine;
        private Dictionary<string, IItemState> lookupTable;

        public Item(Vector2 position, string itemState)
        {
            lookupTable = new Dictionary<string, IItemState>();
            PopulateLookup();

            Position = position;
            Velocity = Vector2.Zero;
            MaxVelocity = new Vector2(GameValues.ItemInitialMaxXVelocity, GameValues.PhysicsMaxYVelocity);
            Acceleration = Vector2.Zero;
            itemStateTransitionMachine = new ItemStateTransitionMachine();
            Spawning = false;
            FinishedSpawning = false;
            FinishedYoshiSpawning = false;

            if (lookupTable.ContainsKey(itemState))
            {
                ItemState = lookupTable[itemState];
            }
            else
            {
                ItemState = new NoItem();
            }

            ItemState.Initialize();
            PreviousItemState = ItemState;

            CollisionRectangle = GameValues.EmptyCollisionRectangle;
            Velocity = new Vector2(MaxVelocity.X, 0);
        }

        public void HandleDynamicCollision(string collisionDirection, IDynamicObject dynamicObject)
        {
            itemStateTransitionMachine.DynamicStateChange(collisionDirection, this, dynamicObject);
        }

        public void HandleStaticCollision(string collisionLocation, IStaticObject staticObject)
        {
            itemStateTransitionMachine.StaticStateChange(collisionLocation, this, staticObject);
        }

        public void Update(GameTime gameTime)
        {
            ItemState.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            ItemState.Draw(spriteBatch, gameTime);
        }

        private void PopulateLookup()
        {
            lookupTable.Add("Powerup", new PowerUp(this));
            lookupTable.Add("RotatingCoin", new RotatingCoin(this));
            lookupTable.Add("FloatingCoin", new FloatingCoin(this));
            lookupTable.Add("Star", new Star(this));
            lookupTable.Add("1up", new Mushroom1Up(this));
            lookupTable.Add("YoshiGreenEgg", new YoshiGreenEgg(this));
        }
    }
}
