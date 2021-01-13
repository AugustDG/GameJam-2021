using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public void Awake()
    {
        MongoClient.Instance = new MongoClient();
    }

    public void Start()
    {
        //todo: add leaderboard code!
    }
}