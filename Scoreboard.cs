using PigNet;
using PigNet.Net.Packets.Mcpe;
using PigNet.Utils;

namespace ScoreboardLibrary;

public class Scoreboard
{
    private const string DefaultObjectiveName = "§r";
    private const string DefaultDisplaySlot = "sidebar";
    private const string DefaultCriteriaName = "dummy";

    private readonly Player _player;
    private readonly string _objectiveName;
    private readonly McpeSetScore _score;
    private readonly ScoreEntries _entries;

    /// <summary>
    /// Represents a scoreboard system that allows creation, modification, and management of player scoreboards.
    /// </summary>
    /// <remarks>
    /// This class provides functionality to manage scoreboards, add lines of text, update specific lines, and clear existing lines.
    /// It is specifically designed for integration with the Pigraid ecosystem.
    /// </remarks>
    public Scoreboard(Player player, string displayName, string objectiveName = DefaultObjectiveName,
        string displaySlot = DefaultDisplaySlot, string criteriaName = DefaultCriteriaName, int sortOrder = 0)
    {
        _player = player;
        _objectiveName = objectiveName;

        _score = McpeSetScore.CreateObject();
        _entries = [];

        var displayObjective = McpeSetDisplayObjective.CreateObject();
        displayObjective.displaySlotName = displaySlot;
        displayObjective.objectiveName = _objectiveName;
        displayObjective.criteriaName = criteriaName;
        displayObjective.objectiveDisplayName = displayName;
        displayObjective.sortOrder = sortOrder;

        _player.SendPacket(displayObjective);
    }

    /// <summary>
    /// Adds a new line of text to the scoreboard at a specified or next available row.
    /// </summary>
    /// <param name="text">The text to add to the scoreboard.</param>
    /// <param name="row">The optional row number where the text should be added. If not provided, it will use the next available row.</param>
    /// <param name="update">Determines whether to update the scoreboard immediately after the line is added.</param>
    public void AddLine(string text, uint? row = null, bool update = false)
    {
        var lineId = row ?? (uint)(_entries.Count + 1);

        _entries.Add(new ScoreEntryChangeFakePlayer
        {
            Id = lineId,
            Score = lineId,
            CustomName = text,
            ObjectiveName = _objectiveName
        });

        _score.entries = _entries;

        if (update) _player.SendPacket(_score);
    }

    /// <summary>
    /// Updates the content of a specific line on the scoreboard.
    /// </summary>
    /// <param name="lineId">The unique identifier of the line to update.</param>
    /// <param name="newText">The new text to display on the specified line.</param>
    public void UpdateLine(uint lineId, string newText)
    {
        var entry = _entries.FirstOrDefault(e => e.Id == lineId);
        if (entry == null) return;

        entry.ObjectiveName = newText;
        _score.entries = _entries;
        _player.SendPacket(_score);
    }

    /// <summary>
    /// Clears all lines from the scoreboard, removing all existing entries.
    /// </summary>
    public void ClearLines()
    {
        _entries.Clear();
        _score.entries = _entries;
        _player.SendPacket(_score);
    }
}