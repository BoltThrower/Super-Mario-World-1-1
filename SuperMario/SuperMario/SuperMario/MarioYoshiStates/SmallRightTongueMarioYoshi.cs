using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    public class SmallRightTongueMarioYoshi : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private int frame;
        private IPlayableObject playableObject;

        public SmallRightTongueMarioYoshi(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            frame = 0;
            sprite = AnimatedSpriteFactory.Instance.BuildSmallRightTongueStartMarioYoshi(playableObject.Position, frame);
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
            this.playableObject.ReverseYoshiSprites = false;

            SoundManager.Instance.PlayYoshiTongueSound();
        }

        public void HandleTongueStates()
        {
            if (frame == 0 && playableObject.ReverseYoshiSprites)
            {
                playableObject.IsYoshiTongueRight = false;

                if (playableObject.IsYoshiEating)
                {
                    playableObject.IsYoshiEating = false;
                    playableObject.IsYoshiFinishedEating = true;
                    playableObject.Position = new Vector2(playableObject.Position.X + 2, playableObject.Position.Y);
                    sprite = AnimatedSpriteFactory.Instance.BuildSmallRightFullMouthMarioYoshi(playableObject.Position);
                    SoundManager.Instance.PlayYoshiSwallowSound();
                }
                else
                {
                    sprite = AnimatedSpriteFactory.Instance.BuildSmallRightTongueStartMarioYoshi(playableObject.Position, frame);
                }
            }

            else if (frame == 1)
            {
                if (playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 5);
                }
                else
                {
                    playableObject.Position = new Vector2(playableObject.Position.X - 2, playableObject.Position.Y);
                }

                sprite = AnimatedSpriteFactory.Instance.BuildSmallRightTongueStartMarioYoshi(playableObject.Position, frame);
            }

            else if (frame == 2)
            {
                if (!playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y + 5);
                    playableObject.IsYoshiTongueRight = true;
                }
                sprite = AnimatedSpriteFactory.Instance.BuildSmallRightTongueSmallMarioYoshi(playableObject.Position);
            }

            else if (frame == 3)
            {
                sprite = AnimatedSpriteFactory.Instance.BuildSmallRightTongueMediumMarioYoshi(playableObject.Position);
            }

            else if (frame == 4)
            {
                sprite = AnimatedSpriteFactory.Instance.BuildSmallRightTongueLargeMarioYoshi(playableObject.Position);
            }

            else if (frame == 5)
            {
                sprite = AnimatedSpriteFactory.Instance.BuildSmallRightTongueXLargeMarioYoshi(playableObject.Position);
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
                    playableObject.PlayableObjectState = new SmallRightIdleMarioYoshi(playableObject);
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
                playableObject.IsYoshiTongueRight = false;
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
                playableObject.IsYoshiTongueRight = false;
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
            playableObject.PlayableObjectState = new SmallRightIdleMario(playableObject);
        }

        public void PickUpPowerup()
        {
            playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 11);
            playableObject.PlayableObjectState = new BigRightIdleMarioYoshi(playableObject);
        }

        public void PickUpStar()
        {
            playableObject.StarPower = true;
        }
    }
}
