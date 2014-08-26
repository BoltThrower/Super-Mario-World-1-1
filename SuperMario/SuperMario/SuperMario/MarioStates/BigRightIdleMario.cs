using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    class BigRightIdleMario : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private IPlayableObject playableObject;

        public BigRightIdleMario(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            sprite = AnimatedSpriteFactory.Instance.BuildBigRightIdleMarioSprite();
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            // big right idle mario only has one animation frame and doesn't need to call AdvanceFrame()
            sprite.UpdateSpritePosition(playableObject.Position);

            if (playableObject.Velocity.X > 0)
            {
                playableObject.PlayableObjectState = new BigRightWalkingMario(playableObject);
            }
            else if (playableObject.Velocity.X < 0)
            {
                playableObject.PlayableObjectState = new BigLeftWalkingMario(playableObject);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!playableObject.StarPower)
            {
                sprite.Draw(spriteBatch, gameTime);
            }
            else
            {
                sprite.StarDraw(spriteBatch, gameTime);
            }
        }

        public void RightInput()
        {
            playableObject.Acceleration = new Vector2(GameValues.MarioHorizontalAcceleration, playableObject.Acceleration.Y);
        }

        public void LeftInput()
        {
            playableObject.Acceleration = new Vector2(-GameValues.MarioHorizontalAcceleration, playableObject.Acceleration.Y);
        }

        public void DownInput()
        {
            if (playableObject.Acceleration.X != 0)
            {
                playableObject.Acceleration = new Vector2(0.0f, playableObject.Acceleration.Y);
            }
            else
            {
                playableObject.PlayableObjectState = new BigRightCrouchingMario(playableObject);
            }
        }

        public void RunButtonInput()
        {

        }

        public void JumpButtonInput()
        {
            playableObject.Acceleration = new Vector2(playableObject.Acceleration.X, GameValues.MarioVerticalAcceleration);
            playableObject.PlayableObjectState = new BigRightJumpingMario(playableObject);
        }

        public void NoInput()
        {

        }

        public void TakeDamage()
        {
            playableObject.PlayableObjectState = new SmallRightIdleMario(playableObject);
        }

        public void PickUpPowerup()
        {
            playableObject.PlayableObjectState = new FireRightIdleMario(playableObject);
        }

        public void PickUpStar()
        {
            playableObject.StarPower = true;
        }
    }
}