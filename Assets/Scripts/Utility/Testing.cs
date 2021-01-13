using System;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public void ClockStart()
    {
        print("Clicked!");
        GameEvents.ClockStarted.Invoke(this, EventArgs.Empty);
    }
}
