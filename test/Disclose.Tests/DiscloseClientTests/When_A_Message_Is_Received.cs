using System;
using System.Threading.Tasks;
using Disclose.DiscordClient;
using Disclose.DiscordClient.DiscordNetAdapters;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace Disclose.Tests.DiscloseClientTests
{
    [TestFixture]
    public class When_A_Message_Is_Received
    {
        private ICommandParser _parser;
        private IDiscordClient _discordClient;
        private DiscloseClient _discloseClient;

        private IMessage _message;
        private IServer _server;

        [SetUp]
        public void Setup()
        {
            _parser = Substitute.For<ICommandParser>();
            _discordClient = Substitute.For<IDiscordClient>();
            _discloseClient = new DiscloseClient(_discordClient, _parser);

            _discloseClient.Init(new DiscloseOptions());

            _message = Substitute.For<IMessage>();
            _server = Substitute.For<IServer>();

            IServerUser serverUser = Substitute.For<IServerUser>();

            serverUser.Id.Returns((ulong)123);

            _message.User.Returns(serverUser);

            _server.Id.Returns((ulong)1);

            _discordClient.OnServerAvailable += Raise.EventWith(new object(), new ServerEventArgs(_server));
        }

        [Test]
        public void Should_Not_Parse_If_Message_Is_Sent_By_Self()
        {
            _discordClient.ClientId.Returns((ulong)123);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(_message));

            _parser.Received(0).ParseCommand(Arg.Any<IMessage>());
        }

        [Test]
        public void Should_Not_Handle_If_ParsedCommand_Is_Not_Successful()
        {
            _parser.ParseCommand(Arg.Any<IMessage>()).Returns(ParsedCommand.Unsuccessful());

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(_message));

            commandHandler.Received(0).Handle(Arg.Any<DiscloseMessage>(), Arg.Any<string>());
        }

        [Test]
        public void Should_Not_Handle_If_No_Matching_Command_Found()
        {
            _parser.ParseCommand(Arg.Any<IMessage>()).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test2");

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(_message));

            commandHandler.Received(0).Handle(Arg.Any<DiscloseMessage>(), Arg.Any<string>());
        }

        [Test]
        public void Should_Handle_If_ParsedCommand_Name_Matches_CommandName()
        {
            _parser.ParseCommand(_message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns((Func<DiscloseUser, bool>)null);
            commandHandler.ChannelFilter.Returns((Func<DiscloseChannel, bool>)null);

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(_message));

            commandHandler.Received(1).Handle(Arg.Any<DiscloseMessage>(), Arg.Any<string>());
        }

        [Test]
        public void Should_Not_Handle_If_UserFilter_Returns_False()
        {
            _parser.ParseCommand(_message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns(u => false);
            commandHandler.ChannelFilter.Returns((Func<DiscloseChannel, bool>)null);

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(_message));

            commandHandler.Received(0).Handle(Arg.Any<DiscloseMessage>(), Arg.Any<string>());
        }

        [Test]
        public void Should_Handle_If_UserFilter_Returns_True()
        {
            _parser.ParseCommand(_message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns(u => true);
            commandHandler.ChannelFilter.Returns((Func<DiscloseChannel, bool>)null);

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(_message));

            commandHandler.Received(1).Handle(Arg.Any<DiscloseMessage>(), Arg.Any<string>());
        }

        [Test]
        public void Should_Not_Handle_If_ChannelFilter_Returns_False()
        {
            _parser.ParseCommand(_message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns((Func<DiscloseUser, bool>)null);
            commandHandler.ChannelFilter.Returns(c => false);

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(_message));

            commandHandler.Received(0).Handle(Arg.Any<DiscloseMessage>(), Arg.Any<string>());
        }

        [Test]
        public void Should_Handle_If_ChannelFilter_Returns_True()
        {
            _parser.ParseCommand(_message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns((Func<DiscloseUser, bool>)null);
            commandHandler.ChannelFilter.Returns(c => true);

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(_message));

            commandHandler.Received(1).Handle(Arg.Any<DiscloseMessage>(), Arg.Any<string>());
        }

        [Test]
        public void Should_Pass_Arguments_From_ParsedCommand()
        {
            _parser.ParseCommand(_message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test",
                Argument = "hello world"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns((Func<DiscloseUser, bool>)null);
            commandHandler.ChannelFilter.Returns((Func<DiscloseChannel, bool>) null);

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(_message));

            commandHandler.Received(1).Handle(Arg.Any<DiscloseMessage>(), "hello world");
        }

        [Test]
        public async Task Should_Not_Handle_If_Direct_Message_And_User_Is_Not_On_Server()
        {
            _message.Channel.IsPrivateMessage.Returns(true);

            _parser.ParseCommand(_message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns((Func<DiscloseUser, bool>)null);
            commandHandler.ChannelFilter.Returns((Func<DiscloseChannel, bool>)null);

            _discloseClient.Register(commandHandler);

            IServerUser serverUser = Substitute.For<IServerUser>();

            serverUser.Id.Returns((ulong)1234);

            _server.GetUsersAsync().Returns(Task.FromResult((new[] {serverUser}).AsEnumerable()));

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(_message));

            await commandHandler.Received(0).Handle(Arg.Any<DiscloseMessage>(), null);
        }

        [Test]
        public void Should_Use_User_From_Server_If_Direct_Message()
        {
            _message.Channel.IsPrivateMessage.Returns(true);

            _parser.ParseCommand(_message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns((Func<DiscloseUser, bool>)null);
            commandHandler.ChannelFilter.Returns((Func<DiscloseChannel, bool>)null);

            _discloseClient.Register(commandHandler);

            IServerUser serverUser = Substitute.For<IServerUser>();

            serverUser.Id.Returns((ulong)123);
            serverUser.Name.Returns("foo");

            _server.GetUsersAsync().Returns(Task.FromResult((new[] { serverUser }).AsEnumerable()));

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(_message));

            commandHandler.Received(1).Handle(Arg.Is<DiscloseMessage>(m => m.User.Name == "foo"), null);
        }
    }
}
