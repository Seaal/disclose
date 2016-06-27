using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Disclose.Tests.DiscloseClientTests
{
    [TestFixture]
    public class When_Registering_User_Joins_Server_Handlers
    {
        private DiscloseClient _client;
        private ICommandParser _parser;
        private IDataStore _dataStore;
        private IDiscordClient _discordClient;

        [SetUp]
        public void Setup()
        {
            _parser = Substitute.For<ICommandParser>();
            _discordClient = Substitute.For<IDiscordClient>();
            _dataStore = Substitute.For<IDataStore>();

            _client = new DiscloseClient(_discordClient, _parser);
        }

        [Test]
        public void Should_Init_Handler()
        {
            IUserJoinsServerHandler handler = Substitute.For<IUserJoinsServerHandler>();

            _client.Register(handler);

            handler.Received(1).Init(_client, _discordClient, _dataStore);
        }

        [Test]
        public void Should_Throw_If_Handler_Is_Null()
        {
            Action act = () => _client.Register((IUserJoinsServerHandler)null); 

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
