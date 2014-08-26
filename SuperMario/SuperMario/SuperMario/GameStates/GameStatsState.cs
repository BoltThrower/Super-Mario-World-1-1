using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;
using System.Collections.Generic;

namespace SuperMario.GameStates
{
    public class GameStatsState : IGameState
    {
        private Dictionary<int, string> GameStats;

        public GameStatsState()
        {
            GameStats = HUD.Instance.SetGameStats();
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GameStateMachine.Instance.DrawBackground = false;
            HUD.Instance.FreezeHUD = true;
            string gameStatsTitle = "-= GAME STATS =-\n";
            Vector2 gameStatsTitleOrigin = GameStateMachine.Instance.GameFont.MeasureString(gameStatsTitle) / 2;

            spriteBatch.End();
            GameStateMachine.Instance.Graphics.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(GameStateMachine.Instance.GameFont, gameStatsTitle, new Vector2((GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Width / 2) - gameStatsTitleOrigin.X, (GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Height / 3)), Color.White);

            for (int i = 2; i >= 0; i--)
            {
                Vector2 statsOrigin = GameStateMachine.Instance.GameFont.MeasureString(GameStats[0]) / 2;
                // spriteBatch.DrawString(GameStateMachine.Instance.GameFont, gameStatsTitle, new Vector2((GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Width / 2) - gameStatsTitleOrigin.X, (GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Height / 2) - statsOrigin.Y), Color.White);
                // spriteBatch.DrawString(GameStateMachine.Instance.GameFont, GameStats[0], new Vector2((GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Width / 2) - statsOrigin.X, GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Height / 2), Color.White);

                spriteBatch.DrawString(GameStateMachine.Instance.GameFont, "RUN" + i + "\n" + GameStats[i], new Vector2((GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Width / 2) - statsOrigin.X, GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Height / 2 + (i * 100)), SetColor(i));
            }
            GameStateMachine.Instance.GameStatsBuffer--;

            if (GameStateMachine.Instance.GameStatsBuffer <= 0)
            {
                GameStateMachine.Instance.GameStatsBuffer = GameValues.GameStateGameStatsBuffer;
                GameStateMachine.Instance.GameState = new StartScreenState();
            }
        }

        public Color SetColor(int i)
        {
            Color color = new Color();

            if (i == 0)
            {
                color = Color.Red;
            }
            else if (i == 1)
            {
                color = Color.Blue;
            }
            else if (i == 2)
            {
                color = Color.Green;
            }

            return color;
        }
    }
}
