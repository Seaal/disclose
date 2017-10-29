using System;
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
        public void Setup()
        {
            commandHandler = Substitute.For<ICommandHandler>();
        }

        [Test]
        [TestCase("test123", "test2345")]
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

            commandHandler = commandHandler.WithCommandName("test");

            commandHandler.Description.Should().Be("helloworld");
        }

        [Test]
        public void UserFilter_Should_Not_Change()
        {
            Func<DiscloseUser, bool> userFilter = u => false;

            commandHandler.UserFilter.Returns(userFilter);

            commandHandler = commandHandler.WithCommandName("test");

            commandHandler.UserFilter.Should().BeSameAs(userFilter);
        }

        [Test]
        public void ChannelFilter_Should_Not_Change()
        {
            Func<DiscloseChannel, bool> channelFilter = c => false;

            commandHandler.ChannelFilter.Returns(channelFilter);

            commandHandler = commandHandler.WithCommandName("test");

            commandHandler.ChannelFilter.Should().BeSameAs(channelFilter);
        }

        [Test]
        public async Task Handle_Should_Still_Be_Called()
        {
            ICommandHandler decoaratedCommandHandler = commandHandler.WithCommandName("test");

            DiscloseMessage message = new DiscloseMessage(Substitute.For<IMessage>(), null);
            string arguments = "test";

            commandHandler.Handle(message, arguments)
                .Returns(Task.FromResult(0));

            await decoaratedCommandHandler.Handle(message, arguments);

            await commandHandler.Received().Handle(message, arguments);
        }

        [Test]
        public void Init_Should_Still_Be_Called()
        {
            ICommandHandler decoaratedCommandHandler = commandHandler.WithCommandName("test");

            IDiscloseFacade disclose = Substitute.For<IDiscloseFacade>();
            IDataStore dataStore = Substitute.For<IDataStore>();

            commandHandler.When(ch => ch.Init(disclose, dataStore)).Do(ci => { });

            decoaratedCommandHandler.Init(disclose, dataStore);

            commandHandler.Received().Init(disclose, dataStore);
        }
    }
}
