using System;
using Disclose.DiscordClient;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Disclose.Tests.DiscloseClientTests
{
    [TestFixture]
    public class When_Registering_Command_Handlers
    {
        private DiscloseClient _client;
        private ICommandParser _parser;
        private IDiscordClient _discordClient;
        private IDataStore _dataStore;

        [SetUp]
        public void Setup()
        {
            _parser = Substitute.For<ICommandParser>();
            _discordClient = Substitute.For<IDiscordClient>();
            _dataStore = Substitute.For<IDataStore>();

            _client = new DiscloseClient(_discordClient, _parser, _dataStore);
        }

        [Test]
        public void Should_Throw_When_Two_Handlers_Have_The_Same_Command()
        {
            ICommandHandler handler1 = Substitute.For<ICommandHandler>();
            ICommandHandler handler2 = Substitute.For<ICommandHandler>();

            handler1.CommandName.Returns("test");
            handler2.CommandName.Returns("test");

            _client.Register(handler1);

            Action action = () => _client.Register(handler2);

            action.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Should_Throw_When_A_Handler_Has_A_Null_CommandName()
        {
            ICommandHandler handler = Substitute.For<ICommandHandler>();

            handler.CommandName.Returns((string) null);

            Action action = () => _client.Register(handler);

            action.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Should_Throw_When_A_Handler_Has_An_Empty_CommandName()
        {
            ICommandHandler handler = Substitute.For<ICommandHandler>();

            handler.CommandName.Returns("");

            Action action = () => _client.Register(handler);

            action.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Should_Throw_When_A_Handler_Has_A_Whitespace_CommandName()
        {
            ICommandHandler handler = Substitute.For<ICommandHandler>();

            handler.CommandName.Returns("   \t");

            Action action = () => _client.Register(handler);

            action.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Should_Init_Handler()
        {
            ICommandHandler handler = Substitute.For<ICommandHandler>();

            handler.CommandName.Returns("test");

            _client.Register(handler);

            handler.Received(1).Init(_client, _dataStore);
        }

        [Test]
        public void Should_Throw_If_Handler_Is_Null()
        {
            Action act = () => _client.Register((ICommandHandler)null);

            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
