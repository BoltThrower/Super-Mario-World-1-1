using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SuperMario
{
    public class AnimatedSprite : IAnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public List<Rectangle> SpriteSourceRectangles { get; set; }
        public Rectangle SpriteDestinationRectangle { get; set; }
        public int CurrentFrame { get; set; }

        private int starCounter;

        public AnimatedSprite()
        {
            SpriteSourceRectangles = new List<Rectangle>();
            SpriteDestinationRectangle = new Rectangle();
            CurrentFrame = 0;
            starCounter = GameValues.AnimatedSpriteStarCountMin;
        }

        public void UpdateSpritePosition(Vector2 newPosition)
        {
            SpriteDestinationRectangle = new Rectangle((int)newPosition.X, (int)newPosition.Y, SpriteDestinationRectangle.Width, SpriteDestinationRectangle.Height);
        }

        public void AdvanceFrame()
        {
            if (CurrentFrame < SpriteSourceRectangles.Count - 1)
            {
                CurrentFrame++;
            }
            else
            {
                CurrentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, SpriteDestinationRectangle, SpriteSourceRectangles[CurrentFrame], Color.White);
        }

        public void StarDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (starCounter < GameValues.AnimatedSpriteStarCounterMax1)
            {
                spriteBatch.Draw(Texture, SpriteDestinationRectangle, SpriteSourceRectangles[CurrentFrame], Color.White);
                starCounter++;
            }
            else if (starCounter < GameValues.AnimatedSpriteStarCounterMax2)
            {
                spriteBatch.Draw(Texture, SpriteDestinationRectangle, SpriteSourceRectangles[CurrentFrame], Color.Blue);
                starCounter++;
            }
            else if (starCounter < GameValues.AnimatedSpriteStarCounterMax3)
            {
                spriteBatch.Draw(Texture, SpriteDestinationRectangle, SpriteSourceRectangles[CurrentFrame], Color.Yellow);
                starCounter++;
            }
            else
            {
                spriteBatch.Draw(Texture, SpriteDestinationRectangle, SpriteSourceRectangles[CurrentFrame], Color.White);
                starCounter = GameValues.AnimatedSpriteStarCountMin;
            }
        }
    }
}