using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.GameStates
{
    public class ExitGameState : IGameState
    {
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GameStateMachine.Instance.DrawBackground = false;
            HUD.Instance.FreezeHUD = true;
            string gameExit = "Press ESC to Exit Game";
            Vector2 gameExitOrigin = GameStateMachine.Instance.GameFont.MeasureString(gameExit) / 2;
            spriteBatch.End();
            GameStateMachine.Instance.Graphics.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(GameStateMachine.Instance.GameFont, gameExit, new Vector2((GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Width / 2) - gameExitOrigin.X, GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Height / 2), Color.White);
        }
    }
}
