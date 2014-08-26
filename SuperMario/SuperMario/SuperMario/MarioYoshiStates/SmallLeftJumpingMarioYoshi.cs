using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;
using SuperMario.MarioStates;

namespace SuperMario.MarioStates
{
    public class SmallLeftJumpingMarioYoshi : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private bool jumpPressed;
        private int jumpTimer;
        private IPlayableObject playableObject;

        public SmallLeftJumpingMarioYoshi(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            if (!playableObject.IsJumping)
            {
                SoundManager.Instance.SmallJump.Play();
            }

            playableObject.IsJumping = true;
            playableObject.InAir = true;
            jumpPressed = true;
            jumpTimer = GameValues.MarioStateJumperTimer;

            sprite = AnimatedSpriteFactory.Instance.BuildSmallLeftJumpingMarioYoshiSprite(playableObject.Position);
            playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
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
                    playableObject.PlayableObjectState = new SmallRightWalkingMarioYoshi(playableObject);
                }
                else if (playableObject.Velocity.X < 0)
                {
                    playableObject.PlayableObjectState = new SmallLeftWalkingMarioYoshi(playableObject);
                }
                else
                {
                    playableObject.PlayableObjectState = new SmallLeftIdleMarioYoshi(playableObject);
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
            playableObject.Acceleration = new Vector2(GameValues.MarioHorizontalAcceleration, playableObject.Acceleration.Y);
        }

        public void LeftInput()
        {
            playableObject.Acceleration = new Vector2(-GameValues.MarioHorizontalAcceleration, playableObject.Acceleration.Y);
        }

        public void DownInput()
        {
            playableObject.Acceleration = new Vector2(-GameValues.MarioHorizontalAcceleration, playableObject.Acceleration.Y);
            NoInput();
        }

        public void RunButtonInput()
        {

        }

        public void JumpButtonInput()
        {
        }

        public void NoInput()
        {
            if (playableObject.Velocity.X + playableObject.Acceleration.X <= 0)
            {
                playableObject.Velocity = new Vector2(0.0f, playableObject.Velocity.Y);
                playableObject.Acceleration = new Vector2(0.0f, playableObject.Acceleration.Y);
            }
            else
            {
                playableObject.Acceleration = new Vector2(-GameValues.MarioHorizontalAcceleration, playableObject.Acceleration.Y);
            }
        }

        public void TakeDamage()
        {
            playableObject.PlayableObjectState = new SmallLeftJumpingMario(playableObject);
        }

        public void PickUpPowerup()
        {
            playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 6);
            playableObject.PlayableObjectState = new BigLeftJumpingMarioYoshi(playableObject);
        }

        public void PickUpStar()
        {
            playableObject.StarPower = true;
        }
    }
}
