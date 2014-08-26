using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.GameStates
{
    public class TimeScoreAnimationState : IGameState
    {
        public void Update(GameTime gameTime)
        {
            if (HUD.Instance.TimeLeft > 0)
            {
                HUD.Instance.ScoreHUDCounter += 100;
                HUD.Instance.TimeLeft--;
                SoundManager.Instance.PlayEndingScoreIncrementSound();
            }

            else
            {
                Level.Instance.Castle.CastleFlagMoving = true;
            }

            Level.Instance.Castle.Update();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GameStateMachine.Instance.DrawBackground = true;
            HUD.Instance.FreezeHUD = true;
            //background.Draw(spriteBatch, gameTime);
            Level.Instance.Draw(spriteBatch, gameTime);
        }
    }
}
