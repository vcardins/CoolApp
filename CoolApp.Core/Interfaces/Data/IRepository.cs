#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.Core
// Author	: Rod Johnson
// Created	: 02-24-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoolApp.Core.Interfaces.Paging;

namespace CoolApp.Core.Interfaces.Data
{
    #region

    

    #endregion

    /// <summary>
    /// The generic base interface for all repositories...
    /// Purpose:
    /// - Implement this on the repository... Regardless of datasource... Xml, MSSQL, MYSQL etc..
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAllReadOnly();

        T GetSingle();

        T GetById(int id);

        T GetByKey(int[] keys);

        void SaveOrUpdate(T entity);

        void Delete(T entity);

        void BulkDelete(List<int> keys);

        IEnumerable<T> Find(Expression<Func<T, bool>> expression, int maxHits = 100);

        IEnumerable<T> FindIn(Expression<Func<T, int>> valueSelector, IEnumerable<int> values);

        IEnumerable<T> FindIn(Expression<Func<T, string>> valueSelector, IEnumerable<string> values);

        Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(
            Expression<Func<T, TValue>> valueSelector, IEnumerable<TValue> values);

        IPage<T> Page(int page = 1, int pageSize = 10);

        long Count();

        long Count(Expression<Func<T, bool>> expression);
        
    }
}