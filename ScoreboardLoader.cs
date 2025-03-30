using PigNet;
using PigNet.Net.Packets.Mcpe;
using PigNet.Plugins;
using PigNet.Plugins.Attributes;

namespace ScoreboardLibrary;

[Plugin(
    PluginName = "Scoreboard Library",
    Description = "Library to create, modify, delete and send scoreboards to players",
    PluginVersion = "1.0.0-RELEASE",
    Author = "Antoine LANGEVIN")]
public abstract class ScoreboardLoader : Plugin
{
    /// <summary>
    /// Sends a scoreboard to a specific player with the provided display name and content lines.
    /// </summary>
    /// <param name="player">The player who will receive the scoreboard.</param>
    /// <param name="displayName">The display name of the scoreboard.</param>
    /// <param name="lines">
    /// An array of strings representing the content lines of the scoreboard.
    /// The number of lines must be between 1 and 15. Throws an exception if the line count is outside this range.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the number of lines is less than 1 or greater than 15.
    /// </exception>
    public static void Send(Player player, string displayName, params string[] lines)
    {
        if (lines.Length is > 15 or < 1)
            throw new ArgumentOutOfRangeException(nameof(lines), @"Scoreboard lines must be between 1 and 15.");

        Remove(player);

        var scoreboard = new Scoreboard(player, displayName);
        for (var i = 0; i < lines.Length - 1; i++)
            scoreboard.AddLine(lines[i]);

        scoreboard.AddLine(lines.Last(), update: true);
    }

    /// <summary>
    /// Removes a scoreboard from a specific player by its objective name.
    /// </summary>
    /// <param name="player">The player from whom the scoreboard will be removed.</param>
    /// <param name="objectiveName">
    /// The internal objective name of the scoreboard to be removed.
    /// Defaults to "§r" if not provided.
    /// </param>
    public static void Remove(Player player, string objectiveName = "§r")
    {
        var packet = McpeRemoveObjective.CreateObject();
        packet.objectiveName = objectiveName;
        player.SendPacket(packet);
    }
}
