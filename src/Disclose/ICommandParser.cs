namespace Disclose
{
    public interface ICommandParser
    {
        void Init(DiscloseOptions options);

        ParsedCommand ParseCommand(string message);
    }
}
