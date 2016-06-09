using Discord;
using NSubstitute;
using NUnit.Framework;

namespace Disclose.Tests.DiscloseClientTests
{
    [TestFixture]
    public class When_A_Message_Is_Received
    {
        private ICommandParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = Substitute.For<ICommandParser>();
        }

        [Test]
        public void Should_Not_Parse_If_Message_Was_Sent_By_Self()
        {
            Message message = Substitute.For<Message>();

            message.IsAuthor.Returns(true);

            IDiscordClient discordClient = new MockDiscordClient(message);

            DiscloseClient client = new DiscloseClient(discordClient, _parser);
            client.Init(new DiscloseOptions());

            client.Connect(null);

            _parser.ParseCommand(Arg.Any<string>()).Received(0);
        }
    }
}
