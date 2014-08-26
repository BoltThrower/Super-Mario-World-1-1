using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario.Interfaces
{
    public interface IPlayableObjectState
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        void LeftInput();
        void RightInput();
        void DownInput();
        void RunButtonInput();
        void JumpButtonInput();
        void NoInput();
        void TakeDamage();
        void PickUpPowerup();
        void PickUpStar();
    }
}