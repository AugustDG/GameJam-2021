using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("In seconds :)")] public int roundLength = 60;

    private int _tickPos;

    public void Awake()
    {
        GameEvents.GoodScoreChanged += GoodScoreHandler;
        GameEvents.BadScoreChanged += BadScoreHandler;
        GameEvents.ClockStarted += ClockStartedHandler;
    }

    public void Start()
    {
        GameEvents.ClockStarted.Invoke(this, EventArgs.Empty);
    }

    private void ClockStartedHandler(object sender, EventArgs args)
    {
        StartCoroutine(ClockTick());
    }

    private void GoodScoreHandler(object sender, int gScore) => GameStats.GoodScore += gScore;
    private void BadScoreHandler(object sender, int bScore) => GameStats.BadScore += bScore;

    private IEnumerator ClockTick()
    {
        print("Started!");

        while (_tickPos < roundLength)
        {
            yield return new WaitForSecondsRealtime(roundLength / 60f);

            print($"Time since start: {_tickPos}");

            _tickPos++;
        }

        print("Finished!");
    }
}