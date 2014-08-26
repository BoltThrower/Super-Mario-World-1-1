using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMario.Interfaces
{
    public interface IPlayableCommand
    {
        void Execute(IPlayableObject playableObject);
    }
}
