using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class Flagpole : IStaticObject
    {
        public Vector2 Position { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        public ScoreSprite ScoreSprite { get; set; }

        private AnimatedSprite flagpole;
        private bool scoreDrawn;
        private int levelcellsize;

        public Flagpole(Vector2 position, int levelCellSize)
        {
            this.levelcellsize = levelCellSize;
            scoreDrawn = false;
            ScoreSprite = new ScoreSprite(GameValues.FlagpoleMinScoreSprite, Mario.Instance.Position, false);
            Position = new Vector2(position.X + levelCellSize/4, position.Y);
            flagpole = AnimatedSpriteFactory.Instance.BuildFlagpoleSprite(Position);
            CollisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, flagpole.Texture.Width, flagpole.Texture.Height);
        }

        public void Update(GameTime gameTime)
        {
           flagpole.UpdateSpritePosition(Position);
           CollisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, flagpole.Texture.Width, flagpole.Texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (ScoreSprite.ScoringOn)
            {
                ScoreSprite.DrawFlagScore(spriteBatch, gameTime, CollisionRectangle.Top + GameValues.FlagpoleDrawFlagScoreOffset);
            }
            flagpole.Draw(spriteBatch, gameTime);
        }

        public void SetScoreValue(float yPosition)
        {
            
            if (!scoreDrawn)
            {
                if (yPosition >= CollisionRectangle.Top && yPosition <= CollisionRectangle.Top + GameValues.FlagpoleYPosition1)
                {
                    ScoreSprite = new ScoreSprite(GameValues.FlagpoleMaxScoreSprite, new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom), true);
                }
                else if (yPosition > CollisionRectangle.Top + GameValues.FlagpoleYPosition1 && yPosition <= CollisionRectangle.Top + GameValues.FlagpoleYPosition2)
                {
                    ScoreSprite = new ScoreSprite(GameValues.FlagpoleYPosScoreSprite7, new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom), true);
                }
                else if (yPosition > CollisionRectangle.Top + GameValues.FlagpoleYPosition2 && yPosition <= CollisionRectangle.Top + GameValues.FlagpoleYPosition3)
                {
                    ScoreSprite = new ScoreSprite(GameValues.FlagpoleYPosScoreSprite6, new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom), true);
                }
                else if (yPosition > CollisionRectangle.Top + GameValues.FlagpoleYPosition3 && yPosition <= CollisionRectangle.Top + GameValues.FlagpoleYPosition4)
                {
                    ScoreSprite = new ScoreSprite(GameValues.FlagpoleYPosScoreSprite5, new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom), true);
                }
                else if (yPosition > CollisionRectangle.Top + GameValues.FlagpoleYPosition4 && yPosition <= CollisionRectangle.Top + GameValues.FlagpoleYPosition5)
                {
                    ScoreSprite = new ScoreSprite(GameValues.FlagpoleYPosScoreSprite4, new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom), true);
                }
                else if (yPosition > CollisionRectangle.Top + GameValues.FlagpoleYPosition5 && yPosition <= CollisionRectangle.Top + GameValues.FlagpoleYPosition6)
                {
                    ScoreSprite = new ScoreSprite(GameValues.FlagpoleYPosScoreSprite3, new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom), true);
                }
                else if (yPosition > CollisionRectangle.Top + GameValues.FlagpoleYPosition6 && yPosition <= CollisionRectangle.Top + GameValues.FlagpoleYPosition7)
                {
                    ScoreSprite = new ScoreSprite(GameValues.FlagpoleYPosScoreSprite2, new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom), true);
                }
                else if (yPosition > CollisionRectangle.Top + GameValues.FlagpoleYPosition7 && yPosition <= CollisionRectangle.Top + GameValues.FlagpoleYPosition8)
                {
                    ScoreSprite = new ScoreSprite(GameValues.FlagpoleYPosScoreSprite1, new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom), true);
                }
                else if (yPosition > CollisionRectangle.Top + GameValues.FlagpoleYPosition8 && yPosition <= CollisionRectangle.Bottom)
                {
                    ScoreSprite = new ScoreSprite(GameValues.FlagpoleMinScoreSprite, new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom), true);
                }

                HUD.Instance.ScoreHUDCounter += Convert.ToInt32(ScoreSprite.ScoreValue());
                scoreDrawn = true;
            }
        }
    }
}
