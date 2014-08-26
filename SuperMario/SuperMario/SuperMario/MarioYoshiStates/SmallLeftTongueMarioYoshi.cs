using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    public class SmallLeftTongueMarioYoshi : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private int frame;
        private IPlayableObject playableObject;

        public SmallLeftTongueMarioYoshi(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            frame = 0;
            sprite = AnimatedSpriteFactory.Instance.BuildSmallLeftTongueStartMarioYoshi(playableObject.Position, frame);
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
            this.playableObject.ReverseYoshiSprites = false;

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
                    sprite = AnimatedSpriteFactory.Instance.BuildSmallLeftFullMouthMarioYoshi(playableObject.Position);
                    SoundManager.Instance.PlayYoshiSwallowSound();
                }
                else
                {
                    sprite = AnimatedSpriteFactory.Instance.BuildSmallLeftTongueStartMarioYoshi(playableObject.Position, frame);
                }
            }

            else if (frame == 1)
            {
                if (playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X + 1, playableObject.Position.Y - 5);
                }
                else
                {
                    playableObject.Position = new Vector2(playableObject.Position.X - 3, playableObject.Position.Y);
                }

                sprite = AnimatedSpriteFactory.Instance.BuildSmallLeftTongueStartMarioYoshi(playableObject.Position, frame);
            }

            else if (frame == 2)
            {
                if (!playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X - 1, playableObject.Position.Y + 5);
                    playableObject.IsYoshiTongueLeft = true;
                }
                else
                {
                    playableObject.Position = new Vector2(playableObject.Position.X + 8, playableObject.Position.Y);
                }
                sprite = AnimatedSpriteFactory.Instance.BuildSmallLeftTongueSmallMarioYoshi(playableObject.Position);
            }

            else if (frame == 3)
            {
                if (!playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X - 8, playableObject.Position.Y);
                }
                else
                {
                    playableObject.Position = new Vector2(playableObject.Position.X + 16, playableObject.Position.Y);
                }
                sprite = AnimatedSpriteFactory.Instance.BuildSmallLeftTongueMediumMarioYoshi(playableObject.Position);
            }

            else if (frame == 4)
            {
                if (!playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X - 16, playableObject.Position.Y);
                }
                else
                {
                    playableObject.Position = new Vector2(playableObject.Position.X + 8, playableObject.Position.Y);
                }
                sprite = AnimatedSpriteFactory.Instance.BuildSmallLeftTongueLargeMarioYoshi(playableObject.Position);
            }

            else if (frame == 5)
            {
                if (!playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X - 8, playableObject.Position.Y);
                }
                sprite = AnimatedSpriteFactory.Instance.BuildSmallLeftTongueXLargeMarioYoshi(playableObject.Position);
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
                    playableObject.PlayableObjectState = new SmallLeftIdleMarioYoshi(playableObject);
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
                    playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 5);
                }
                playableObject.IsYoshiTongueLeft = false;
                playableObject.IsYoshiEating = false;
                playableObject.IsYoshiFinishedEating = false;
                playableObject.PlayableObjectState = new SmallRightWalkingMarioYoshi(playableObject);
            }
            else if (playableObject.Velocity.X < 0)
            {
                if (frame >= 1)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 5);
                }
                playableObject.IsYoshiTongueLeft = false;
                playableObject.IsYoshiEating = false;
                playableObject.IsYoshiFinishedEating = false;
                playableObject.PlayableObjectState = new SmallLeftWalkingMarioYoshi(playableObject);
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
            playableObject.IsYoshiEating = false;
            playableObject.IsYoshiFinishedEating = false;
            playableObject.Acceleration = new Vector2(playableObject.Acceleration.X, GameValues.MarioVerticalAcceleration);
            playableObject.PlayableObjectState = new SmallRightJumpingMarioYoshi(playableObject);
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
            playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 11);
            playableObject.PlayableObjectState = new BigLeftIdleMarioYoshi(playableObject);
        }

        public void PickUpStar()
        {
            playableObject.StarPower = true;
        }
    }
}
