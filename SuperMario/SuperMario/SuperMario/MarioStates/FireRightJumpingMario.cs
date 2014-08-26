using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    class FireRightJumpingMario : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private bool jumpPressed;
        private int jumpTimer;
        private IPlayableObject playableObject;

        public FireRightJumpingMario(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            if (!this.playableObject.IsJumping)
            {
                SoundManager.Instance.BigJump.Play();
            }

            this.playableObject.IsJumping = true;
            this.playableObject.InAir = true;
            jumpPressed = true;
            jumpTimer = GameValues.MarioStateJumperTimer;

            sprite = AnimatedSpriteFactory.Instance.BuildFireRightJumpingMarioSprite();
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            // fire right jumping mario only has one animation frame and doesn't need to call AdvanceFrame()
            sprite.UpdateSpritePosition(playableObject.Position);

            jumpTimer--;
            if (jumpTimer <= 0)
            {
                jumpPressed = false;
            }
            if (!jumpPressed)
            {
                playableObject.Acceleration = new Vector2(playableObject.Acceleration.X, 0.0f);
            }

            if (!playableObject.InAir)
            {
                if (playableObject.Velocity.X > 0)
                {
                    playableObject.PlayableObjectState = new FireRightWalkingMario(playableObject);
                }
                else if (playableObject.Velocity.X < 0)
                {
                    playableObject.PlayableObjectState = new FireLeftWalkingMario(playableObject);
                }
                else
                {
                    playableObject.PlayableObjectState = new FireRightIdleMario(playableObject);
                }
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
            playableObject.Acceleration = new Vector2(GameValues.MarioHorizontalAcceleration / GameValues.MarioStateAccelerationOffset, playableObject.Acceleration.Y);
        }

        public void LeftInput()
        {
            playableObject.Acceleration = new Vector2(-GameValues.MarioHorizontalAcceleration / GameValues.MarioStateAccelerationOffset, playableObject.Acceleration.Y);
        }

        public void DownInput()
        {

        }

        public void RunButtonInput()
        {
            playableObject.PlayableObjectState = new FireRightFireballThrowingMario(playableObject);
        }

        public void JumpButtonInput()
        {

        }

        public void NoInput()
        {
            playableObject.Acceleration = new Vector2(playableObject.Acceleration.X, 0.0f);
        }

        public void TakeDamage()
        {
            playableObject.PlayableObjectState = new SmallRightJumpingMario(playableObject);
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