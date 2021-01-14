using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Tooltip("In seconds :)")] public int roundLength = 60;

    public GameObject tabletUi;
    public GameObject tabletBg;
    public TMP_Text timerText;

    private int _tickPos;
    private string _timeString;

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

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabletBg.GetComponent<RectTransform>().DoAnchorPosY(0, 0.5f).Play();
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            tabletBg.GetComponent<RectTransform>().DoAnchorPosY(Screen.height, 0.5f).Play();
        }

        timerText.text = _timeString;
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

            _timeString = $"16:{_tickPos}";

            _tickPos++;
        }

        yield return new WaitForSeconds(5f);

        SceneManager.LoadSceneAsync(GameStats.BadScore - GameStats.GoodScore <= 50 ? 3 : 4);

        print("Finished!");
    }
}