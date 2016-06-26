using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose
{
    public static class ICommandHandlerExtensions
    {
        /// <summary>
        /// Overwrite the command name of a Command Handler. Required if two commands have the same CommandName.
        /// </summary>
        /// <param name="newCommand">The command name to set for this command.</param>
        /// <returns></returns>
        public static ICommandHandler WithCommandName(this ICommandHandler commandHandler, string newCommand)
        {
            return new NewCommandNameDecorator(commandHandler, newCommand);
        }
    }
}
