using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    class DeadMario : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private IPlayableObject playableObject;

        public DeadMario(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            this.playableObject.Acceleration = new Vector2(0.0f, GameValues.MarioVerticalAcceleration * 2);
            this.playableObject.Velocity = Vector2.Zero;
            SoundManager.Instance.MarioDeath.Play();

            sprite = AnimatedSpriteFactory.Instance.BuildDeadMarioSprite();
            this.playableObject.CollisionRectangle = new Rectangle();
        }

        public void Update(GameTime gameTime)
        {
            // dead mario only has one animation frame and doesn't need to call AdvanceFrame()
            sprite.UpdateSpritePosition(playableObject.Position);

            if (playableObject.Acceleration.Y < 0)
            {
                playableObject.Acceleration = new Vector2(0.0f, playableObject.Acceleration.Y + 1);
            }
            else
            {
                playableObject.Acceleration = Vector2.Zero;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
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
            
        }
    }
}