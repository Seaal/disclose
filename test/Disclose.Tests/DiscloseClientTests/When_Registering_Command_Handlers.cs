using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Disclose.Tests.DiscloseClientTests
{
    [TestFixture]
    public class When_Registering_Command_Handlers
    {
        private Disclose.DiscloseClient _client;
        private ICommandParser _parser;
        private IDiscordClient _discordClient;

        [SetUp]
        public void Setup()
        {
            _parser = Substitute.For<ICommandParser>();
            _discordClient = Substitute.For<IDiscordClient>();

            _client = new DiscloseClient(_discordClient, _parser);
        }

        [Test]
        public void Should_Throw_When_Two_Handlers_Have_The_Same_Command()
        {
            ICommandHandler handler1 = Substitute.For<ICommandHandler>();
            ICommandHandler handler2 = Substitute.For<ICommandHandler>();

            handler1.CommandName.Returns("test");
            handler2.CommandName.Returns("test");

            _client.RegisterCommandHandler(handler1);

            Action action = () => _client.RegisterCommandHandler(handler2);

            action.ShouldThrow<ArgumentException>();
        }
    }
}
