using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class Pipe : IStaticObject
    {
        public Vector2 Position { get; set; }
        public Rectangle CollisionRectangle { get; set; }
        public bool IsWarpPipe { get; set; }
        public bool IsHorizontalPipe { get; set; }

        private AnimatedSprite pipeSprite;
        private List<AnimatedSprite> baseSprites;

        public Pipe(Vector2 position, int height, bool isWarpPipe, string pipeType)
        {

            this.IsWarpPipe = isWarpPipe;
            baseSprites = new List<AnimatedSprite>();
            Position = position;

            if (pipeType == "Top")
            {
                pipeSprite = AnimatedSpriteFactory.Instance.BuildPipeTopSprite(position);
                for (int i = 1; i < height; i++)
                {
                    AnimatedSprite baseSprite = AnimatedSpriteFactory.Instance.BuildPipeBaseSprite(new Vector2(position.X, position.Y + (GameValues.LevelCellSize * i)));
                    baseSprites.Add(baseSprite);
                }
                CollisionRectangle = new Rectangle((int)position.X, (int)position.Y, pipeSprite.Texture.Width / 2, GameValues.LevelCellSize * height);
            }
            else if(pipeType == "Horizontal")
            {
                this.IsHorizontalPipe = true;
                pipeSprite = AnimatedSpriteFactory.Instance.BuildPipeHorizontalSprite(position);
                CollisionRectangle = new Rectangle((int)position.X, (int)position.Y, pipeSprite.Texture.Width, pipeSprite.Texture.Height);
            }
            else if (pipeType == "Connector")
            {
                pipeSprite = AnimatedSpriteFactory.Instance.BuildPipeHorizontalConnectorSprite(position);
                for (int i = 1; i < height; i++)
                {
                    AnimatedSprite baseSprite = AnimatedSpriteFactory.Instance.BuildPipeBaseSprite(new Vector2(position.X - 2, position.Y - (GameValues.LevelCellSize * i)));
                    baseSprites.Add(baseSprite);
                }
                CollisionRectangle = new Rectangle((int)position.X, (int)position.Y - (GameValues.LevelCellSize * height), pipeSprite.Texture.Width / 2, GameValues.LevelCellSize * height + pipeSprite.Texture.Height);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            pipeSprite.Draw(spriteBatch, gameTime);

            foreach (AnimatedSprite baseSprite in baseSprites)
            {
                baseSprite.Draw(spriteBatch, gameTime);
            }
        }
    }
}
