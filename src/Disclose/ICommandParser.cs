using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.NET
{
    public interface ICommandParser
    {
        void Init(DiscloseOptions options);

        ParsedCommand ParseCommand(string message);
    }
}
