using System;
using Microsoft.Xna.Framework;
using SuperMario.Interfaces;

namespace SuperMario
{
    public class CollisionManager
    {
        public struct CollisionStructure
        {
            public String firstObjectCollisionDirection { get; set; }
            public String secondObjectCollisionDirection { get; set; }
        }

        public CollisionStructure dynamicCollisionStruct, staticCollisionStruct;
	
	    public void DetectCollision(IDynamicObject dynamic1)
	    {
			foreach (IDynamicObject dynamic2 in Level.Instance.DynamicObjects)
			{
                // Make sure that dynamic1 is not the same object as dynamic2, and dynamic1 is on screen, and they're intersecting (might change second check to if the objects are near each other)
                if (dynamic1 != dynamic2 && Math.Abs(dynamic2.Position.X - Mario.Instance.Position.X) <= GameValues.CollisionManagerDetectCollisionMax && dynamic1.CollisionRectangle.Intersects(dynamic2.CollisionRectangle))
                {
                    dynamicCollisionStruct = CalculateCollisionDirection(dynamic1, dynamic2);

                    dynamic1.HandleDynamicCollision(dynamicCollisionStruct.firstObjectCollisionDirection, dynamic2);
                    dynamic2.HandleDynamicCollision(dynamicCollisionStruct.secondObjectCollisionDirection, dynamic1);

                }
			}

			foreach (IStaticObject staticObject in Level.Instance.StaticObjects)
			{
                if (dynamic1.CollisionRectangle.Intersects(staticObject.CollisionRectangle))
                {
                    staticCollisionStruct = CalculateCollisionDirection(dynamic1, staticObject);

                    dynamic1.HandleStaticCollision(staticCollisionStruct.firstObjectCollisionDirection, staticObject);
                }
            }
	    }
        

        private CollisionStructure CalculateCollisionDirection(IDynamicObject dynamic1, IDynamicObject dynamic2)
        {
            CollisionStructure collisionStruct = new CollisionStructure();

            bool collisionHappened = false;
            Rectangle dynamicOneOldCollisionRectangle = CalculateOldCollisionRectangle(dynamic1);
           
            if (dynamic1.Velocity.Y > 0) // dynamic1 is moving down
            {

                // Top of d2 is farther down than bottom of old d1
                if (dynamic2.CollisionRectangle.Top >= dynamicOneOldCollisionRectangle.Bottom)
                {
                    // dynamic1 on top of dynamic2 : dynamic2 on bottom of dynamic1
                    collisionStruct.firstObjectCollisionDirection = GameValues.CollisionDirectionBottom;
                    collisionStruct.secondObjectCollisionDirection = GameValues.CollisionDirectionTop;
                    collisionHappened = true;
                }

            }
            else if (dynamic1.Velocity.Y < 0) // dynamic1 is moving up
            {
                // Bottom of d2 is higher up than top of old d1
                if (dynamic2.CollisionRectangle.Bottom <= dynamicOneOldCollisionRectangle.Top)
                {
                    // dynamic1 on bottom of dynamic2 : dynamic2 on top of dynamic1
                    collisionStruct.firstObjectCollisionDirection = GameValues.CollisionDirectionTop;
                    collisionStruct.secondObjectCollisionDirection = GameValues.CollisionDirectionBottom;
                    collisionHappened = true;
                }

            }
            if (dynamic1.Velocity.X > 0 && !collisionHappened) // dynamic1 is moving right
            {

                // Left side of d2 is farther right than right side of old d1
                if (dynamic2.CollisionRectangle.Left >= dynamicOneOldCollisionRectangle.Right)
                {
                    // dynamic1 on left side of dynamic2 : dynamic2 on right side of dynamic1
                    collisionStruct.firstObjectCollisionDirection = GameValues.CollisionDirectionRight;
                    collisionStruct.secondObjectCollisionDirection = GameValues.CollisionDirectionLeft;
                }

            }
            else if (dynamic1.Velocity.X < 0 && !collisionHappened) // dynamic1 is moving left
            {

                // Right side of d2 is farther left than left side of old d1
                if (dynamic2.CollisionRectangle.Right <= dynamicOneOldCollisionRectangle.Left)
                {
                    // dynamic1 on right side of dynamic2 : dynamic2 on left side of dynamic1
                    collisionStruct.firstObjectCollisionDirection = GameValues.CollisionDirectionLeft;
                    collisionStruct.secondObjectCollisionDirection = GameValues.CollisionDirectionRight;
                }

            }

            return collisionStruct;
        }
        private CollisionStructure CalculateCollisionDirection(IDynamicObject dynamicObject, IStaticObject staticObject)
        {
            CollisionStructure collisionStruct = new CollisionStructure();
            bool collisionHappened = false;
            Rectangle dynamicObjectOldCollisionRectangle = CalculateOldCollisionRectangle(dynamicObject);

            if (dynamicObject.Velocity.X > 0) // dynamicObject is moving right
            {
                // Left side of s is farther right than right side of old d
                if (staticObject.CollisionRectangle.Left >= dynamicObjectOldCollisionRectangle.Right)
                {
                    // dynamicObject on left side of staticObject : dynamicObject on right side of dynamicObject
                    collisionStruct.firstObjectCollisionDirection = GameValues.CollisionDirectionRight;
                    collisionStruct.secondObjectCollisionDirection = GameValues.CollisionDirectionLeft;
                    collisionHappened = true;
                }

            }
            else if (dynamicObject.Velocity.X < 0) // dynamicObject is moving left
            {

                // Right side of s is farther left than left side of old d
                if (staticObject.CollisionRectangle.Right <= dynamicObjectOldCollisionRectangle.Left)
                {
                    // dynamicObject on right side of staticObject : staticObject on left side of dynamicObject
                    collisionStruct.firstObjectCollisionDirection = GameValues.CollisionDirectionLeft;
                    collisionStruct.secondObjectCollisionDirection = GameValues.CollisionDirectionRight;
                    collisionHappened = true;
                }

            }

            if (dynamicObject.Velocity.Y > 0 && !collisionHappened) // dynamicObject is moving down
            {

                // Top of d2 is farther down than bottom of old d1
                if (staticObject.CollisionRectangle.Top >= dynamicObjectOldCollisionRectangle.Bottom)
                {
                    // dynamicObject on top of staticObject : staticObject on bottom of dynamicObject
                    collisionStruct.firstObjectCollisionDirection = GameValues.CollisionDirectionBottom;
                    collisionStruct.secondObjectCollisionDirection = GameValues.CollisionDirectionTop;
                }
            }
          
            return collisionStruct;
        }

        private Rectangle CalculateOldCollisionRectangle(IDynamicObject dynamicObject)
        {
            Vector2 oldPos = dynamicObject.Position - dynamicObject.Velocity;
            return new Rectangle((int)oldPos.X, (int)oldPos.Y, dynamicObject.CollisionRectangle.Width, dynamicObject.CollisionRectangle.Height);
        }
    }
}
