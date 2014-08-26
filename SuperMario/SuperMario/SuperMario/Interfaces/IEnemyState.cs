using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario.Interfaces
{
    public interface IEnemyState
    {
        Rectangle CollisionRectangle { get; set; }
        int ScoreValue { get; set; }
        ScoreSprite ScoreSprite { get; set; }

        void Update(GameTime gameTime, Vector2 position);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
