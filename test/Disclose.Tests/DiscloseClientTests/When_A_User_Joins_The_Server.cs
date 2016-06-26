using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace Disclose.Tests.DiscloseClientTests
{
    [TestFixture]
    public class When_A_User_Joins_The_Server
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
        public void Should_Run_All_Handlers()
        {
            IUser user = Substitute.For<IUser>();
            IServer server = Substitute.For<IServer>();

            IUserJoinsServerHandler handler1 = Substitute.For<IUserJoinsServerHandler>();
            IUserJoinsServerHandler handler2 = Substitute.For<IUserJoinsServerHandler>();

            handler1.Handle(user, server).Returns(Task.FromResult(0));
            handler2.Handle(user, server).Returns(Task.FromResult(0));

            _discloseClient.Register(handler1);
            _discloseClient.Register(handler2);

            _discordClient.OnUserJoinedServer += Raise.EventWith(new object(), new UserEventArgs(user, server));

            handler1.Received(1).Handle(user, server);
            handler2.Received(1).Handle(user, server);
        }

        [Test]
        public void Should_Run_Second_Handler_If_First_Throws()
        {
            IUser user = Substitute.For<IUser>();
            IServer server = Substitute.For<IServer>();

            IUserJoinsServerHandler handler1 = Substitute.For<IUserJoinsServerHandler>();
            IUserJoinsServerHandler handler2 = Substitute.For<IUserJoinsServerHandler>();

            handler1.Handle(user, server).Throws(new Exception());
            handler2.Handle(user, server).Returns(Task.FromResult(0));

            _discloseClient.Register(handler1);
            _discloseClient.Register(handler2);

            _discordClient.OnUserJoinedServer += Raise.EventWith(new object(), new UserEventArgs(user, server));

            handler1.Received(1).Handle(user, server);
            handler2.Received(1).Handle(user, server);
        }
    }
}
