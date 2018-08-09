using System;
using System.Collections.Generic;
using Veises.SocialNet.Message.Domaian;

namespace Veises.SocialNet.Message.Adapters.Database
{
    internal interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        void Add(TEntity entity);

        void Delete(Guid id);

        TEntity Get(Guid id);

        IEnumerable<TEntity> All();

        void Update(TEntity entity);
    }
}