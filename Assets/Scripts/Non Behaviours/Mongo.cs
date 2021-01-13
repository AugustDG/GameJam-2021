﻿using System;
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
    public MongoDB.Driver.MongoClient Client;
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

    /*
    //all of the GET operations
    
    #region Fetch

    public bool DoesUserExist(string UserId)
    {
        return Task.Run(async () =>
        {
            //creates a [filter] to find one document by name
            var builder = Builders<UserDataModel>.Filter;
            var filter = builder.Eq(model => model.UserId, UserId);

            var doesExist = await UserCollection.FindAsync(filter).Result.ToListAsync();

            return doesExist.Count > 0;
        }).Result;
    }

    public UserDataModel GetOneUserModel(string username)
    {
        return Task.Run(async () =>
        {
            //creates a [filter] to find one document by name
            var builder = Builders<UserDataModel>.Filter;
            var filter = builder.Eq(model => model.Username, username);

            //finds the documents and returns the first one
            var userFound = await UserCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

            return userFound;
        }).Result;
    }

    public Task<UserDataModel> GetOneUserModelTask(string username)
    {
        return Task.Run(async () =>
        {
            //creates a [filter] to find one document by name
            var builder = Builders<UserDataModel>.Filter;
            var filter = builder.Eq(model => model.Username, username);

            //finds the documents and returns the first one
            var userFound = await UserCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

            return userFound;
        });
    }

    public UserDataModel GetOneUserModelById(string userId)
    {
        return Task.Run(async () =>
        {
            //creates a [filter] to find one document by name
            var builder = Builders<UserDataModel>.Filter;
            var filter = builder.Eq(model => model.UserId, userId);

            //finds the documents and returns the first one
            var userFound = await UserCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

            return userFound;
        }).Result;
    }

    public List<UserDataModel> GetAllUserModels(UserType type = UserType.None)
    {
        return Task.Run(async () =>
        {
            //creates a [filter] to find one document by name
            var builder = Builders<UserDataModel>.Filter;
            FilterDefinition<UserDataModel> filter;

            filter = type == UserType.None ? builder.Empty : builder.Eq(model => model.UserType, type);

            //finds the documents and returns the first one
            var usersFound = await UserCollection.FindAsync(filter).Result.ToListAsync();

            return usersFound;
        }).Result;
    }

    public Task<List<UserDataModel>> GetAllUserModelsTask(UserType type = UserType.None)
    {
        return Task.Run(async () =>
        {
            //creates a [filter] to find one document by name
            var builder = Builders<UserDataModel>.Filter;
            FilterDefinition<UserDataModel> filter;

            filter = type == UserType.None ? builder.Empty : builder.Eq(model => model.UserType, type);

            //finds the documents and returns the first one
            var usersFound = await UserCollection.FindAsync(filter).Result.ToListAsync();

            return usersFound;
        });
    }

    public Task<AccessModel> GetOneAccessCodeModelTask(string code)
    {
        return Task.Run(async () =>
        {
            //creates a [filter] to find one document by name
            var builder = Builders<AccessModel>.Filter;
            var filter = builder.Eq(model => model.AccessCode, code);

            //finds the documents and returns the first one
            var accessFound = await accessCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

            return accessFound;
        });
    }

    public InfoModel GetOneInfoModel() //gets the document associated with the name in the database (news collection)
    {
        return Task.Run(async () =>
        {
            //creates a [filter] to find one document by name
            var builder = Builders<InfoModel>.Filter;
            var filter = builder.Empty;

            //finds the documents and returns the first one
            var newsFound = await DataCollection.FindAsync(filter).Result.FirstOrDefaultAsync();

            return newsFound;
        }).Result;
    }

    public List<InfoModel>
        GetManyNewsModels(int numbOfItems = 50) //gets the documents associated with the database (news collection)
    {
        return Task.Run(async () =>
        {
            //creates a [filter] to find the documents requested
            var filter = Builders<InfoModel>.Filter.Empty;
            var find = new FindOptions<InfoModel>
            {
                Limit = numbOfItems,
            };

            //finds the documents and returns the first 50
            var newsFound = await DataCollection.FindAsync(filter, find).Result.ToListAsync();
            return newsFound;
        }).Result;
    }

    /*
    public byte[] GetOneImage(string filename)
    {
        return Task.Run(async () =>
        {
            byte[] source;

            using MemoryStream memStream = new MemoryStream();
            //await imagesBucket.DownloadToStreamByNameAsync(filename, memStream);

            source = memStream.ToArray();

            return source;
        }).Result;
    }
    

    #endregion

    //all of the UPDATE operations

    #region Update

    public async Task<long> UpdateUsersAsync<T>(T toChange, string matchField, string filterField = null,
        UpdateType updateType = UpdateType.All) //updates classes information on the database
    {
        //gets the document with the same id as the previous and updates it

        FilterDefinition<UserDataModel> filter;

        switch (updateType)
        {
            case UpdateType.MatchSpecificField:
                if (filterField is null)
                    throw new ArgumentException("Filter field is null, but update type is still 'MatchSpecificField'.",
                        nameof(filterField));

                var nameValueArray = filterField.Split('+');
                filter = Builders<UserDataModel>.Filter.Eq(nameValueArray[0], nameValueArray[1]);
                break;
            case UpdateType.MatchGivenField:
                if (filterField is null)
                    throw new ArgumentException("Filter field is null, but update type is still 'MatchChangedField'.",
                        nameof(filterField));

                filter = Builders<UserDataModel>.Filter.Eq(matchField, filterField);
                break;
            case UpdateType.All:
                filter = Builders<UserDataModel>.Filter.Empty;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null);
        }

        var update = Builders<UserDataModel>.Update.Set(matchField, toChange);

        var result = await UserCollection.UpdateManyAsync(filter, update);

        return result.ModifiedCount;
    }

    #endregion
    
    ////all of the DELETE operations
    //#region Delete
    //#endregion
    */
}