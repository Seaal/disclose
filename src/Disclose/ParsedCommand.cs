namespace Disclose
{
    public class ParsedCommand
    {
        public bool Success { get; set; }
        public string Command { get; set; }
        public string Argument { get; set; }

        public static ParsedCommand Unsuccessful()
        {
            return new ParsedCommand()
            {
                Success = false
            };
        }
    } 
}
