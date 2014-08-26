using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    class BigRightJumpingMario : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private bool jumpPressed;
        private int jumpTimer;

        private IPlayableObject playableObject;

        public BigRightJumpingMario(IPlayableObject playableObject)
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

            sprite = AnimatedSpriteFactory.Instance.BuildBigRightJumpingMarioSprite();
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            // big right jumping mario only has one animation frame and doesn't need to call AdvanceFrame()
            sprite.UpdateSpritePosition(playableObject.Position);

            jumpTimer--;
            if (jumpTimer <= 0)
            {
                jumpPressed = false;
            }
            if (!jumpPressed)
            {
                Mario.Instance.Acceleration = new Vector2(playableObject.Acceleration.X, 0.0f);
            }

            if (!Mario.Instance.InAir)
            {
                if (playableObject.Velocity.X > 0)
                {
                    playableObject.PlayableObjectState = new BigRightWalkingMario(playableObject);
                }
                else if (playableObject.Velocity.X < 0)
                {
                    playableObject.PlayableObjectState = new BigLeftWalkingMario(playableObject);
                }
                else
                {
                    playableObject.PlayableObjectState = new BigRightIdleMario(playableObject);
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
            playableObject.PlayableObjectState = new FireRightJumpingMario(playableObject);
        }

        public void PickUpStar()
        {
            playableObject.StarPower = true;
        }
    }
}