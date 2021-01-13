using System;
using System.Security.Authentication;
using MongoDB.Driver;

//manages all the database connection
public class MongoClient
{
    //Client instance available to entire app
    public static MongoClient Instance;

    //MongoDB credentials to access the database
    private const string MONGO_URI =
        "mongodb+srv://game:2LBxkSmF2ChxNzLC@cluster0.92h1e.mongodb.net/PlayerData?retryWrites=true&w=majority";

    private const string DATA_DATABASE_NAME = "PlayerData";

    //MongoClient instance to access the database
    private MongoDB.Driver.MongoClient Client;
    private IMongoDatabase DataDatabase;

    //References to the database collections for each document type
    //Collection containing information about every login info
    public IMongoCollection<PlayerInfo> DataCollection;

    //Initializes the mongo client
    public MongoClient()
    {
        //connects and gets the right database from the supplied credentials
        var settings = MongoClientSettings.FromUrl(new MongoUrl(MONGO_URI));
        settings.SslSettings = new SslSettings() {EnabledSslProtocols = SslProtocols.Tls12};
        settings.UseSsl = true;
        settings.ConnectionMode = ConnectionMode.ReplicaSet;
        settings.RetryWrites = true;
        settings.ConnectTimeout = TimeSpan.FromMilliseconds(60000);
        settings.SocketTimeout = TimeSpan.FromMilliseconds(60000);

        Client = new MongoDB.Driver.MongoClient(MONGO_URI);
        DataDatabase = Client.GetDatabase(DATA_DATABASE_NAME);

        //Initializing all collections
        DataCollection = DataDatabase.GetCollection<PlayerInfo>("Data");
    }
}