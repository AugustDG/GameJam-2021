using System;
using System.Collections.Generic;
using DG.Tweening;
using MongoDB.Driver;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject containerParent;
    [SerializeField] private GameObject containerToSpawn;
    [SerializeField] private GameObject containerUi;
    [SerializeField] private GameObject nameField;
    [SerializeField] private GameObject loading;
    [SerializeField] private GameObject bg;
    [SerializeField] private LeaderboardContainer containerLocal;

    private List<PlayerInfo> _leaderboardPlayers = new List<PlayerInfo>();

    public void Awake()
    {
        MongoClient.Instance = new MongoClient();

        GameEvents.ShowLeaderboard += ShowLeaderboardHandler;
    }

    public void Start()
    {
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        GetComponent<Canvas>().worldCamera = Camera.current;
    }

    public void Restart()
    {
        DOTween.KillAll();
        SceneManager.LoadSceneAsync(2);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void FieldChanged(string change)
    {
        GameStats.PlayerName = change;
    }

    private void ShowLeaderboardHandler(object sender, EventArgs args)
    {
        nameField.SetActive(true);
        bg.SetActive(true);
        containerUi.SetActive(false);
    }

    public async void ShowLeaderboard()
    {
        nameField.SetActive(false);
        containerUi.SetActive(true);
        loading.SetActive(true);

        containerLocal.playerName.text = GameStats.PlayerName;
        containerLocal.goodScore.text = GameStats.GoodScore.ToString();
        containerLocal.badScore.text = GameStats.BadScore.ToString();

        var totalLocal = GameStats.BadScore - GameStats.GoodScore;

        containerLocal.totalScore.text = totalLocal.ToString();

        var infoToSend = new PlayerInfo(GameStats.PlayerName, GameStats.GoodScore, GameStats.BadScore);

        await MongoClient.Instance.DataCollection.InsertOneAsync(infoToSend);

        _leaderboardPlayers = await MongoClient.Instance.DataCollection
            .Find(info => info.GoodScore > 0 && info.BadScore > 0 && info.id != infoToSend.id).ToListAsync();

        loading.SetActive(false);

        var iteration = 0;

        foreach (var playerInfo in _leaderboardPlayers)
        {
            var currGo = Instantiate(containerToSpawn,
                containerToSpawn.transform.position, Quaternion.identity,
                containerParent.transform);

            currGo.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 220 - 320 * iteration);

            currGo.GetComponent<LeaderboardContainer>().playerName.text = playerInfo.Name;
            currGo.GetComponent<LeaderboardContainer>().goodScore.text = playerInfo.GoodScore.ToString();
            currGo.GetComponent<LeaderboardContainer>().badScore.text = playerInfo.BadScore.ToString();

            var total = playerInfo.BadScore - playerInfo.GoodScore;

            currGo.GetComponent<LeaderboardContainer>().totalScore.text = total.ToString();

            iteration++;
        }
    }
}