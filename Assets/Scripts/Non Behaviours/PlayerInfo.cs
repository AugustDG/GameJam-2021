using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class PlayerInfo
{
    [BsonId] public ObjectId id { get; set; } = ObjectId.GenerateNewId();
    public string Name { get; set; }
    public int Score { get; set; }

    public PlayerInfo(string name, int score)
    {
        Name = name;
        Score = score;
    }
}