using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;

namespace MongoDBConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MongoDB App");
            var dbClient = new MongoClient("mongodb+srv://user1:mongopassword123@cluster19.u3vozoa.mongodb.net/?retryWrites=true&w=majority");
            var database = dbClient.GetDatabase("MyApp");
            var collection = database.GetCollection<BsonDocument>("WareHouse");

            var document = new BsonDocument { { "ProductName", "Phone" }, { "Description", "Mobile Electronics" }, { "Price", 500 } };
            Console.WriteLine("Inserting new item");
            Console.WriteLine(document);
            Console.WriteLine();
            //*I am inserting data here-This satisfies create in CRUD
            Insert(collection, document);

            //*I am printing to see if the data is inserted - This satisfies read in CRUD
            PrintAll(collection);

            var findFilter = Builders<BsonDocument>.Filter.Eq("ProductName", "Phone");
            //* I am finding the record to update- 
            var findData = FindOne(collection, findFilter);
            foreach (var item in findData)
            {
                Console.WriteLine();
                Console.WriteLine("Found Item");
                Console.WriteLine(item);
            }

            var update = Builders<BsonDocument>.Update.Set("Price", 1000);
            //* I am updating the record found above - This satisfies update in CRUD
            Update(collection, findFilter, update);
            var updatedData = FindOne(collection, findFilter);
            foreach (var item in updatedData)
            {
                Console.WriteLine();
                Console.WriteLine("Updated Item");
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine("Deleting Item Phone");
            //* I am deleting the record here- This satisfies delete in CRUD
            Delete(collection, findFilter);


            PrintAll(collection);

        }

        private static void PrintAll(IMongoCollection<BsonDocument> collection)
        {
            Console.WriteLine("Printing All");
            var all = GetAll(collection);
            foreach (var item in all)
            {
                Console.WriteLine(item);
            }
        }

        public static void Insert(IMongoCollection<BsonDocument> collection, BsonDocument document)
        {
            collection.InsertOne(document);
        }
        public static List<BsonDocument> GetAll(IMongoCollection<BsonDocument> collection)
        {
            return collection.Find(new BsonDocument()).ToList();
        }

        public static List<BsonDocument> FindOne(IMongoCollection<BsonDocument> collection, FilterDefinition<BsonDocument> Filters)
        {
            return collection.Find(Filters).ToList();
        }
        public static void Update(IMongoCollection<BsonDocument> collection, FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            collection.UpdateOne(filter, update);
        }
        public static void Delete(IMongoCollection<BsonDocument> collection, FilterDefinition<BsonDocument> filter)
        {
            collection.DeleteOne(filter);
        }
    }
}