using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class Fireball : IDynamicObject
    {
        private AnimatedSprite sprite;

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 MaxVelocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        public bool IsAlive { get; set; }
        public bool IsEnemyFireball { get; set; }

        private FireballStateTransitionMachine fireballStateTransitionMachine;
        private int expirationBuffer;

        public Fireball(Vector2 position, bool isRight, bool isEnemyFireball)
        {
            expirationBuffer = GameValues.FireBallExpirationBuffer;
            fireballStateTransitionMachine = new FireballStateTransitionMachine();
            sprite = AnimatedSpriteFactory.Instance.BuildFireballSprite(position);
            CollisionRectangle = sprite.SpriteDestinationRectangle;

            this.Position = new Vector2(position.X, position.Y);

            // isRight is determined by if Mario is facing right or left, which leads to which direction
            // this fireball should go.
            if (isRight)
            {
                this.Velocity = GameValues.FireBallVelocity;
                this.MaxVelocity = GameValues.FireBallMaxVelocity;
            }

            else
            {
                this.Velocity = -GameValues.FireBallVelocity;
                this.MaxVelocity = GameValues.FireBallMaxVelocity;
            }

            this.IsAlive = false;
            this.IsEnemyFireball = isEnemyFireball;
        }

        public void HandleDynamicCollision(string collisionLocation, IDynamicObject dynamicObjectState)
        {
            fireballStateTransitionMachine.DynamicStateChange(collisionLocation, this, dynamicObjectState);
        }

        public void HandleStaticCollision(string collisionLocation, IStaticObject staticObjectState)
        {
            fireballStateTransitionMachine.StaticStateChange(collisionLocation, this, staticObjectState);
        }

        public void Update(GameTime gameTime)
        {
            if (this.IsAlive)
            {
                expirationBuffer--;
                Physics.Move(this);
                sprite.AdvanceFrame();
                sprite.UpdateSpritePosition(this.Position);

                // Make the fireball die if it's far enough away from Mario.
                if (this.Position.X > Mario.Instance.Position.X + GameValues.FireBallLeftMaxVisiblePosition || this.Position.X < Mario.Instance.Position.X - GameValues.FireBallRightMaxVisiblePosition)
                {
                    this.IsAlive = false;
                }

                if (expirationBuffer <= 0)
                {
                    this.IsAlive = false;
                    expirationBuffer = GameValues.FireBallExpirationBuffer;
                }
            }

            else
            {
                this.CollisionRectangle = GameValues.EmptyCollisionRectangle;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (this.IsAlive)
            {
                sprite.Draw(spriteBatch, gameTime);
            }
        }
    }
}
