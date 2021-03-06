﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Disclose.Tests.CommandParserTests
{
    [TestFixture]
    public class When_Parsing_With_Aliases
    {
        private CommandParser _parser;
        private DiscloseOptions _options;
        private IMessage _message;

        [SetUp]
        public void Setup()
        {
            _parser = new CommandParser();

            _options = new DiscloseOptions()
            {
                UseAlias = true,
                CommandCharacter = "!",
                Aliases = new [] { "test", "testbot" }
            };

            _message = Substitute.For<IMessage>();
        }

        [Test]
        public void Should_Not_Match_Only_The_Command_Character()
        {
            _parser.Init(_options);

            _message.Content.Returns("!");

            ParsedCommand parsedCommand = _parser.ParseCommand(_message);

            parsedCommand.Success.Should().BeFalse();
        }

        [Test]
        [TestCase("!", "b")]
        [TestCase("&", "ehbot")]
        [TestCase("bot", "%£%£")]
        public void Should_Only_Match_If_Message_Starts_With_Command_Character_And_Alias(string character, string alias)
        {
            _options.CommandCharacter = character;
            _options.Aliases = new[] {alias};

            _parser.Init(_options);

            _message.Content.Returns(character + alias + " test");

            ParsedCommand parsedCommand = _parser.ParseCommand(_message);

            parsedCommand.Success.Should().BeTrue();
        }

        [Test]
        [TestCase("^")]
        [TestCase("&")]
        [TestCase("hello world")]
        public void Should_Not_Match_If_Message_Does_Not_Start_With_Command_Character(string testString)
        {
            _parser.Init(_options);

            _message.Content.Returns(testString);

            ParsedCommand parsedCommand = _parser.ParseCommand(_message);

            parsedCommand.Success.Should().BeFalse();
        }

        [Test]
        [TestCase("!chicken hello")]
        [TestCase("!!!!")]
        [TestCase("!tesy help")]
        public void Should_Not_Match_If_Message_Does_Not_Use_A_Valid_Alias(string testString)
        {
            _parser.Init(_options);

            _message.Content.Returns(testString);

            ParsedCommand parsedCommand = _parser.ParseCommand(_message);

            parsedCommand.Success.Should().BeFalse();
        }

        [Test]
        [TestCase("test")]
        [TestCase("help")]
        [TestCase("hello")]
        public void Should_Have_Command_As_First_Word(string command)
        {
            _parser.Init(_options);

            _message.Content.Returns("!test " + command);

            ParsedCommand parsedCommand = _parser.ParseCommand(_message);

            parsedCommand.Success.Should().BeTrue();
            parsedCommand.Command.Should().Be(command);
        }

        [Test]
        [TestCase("test")]
        [TestCase("help")]
        [TestCase("hello")]
        public void Should_Have_Command_As_First_Word_With_Argument(string command)
        {
            _parser.Init(_options);

            _message.Content.Returns("!test " + command + " arguments");

            ParsedCommand parsedCommand = _parser.ParseCommand(_message);

            parsedCommand.Success.Should().BeTrue();
            parsedCommand.Command.Should().Be(command);
        }

        [Test]
        [TestCase("test")]
        [TestCase("help me!!!")]
        [TestCase("hello world, I am a robot!")]
        public void Should_Have_Argument_As_Everything_After_The_First_Word(string argument)
        {
            _parser.Init(_options);

            _message.Content.Returns("!test command " + argument);

            ParsedCommand parsedCommand = _parser.ParseCommand(_message);

            parsedCommand.Success.Should().BeTrue();
            parsedCommand.Argument.Should().Be(argument);
        }

        [Test]
        [TestCase("TEST")]
        [TestCase("hElpMe!!!")]
        [TestCase("heLLowoRld,IamarObot!")]
        public void Command_Should_Be_Lowercase(string command)
        {
            _parser.Init(_options);

            _message.Content.Returns("!test " + command);

            ParsedCommand parsedCommand = _parser.ParseCommand(_message);

            parsedCommand.Success.Should().BeTrue();
            parsedCommand.Command.Should().Be(command.ToLowerInvariant());
        }

        [Test]
        [TestCase(" ")]
        [TestCase("\t")]
        [TestCase("\n\n")]
        [TestCase(" \t\t \n\n \t  ")]
        [TestCase("\r\n")]
        public void Should_Match_If_Any_White_Space_Between_Alias_And_Command(string whiteSpace)
        {
            _parser.Init(_options);

            _message.Content.Returns("!test" + whiteSpace + "command");

            ParsedCommand parsedCommand = _parser.ParseCommand(_message);

            parsedCommand.Success.Should().BeTrue();
            parsedCommand.Command.Should().Be("command");
        }

        [Test]
        [TestCase(" ")]
        [TestCase("\t")]
        [TestCase("\n\n")]
        [TestCase(" \t\t \n\n \t  ")]
        [TestCase("\r\n")]
        public void Should_Match_If_Any_White_Space_Between_Command_And_Argument(string whiteSpace)
        {
            _parser.Init(_options);

            _message.Content.Returns("!test command" + whiteSpace + "argument test");

            ParsedCommand parsedCommand = _parser.ParseCommand(_message);

            parsedCommand.Success.Should().BeTrue();
            parsedCommand.Command.Should().Be("command");
            parsedCommand.Argument.Should().Be("argument test");
        }
    }
}
