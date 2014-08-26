using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    public class BigLeftTongueMarioYoshi : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private int frame;
        private IPlayableObject playableObject;

        public BigLeftTongueMarioYoshi(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            this.playableObject.ReverseYoshiSprites = false;
            frame = 0;
            sprite = AnimatedSpriteFactory.Instance.BuildBigLeftTongueStartMarioYoshi(playableObject.Position, frame);
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
            this.playableObject.IsYoshiTongueLeft = true;

            SoundManager.Instance.PlayYoshiTongueSound();
        }

        public void HandleTongueStates()
        {
            // There are X Offsets due to how we need to shift Mario to the right as the tongue gets bigger to the left.
            // The Y Offsets are there due to the difference of height in the sprites.
            if (frame == 0 && playableObject.ReverseYoshiSprites)
            {
                playableObject.IsYoshiTongueLeft = false;

                if (playableObject.IsYoshiEating)
                {
                    playableObject.IsYoshiEating = false;
                    playableObject.IsYoshiFinishedEating = true;
                    sprite = AnimatedSpriteFactory.Instance.BuildBigLeftFullMouthMarioYoshi(playableObject.Position);
                    SoundManager.Instance.PlayYoshiSwallowSound();
                }
                else
                {
                    sprite = AnimatedSpriteFactory.Instance.BuildBigLeftTongueStartMarioYoshi(playableObject.Position, frame);
                }
            }

            else if (frame == 1)
            {
                if (playableObject.ReverseYoshiSprites)
                {
                    playableObject.IsYoshiTongueLeft = false;
                    playableObject.Position = new Vector2(playableObject.Position.X + 3, playableObject.Position.Y - 4);
                }
                else
                {
                    playableObject.Position = new Vector2(playableObject.Position.X - 3, playableObject.Position.Y);
                }
                sprite = AnimatedSpriteFactory.Instance.BuildBigLeftTongueStartMarioYoshi(playableObject.Position, frame);
            }

            else if (frame == 2)
            {
                if (!playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X - 3, playableObject.Position.Y + 4);
                }
                else
                {
                    playableObject.Position = new Vector2(playableObject.Position.X + 10, playableObject.Position.Y);
                }
                sprite = AnimatedSpriteFactory.Instance.BuildBigLeftTongueSmallMarioYoshi(playableObject.Position);
            }

            else if (frame == 3)
            {
                if (!playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X - 10, playableObject.Position.Y);
                }
                else
                {
                    playableObject.Position = new Vector2(playableObject.Position.X + 10, playableObject.Position.Y);
                }
                sprite = AnimatedSpriteFactory.Instance.BuildBigLeftTongueMediumMarioYoshi(playableObject.Position);
            }

            else if (frame == 4)
            {
                if (!playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X - 10, playableObject.Position.Y);
                }
                else
                {
                    playableObject.Position = new Vector2(playableObject.Position.X + 12, playableObject.Position.Y);
                }
                sprite = AnimatedSpriteFactory.Instance.BuildBigLeftTongueLargeMarioYoshi(playableObject.Position);
            }

            else if (frame == 5)
            {
                if (!playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X - 12, playableObject.Position.Y);
                }
                sprite = AnimatedSpriteFactory.Instance.BuildBigLeftTongueXLargeMarioYoshi(playableObject.Position);
            }

            if (!playableObject.ReverseYoshiSprites)
            {
                if (frame > 5)
                {
                    frame--;
                    playableObject.ReverseYoshiSprites = true;
                }
                else
                {
                    frame++;
                }
            }

            else if (playableObject.ReverseYoshiSprites)
            {
                if (frame < 0)
                {
                    frame = 0;
                    playableObject.ReverseYoshiSprites = false;
                    playableObject.PlayableObjectState = new BigLeftIdleMarioYoshi(playableObject);
                }
                else
                {
                    frame--;
                }
            }

            sprite.UpdateSpritePosition(playableObject.Position);
            playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Milliseconds % 100 == 0)
            {
                HandleTongueStates();
            }

            if (playableObject.Velocity.X > 0)
            {
                if (frame >= 1)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 4);
                }
                playableObject.IsYoshiTongueLeft = false;
                playableObject.IsYoshiEating = false;
                playableObject.PlayableObjectState = new BigRightWalkingMarioYoshi(playableObject);
            }
            else if (playableObject.Velocity.X < 0)
            {
                if (frame >= 1)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 4);
                }
                playableObject.IsYoshiTongueLeft = false;
                playableObject.IsYoshiEating = false;
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

        }

        public void JumpButtonInput()
        {
            playableObject.Acceleration = new Vector2(playableObject.Acceleration.X, GameValues.MarioVerticalAcceleration);

            if (frame == 1)
            {

            }
            else if (frame == 2)
            {

            }
            else if (frame == 3)
            {

            }
            else if (frame == 4)
            {

            }
            else if (frame == 5)
            {

            }

            playableObject.IsYoshiTongueLeft = false;
            playableObject.IsYoshiEating = false;
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
