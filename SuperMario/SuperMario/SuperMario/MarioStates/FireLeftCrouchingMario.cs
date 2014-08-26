using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    class FireLeftCrouchingMario : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private bool crouchPressed;
        private IPlayableObject playableObject;

        public FireLeftCrouchingMario(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            sprite = AnimatedSpriteFactory.Instance.BuildFireLeftCrouchingMarioSprite();
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
            crouchPressed = true;
        }

        public void Update(GameTime gameTime)
        {
            // fire left crouching mario only has one animation frame and doesn't need to call AdvanceFrame()

            sprite.UpdateSpritePosition(playableObject.Position);

            if (playableObject.Velocity.X > 0)
            {
                playableObject.PlayableObjectState = new FireRightWalkingMario(playableObject);
            }
            else if (playableObject.Velocity.X < 0)
            {
                playableObject.PlayableObjectState = new FireLeftWalkingMario(playableObject);
            }
            else if (!crouchPressed)
            {
                playableObject.PlayableObjectState = new FireLeftIdleMario(playableObject);
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
            if (playableObject.Acceleration.X != 0)
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
            playableObject.PlayableObjectState = new SmallLeftIdleMario(playableObject);
        }

        public void PickUpPowerup()
        {

        }

        public void PickUpStar()
        {
            playableObject.StarPower = true;
        }
    }
}