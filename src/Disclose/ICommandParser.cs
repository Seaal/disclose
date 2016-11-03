using Disclose.DiscordClient;

namespace Disclose
{
    public interface ICommandParser
    {
        void Init(DiscloseOptions options);

        ParsedCommand ParseCommand(IMessage message);
    }
}
