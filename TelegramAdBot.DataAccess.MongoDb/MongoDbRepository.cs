using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TelegramAdBot.Entities;

namespace TelegramAdBot.DataAccess.MongoDb
{
    public class MongoRepository<T> : IMongoDbRepository<T> where T : class, IEntity<string>, new()
    {
        protected internal IMongoCollection<T> collection;

        public MongoRepository(Util<T> util, string connectionString)
        {
            this.collection = util.GetCollectionFromConnectionString(connectionString);
        }

        public MongoRepository(string connectionString, string collectionName, Util<T> util)
        {
            this.collection = util.GetCollectionFromConnectionString(connectionString, collectionName);
        }

        public MongoRepository(MongoUrl url, Util<T> util)
        {
            this.collection = util.GetCollectionFromUrl(url);
        }

        public MongoRepository(MongoUrl url, Util<T> util, string collectionName)
        {
            this.collection = util.GetCollectionFromUrl(url, collectionName);
        }

        public IMongoCollection<T> Collection
        {
            get { return this.collection; }
        }

        public string CollectionName
        {
            get { return this.collection.CollectionNamespace.CollectionName; }
        }

        public virtual async Task<T> GetByIdAsync(T entity)
        {
            return await this.GetByIdAsync(entity.Id);
        }

        public async virtual Task<T> GetByIdAsync(string id)
        {
            return (await this.collection.FindAsync(c => c.Id == id)).SingleOrDefault();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await this.collection.InsertOneAsync(entity);

            return entity;
        }

        public virtual async void AddAsync(IEnumerable<T> entities)
        {
            await this.collection.InsertManyAsync(entities);
        }

        public virtual async void UpdateAsync(T c)
        {
            await collection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(c.Id)), c);
        }

        public virtual async void UpdateAsync(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                await this.collection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(entity.Id)), entity);
            }
        }

        /// <summary>
        /// Deletes an entity from the repository by its id.
        /// </summary>
        /// <param name="id">The entity's id.</param>
        public virtual async Task DeleteAsync(string id)
        {
            await this.collection.DeleteOneAsync(Builders<T>.Filter.Eq(c => c.Id, id));
        }

        public virtual async Task DeleteAsync(T entity)
        {
            await this.DeleteAsync(entity.Id);
        }

        public virtual async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            foreach (T entity in this.collection.AsQueryable().Where(predicate))
            {
                await this.DeleteAsync(entity.Id);
            }
        }
        
        public virtual async Task<long> CountAsync()
        {
            return await this.collection.EstimatedDocumentCountAsync();
        }

        /// <summary>
        /// Checks if the entity exists for given predicate.
        /// </summary>
        /// <param name="predicate">The expression.</param>
        /// <returns>True when an entity matching the predicate exists, false otherwise.</returns>
        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return (await this.collection.AsQueryable().FirstOrDefaultAsync(predicate)) != null;
        }
    }
}