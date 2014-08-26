using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;
using SuperMario.MarioStates;

namespace SuperMario.GameStates
{
    public class EndLevelState : IGameState
    {
        public void Update(GameTime gameTime)
        {
            if (Mario.Instance.Position.X >= Level.Instance.Castle.BoundingRectangle.Center.X - (Mario.Instance.CollisionRectangle.Width / 2))
            {
                Mario.Instance.PlayableObjectState = new NoMario();
                Mario.Instance.Velocity = Vector2.Zero;
                GameStateMachine.Instance.GameState = new TimeScoreAnimationState();
            }

            else
            {
                Mario.Instance.Acceleration = new Vector2(GameValues.MarioHorizontalAcceleration, Mario.Instance.Acceleration.Y);
                Mario.Instance.MaxHorizontalVelocity = GameValues.MarioWalkingSpeed;
            }

            Level.Instance.Update(gameTime);
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
