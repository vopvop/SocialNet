using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Veises.Common.Service.IoC;
using Veises.SocialNet.Message.Domaian;

namespace Veises.SocialNet.Message.Adapters.Database
{
    [InjectDependency(DependencyScope.Singleton)]
    internal sealed class InMemoryRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly ConcurrentDictionary<Guid, TEntity> _dictionary = new ConcurrentDictionary<Guid, TEntity>();

        public void Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dictionary.TryAdd(entity.Id, entity);
        }

        public void Delete(Guid id)
        {
            _dictionary.Remove(id, out var entity);
        }

        public TEntity Get(Guid id)
        {
            return _dictionary[id];
        }

        public IEnumerable<TEntity> All()
        {
            return _dictionary.Values;
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dictionary.AddOrUpdate(entity.Id, entity, (id, exists) => entity);
        }
    }
}