using System;
using UnityEngine;

public static class GameStats
{
    public static string PlayerName;

    public static int GoodScore;
    public static int BadScore;

    public enum ScoreTable {
        Box = 10,
        Toilet = 30,
        Pipes = 15,
        Fuel = 50,
        Mail = 20
    }
}
