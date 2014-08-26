using Microsoft.Xna.Framework;
using SuperMario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMario.Commands.GenericCollisionCommands
{
    class GenericSideCollisionCommand : ICommand
    {
        private IDynamicObject dynamicObject;

        public GenericSideCollisionCommand(IDynamicObject dynamicObject)
        {
            this.dynamicObject = dynamicObject;
        }

        public void Execute()
        {
            dynamicObject.Velocity = new Vector2(dynamicObject.Velocity.X * (-1), dynamicObject.Velocity.Y);
        }
    }
}
