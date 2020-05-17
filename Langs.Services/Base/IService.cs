using Langs.Data.Objects.Base;
using System;
using System.Collections.Generic;

namespace Langs.Services
{
    public interface IService<T> where T : IHaveID
    {
        T Get(int id);
        IEnumerable<T> GetAll();

        T Add(T obj);
        void Add(IEnumerable<T> objs);

        void Remove(T obj);
        void Remove(IEnumerable<T> objs);

        void Update(T obj);
        void Update(IEnumerable<T> objs);

        bool SavesChanges { get; }
        void StartBatchingRequests();
        void EndBatchingRequestsAndSave();

        ServiceBatchRequest<T> BatchRequests();
    }

    public class ServiceBatchRequest<T> : IDisposable where T : IHaveID
    {
        private readonly IService<T> m_Service;
        public ServiceBatchRequest(IService<T> Service)
        {
            m_Service = Service;
            m_Service.StartBatchingRequests();
        }

        public void Dispose()
        {
            m_Service.EndBatchingRequestsAndSave();
        }
    }
}
