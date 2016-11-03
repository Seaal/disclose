using System;
using System.Collections.Generic;
using Disclose.DiscordClient;

namespace Disclose
{
    /// <summary>
    /// Options to customise how your Disclose Client works.
    /// </summary>
    public class DiscloseOptions
    {
        /// <summary>
        /// <para>The character(s) used to start a command. e.g. in <c>!disclose help</c>, the <c>!</c> is the CommandCharacter.</para>
        /// <para>Be careful with using "/" as Discord uses this for some of its own commands.</para>
        /// <para>Default value: !</para>
        /// </summary>
        public string CommandCharacter { get ; set; }

        /// <summary>
        /// <para>If set to true, the user will have to type an alias to get the bot to respond. e.g. !disclose help</para>
        /// <para>If set to false, the user will just have to type the CommandCharacter, e.g. !help</para>
        /// <para>Default value: true</para>
        /// </summary>
        public bool UseAlias { get; set; }

        /// <summary>
        /// <para>The aliases that the client uses to identify a user is typing a command. e.g. in <c>!disclose help</c>, <c>disclose</c> is the Alias.</para>
        /// <para>Does nothing if UseAlias is set to false.</para>
        /// <para>Default value: ["disclose", "disclosebot"]</para>
        /// </summary>
        public IEnumerable<string> Aliases { get; set; }

        /// <summary>
        /// Use this to filter which server to use for Private Message contexts. Can be null if the bot is only connected to one server.
        /// </summary>
        public Func<IServer, bool> ServerFilter { get; set; }

        /// <summary>
        /// If true, when Direct Messaging the bot only the command and its arguments need to be typed. e.g. <c>help</c> instead of <c>!disclose help</c>
        /// </summary>
        public bool SimpleDirectMessages { get; set; }

        public DiscloseOptions()
        {
            CommandCharacter = "!";
            UseAlias = true;
            Aliases = new[] {"disclose", "disclosebot"};
            ServerFilter = null;
            SimpleDirectMessages = true;
        }
    }
}
