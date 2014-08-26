using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.MarioStates
{
    public class FireMarioFlagSlideEnd : IPlayableObjectState
    {
        private AnimatedSprite sprite;
        private IPlayableObject playableObject;

        public FireMarioFlagSlideEnd(IPlayableObject playableObject)
        {
            this.playableObject = playableObject;
            this.playableObject.StarPower = false;
            sprite = AnimatedSpriteFactory.Instance.BuildFireFlagSlideEndMarioSprite();
        }
        public void Update(GameTime gameTime)
        {
            sprite.UpdateSpritePosition(playableObject.Position);
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(spriteBatch, gameTime);
        }
        public void LeftInput()
        {
        }
        public void RightInput()
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
