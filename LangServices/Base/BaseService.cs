using LangData.Context;
using LangData.Objects;
using LangData.Objects.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LangServices
{
    public abstract class BaseService<T> : IService<T> where T : BaseObject
    {
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
            m_Context.SaveChanges();
            return entry.Entity;
        }

        public virtual void Add(IEnumerable<T> objs)
        {
            EntitiesProxy.AddRange(objs);
            m_Context.SaveChanges();
        }

        public virtual void Remove(T obj)
        {
            EntitiesProxy.Remove(obj);
            m_Context.SaveChanges();
        }

        public virtual void Update(T obj)
        {
            EntitiesProxy.Update(obj);
            m_Context.SaveChanges();
        }
    }
}
