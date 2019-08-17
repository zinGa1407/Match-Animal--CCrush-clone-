using System;

[Flags]
public enum BonusType
{
    None,
    DestroyWholeRowColumn,
    Explode
}

public enum GameState
{
    None,
    SelectionStarted,
    Animating
}

public static class BonusTiles
{
    public static bool ContainsDestroyWholeRowColumn(BonusType bt){
        return (bt & BonusType.DestroyWholeRowColumn) == BonusType.DestroyWholeRowColumn;
    }

}
