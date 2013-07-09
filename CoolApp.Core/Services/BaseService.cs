using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoolApp.Core.Extensions;
using CoolApp.Core.Interfaces.Data;
using CoolApp.Core.Interfaces.Paging;
using CoolApp.Core.Interfaces.Service;
using CoolApp.Core.Interfaces.Validation;
using CoolApp.Core.Models;

namespace CoolApp.Core.Services
{
    /// <summary>
    /// Base for all services... If you need specific businesslogic
    /// override these methods in inherited classes and implement the logic there.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : IService<T> where T : DomainObject
    {
        protected IRepository<T> Repository;

        protected IUnitOfWork UnitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual IQueryable<T> GetAll()
        {
            return Repository.GetAll();
        }

        public virtual IQueryable<T> GetAllReadOnly()
        {
            return Repository.GetAllReadOnly();
        }

        public virtual T GetById(int id)
        {
            return Repository.GetById(id);
        }

        public T GetByKey(int[] keys)
        {
            return Repository.GetByKey(keys);
        }

        public virtual IValidationContainer<T> SaveOrUpdate(T entity)
        {
            var validation = entity.GetValidationContainer();
            if (!validation.IsValid)
                return validation;

            Repository.SaveOrUpdate(entity);
            UnitOfWork.Commit();
            return validation;
        }

        public virtual void Delete(T entity)
        {
            Repository.Delete(entity);
            UnitOfWork.Commit();
        }

        public void BulkDelete(List<int> keys)
        {
            Repository.BulkDelete(keys);
            UnitOfWork.Commit();
        }


        public IPage<T> Page(int page = 1, int pageSize = 10)
        {
            return Repository.Page(page, pageSize);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression, int maxHits = 100)
        {
            return Repository.Find(expression, maxHits);
        }

        public IEnumerable<T> FindIn(Expression<Func<T, int>> valueSelector, IEnumerable<int> values)
        {
            return Repository.GetAll().Where(BuildContainsExpression<T, int>(valueSelector, values));
        }

        public IEnumerable<T> FindIn(Expression<Func<T, string>> valueSelector, IEnumerable<string> values)
        {
            return Repository.GetAll().Where(BuildContainsExpression<T, string>(valueSelector, values));
        }

        public Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(Expression<Func<T, TValue>> valueSelector, IEnumerable<TValue> values)
        {
            if (null == valueSelector) { throw new ArgumentNullException("valueSelector"); }
            if (null == values) { throw new ArgumentNullException("values"); }
            ParameterExpression p = valueSelector.Parameters.Single();
            var enumerable = values as TValue[] ?? values.ToArray();
            if (!enumerable.Any())
            {
                return e => false;
            }
            var equals = enumerable.Select(value => (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate(Expression.Or);
            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

        public long Count()
        {
            return Repository.Count();
        }

        public long Count(Expression<Func<T, bool>> expression)
        {
            return Repository.Count(expression);
        }
    }
}
