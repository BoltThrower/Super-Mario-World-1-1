using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario
{
    public class ScoreSprite
    {
        public Vector2 Position { get; set; }
        public SpriteFont ScoreFont { get; set; }
        public bool ScoringOn { get; set; }

        private string scoreText;
        private Vector2 scoreOrigin, startingPosition, stoppingPosition;
        private int scoreBuffer;

        public ScoreSprite(string scoreValue, Vector2 position, bool scoringOn)
        {
            Position = position;
            startingPosition = position;
            stoppingPosition = new Vector2(position.X, position.Y - GameValues.ScoreSpriteInitialYPositionOffset);
            scoreText = scoreValue;
            ScoreFont = HUD.Instance.HudFont;
            ScoringOn = scoringOn;
            scoreBuffer = GameValues.ScoreSpriteScoreBuffer;
        }

        public string ScoreValue()
        {
            return scoreText;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            scoreOrigin = ScoreFont.MeasureString(scoreText) / GameValues.ScoreSpriteScoreOriginOffset;

            spriteBatch.DrawString(ScoreFont, scoreText, Position, Color.White, 0, scoreOrigin, 0.4f, SpriteEffects.None, 0f);

            if (Position.Y > stoppingPosition.Y)
            {
                Position = new Vector2(Position.X, Position.Y - 1);
            }
            else
            {
                ScoringOn = !ScoringOn;
                Position = startingPosition;
            }
        }

        public void DrawFlagScore(SpriteBatch spriteBatch, GameTime gameTime, float stoppingHeight)
        {
            scoreOrigin = ScoreFont.MeasureString(scoreText) / GameValues.ScoreSpriteScoreOriginOffset;

            spriteBatch.DrawString(ScoreFont, scoreText, new Vector2(Position.X, Position.Y - GameValues.ScoreSpriteDrawFlagScoreYOffset), Color.White, 0, scoreOrigin, 0.4f, SpriteEffects.None, 0f);

            if (Position.Y > stoppingHeight)
            {
                Position = new Vector2(Position.X, Position.Y - GameValues.ScoreSpriteDrawFlagScoreDropOffet);
            }
            else if (Position.Y <= stoppingHeight)
            {
                //Position.Y = stoppingHeight;
                Position = new Vector2(Position.X, stoppingHeight);
                if (scoreBuffer <= 0)
                {
                    scoreBuffer = GameValues.ScoreSpriteScoreBuffer;
                    ScoringOn = !ScoringOn;
                }
                else
                {
                    scoreBuffer--;
                }
            }
        }
    }
}
