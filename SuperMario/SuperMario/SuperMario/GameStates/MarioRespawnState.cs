using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;
using SuperMario.GameStates;

namespace SuperMario.GameStates
{
    public class MarioRespawnState : IGameState
    {
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GameStateMachine.Instance.DrawBackground = false;
            HUD.Instance.FreezeHUD = true;
            string worldDisplayText = "WORLD " + HUD.Instance.CurrentWorld.ToString() + "-" + HUD.Instance.CurrentStage.ToString() + "\n";
            string livesRemaining = " x " + Mario.Instance.Lives;
            Vector2 livesOrigin = GameStateMachine.Instance.GameFont.MeasureString(livesRemaining) / 2;
            Vector2 worldDisplayOrigin = GameStateMachine.Instance.GameFont.MeasureString(worldDisplayText) / 2;
            spriteBatch.End();

            GameStateMachine.Instance.Graphics.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(GameStateMachine.Instance.GameFont, worldDisplayText, new Vector2((GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Width / 2) - worldDisplayOrigin.X, (GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Height / 2) - livesOrigin.Y * 2), Color.White);
            spriteBatch.DrawString(GameStateMachine.Instance.GameFont, livesRemaining, new Vector2((GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Width / 2) - livesOrigin.X, GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Height / 2), Color.White);
            AnimatedSprite marioLife = AnimatedSpriteFactory.Instance.BuildMarioLifeSprite(new Vector2((GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Width / 2) - livesOrigin.X * 2, GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Height / 2 + worldDisplayOrigin.Y / 3));
            marioLife.Draw(spriteBatch, gameTime);

            //respawnBuffer--;

            if (GameStateMachine.Instance.RespawnBuffer <= 0)
            {
                GameStateMachine.Instance.RespawnBuffer = GameValues.GameStateRespawnBuffer;
                GameStateMachine.Instance.GameState = new PlayingState();

                CheckPoint latestCheckPoint = new CheckPoint(new Vector2(0, 0));

                // Determines the furthest checkpoint achieved and makes that our latestCheckPoint, otherwise disables the rest.
                foreach (CheckPoint checkpoint in Level.Instance.Checkpoints)
                {
                    if (checkpoint.CheckPointEnabled == true && checkpoint.Position.X >= latestCheckPoint.Position.X)
                    {
                        latestCheckPoint = checkpoint;
                    }
                    else
                    {
                        checkpoint.CheckPointEnabled = false;
                    }

                }

                Level.Instance.Reset();
                HUD.Instance.ResetTime();

                // This check has to take place after Level.Instance.Reset() otherwise Mario does not reset correctly on
                // on the respawn and neither does the barrier.
                if (latestCheckPoint.CheckPointEnabled)
                {
                    Mario.Instance.Position = latestCheckPoint.Position;
                    Level.Instance.InvisibleBarrier.Position = new Vector2(latestCheckPoint.Position.X - 96, latestCheckPoint.Position.Y);
                }
            }
            else
            {
                GameStateMachine.Instance.RespawnBuffer--;
            }
        }
    }
}
