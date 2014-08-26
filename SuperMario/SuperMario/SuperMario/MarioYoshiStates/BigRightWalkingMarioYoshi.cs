using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;
using SuperMario.MarioStates;

namespace SuperMario.MarioStates
{
    public class BigRightWalkingMarioYoshi : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private IPlayableObject playableObject;

        public BigRightWalkingMarioYoshi(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            sprite = AnimatedSpriteFactory.Instance.BuildBigRightWalkingMarioYoshiSprite(playableObject.Position);
            playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Milliseconds % GameValues.MarioStateWalkingUpdateDelay == 0)
            {
                sprite.AdvanceFrame();
            }
            sprite.UpdateSpritePosition(playableObject.Position);

            if (playableObject.Velocity.X == 0)
            {
                playableObject.Acceleration = Vector2.Zero;
                playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y + 3);
                playableObject.PlayableObjectState = new BigRightIdleMarioYoshi(playableObject);
            }
            else if (playableObject.Velocity.X < 0)
            {
                playableObject.Acceleration = Vector2.Zero;
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
            playableObject.Acceleration = new Vector2(-GameValues.MarioHorizontalAcceleration, playableObject.Acceleration.Y);
            NoInput();
        }

        public void RunButtonInput()
        {

        }

        public void JumpButtonInput()
        {
            playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 2);
            playableObject.Acceleration = new Vector2(0.0f, GameValues.MarioVerticalAcceleration);
            playableObject.PlayableObjectState = new BigRightJumpingMarioYoshi(playableObject);
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
            if (playableObject.IsFire)
            {
                playableObject.PlayableObjectState = new FireRightWalkingMario(playableObject);
            }
            else
            {
                playableObject.PlayableObjectState = new BigRightWalkingMario(playableObject);
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
