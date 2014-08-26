using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.ItemStates
{
    class PowerUp : IItemState
    {
        public Rectangle CollisionRectangle { get; set; }
        public int ScoreValue { get; set; }
        public ScoreSprite ScoreSprite { get; set; }

        private AnimatedSprite mushroomSprite, fireFlowerSprite;
        private float stoppingPosition;
        private Item parent;

        public PowerUp(Item parent)
        {
            this.parent = parent;
        }

        public void Initialize()
        {
            ScoreValue = GameValues.PowerUpScoreValue;
            ScoreSprite = new ScoreSprite(ScoreValue.ToString(), parent.Position, false);
            mushroomSprite = AnimatedSpriteFactory.Instance.BuildMushroomSprite(parent.Position);
            fireFlowerSprite = AnimatedSpriteFactory.Instance.BuildFireFlowerSprite(parent.Position);
            stoppingPosition = parent.Position.Y - mushroomSprite.Texture.Height;
            CollisionRectangle = mushroomSprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            if (parent.Spawning)
            {
                if (parent.Position.Y <= stoppingPosition)
                {
                    parent.Spawning = false;
                    parent.FinishedSpawning = true;
                    parent.CollisionRectangle = CollisionRectangle;
                    if (Level.Instance.PowerUpState) // mushroom
                    {
                        parent.Velocity = Vector2.Zero;
                    }
                    else // fireflower
                    {
                        parent.Velocity = new Vector2(parent.MaxVelocity.X, 0.0f);

                    }
                }

                CollisionRectangle = mushroomSprite.SpriteDestinationRectangle;
                parent.Position = new Vector2(parent.Position.X, parent.Position.Y + parent.Velocity.Y);
            }

            if (parent.FinishedSpawning && !Level.Instance.PowerUpState)
            {
                Physics.Move(parent);
            }

            mushroomSprite.UpdateSpritePosition(parent.Position);
            fireFlowerSprite.AdvanceFrame();
            fireFlowerSprite.UpdateSpritePosition(parent.Position);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Level.Instance.PowerUpState)
            {
                // fire flower
                fireFlowerSprite.Draw(spriteBatch, gameTime);
            }
            else
            {
                // mushroom
                mushroomSprite.Draw(spriteBatch, gameTime);
            }

            if (ScoreSprite.ScoringOn)
            {
                ScoreSprite.Draw(spriteBatch, gameTime);
            }
        }
    }
}
