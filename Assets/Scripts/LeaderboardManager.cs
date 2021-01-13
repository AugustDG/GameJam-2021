using System;
using System.Collections.Generic;
using MongoDB.Driver;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public Canvas endScreenCanvas;

    private List<PlayerInfo> _leaderboardPlayers = new List<PlayerInfo>();
    
    public void Awake()
    {
        MongoClient.Instance = new MongoClient();
        
        GameEvents.RoundFinished += RoundFinishedHandler;
    }

    private async void RoundFinishedHandler(object sender, EventArgs args)
    {
        endScreenCanvas.enabled = true;

        _leaderboardPlayers = await MongoClient.Instance.DataCollection.Find(info => info.Score > 0).ToListAsync();
        
        print(_leaderboardPlayers.Count);
    }
}