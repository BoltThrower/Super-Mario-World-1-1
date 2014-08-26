using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario.GameStates
{
    public class NextLevelState : IGameState
    {
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Level.Instance.LoadNewLevel();
            GameStateMachine.Instance.GameState = new MarioRespawnState();
        }
    }
}

