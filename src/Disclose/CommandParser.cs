using System;
using System.Text.RegularExpressions;
using Disclose.DiscordClient;

namespace Disclose
{
    public class CommandParser : ICommandParser
    {
        private Regex _commandRegex;
        private Regex _directMessageCommandRegex;

        public void Init(DiscloseOptions options)
        {
            string identifier = Regex.Escape(options.CommandCharacter);
            string regex;

            if (options.UseAlias)
            {
                string aliases = String.Join("|", options.Aliases);

                regex = $"^(?:{identifier}(?:{aliases}))\\s+(\\S+)(?:\\s+([\\s\\S]+))?";
            }
            else
            {
                regex = $"^(?:{identifier})(\\S+)(?:\\s+([\\s\\S]+))?";
            }

            _commandRegex = new Regex(regex, RegexOptions.Singleline | RegexOptions.Compiled);

            if (options.SimpleDirectMessages)
            {
                string directMessageRegex = "^(\\S+)(?:\\s+([\\s\\S]+))?";

                _directMessageCommandRegex = new Regex(directMessageRegex, RegexOptions.Singleline | RegexOptions.Compiled);
            }
            else
            {
                _directMessageCommandRegex = _commandRegex;
            }
    }

        public ParsedCommand ParseCommand(IMessage message)
        {
            Regex parsingRegex = GetParsingRegex(message);

            Match match = parsingRegex.Match(message.Text);

            if (!match.Success)
            {
                return ParsedCommand.Unsuccessful();
            }

            string command = match.Groups[1].Value.ToLowerInvariant();

            string argument = null;

            if (match.Groups.Count == 3 && match.Groups[2].Value != "")
            {
                argument = match.Groups[2].Value;
            }

            return new ParsedCommand()
            {
                Argument = argument,
                Command = command.ToLowerInvariant(),
                Success = true
            };
        }

        private Regex GetParsingRegex(IMessage message)
        {
            if (message.Channel.IsPrivateMessage)
            {
                return _directMessageCommandRegex;
            }

            return _commandRegex;
        }
    }
}
