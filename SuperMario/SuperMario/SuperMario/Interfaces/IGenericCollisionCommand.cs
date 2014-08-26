using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMario.Interfaces
{
    interface IGenericCollisionCommand
    {
        void Execute(IDynamicObject dynamicObject1, IDynamicObject dynamicObject2);
        void Execute(IDynamicObject dynamicObject, IStaticObject staticObject);
    }
}
