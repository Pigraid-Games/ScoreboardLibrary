﻿using MiNET;
using MiNET.Net;
using MiNET.Net.Packets.Mcpe;
using MiNET.Utils;

namespace ScoreboardLibrary;

public class Scoreboard
{
    private const string ObjectiveName = "§r";
    private const string DisplaySlot = "sidebar";
    private const string CriteriaName = "dummy";

    public Scoreboard(Player player, string displayName, int sortOrder = 0)
    {
        SortOrder = sortOrder;
        DisplayName = displayName;

        // Create & send packet
        DisplayObjective.displaySlotName = DisplaySlot;
        DisplayObjective.objectiveName = ObjectiveName;
        DisplayObjective.criteriaName = CriteriaName;
        DisplayObjective.objectiveDisplayName = DisplayName;
        DisplayObjective.sortOrder = SortOrder;

        player.SendPacket(DisplayObjective);
    }

    private int SortOrder { get; }
    private string DisplayName { get; }
    private McpeSetScore Score { get; } = McpeSetScore.CreateObject();
    private ScoreEntries Entries { get; } = [];
    private McpeSetDisplayObjective DisplayObjective { get; } = McpeSetDisplayObjective.CreateObject();

    // Add a new line to the scoreboard
    public void AddLine(Player player, uint row, string text, bool update = false)
    {
        Entries.Add(new ScoreEntryChangeFakePlayer
        {
            Id = row + 1,
            Score = row,
            CustomName = text,
            ObjectiveName = ObjectiveName
        });

        Score.entries = Entries;

        if (update) player.SendPacket(Score);
    }
}