using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.NET
{
    public static class ICommandHandlerExtensions
    {
        public static ICommandHandler WithCommandName(this ICommandHandler commandHandler, string newCommand)
        {
            return new NewCommandNameDecorator(commandHandler, newCommand);
        }
    }
}
