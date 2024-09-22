namespace ScoreboardLibrary;

public class Line(uint row, string text)
{
    public uint Row { get; set; } = row;
    public string Text { get; set; } = text;
}