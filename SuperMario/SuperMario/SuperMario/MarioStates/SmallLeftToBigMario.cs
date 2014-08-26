using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.GameStates;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    class SmallLeftToBig : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private int growthBuffer;
        private IPlayableObject playableObject;

        public SmallLeftToBig(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            sprite = AnimatedSpriteFactory.Instance.BuildSmallLeftMarioToBigMarioSprite();
            growthBuffer = GameValues.MarioStateSmallToBigGrowthBuffer;
            this.playableObject.CollisionRectangle = sprite.SpriteDestinationRectangle;
            GameStateMachine.Instance.GameState = new MarioFreezeGameAnimationState();
        }

        public void Update(GameTime gameTime)
        {
            if (sprite.CurrentFrame < GameValues.MarioStateSmallToBigMaxSpriteFrames)
            {
                if (growthBuffer <= 0)
                {
                    growthBuffer = GameValues.MarioStateSmallToBigGrowthBuffer;
                    sprite.AdvanceFrame();
                }
                else
                {
                    growthBuffer--;
                }
            }
            else
            {
                playableObject.PlayableObjectState = new BigLeftIdleMario(playableObject);
                GameStateMachine.Instance.GameState = new PlayingState();
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
        }

        public void LeftInput()
        {
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
        }

        public void TakeDamage()
        {
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