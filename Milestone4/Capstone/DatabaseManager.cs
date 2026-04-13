using MongoDB.Driver;

namespace Capstone;

public class DatabaseManager
{
    public const string DATABASE_NAME = "HeightMapDatabase";
    public const string COLLECTION_NAME = "HeightMaps";
    MongoClient mongoClient;
    public DatabaseManager()
    {
        mongoClient = new MongoClient("mongodb://localhost:27017");

    }
    // Retrieves the height map stored in the database
    public HeightMap? GetStoredHeightMap(int slot)
    {
        var database = mongoClient.GetDatabase(DATABASE_NAME);
        var collection = database.GetCollection<HeightMap>(COLLECTION_NAME);
        HeightMap? queryResult = collection.Find(map => map.Slot.Equals(slot)).FirstOrDefault();
        return queryResult;
    }
    // Stores the height map in the database
    public void StoreHeightMap(HeightMap heightMap)
    {
        var database = mongoClient.GetDatabase(DATABASE_NAME);
        var collection = database.GetCollection<HeightMap>(COLLECTION_NAME);
        var updated = collection.FindOneAndReplace(map => map.Slot.Equals(heightMap.Slot), heightMap);
        if(updated == null)
        {
            collection.InsertOne(heightMap);
        }
    }
}