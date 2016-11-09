# Disclose

Disclose is a framework for creating bots in the Discord chat application. It's built from the ground up to be flexible and simplify the creation of a bot. Disclose currently builds on top of Discord.NET, a free open-source library for connecting to the Discord API.

## Why Disclose?

Discord.NET is a great library, but I found myself writing a gigantic configuration in a static startup method, with no easy way to break things down. This made it difficult to keep things properly modularized and to write easy, clean code. I created Disclose to help keep commands and other features very modular, making the code simpler, easier to read and shareable.

## Quick Start

*(This assumes you already have a bot application set up on Discord. If not check out the [Discord Documentation](https://discordapp.com/developers/docs/intro))*

* Create a new .NET Console Application (.NET Core optional).

* Install the Disclose package: `Install-Package Disclose -Prerelease`

* Create a command handler for your bot:

``` C#
public class EchoCommandHandler : Handler, ICommandHandler
{
    public string CommandName => "echo";

    public string Description => "Repeats what the user says.";

    public async Task Handle(IMessage message, string arguments)
    {
        await Discord.SendMessageToChannel(message.Channel, arguments);
    }
}
```

* In your main method, bootstrap the bot, register your new command handler and connect to Discord:

``` C#
static void Main(string[] args)
{
    DiscloseClient client = DiscloseClient.Bootstrap(options =>
    {
        options.UseAlias = false;
        options.CommandCharacter = "!";
    });

    client.Register(new EchoCommandHandler());

    client.Connect("YOUR_BOT_TOKEN_HERE");
}
```

* Run your bot program, and type `!echo test` into a channel your bot can see.

**You can find common command handlers at the [Disclose Commands](https://github.com/Seaal/disclose-commands) repository.**

## I want to contribute

If you want to help out with features, suggestions or bug reports, just create an issue in this repository. If your issue is specific to a component (like a data store or a specific disclose provided command), then add the issue to that specific repository.
