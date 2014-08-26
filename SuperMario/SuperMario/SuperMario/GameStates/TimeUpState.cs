using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.GameStates;
using SuperMario.Interfaces;

namespace SuperMario.GameStates
{
    public class TimeUpState : IGameState
    {
        public void Update(GameTime gameTime)
        {
            //Level.Instance.Update(gameTime);

            if (Mario.Instance.PlayableObjectState.ToString() == "SuperMario.MarioStates.DeadMario" && Mario.Instance.Acceleration == Vector2.Zero)
            {
                if (GameStateMachine.Instance.MarioDeathBuffer <= 0)
                {
                    GameStateMachine.Instance.MarioDeathBuffer = GameValues.GameStateMarioDeathBuffer;
                    Mario.Instance.Lives--;
                    if (Mario.Instance.Lives < 1)
                    {
                        GameStateMachine.Instance.GameState = new GameOverState();
                    }
                    else
                    {
                        GameStateMachine.Instance.GameState = new MarioRespawnState();
                    }
                }
                else
                {
                    GameStateMachine.Instance.MarioDeathBuffer--;
                    Mario.Instance.Update(gameTime);
                }
            }
            else
            {
                Level.Instance.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GameStateMachine.Instance.DrawBackground = true;
            HUD.Instance.FreezeHUD = true;
            //background.Draw(spriteBatch, gameTime);
            Level.Instance.Draw(spriteBatch, gameTime);

            string timeUp = "TIME UP!";
            Vector2 timeUpOrigin = GameStateMachine.Instance.GameFont.MeasureString(timeUp) / 2;
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(GameStateMachine.Instance.GameFont, timeUp, new Vector2((GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Width / 2) - timeUpOrigin.X, GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Height / 2), Color.White);

            GameStateMachine.Instance.TimeUpBuffer--;

            if (GameStateMachine.Instance.TimeUpBuffer <= 0)
            {
                GameStateMachine.Instance.TimeUpBuffer = GameValues.GameStateTimeUpBuffer;

                if (Mario.Instance.Lives < 1)
                {
                    GameStateMachine.Instance.GameState = new GameOverState();
                }
                else
                {
                    GameStateMachine.Instance.GameState = new MarioRespawnState();
                }
            }
        }
    }
}
