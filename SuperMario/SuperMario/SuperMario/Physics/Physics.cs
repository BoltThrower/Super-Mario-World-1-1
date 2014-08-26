using Microsoft.Xna.Framework;
using SuperMario.Interfaces;
using System;

namespace SuperMario
{
    static class Physics
    {
        private static CollisionManager collisionManager = new CollisionManager();

        //Update collisionRectangle with every movement.
        public static void Move(IDynamicObject dynamicObject)
        {
            dynamicObject.Position += dynamicObject.Velocity;
            dynamicObject.CollisionRectangle = new Rectangle((int)dynamicObject.Position.X, (int)dynamicObject.Position.Y, dynamicObject.CollisionRectangle.Width, dynamicObject.CollisionRectangle.Height);

            Vector2 newVelocity = new Vector2();
            newVelocity.X = Accelerate(dynamicObject.MaxVelocity.X, (dynamicObject.Velocity + dynamicObject.Acceleration).X);
            newVelocity.Y = Accelerate(dynamicObject.MaxVelocity.Y, (dynamicObject.Velocity + dynamicObject.Acceleration).Y + GameValues.PhysicsGravity);

            dynamicObject.Velocity = newVelocity;
            
            collisionManager.DetectCollision(dynamicObject);
        }

        private static float Accelerate(float MaxVelocity, float newValue)
        {
            float result;
            if (Math.Abs(newValue) > MaxVelocity)
            {
                result = newValue > 0 ? MaxVelocity : -MaxVelocity;
            }
            else
            {
                result = newValue;
            }
            return result;
        }
    }
}
