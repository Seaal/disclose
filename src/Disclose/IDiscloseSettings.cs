using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose
{
    public interface IDiscloseSettings
    {
        IReadOnlyCollection<ICommandHandler> CommandHandlers { get; }
    }
}
