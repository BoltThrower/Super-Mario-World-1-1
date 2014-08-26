using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    public class BigRightTongueMarioYoshi : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private int frame;
        private IPlayableObject playableObject;

        public BigRightTongueMarioYoshi(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            frame = 0;
            sprite = AnimatedSpriteFactory.Instance.BuildBigRightTongueStartMarioYoshi(playableObject.Position, frame);
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
                    sprite = AnimatedSpriteFactory.Instance.BuildBigRightFullMouthMarioYoshi(playableObject.Position);
                    SoundManager.Instance.PlayYoshiSwallowSound();
                }
                else
                {
                    sprite = AnimatedSpriteFactory.Instance.BuildBigRightTongueStartMarioYoshi(playableObject.Position, frame);
                }
            }

            else if (frame == 1)
            {
                if (playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 4);
                }

                sprite = AnimatedSpriteFactory.Instance.BuildBigRightTongueStartMarioYoshi(playableObject.Position, frame);
            }

            else if (frame == 2)
            {
                if (!playableObject.ReverseYoshiSprites)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y + 4);
                    playableObject.IsYoshiTongueRight = true;
                }
                sprite = AnimatedSpriteFactory.Instance.BuildBigRightTongueSmallMarioYoshi(playableObject.Position);
            }

            else if (frame == 3)
            {
                sprite = AnimatedSpriteFactory.Instance.BuildBigRightTongueMediumMarioYoshi(playableObject.Position);
            }

            else if (frame == 4)
            {
                sprite = AnimatedSpriteFactory.Instance.BuildBigRightTongueLargeMarioYoshi(playableObject.Position);
            }

            else if (frame == 5)
            {
                sprite = AnimatedSpriteFactory.Instance.BuildBigRightTongueXLargeMarioYoshi(playableObject.Position);
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
                    playableObject.PlayableObjectState = new BigRightIdleMarioYoshi(playableObject);
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
                playableObject.IsYoshiTongueRight = false;
                playableObject.IsYoshiEating = false;
                playableObject.IsYoshiFinishedEating = false;
                playableObject.PlayableObjectState = new BigRightWalkingMarioYoshi(playableObject);
            }
            else if (playableObject.Velocity.X < 0)
            {
                if (frame >= 1)
                {
                    playableObject.Position = new Vector2(playableObject.Position.X, playableObject.Position.Y - 4);
                }
                playableObject.IsYoshiTongueRight = false;
                playableObject.IsYoshiEating = false;
                playableObject.IsYoshiFinishedEating = false;
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
            playableObject.IsYoshiEating = false;
            playableObject.IsYoshiFinishedEating = false;
            playableObject.Acceleration = new Vector2(playableObject.Acceleration.X, GameValues.MarioVerticalAcceleration);
            playableObject.PlayableObjectState = new BigRightJumpingMarioYoshi(playableObject);
        }

        public void NoInput()
        {

        }

        public void TakeDamage()
        {
            if (playableObject.IsFire)
            {
                playableObject.PlayableObjectState = new FireRightIdleMario(playableObject);
            }
            else
            {
                playableObject.PlayableObjectState = new BigRightIdleMario(playableObject);
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
