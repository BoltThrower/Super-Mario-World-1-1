using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.GameStates
{
    public class MarioFreezeGameAnimationState : IGameState
    {
        public void Update(GameTime gameTime)
        {
            Mario.Instance.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GameStateMachine.Instance.DrawBackground = true;
            HUD.Instance.FreezeHUD = true;
            Level.Instance.Draw(spriteBatch, gameTime);
        }
    }
}
