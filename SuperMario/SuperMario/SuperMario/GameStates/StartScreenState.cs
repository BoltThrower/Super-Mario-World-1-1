using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.GameStates
{
    public class StartScreenState : IGameState
    {
        public void ChangeSelection()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            HUD.Instance.FreezeHUD = true;
            GameStateMachine.Instance.DrawBackground = true;
            //background.Draw(spriteBatch, gameTime);
            Level.Instance.Draw(spriteBatch, gameTime);

            string menu = "Press START to begin";
            Vector2 menuOrigin = GameStateMachine.Instance.GameFont.MeasureString(menu) / 2;
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(GameStateMachine.Instance.GameFont, menu, new Vector2((GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Width / 2) - menuOrigin.X, GameStateMachine.Instance.Graphics.GraphicsDevice.Viewport.Height / 2), Color.White);
        }
    }
}
