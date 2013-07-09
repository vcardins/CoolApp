using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using CoolApp.Core.Common.Paging;
using CoolApp.Core.Interfaces.Data;
using CoolApp.Core.Interfaces.Paging;
using CoolApp.Core.Models;
using CoolApp.Infraestructure.Data;

namespace CoolApp.Infraestructure.Data
{
    /// <summary>
    /// An abstract baseclass handling basic CRUD operations against the context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> : IDisposable, IRepository<T> where T : DomainObject
    {
        protected readonly IDbSet<T> _dbset;
        protected readonly IDatabaseFactory _databaseFactory;
        private IDataContext _context;
        protected ObjectContext _ObjectContext { get; set; }
        private readonly string _entitySetName;
        private readonly string[] _keyNames;
        protected ObjectSet<T> ObjectSet { get; private set; }

        protected BaseRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
            _dbset = DataContext.DbSet<T>();

            if (_ObjectContext == null)
                _ObjectContext = ((IObjectContextAdapter)DataContext).ObjectContext;

            ObjectSet = _ObjectContext.CreateObjectSet<T>();

            // Get entity set for current entity type
            var entitySet = ObjectSet.EntitySet;
            // Build full name of entity set for current entity type
            _entitySetName = _ObjectContext.DefaultContainerName + "." + entitySet.Name;
            // Get name of the entity's key properties
            _keyNames = entitySet.ElementType.KeyMembers.Select(k => k.Name).ToArray();
        }

        public virtual IQueryable<T> Query
        {
            get { return _dbset; }
        }

        public IDataContext DataContext
        {
            get { return _context ?? (_context = _databaseFactory.Get()); }
        }

        public T GetByKey(object keyValue) 
        {
            EntityKey key = GetEntityKey(keyValue);

            object originalItem;
            if (((IObjectContextAdapter)_context).ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                return (T)originalItem;
            }
            return default(T);
        } 

        protected string EntitySetName { get; set; }

        public virtual void SaveOrUpdate(T entity)
        {
            if (UnitOfWork.IsPersistent(entity))
            {
                DataContext.Entry(entity).State = EntityState.Modified;
            }
            else
                _dbset.Add(entity);
        }

        public virtual T GetByKey(int[] keys)
        {
            if (keys.Length != _keyNames.Length)
            {
                throw new ArgumentException("Invalid number of key members");
            }

            // Merge key names and values by its order in array
            var keyPairs = _keyNames.Zip(keys, (keyName, keyValue) =>
                new KeyValuePair<string, object>(keyName, keyValue));

            // Build entity key
            var entityKey = new EntityKey(_entitySetName, keyPairs);
            // Query first current state manager and if entity is not found query database!!!
            return (T)_ObjectContext.GetObjectByKey(entityKey);
        }

        public virtual T GetById(int id)
        {
            return _dbset.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return Query;
        }

        public virtual IQueryable<T> GetAllReadOnly()
        {
            return Query.AsNoTracking();
        }

        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public void BulkDelete(List<int> keys)
        {
            keys.ForEach(i => Delete(GetById(i)));
        }

        public virtual T GetSingle()
        {
            return _dbset.Find();
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return Query.Where(expression);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression, int maxHits = 100)
        {
            return Find(expression).Take(maxHits);
        }

        public IEnumerable<T> FindIn(Expression<Func<T, int>> valueSelector, IEnumerable<int> values)
        {
            return GetAll().Where(BuildContainsExpression<T, int>(valueSelector, values));
        }

        public IEnumerable<T> FindIn(Expression<Func<T, string>> valueSelector, IEnumerable<string> values)
        {
            return GetAll().Where(BuildContainsExpression<T, string>(valueSelector, values));
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

        public IPage<T> Page(int page = 1, int pageSize = 10)
        {
            var internalPage = page - 1;
            var data = Query.OrderByDescending(k => k.Created).Skip(pageSize * internalPage).Take(pageSize).AsEnumerable();
            return new Page<T>(data, _dbset.Count(), pageSize, page);
        }

        public long Count()
        {
            return _dbset.LongCount();
        }

        public long Count(Expression<Func<T, bool>> expression)
        {
            return expression != null ? _dbset.Where(expression).LongCount() : Count();
        }

        public void Dispose()
        {
            DataContext.ObjectContext().Dispose();
        }

        private EntityKey GetEntityKey(object keyValue) 
        {
            var entitySetName = GetEntityName();
            var objectSet = ((IObjectContextAdapter)_context).ObjectContext.CreateObjectSet<T>();
            var keyPropertyName = objectSet.EntitySet.ElementType.KeyMembers[0].ToString();
            var entityKey = new EntityKey(entitySetName, new[] { new EntityKeyMember(keyPropertyName, keyValue) });
            return entityKey;
        }

        private string GetEntityName()
        {
            return string.Format("{0}.{1}", ((IObjectContextAdapter)_context).ObjectContext.DefaultContainerName, typeof(T).Name);
        } 
    }
}