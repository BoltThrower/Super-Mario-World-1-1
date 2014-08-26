using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario.Interfaces
{
    public interface IDynamicObject
    {
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
        Vector2 MaxVelocity { get; set; }
        Vector2 Acceleration { get; set; }
        Rectangle CollisionRectangle {get; set;}

        void HandleDynamicCollision(string collisionDirection, IDynamicObject dynamicObjectState);
        void HandleStaticCollision(string collisionDirection, IStaticObject staticObjectState);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
