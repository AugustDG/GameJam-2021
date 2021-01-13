using System;

public static class GameEvents
{
    public static EventHandler<int> GoodScoreChanged;
    public static EventHandler<int> BadScoreChanged;

    public static EventHandler ClockStarted;
}
