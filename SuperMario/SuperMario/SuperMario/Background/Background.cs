using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario
{
    class Background 
    {
        private AnimatedSprite backgroundBlackSprite, backgroundBlueSprite;
        public Vector2 Position { get; set; }
        private int screenWidth, screenHeight;

        public Background(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            backgroundBlueSprite = AnimatedSpriteFactory.Instance.BuildBackgroundBlueSprite(Mario.Instance.Position, screenWidth * 2, screenHeight * 2);
            backgroundBlackSprite = AnimatedSpriteFactory.Instance.BuildBackgroundBlackSprite(Level.Instance.CoinRoomPosition, screenWidth * 2, screenHeight * 2);
        }

        public void Update()
        {
            // We only want to update the backgrounds position if Mario is not in the coin room.
            if (!Mario.Instance.InCoinRoom)
            {
                backgroundBlueSprite.UpdateSpritePosition(new Vector2((Mario.Instance.Position.X) - screenWidth / GameValues.BackgroundBlueSpriteOffset, 0));
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Mario.Instance.InCoinRoom)
            {
                spriteBatch.Draw(backgroundBlackSprite.Texture, backgroundBlackSprite.SpriteDestinationRectangle, backgroundBlackSprite.SpriteSourceRectangles[0], Color.White);
            }
            else
            {
                spriteBatch.Draw(backgroundBlueSprite.Texture, backgroundBlueSprite.SpriteDestinationRectangle, backgroundBlueSprite.SpriteSourceRectangles[0], Color.White);
            }
        }
    }
}
