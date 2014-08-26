using Microsoft.Xna.Framework;
using SuperMario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMario.Commands.GenericCollisionCommands
{
    class GenericDynamicBottomCollisionCommand : ICommand
    {
        private IDynamicObject dynamicObject1;
        private IDynamicObject dynamicObject2;

        public GenericDynamicBottomCollisionCommand(IDynamicObject dynamicObject1, IDynamicObject dynamicObject2)
        {
            this.dynamicObject1 = dynamicObject1;
            this.dynamicObject2 = dynamicObject2;
        }

        public void Execute()
        {
            dynamicObject1.Position = new Vector2(dynamicObject1.Position.X, dynamicObject2.CollisionRectangle.Top - dynamicObject1.CollisionRectangle.Height);
            dynamicObject1.CollisionRectangle = new Rectangle((int)dynamicObject1.Position.X, (int)dynamicObject1.Position.Y, dynamicObject1.CollisionRectangle.Width, dynamicObject1.CollisionRectangle.Height);
        }

        
    }
}
