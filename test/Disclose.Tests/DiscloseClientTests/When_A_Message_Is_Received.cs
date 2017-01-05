using System;
using System.Threading.Tasks;
using Disclose.DiscordClient;
using Disclose.DiscordClient.DiscordNetAdapters;
using NSubstitute;
using NUnit.Framework;

namespace Disclose.Tests.DiscloseClientTests
{
    [TestFixture]
    public class When_A_Message_Is_Received
    {
        private ICommandParser _parser;
        private IDiscordClient _discordClient;
        private DiscloseClient _discloseClient;

        [SetUp]
        public void Setup()
        {
            _parser = Substitute.For<ICommandParser>();
            _discordClient = Substitute.For<IDiscordClient>();
            _discloseClient = new DiscloseClient(_discordClient, _parser);

            _discloseClient.Init(new DiscloseOptions());
        }

        [Test]
        public void Should_Not_Parse_If_Message_Is_Sent_By_Self()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _discordClient.ClientId.Returns((ulong)123);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            _parser.Received(0).ParseCommand(Arg.Any<IMessage>());
        }

        [Test]
        public void Should_Not_Handle_If_ParsedCommand_Is_Not_Successful()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _parser.ParseCommand(Arg.Any<IMessage>()).Returns(ParsedCommand.Unsuccessful());

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(0).Handle(message, Arg.Any<string>());
        }

        [Test]
        public void Should_Not_Handle_If_No_Matching_Command_Found()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _parser.ParseCommand(Arg.Any<IMessage>()).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test2");

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(0).Handle(message, Arg.Any<string>());
        }

        [Test]
        public void Should_Handle_If_ParsedCommand_Name_Matches_CommandName()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _parser.ParseCommand(message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns((Func<IUser, bool>)null);
            commandHandler.ChannelFilter.Returns((Func<IChannel, bool>)null);

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(1).Handle(message, Arg.Any<string>());
        }

        [Test]
        public void Should_Not_Handle_If_UserFilter_Returns_False()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _parser.ParseCommand(message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns(u => false);
            commandHandler.ChannelFilter.Returns((Func<IChannel, bool>)null);

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(0).Handle(message, Arg.Any<string>());
        }

        [Test]
        public void Should_Handle_If_UserFilter_Returns_True()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _parser.ParseCommand(message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns(u => true);
            commandHandler.ChannelFilter.Returns((Func<IChannel, bool>)null);

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(1).Handle(message, Arg.Any<string>());
        }

        [Test]
        public void Should_Not_Handle_If_ChannelFilter_Returns_False()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _parser.ParseCommand(message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns((Func<IUser, bool>)null);
            commandHandler.ChannelFilter.Returns(c => false);

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(0).Handle(message, Arg.Any<string>());
        }

        [Test]
        public void Should_Handle_If_ChannelFilter_Returns_True()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _parser.ParseCommand(message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns((Func<IUser, bool>)null);
            commandHandler.ChannelFilter.Returns(c => true);

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(1).Handle(message, Arg.Any<string>());
        }

        [Test]
        public void Should_Pass_Arguments_From_ParsedCommand()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _parser.ParseCommand(message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test",
                Argument = "hello world"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns((Func<IUser, bool>)null);
            commandHandler.ChannelFilter.Returns((Func<IChannel, bool>) null);

            _discloseClient.Register(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(1).Handle(message, "hello world");
        }

        [Test]
        public void Should_Not_Handle_If_Direct_Message_And_User_Is_Not_On_Server()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);
            message.Channel.IsPrivateMessage.Returns(true);

            _parser.ParseCommand(message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns((Func<IUser, bool>)null);
            commandHandler.ChannelFilter.Returns((Func<IChannel, bool>)null);

            _discloseClient.Register(commandHandler);

            IServer server = Substitute.For<IServer>();

            IUser serverUser = Substitute.For<IUser>();

            serverUser.Id.Returns((ulong)1234);

            server.Users.Returns(new[] {serverUser});

            _discordClient.OnServerAvailable += Raise.EventWith(new object(), new ServerEventArgs(server));

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(0).Handle(message, null);
        }

        [Test]
        public void Should_Use_User_From_Server_If_Direct_Message()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);
            message.Channel.IsPrivateMessage.Returns(true);

            _parser.ParseCommand(message).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");
            commandHandler.UserFilter.Returns((Func<IUser, bool>)null);
            commandHandler.ChannelFilter.Returns((Func<IChannel, bool>)null);

            _discloseClient.Register(commandHandler);

            IServer server = Substitute.For<IServer>();

            IUser serverUser = Substitute.For<IUser>();

            serverUser.Id.Returns((ulong)123);

            server.Users.Returns(new[] { serverUser });

            _discordClient.OnServerAvailable += Raise.EventWith(new object(), new ServerEventArgs(server));

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(1).Handle(Arg.Is<IMessage>(m => m.User == serverUser), null);
        }
    }
}
