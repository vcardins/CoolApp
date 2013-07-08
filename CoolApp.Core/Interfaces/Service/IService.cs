using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using CoolApp.Core.Interfaces.Paging;
using CoolApp.Core.Interfaces.Validation;

namespace CoolApp.Core.Interfaces.Service
{
    public interface IService<T>
    {

        [OperationContract]
        IQueryable<T> GetAll();

        [OperationContract]
        IQueryable<T> GetAllReadOnly();

        [OperationContract]
        T GetById(int id);

        [OperationContract]
        T GetByKey(int[] keys);

        [OperationContract]
        IValidationContainer<T> SaveOrUpdate(T entity);

        [OperationContract]
        void Delete(T entity);

        [OperationContract]
        void BulkDelete(List<int> keys);

        [OperationContract]
        IEnumerable<T> Find(Expression<Func<T, bool>> expression, int maxHits = 100);

        [OperationContract]
        IEnumerable<T> FindIn(Expression<Func<T, int>> valueSelector, IEnumerable<int> values);

        [OperationContract]
        IPage<T> Page(int page = 1, int pageSize = 10);

        [OperationContract]
        long Count();

        [OperationContract]
        long Count(Expression<Func<T, bool>> expression);
    }
}
