using System;
using System.Collections.Generic;
using MongoDB.Driver;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public Canvas endScreenCanvas;

    [SerializeField] private GameObject containerParent;
    [SerializeField] private GameObject containerToSpawn;

    private List<PlayerInfo> _leaderboardPlayers = new List<PlayerInfo>();

    public void Awake()
    {
        MongoClient.Instance = new MongoClient();

        GameEvents.RoundFinished += RoundFinishedHandler;
    }

    public void Start()
    {
        GameEvents.RoundFinished.Invoke(this, EventArgs.Empty);
    }

    private async void RoundFinishedHandler(object sender, EventArgs args)
    {
        //endScreenCanvas.enabled = true;

        //await MongoClient.Instance.DataCollection.InsertOneAsync(new PlayerInfo(GameStats.PlayerName, GameStats.GoodScore, GameStats.BadScore));

        _leaderboardPlayers = await MongoClient.Instance.DataCollection
            .Find(info => info.GoodScore > 0 && info.BadScore > 0).ToListAsync();

        var iteration = 0;

        foreach (var playerInfo in _leaderboardPlayers)
        {
            var currGo = Instantiate(containerToSpawn,
                containerToSpawn.transform.position, Quaternion.identity,
                containerParent.transform);

            currGo.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 270 - 200 * iteration);

            currGo.GetComponent<LeaderboardContainer>().playerName.text = playerInfo.Name;
            currGo.GetComponent<LeaderboardContainer>().goodScore.text = playerInfo.GoodScore.ToString();
            currGo.GetComponent<LeaderboardContainer>().badScore.text = playerInfo.BadScore.ToString();

            iteration++;
        }
    }
}