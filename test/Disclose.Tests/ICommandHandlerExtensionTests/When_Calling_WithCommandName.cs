using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.NET;
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
            commandHandler.Command.Returns(originalCommandName);

            commandHandler = commandHandler.WithCommandName(newCommandName);

            commandHandler.Command.Should().Be(newCommandName);
        }

        [Test]
        public void Description_Should_Not_Change()
        {
            commandHandler.Description.Returns("helloworld");

            commandHandler = commandHandler.WithCommandName(Arg.Any<string>());

            commandHandler.Description.Should().Be("helloworld");
        }
    }
}
