using System;

/// <summary>Container for game events</summary>
public static class GameEvents
{
    public static Action playerDeath; // Event that notify player death
    public static Action playerWin; // Event that notify player has completed objective
    public static Action healthChange; // Event that notify a health change
    public static Action pointsChange; // Event that notify a game points change
}
