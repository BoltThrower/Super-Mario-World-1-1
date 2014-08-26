using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.GameStates;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class Castle : IBackgroundItem
    {
        public Vector2 Position { get; set; }
        public Rectangle BoundingRectangle { get; set; }
        public bool CastleFlagMoving { get; set; }

        private AnimatedSprite castleSprite;
        private AnimatedSprite castleFlagSprite;
        private int gameNextLevelBuffer;

        public Castle(Vector2 position)
        {
            Position = position;
            castleSprite = AnimatedSpriteFactory.Instance.BuildBackgroundCastleSprite(position);
            BoundingRectangle = castleSprite.SpriteDestinationRectangle;

            castleFlagSprite = AnimatedSpriteFactory.Instance.BuildCastleFlag(new Vector2(BoundingRectangle.Center.X - GameValues.CastleFlagPositionOffset.X, BoundingRectangle.Top + GameValues.CastleFlagPositionOffset.Y));
            CastleFlagMoving = false;

            gameNextLevelBuffer = GameValues.CastleNextLevelBuffer;
        }

        public void Update()
        {
            if (CastleFlagMoving)
            {
                if (castleFlagSprite.SpriteDestinationRectangle.Top > BoundingRectangle.Top - GameValues.CastleFlagPositionOffset.Y)
                {
                    castleFlagSprite.SpriteDestinationRectangle = new Rectangle(castleFlagSprite.SpriteDestinationRectangle.X, castleFlagSprite.SpriteDestinationRectangle.Y - 1, castleFlagSprite.SpriteDestinationRectangle.Width, castleFlagSprite.SpriteDestinationRectangle.Height);
                }

                else
                {
                    if (gameNextLevelBuffer <= 0)
                    {
                        CastleFlagMoving = false;
                        GameStateMachine.Instance.GameState = new NextLevelState();
                    }
                    gameNextLevelBuffer--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            castleFlagSprite.Draw(spriteBatch, gameTime);
            castleSprite.Draw(spriteBatch, gameTime);
        }
    }
}
