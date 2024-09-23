using MiNET;
using MiNET.Net;
using MiNET.Utils;

namespace ScoreboardLibrary;

public class Scoreboard
{
    private const string ObjectiveName = "§r";
    private const string DisplaySlot = "sidebar";
    private const string CriteriaName = "dummy";

    private int SortOrder { get; set; }
    private string DisplayName { get; set; }
    McpeSetScore Score { get; } = McpeSetScore.CreateObject();
    ScoreEntries Entries { get; set; } = [];
    McpeSetDisplayObjective DisplayObjective { get; } = McpeSetDisplayObjective.CreateObject();

    public Scoreboard(Player player, string displayName, int sortOrder = 0)
    {
        SortOrder = sortOrder;
        DisplayName = displayName;
        
        // Create & send packet
        DisplayObjective.displaySlot = DisplaySlot;
        DisplayObjective.objectiveName = ObjectiveName;
        DisplayObjective.criteriaName = CriteriaName;
        DisplayObjective.displayName = DisplayName;
        DisplayObjective.sortOrder = SortOrder;
        
        player.SendPacket(DisplayObjective);
    }

    // Add a new line to the scoreboard
    public void AddLine(Player player, uint row, string text, bool update = false)
    {
        Entries.Add(new ScoreEntryChangeFakePlayer()
        {
            Id = row + 1,
            Score = row,
            CustomName = text,
            ObjectiveName = ObjectiveName
        });

        Score.entries = Entries;

        if (update)
        {
            player.SendPacket(Score);
        }
    }
}
