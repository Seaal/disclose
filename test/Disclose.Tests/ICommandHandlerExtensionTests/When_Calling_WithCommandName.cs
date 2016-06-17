﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Disclose.DiscordClient;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Disclose.Tests.ICommandHandlerExtensionTests
{
    [TestFixture]
    public class When_Calling_WithCommandName
    {
        private ICommandHandler commandHandler;

        [SetUp]
        public void setup()
        {
            commandHandler = Substitute.For<ICommandHandler>();
        }

        [Test]
        [TestCase("test", "test2")]
        public void CommandName_Should_Be_New_CommandName(string originalCommandName, string newCommandName)
        {
            commandHandler.CommandName.Returns(originalCommandName);

            commandHandler = commandHandler.WithCommandName(newCommandName);

            commandHandler.CommandName.Should().Be(newCommandName);
        }

        [Test]
        public void Description_Should_Not_Change()
        {
            commandHandler.Description.Returns("helloworld");

            commandHandler = commandHandler.WithCommandName(Arg.Any<string>());

            commandHandler.Description.Should().Be("helloworld");
        }

        [Test]
        public async Task Handle_Should_Still_Be_Called()
        {
            ICommandHandler decoaratedCommandHandler = commandHandler.WithCommandName(Arg.Any<string>());

            IMessage message = Substitute.For<IMessage>();
            string arguments = "test";

            commandHandler.Handle(message, arguments)
                .Returns(Task.FromResult(0));

            await decoaratedCommandHandler.Handle(message, arguments);

            commandHandler.Received().Handle(message, arguments);
        }

        [Test]
        public void Init_Should_Still_Be_Called()
        {
            ICommandHandler decoaratedCommandHandler = commandHandler.WithCommandName(Arg.Any<string>());

            IDiscloseSettings disclose = Substitute.For<IDiscloseSettings>();
            IDiscordCommands discordCommands = Substitute.For<IDiscordCommands>();

            commandHandler.When(ch => ch.Init(disclose, discordCommands)).Do(ci => { });

            decoaratedCommandHandler.Init(disclose, discordCommands);

            commandHandler.Received().Init(disclose, discordCommands);
        }
    }
}
