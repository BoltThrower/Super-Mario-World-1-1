using Microsoft.Xna.Framework;
using SuperMario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMario.Commands.GenericCollisionCommands
{
    class GenericStaticGroundCollisionCommand : ICommand
    {
        private IDynamicObject dynamicObject;
        private IStaticObject staticObject;

        public GenericStaticGroundCollisionCommand(IDynamicObject dynamicObject, IStaticObject staticObject)
        {
            this.dynamicObject = dynamicObject;
            this.staticObject = staticObject;
        }

        public void Execute()
        {
            dynamicObject.Position = new Vector2(dynamicObject.Position.X, staticObject.CollisionRectangle.Top - dynamicObject.CollisionRectangle.Height);
            dynamicObject.CollisionRectangle = new Rectangle((int)dynamicObject.Position.X, (int)dynamicObject.Position.Y, dynamicObject.CollisionRectangle.Width, dynamicObject.CollisionRectangle.Height);
        }
    }
}
