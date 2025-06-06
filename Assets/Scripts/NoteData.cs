using System;

public enum Lane
{
    Top = 0,
    Bottom = 1
}

[Serializable]
public class NoteData
{
    public double time; // √  ¥‹¿ß
    public Lane lane;
}
