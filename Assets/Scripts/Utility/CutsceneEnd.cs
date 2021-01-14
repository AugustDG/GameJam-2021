using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnd : MonoBehaviour
{
    private bool _hidden = true;

    private void Update()
    {
        if (Input.anyKey && _hidden)
        {
            _hidden = false;
            GameEvents.ShowLeaderboard.Invoke(this, EventArgs.Empty);
        }
    }
}