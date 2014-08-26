using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    public class BigLeftIdleMarioYoshi : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private IPlayableObject playableObject;

        public BigLeftIdleMarioYoshi(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            sprite = AnimatedSpriteFactory.Instance.BuildBigLeftIdleMarioYoshiSprite(playableObject.Position);
            playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            sprite.UpdateSpritePosition(playableObject.Position);

            if (playableObject.Velocity.X > 0)
            {
                playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 3);
                playableObject.PlayableObjectState = new BigRightWalkingMarioYoshi(playableObject);
            }
            else if (playableObject.Velocity.X < 0)
            {
                playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 3);
                playableObject.PlayableObjectState = new BigLeftWalkingMarioYoshi(playableObject);
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
            if (playableObject.Velocity.X == 0)
            {
                playableObject.PlayableObjectState = new BigLeftTongueMarioYoshi(playableObject);
            }
        }

        public void JumpButtonInput()
        {
            playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 1);
            playableObject.Acceleration = new Vector2(playableObject.Acceleration.X, GameValues.MarioVerticalAcceleration);
            playableObject.PlayableObjectState = new BigLeftJumpingMarioYoshi(playableObject);
        }

        public void NoInput()
        {

        }

        public void TakeDamage()
        {
            if (playableObject.IsFire)
            {
                playableObject.PlayableObjectState = new FireLeftIdleMario(playableObject);
            }
            else
            {
                playableObject.PlayableObjectState = new BigLeftIdleMario(playableObject);
            }
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
