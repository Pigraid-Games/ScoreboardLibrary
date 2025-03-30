# 🪧 Scoreboard Library for PigNet

A lightweight and easy-to-use scoreboard library for [PigNet](https://github.com/Pigraid/PigNet) Minecraft Bedrock Edition servers. This plugin allows developers to **create**, **update**, and **remove** scoreboards dynamically for players with just a few lines of code.

---

## 📦 Features

- 🧑‍🎓 Simple API to display scoreboards to players
- ✏️ Dynamically add and update scoreboard lines
- ❌ Easily remove scoreboards
- 🧙‍♂️ Fully compatible with PigNet

---

## 🚀 Getting Started

### 📥 Installation

1. Clone or download the repository.
2. Add a reference to the compiled DLL in your PigNet-based plugin.
3. Make sure that the DLL is in your plugins folder, with your plugins using it.

---

## 🥪 Usage Examples

### ✅ Displaying a Scoreboard

```csharp
ScoreboardLoader.Send(player, "§eYour Stats", 
    "§aKills: §f10",
    "§cDeaths: §f2",
    "§6Coins: §f1420"
);
```

This will create a sidebar scoreboard for the given player with the provided title and lines.

---

### ❌ Removing a Scoreboard

```csharp
ScoreboardLoader.Remove(player);
```

You can optionally pass a custom objective name:

```csharp
ScoreboardLoader.Remove(player, "§r");
```

---

### 🛠 Advanced: Manual Scoreboard Control

```csharp
var scoreboard = new Scoreboard(player, "§bCustom Stats");

scoreboard.AddLine("§aLevel: §f42");
scoreboard.AddLine("§cXP: §f1200");
scoreboard.AddLine("§dRank: §fKnight", update: true);

// Update a specific line (lineId starts at 1)
scoreboard.UpdateLine(2, "§cXP: §f1250");

// Clear all lines
scoreboard.ClearLines();
```

---

## 📘 API Reference

### `ScoreboardLoader.Send(Player player, string displayName, params string[] lines)`
Creates and sends a scoreboard to a player.  
Lines must be between 1 and 15.

### `ScoreboardLoader.Remove(Player player, string objectiveName = "§r")`
Removes the scoreboard with the given objective name from the player.

---

## 🧱 Scoreboard Class

### Constructor
```csharp
new Scoreboard(Player player, string displayName, string objectiveName = "§r", string displaySlot = "sidebar", string criteriaName = "dummy", int sortOrder = 0);
```

### Methods

- `AddLine(string text, uint? row = null, bool update = false)`
- `UpdateLine(uint lineId, string newText)`
- `ClearLines()`

---

## 🧑‍💼 Author

**Antoine LANGEVIN aka CupidonSauce173**  
Maintained as part of the [Pigraid](https://github.com/Pigraid) project.

---

## 📜 Contact Us

You can create a new issue at any given time to report a problem with the library or  join our [Discord](https://discord.gg/9gzutNXX2D) if you have any question!

---