using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.GameStates;
using SuperMario.Interfaces;

namespace SuperMario.GameStates
{
    public class PlayingState : IGameState
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
            HUD.Instance.FreezeHUD = false;
            Level.Instance.Draw(spriteBatch, gameTime);

            // When Mario is dead everything in Level needs to freeze except Mario.
            if (Mario.Instance.PlayableObjectState.ToString() == "SuperMario.MarioStates.DeadMario")
            {
                HUD.Instance.FreezeHUD = true;
            }
        }
    }
}
