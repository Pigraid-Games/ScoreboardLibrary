using System.Reflection;
using MiNET;
using MiNET.Net;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;

namespace ScoreboardLibrary;

[Plugin(
    PluginName = "Scoreboard Library",
    Description = "Library to create, modify, delete and send scoreboards to players within the Pigraid ecosystem",
    PluginVersion = "ALPHA-20240922",
    Author = "Antoine Langevin")]
public class ScoreboardLoader : Plugin
{
    
    // Test commands
    [Command]
    public static void EnableScore(Player player)
    {
        // Creates & Sends the scoreboard to the player.
        SendScoreboard(player, "PigraidGames", [
            new Line(0, "Hello there"),
            new Line(1, "Hello there"),
            new Line(2, "This is a test"),
            new Line(3, "very nice")
        ]);
    }

    [Command]
    public static void DisableScore(Player player)
    {
        Remove(player);
    }

    [Command]
    public static void UpdateScore(Player player, string text)
    {
        // Creates & Sends the scoreboard to the player.
        SendScoreboard(player, "PigraidGames", [
            new Line(0, "Welcome!"),
            new Line(1, "Server: Lobby"),
            new Line(2, "Coins: 1000"),
            new Line(3,  "2024-09-22")
        ]);
    }

    static void SendScoreboard(Player player, string displayName, params Line[] lines)
    {
        var length = lines.Length;
        if (length is > 15 or < 1)
        {
            throw new Exception("Score must be between the value of 1-15. Out of range, value: " + length);
        }
        
        // Clear current scoreboard
        Remove(player);
        
        // Create new scoreboard
        var scoreboard = new Scoreboard(player, displayName);
        
        // Get last line & remove it from the params array
        var lastLine = lines.Last();
        lines = lines.Take(lines.Length - 1).ToArray();
        
        // Add the lines
        foreach (var line in lines)
        {
            scoreboard.AddLine(player, line.Row, line.Text);
        }
        // Keep last line for the update = true
        scoreboard.AddLine(player, lastLine.Row, lastLine.Text, true);
    }

    static void Remove(Player player)
    {
        var packet = McpeRemoveObjective.CreateObject();
        packet.objectiveName = "§r";
        player.SendPacket(packet);
    }
}