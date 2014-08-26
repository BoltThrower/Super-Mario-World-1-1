using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.GameStates
{
    public class GameWonState : IGameState
    {
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GameStateMachine.Instance.DrawBackground = false;
            HUD.Instance.FreezeHUD = true;
            string gameWon = "YOU WON!";
            Vector2 gameWonOrigin = GameStateMachine.Instance.GameFont.MeasureString(gameWon) / 2;
            spriteBatch.End();
            GameStateMachine.Instance.Graphics.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(GameStateMachine.Instance.GameFont, gameWon, new Vector2((GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Width / 2) - gameWonOrigin.X, GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Height / 2), Color.White);
            GameStateMachine.Instance.GameWonBuffer--;

            if (GameStateMachine.Instance.GameWonBuffer <= 0)
            {
                GameStateMachine.Instance.GameWonBuffer = GameValues.GameStateGameWonBuffer;
                GameStateMachine.Instance.GameState = new ExitGameState();
            }
        }
    }
}
