using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using We.Sparkie.History.Api.Domain;
using We.Sparkie.History.Api.Repository;

namespace We.Sparkie.DigitalAsset.Api.Repository
{
    public class DomainEventDomainEventRepository<TEvent> : IDomainEventRepository<TEvent> where TEvent : DomainEvent
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<TEvent> _entities;

        public DomainEventDomainEventRepository(IMongoDatabase database)
        {
            _database = database;
            _entities = _database.GetCollection<TEvent>("DomainEvents");
        }

        public Task Insert(TEvent entity)
        {
            return _entities.InsertOneAsync(entity);
        }


        public Task<bool> Delete(TEvent entity)
        {
            return Delete(entity.Id);
        }

        public async Task<bool> Delete(Guid id)
        {
            var query = Builders<TEvent>.Filter.Eq("Id", id);
            var deleteResult = await _entities.DeleteOneAsync(query);
            return deleteResult.IsAcknowledged;
        }
    }
}