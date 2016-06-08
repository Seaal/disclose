using System;
using System.Text.RegularExpressions;

namespace Disclose
{
    public class CommandParser : ICommandParser
    {
        private Regex _commandRegex;

        public void Init(DiscloseOptions options)
        {
            string identifier = Regex.Escape(options.CommandCharacter);
            string regex;

            if (options.UseAlias)
            {
                string aliases = String.Join("|", options.Aliases);

                regex = $"^(?:{identifier}(?:{aliases})) (\\S+)(?: ([\\s\\S]+))?";
            }
            else
            {
                regex = $"^(?:{identifier})(\\S+)(?: ([\\s\\S]+))?";
            }

            _commandRegex = new Regex(regex);
    }

        public ParsedCommand ParseCommand(string message)
        {
            Match match = _commandRegex.Match(message);

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
    }
}
