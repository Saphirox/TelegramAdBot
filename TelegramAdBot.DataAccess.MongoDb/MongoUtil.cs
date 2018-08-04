namespace TelegramAdBot.DataAccess.MongoDb
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using System;
    using TelegramAdBot.Configurations;
    using TelegramAdBot.Entities;

    public class Util<T> where T: IEntity<string>, new()
    {
        private IOptions<MongoDbSettings> mongoDbSettings;

        public Util(IOptions<MongoDbSettings> configurationManager)
        {
            this.mongoDbSettings = configurationManager;
        }

        private const string DefaultConnectionstringName = nameof(MongoDbSettings.ConnectionString);

        public string GetDefaultConnectionString()
        {
            return mongoDbSettings.Value.ConnectionString;
        }

        private IMongoDatabase GetDatabaseFromUrl(MongoUrl url)
        {
            var client = new MongoClient(url);
            return client.GetDatabase(url.DatabaseName); 
        }

        public IMongoCollection<T> GetCollectionFromConnectionString(string connectionString)
        {
            return GetCollectionFromConnectionString(connectionString, GetCollectionName());
        }

        public IMongoCollection<T> GetCollectionFromConnectionString(string connectionString, string collectionName)
        {
            return GetDatabaseFromUrl(new MongoUrl(connectionString))
                .GetCollection<T>(collectionName);
        }

        public IMongoCollection<T> GetCollectionFromUrl(MongoUrl url)
        {
            return GetCollectionFromUrl(url, GetCollectionName());
        }

        public  IMongoCollection<T> GetCollectionFromUrl(MongoUrl url, string collectionName)
        {
            return GetDatabaseFromUrl(url).GetCollection<T>(collectionName);
        }

        private static string GetCollectionName() 
        {
            string collectionName;
            if (typeof(T).BaseType.Equals(typeof(object)))
            {
                collectionName = GetCollectioNameFromInterface();
            }
            else
            {
                collectionName = GetCollectionNameFromType(typeof(T));
            }

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }
            return collectionName;
        }

        private static string GetCollectioNameFromInterface()
        {
            string collectionname;

            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionName));
            if (att != null)
            {
                // It does! Return the value specified by the CollectionName attribute
                collectionname = ((CollectionName)att).Name;
            }
            else
            {
                collectionname = typeof(T).Name;
            }

            return collectionname;
        }

        private static string GetCollectionNameFromType(Type entitytype)
        {
            string collectionname;

            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(entitytype, typeof(CollectionName));
            if (att != null)
            {
                // It does! Return the value specified by the CollectionName attribute
                collectionname = ((CollectionName)att).Name;
            }
            else
            {
                if (typeof(Entity).IsAssignableFrom(entitytype))
                {
                    // No attribute found, get the basetype
                    while (!entitytype.BaseType.Equals(typeof(Entity)))
                    {
                        entitytype = entitytype.BaseType;
                    }
                }
                collectionname = entitytype.Name;
            }

            return collectionname;
        }
    }
}