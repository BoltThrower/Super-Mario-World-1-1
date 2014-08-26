using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class FloorTile : IStaticObject
    {
        public Vector2 Position { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        private List<AnimatedSprite> sprites;

        public FloorTile(Vector2 position, Vector2 size, string color)
        {
            sprites = new List<AnimatedSprite>();
            Position = position;
            for (int i = 0; i < size.X; i++)
            {
                for (int j = 0; j < size.Y; j++)
                {
                    Vector2 tempPosition = new Vector2(position.X + (i * GameValues.LevelCellSize), position.Y + (j * GameValues.LevelCellSize));
                    if (color == "Red")
                    {
                        AnimatedSprite sprite = AnimatedSpriteFactory.Instance.BuildFloorTileSprite(tempPosition);
                        sprite.UpdateSpritePosition(tempPosition);
                        sprites.Add(sprite);
                    }
                    else
                    {
                        AnimatedSprite sprite = AnimatedSpriteFactory.Instance.BuildFloorTileBlueSprite(Position);
                        sprite.UpdateSpritePosition(tempPosition);
                        sprites.Add(sprite);
                    }
                }
            }
            CollisionRectangle = new Rectangle((int)position.X, (int)position.Y, GameValues.LevelCellSize * (int)size.X, GameValues.LevelCellSize * (int)size.Y);
        }

        public void Update(GameTime gameTime)
        {
            // floor tile only has one animation frame and doesn't need to call AdvanceFrame()
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (AnimatedSprite sprite in sprites)
            {
                sprite.Draw(spriteBatch, gameTime);
            }
        }
    }
}