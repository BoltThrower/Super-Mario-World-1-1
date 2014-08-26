using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario.Interfaces
{
    public interface IItemState
    {
        Rectangle CollisionRectangle { get; set; }
        int ScoreValue { get; set; }
        ScoreSprite ScoreSprite { get; set; }

        void Initialize();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
