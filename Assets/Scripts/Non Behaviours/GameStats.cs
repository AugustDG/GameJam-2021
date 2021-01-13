using System;
using UnityEngine;

public static class GameStats
{
    public static string PlayerName
    {
        get => PlayerPrefs.GetString(nameof(PlayerName), String.Empty);
        set
        {
            PlayerPrefs.SetString(nameof(PlayerName), value);
            PlayerPrefs.Save();
        }
    }
    
    public static int GoodScore;
    public static int BadScore;
}
