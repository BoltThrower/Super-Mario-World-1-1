using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperMario.Commands.MarioInputCommands;
using SuperMario.GameStates;
using SuperMario.Interfaces;
using SuperMario.MarioStates;
using System.Collections.Generic;

namespace SuperMario
{
    public class GameStateMachine
    {
        private static GameStateMachine instance;

        public IGameState GameState { get; set; }
        public GraphicsDeviceManager Graphics { get; set; }
        public ContentManager Content { get; set; }
        public bool FirstRun { get; set; }
        public bool DrawBackground { get; set; }
        public int MarioDeathBuffer { get; set; }
        public int RespawnBuffer { get; set; }
        public int GameOverBuffer { get; set; }
        public int TimeUpBuffer { get; set; }
        public int GameStatsBuffer { get; set; }
        public SpriteFont GameFont { get; set; }
        public List<IPlayableObject> PlayableObjects { get; set; }

        public GameStateMachine()
        {
            PlayableObjects = new List<IPlayableObject> { };
            PlayableObjects.Add(Mario.Instance);
            GameState = new StartScreenState();
            FirstRun = true;
            DrawBackground = true;
            MarioDeathBuffer = GameValues.GameStateMarioDeathBuffer;
            RespawnBuffer = GameValues.GameStateRespawnBuffer;
            GameOverBuffer = GameValues.GameStateGameOverBuffer;
            TimeUpBuffer = GameValues.GameStateTimeUpBuffer;
            GameStatsBuffer = GameValues.GameStateGameStatsBuffer;
        }

        public static GameStateMachine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameStateMachine();
                }
                return instance;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (FirstRun)
            {
                // Call update once to fix Mario's position for Menu Screen state.
                FirstRun = false;
                Level.Instance.Update(gameTime);
            }

            GameState.Update(gameTime);

            HUD.Instance.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GameState.Draw(spriteBatch, gameTime);
            spriteBatch.End();

            spriteBatch.Begin();
            HUD.Instance.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
