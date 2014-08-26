using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    class BigRightCrouchingMario : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private bool crouchPressed;

        private IPlayableObject playableObject;

        public BigRightCrouchingMario(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            sprite = AnimatedSpriteFactory.Instance.BuildBigRightCrouchingMarioSprite();
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
            crouchPressed = true;
        }

        public void Update(GameTime gameTime)
        {
            sprite.UpdateSpritePosition(playableObject.Position);

            if (playableObject.Velocity.X > 0)
            {
                playableObject.PlayableObjectState = new BigRightWalkingMario(playableObject);
            }
            else if (playableObject.Velocity.X < 0)
            {
                playableObject.PlayableObjectState = new BigLeftWalkingMario(playableObject);
            }
            else if (!crouchPressed)
            {
                playableObject.PlayableObjectState = new BigRightIdleMario(playableObject);
            }
            crouchPressed = false;
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
            if (Mario.Instance.Acceleration.X != 0)
            {
                playableObject.Acceleration = new Vector2(0.0f, playableObject.Acceleration.Y);
            }
            crouchPressed = true;
        }

        public void RunButtonInput()
        {

        }

        public void JumpButtonInput()
        {
           
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
            playableObject.PlayableObjectState = new FireRightCrouchingMario(playableObject);
        }

        public void PickUpStar()
        {
            playableObject.StarPower = true;
        }
    }
}