using Microsoft.Xna.Framework;
using SuperMario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMario.Commands.GenericCollisionCommands
{
    class GenericTopOrBottomCollisionCommand : ICommand
    {
        private IDynamicObject dynamicObject;

        public GenericTopOrBottomCollisionCommand(IDynamicObject dynamicObject)
        {
            this.dynamicObject = dynamicObject;
        }

        public void Execute()
        {
            dynamicObject.Velocity = new Vector2(dynamicObject.Velocity.X, dynamicObject.Velocity.Y * -1);
        }
    }
}
