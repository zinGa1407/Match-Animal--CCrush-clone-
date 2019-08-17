using System;

public static class GameVariables
{
    // Grid Size
    public static int Rows = 12;
    public static int Columns = 8;

    //Animation Durations
    public static float AnimationDuration = 0.2f;
    public static float MoveAnimationDuration = 0.05f;
    public static float ExplosionAnimationDuration = 0.3f;

    public static float WaitBeforePotentialMatchesCheck = 2f;
    public static float OpacityAnimationDelay = 0.05f;
    
    //Minimum amount for matches
    public static int MinimumMatches = 3;
    public static int MinimumMatchesForBonus = 4;

    //Scores
    public static int Match3Score = 100;
    //public static int Match4Score = 300;
    //public static int MatchPlusScore = 500;
    public static int SubsequelMatchScore = 1000;

}
