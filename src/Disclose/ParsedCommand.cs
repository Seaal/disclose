using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.NET
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
