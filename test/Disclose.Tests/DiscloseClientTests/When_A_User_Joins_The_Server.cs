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

        private IServerUser _user;
        private IServer _server;
        private IUserJoinsServerHandler _handler1;
        private IUserJoinsServerHandler _handler2;

        [SetUp]
        public void Setup()
        {
            _parser = Substitute.For<ICommandParser>();
            _discordClient = Substitute.For<IDiscordClient>();
            _discloseClient = new DiscloseClient(_discordClient, _parser);

            _discloseClient.Init(new DiscloseOptions());

            _user = Substitute.For<IServerUser>();
            _server = Substitute.For<IServer>();

            _user.Id.Returns((ulong)1);
            _server.Id.Returns((ulong)2);

            _handler1 = Substitute.For<IUserJoinsServerHandler>();
            _handler2 = Substitute.For<IUserJoinsServerHandler>();

            _discloseClient.Register(_handler1);
            _discloseClient.Register(_handler2);

            _discordClient.OnServerAvailable += Raise.EventWith<ServerEventArgs>(new object(), new ServerEventArgs(_server));
        }

        [Test]
        public void Should_Run_All_Handlers()
        {
            _handler1.Handle(Arg.Any<DiscloseUser>(), Arg.Any<DiscloseServer>()).Returns(Task.FromResult(0));
            _handler2.Handle(Arg.Any<DiscloseUser>(), Arg.Any<DiscloseServer>()).Returns(Task.FromResult(0));

            _discordClient.OnUserJoinedServer += Raise.EventWith(new object(), new UserEventArgs(_user, _server));

            _handler1.Received(1).Handle(Arg.Is<DiscloseUser>(u => u.Id == 1), Arg.Is<DiscloseServer>(s => s.Id == 2));
            _handler2.Received(1).Handle(Arg.Is<DiscloseUser>(u => u.Id == 1), Arg.Is<DiscloseServer>(s => s.Id == 2));
        }

        [Test]
        public void Should_Run_Second_Handler_If_First_Throws()
        {
            _handler1.Handle(Arg.Any<DiscloseUser>(), Arg.Any<DiscloseServer>()).Throws(new Exception());
            _handler2.Handle(Arg.Any<DiscloseUser>(), Arg.Any<DiscloseServer>()).Returns(Task.FromResult(0));

            _discordClient.OnUserJoinedServer += Raise.EventWith(new object(), new UserEventArgs(_user, _server));

            _handler1.Received(1).Handle(Arg.Any<DiscloseUser>(), Arg.Any<DiscloseServer>());
            _handler2.Received(1).Handle(Arg.Any<DiscloseUser>(), Arg.Any<DiscloseServer>());
        }
    }
}
