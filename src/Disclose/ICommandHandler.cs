using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;

namespace Disclose.NET
{
    public interface ICommandHandler
    {
        string Command { get; }

        string Description { get; }

        Task Handle(DiscloseClient client, MessageEventArgs e, string arguments);
    }
}
