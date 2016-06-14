using Disclose.DiscordClient;
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

            _parser.Received(0).ParseCommand(Arg.Any<string>());
        }

        [Test]
        public void Should_Not_Handle_If_ParsedCommand_Is_Not_Successful()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _parser.ParseCommand(Arg.Any<string>()).Returns(ParsedCommand.Unsuccessful());

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");

            _discloseClient.RegisterCommandHandler(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(0).Handle(message, Arg.Any<string>());
        }

        [Test]
        public void Should_Not_Handle_If_No_Matching_Command_Found()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _parser.ParseCommand(Arg.Any<string>()).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test2");

            _discloseClient.RegisterCommandHandler(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(0).Handle(message, Arg.Any<string>());
        }

        [Test]
        public void Should_Handle_If_ParsedCommand_Name_Matches_CommandName()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _parser.ParseCommand(Arg.Any<string>()).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");

            _discloseClient.RegisterCommandHandler(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(1).Handle(message, Arg.Any<string>());
        }

        [Test]
        public void Should_Pass_Arguments_From_ParsedCommand()
        {
            IMessage message = Substitute.For<IMessage>();

            message.User.Id.Returns((ulong)123);

            _parser.ParseCommand(Arg.Any<string>()).Returns(new ParsedCommand()
            {
                Success = true,
                Command = "test",
                Argument = "hello world"
            });

            ICommandHandler commandHandler = Substitute.For<ICommandHandler>();

            commandHandler.CommandName.Returns("test");

            _discloseClient.RegisterCommandHandler(commandHandler);

            _discordClient.OnMessageReceived += Raise.EventWith(new object(), new MessageEventArgs(message));

            commandHandler.Received(1).Handle(message, "hello world");
        }
    }
}
