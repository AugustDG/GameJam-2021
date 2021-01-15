using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnd : MonoBehaviour
{
    public void ShowLeaderboard()
    {
        GameEvents.ShowLeaderboard.Invoke(this, EventArgs.Empty);
    }
}