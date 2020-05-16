using Langs.Data.Context;
using Langs.Data.Objects.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Langs.Services
{
    public abstract class BaseService<T> : IService<T> where T : BaseObject
    {
        public bool SavesChanges => m_BatchingRefCount == 0;
        private int m_BatchingRefCount;

        protected abstract DbSet<T> EntitiesProxy { get; }

        protected DatabaseContext m_Context;
        public BaseService(DatabaseContext context)
        {
            m_Context = context;
        }

        public virtual T Get(int id) => EntitiesProxy.SingleOrDefault(e => e.ID == id);
        public virtual IEnumerable<T> GetAll() => EntitiesProxy;

        public virtual T Add(T obj)
        {
            var entry = EntitiesProxy.Add(obj);
            if (SavesChanges)
                m_Context.SaveChanges();

            return entry.Entity;
        }

        public virtual void Add(IEnumerable<T> objs)
        {
            EntitiesProxy.AddRange(objs);

            if (SavesChanges)
                m_Context.SaveChanges();
        }

        public virtual void Remove(T obj)
        {
            EntitiesProxy.Remove(obj);

            if (SavesChanges)
                m_Context.SaveChanges();
        }

        public virtual void Remove(IEnumerable<T> objs)
        {
            EntitiesProxy.RemoveRange(objs);

            if (SavesChanges)
                m_Context.SaveChanges();
        }

        public virtual void Update(T obj)
        {
            EntitiesProxy.Update(obj);

            if (SavesChanges)
                m_Context.SaveChanges();
        }

        public virtual void Update(IEnumerable<T> objs)
        {
            EntitiesProxy.RemoveRange(objs);

            if (SavesChanges)
                m_Context.SaveChanges();
        }

        public void StartBatchingRequests() => m_BatchingRefCount++;
        public void EndBatchingRequestsAndSave()
        {
            m_BatchingRefCount--;
            if (m_BatchingRefCount < 0)
            {
                m_BatchingRefCount = 0;
                throw new InvalidOperationException("EndBatchingRequests called without appropriate StartBatchingRequests. m_BatchingRefCount cannot go below 0");
            }

            if (m_BatchingRefCount == 0)
                m_Context.SaveChanges();
        }
    }
}
