using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.BlockStates;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class Block : IDynamicObject
    {
        public IBlockState BlockState { get; set; }
        public Vector2 InitialPosition { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 MaxVelocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        public Item Item { get; set; }
        public bool IsMoving { get; set; }
        public int CoinCount { get; set; }

        private BlockStateTransitionMachine blockStateTransitionMachine;

        public Block(Vector2 position, string blockState, string itemType)
        {
            Position = position;
            Acceleration = Vector2.Zero;
            InitialPosition = position;
            blockStateTransitionMachine = new BlockStateTransitionMachine();
            IsMoving = false;

            if (blockState == "Brick")
            {
                BlockState = new BrickBlock(position, this, "Red");
            }
            else if (blockState == "BrickBlue")
            {
                BlockState = new BrickBlock(position, this, "Blue");
            }
            else if (blockState == "Question")
            {
                BlockState = new QuestionBlock(position, this);
            }
            else if (blockState == "Metal")
            {
                this.BlockState = new MetalBlock(position, this);
            }
            else if (blockState == "CoinBrick")
            {
                BlockState = new CoinBrickBlock(position, this);
                CoinCount = GameValues.BlockCoinBrickCoinAmount;
            }
            else if (blockState == "Used")
            {
                BlockState = new UsedBlock(position, this);
            }

            CollisionRectangle = BlockState.CollisionRectangle;

            if (itemType == "RotatingCoin")
            {
                Item = new Item(new Vector2(Position.X + GameValues.BlockRotatingCoinInitialXPositionOffset, Position.Y), itemType);
            }
            else
            {
                Item = new Item(Position, itemType);
            }
        }

        public void HandleDynamicCollision(string collisionDirection, IDynamicObject dynamicObject)
        {
            // The state of blocks should not change at all unless the dynamic object is Mario.
            if (!Mario.Instance.InCoinRoom && dynamicObject.ToString() == "SuperMario.Mario" && collisionDirection == "Bottom")
            {
                blockStateTransitionMachine.DynamicStateChange(collisionDirection, this, dynamicObject);
            }
        }

        public void HandleStaticCollision(string collisionLocation, IStaticObject staticObjectState)
        {
            // Will never collide with a static object
        }

        public void Update(GameTime gameTime)
        {
            float yAcceleration = GameValues.BlockItemInitialYAcceleration;
            BlockState.Update(gameTime, Position);

            if (IsMoving == true)
            {
                Position = new Vector2(Position.X, Position.Y + Velocity.Y);
                Velocity = new Vector2(Velocity.X, Velocity.Y + yAcceleration);

                if (Position.Equals(InitialPosition))
                {
                    IsMoving = false;
                    Item.Spawning = true;
                 
                    if (Item.ItemState.ToString() != "SuperMario.ItemStates.RotatingCoin")
                    {
                        Item.Velocity = GameValues.BlockItemInitialSpawnVelocity;
                    }

                    else
                    {
                        Item.Velocity = GameValues.BlockRotatingCoinSpawningVelocity;
                    }
                }

                CollisionRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.CollisionRectangle.Width, (int)this.CollisionRectangle.Height);
            }

            Item.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Item.ItemState.ToString() == "SuperMario.ItemStates.RotatingCoin")
            {
                if (Item.CollisionRectangle.Center.Y < Position.Y - GameValues.BlockRotatingCoinStoppingYPosition)
                {
                    Item.Draw(spriteBatch, gameTime);
                }
            }

            else if (Item.Spawning || Item.FinishedSpawning)
            {
                Item.Draw(spriteBatch, gameTime);
            }

            BlockState.Draw(spriteBatch, gameTime);
        }


    }
}