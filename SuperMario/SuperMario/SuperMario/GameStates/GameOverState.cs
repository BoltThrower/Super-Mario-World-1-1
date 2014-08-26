using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.GameStates
{
    public class GameOverState : IGameState
    {
        public void Update(GameTime gameTime)
        {
            SoundManager.Instance.PlayGameOverMusic();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GameStateMachine.Instance.DrawBackground = false;
            HUD.Instance.FreezeHUD = true;
            string gameOver = "GAME OVER";
            Vector2 gameOverOrigin = GameStateMachine.Instance.GameFont.MeasureString(gameOver) / 2;
            spriteBatch.End();
            GameStateMachine.Instance.Graphics.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(GameStateMachine.Instance.GameFont, gameOver, new Vector2((GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Width / 2) - gameOverOrigin.X, GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Height / 2), Color.White);
            GameStateMachine.Instance.GameOverBuffer--;

            if (GameStateMachine.Instance.GameOverBuffer <= 0)
            {
                GameStateMachine.Instance.GameOverBuffer = GameValues.GameStateGameOverBuffer;
               // GameStateMachine.Instance.GameState = new StartScreenState();
                GameStateMachine.Instance.GameState = new GameStatsState();
                Level.Instance.Reset();
                HUD.Instance.ResetHUD();
                Mario.Instance.Lives = 3;
                GameStateMachine.Instance.FirstRun = true;
            }
        }
    }
}
