using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SuperMario.Commands.GameStateCommands;

namespace SuperMario
{
    public sealed class HUD
    {
        private static HUD instance = new HUD();
        private int timeCounter;
        private AnimatedSprite sprite;
        private Dictionary<int, string> GameStats;
        private bool gameOver;

        public Vector2 Position { get; set; }
        public int CoinHUDCounter { get; set; }
        public int ScoreHUDCounter { get; set; }
        public int TimerLife { get; set; }
        public int CurrentWorld { get; set; }
        public int CurrentStage { get; set; }
        public int TimeLeft { get; set; }
        public bool TimeIsLow { get; set; }
        public bool FreezeHUD { get; set; }
        public SpriteFont HudFont { get; set; }
        public ContentManager Content { get; set; }

        public HUD()
        {
            sprite = AnimatedSpriteFactory.Instance.BuildFloatingCoinSprite(Position);
            CoinHUDCounter = 0;
            ScoreHUDCounter = 0;
            TimerLife = GameValues.HUDTimerLife;
            CurrentWorld = 1;
            CurrentStage = 1;
            TimeLeft = 0;
            TimeIsLow = false;
            FreezeHUD = false;
            GameStats = new Dictionary<int, string> { { 0, "" }, { 1, "" }, { 2, "" } };
            gameOver = true;
        }

        public static HUD Instance
        {
            get
            {
                return instance;
            }
        }

        public void LoadFonts()
        {
            HudFont = Content.Load<SpriteFont>("HUDFont");
        }

        public void UpdateGameStats(string scoreHUDText, string coinHUDText, string worldHUDText, string timerHUDText, int statPosition)
        {
            GameStats[statPosition] = "SCORE = " + scoreHUDText + "\nCOINS = " + coinHUDText + "\nWORLD = " + worldHUDText + "\nTIME = " + timerHUDText;
        }

        public Dictionary<int, string> SetGameStats()
        {
            return GameStats;
        }

        public void ResetHUD()
        {
            CoinHUDCounter = 0;
            timeCounter = 0;
            ScoreHUDCounter = 0;
            CurrentWorld = 1;
            CurrentStage = 1;
            gameOver = true;
        }

        public void ResetTime()
        {
            timeCounter = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Milliseconds % GameValues.HUDUpdateDelay == 0)
            {
                sprite.AdvanceFrame();
            }
            sprite.UpdateSpritePosition(Position);

            if (GameStateMachine.Instance.GameState.ToString() != "SuperMario.GameStates.TimeScoreAnimationState")
            {
                TimeLeft = TimerLife - timeCounter;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            string scoreHUDText = ScoreHUDCounter.ToString();
            string coinHUDText = CoinHUDCounter.ToString();
            string worldHUDText = CurrentWorld.ToString() + "-" + CurrentStage.ToString();

            // Sets our timeCounter such that our HUD Timer decrements every half of a second.
            if (gameTime.TotalGameTime.Milliseconds % GameValues.HUDDrawDelay == 0 && !FreezeHUD)
            {
                if (timeCounter < TimerLife)
                {
                    if (timeCounter == GameValues.HUDTimeWarningAmount)
                    {
                        TimeIsLow = true;
                        SoundManager.Instance.PlayTimeWarning();
                    }
                    timeCounter++;
                }
                else
                {
                    new TimeUpCommand(GameStateMachine.Instance.PlayableObjects).Execute();
                }
            }

            string timerHUDText = "";

            if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.TimeScoreAnimationState")
            {
                timerHUDText = TimeLeft.ToString();
            }
            else if(GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.ExitGameState" || GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.GameWonState")
            {
                timerHUDText = GameValues.HUDEmptyTimeCounter;
            }
            else
            {
                timerHUDText = (TimerLife - timeCounter).ToString();
            }


            // Pads all our HUD values with 0's.
            for (int i = 1; scoreHUDText.Length < GameValues.HUDScoreMaxLength; i++)
            {
                scoreHUDText = "0" + scoreHUDText;
            }

            for (int i = 1; coinHUDText.Length < GameValues.HUDCoinMaxLength; i++)
            {
                coinHUDText = "0" + coinHUDText;
            }

            for (int i = 1; timerHUDText.Length < GameValues.HUDTimerMaxLength; i++)
            {
                timerHUDText = "0" + timerHUDText;
            }

            //if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.GameOverState")
            if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.MarioRespawnState")
            {
                if (!(Mario.Instance.Lives > GameValues.MarioStartingLives))
                {
                    UpdateGameStats(scoreHUDText, coinHUDText, worldHUDText, timerHUDText, Mario.Instance.Lives - 1);
                }
            }
            else if (GameStateMachine.Instance.GameState.ToString() == "SuperMario.GameStates.GameOverState" && gameOver)
            {
                UpdateGameStats(scoreHUDText, coinHUDText, worldHUDText, timerHUDText, Mario.Instance.Lives);
                gameOver = false;
            }

            scoreHUDText = "MARIO\n" + scoreHUDText;
            coinHUDText = "\nx" + coinHUDText;
            worldHUDText = "WORLD\n  " + worldHUDText;
            timerHUDText = "TIME\n " + timerHUDText;

            Vector2 scoreHUDOrigin = HudFont.MeasureString(scoreHUDText) / 2;
            Vector2 coinHUDOrigin = HudFont.MeasureString(coinHUDText) / 2;
            Vector2 worldHUDOrigin = HudFont.MeasureString(worldHUDText) / 2;
            Vector2 timerHUDOrigin = HudFont.MeasureString(timerHUDText) / 2;

            int xPositionPadding = (int)Position.X / 4;
            int yPositionPadding = (int)Position.Y;

            // Sets the distance between each line of text on the HUD.
            HudFont.LineSpacing = 3 * ((int) HudFont.MeasureString(scoreHUDText).Y / 7);

            sprite.UpdateSpritePosition(new Vector2(Position.X - HudFont.MeasureString(coinHUDText).X - xPositionPadding, yPositionPadding + 5));
            sprite.Draw(spriteBatch, gameTime);
            spriteBatch.DrawString(HudFont, scoreHUDText, new Vector2(Position.X - (3 * xPositionPadding), yPositionPadding), Color.White, 0, scoreHUDOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(HudFont, scoreHUDText, new Vector2(Position.X - (3 * xPositionPadding), yPositionPadding), Color.White, 0, scoreHUDOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(HudFont, coinHUDText, new Vector2(Position.X - xPositionPadding, yPositionPadding), Color.White, 0, coinHUDOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(HudFont, worldHUDText, new Vector2(Position.X + xPositionPadding, yPositionPadding), Color.White, 0, worldHUDOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(HudFont, timerHUDText, new Vector2(Position.X + (3 * xPositionPadding), yPositionPadding), Color.White, 0, timerHUDOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
