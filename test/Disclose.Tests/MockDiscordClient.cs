using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using NSubstitute;

namespace Disclose.Tests
{
    public class MockDiscordClient : IDiscordClient
    {
        private readonly Message _message;

        public MockDiscordClient(Message message)
        {
            _message = message;
        }

        public void ExecuteAndWait(Func<Task> action)
        {
            action();
        }

        public Task Connect(string token)
        {
            MessageEventArgs e = new MessageEventArgs(_message);

            OnMessageReceived(this, e);

            return Task.FromResult(0);
        }

        public event EventHandler<MessageEventArgs> OnMessageReceived;
    }
}
