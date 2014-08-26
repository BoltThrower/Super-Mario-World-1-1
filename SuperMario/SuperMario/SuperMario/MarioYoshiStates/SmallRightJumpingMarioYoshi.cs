using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    public class SmallRightJumpingMarioYoshi : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private bool jumpPressed;
        private int jumpTimer;
        private IPlayableObject playableObject;

        public SmallRightJumpingMarioYoshi(IPlayableObject playableObject)
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

            sprite = AnimatedSpriteFactory.Instance.BuildSmallRightJumpingMarioYoshiSprite(playableObject.Position);
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
                    playableObject.PlayableObjectState = new SmallRightIdleMarioYoshi(playableObject);
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
            playableObject.Acceleration = Vector2.Zero;
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
            playableObject.PlayableObjectState = new SmallRightJumpingMario(playableObject);
        }

        public void PickUpPowerup()
        {
            playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 6);
            playableObject.PlayableObjectState = new BigRightJumpingMarioYoshi(playableObject);
        }

        public void PickUpStar()
        {
            playableObject.StarPower = true;
        }
    }
}
