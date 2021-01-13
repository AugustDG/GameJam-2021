using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class PlayerInfo
{
    [BsonId] public ObjectId id { get; set; } = ObjectId.GenerateNewId();
    public string Name { get; set; }
    public int GoodScore { get; set; }
    public int BadScore { get; set; }

    public PlayerInfo(string name, int gScore, int bScore)
    {
        Name = name;
        GoodScore = gScore;
        BadScore = bScore;
    }
}