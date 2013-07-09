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

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using CoolApp.Core.Models;

namespace CoolApp.Core.Interfaces.Data
{
    #region

    #endregion

    public interface IDataContext
    {
        ObjectContext ObjectContext();

        IDbSet<T> DbSet<T>() where T : DomainObject;

        DbEntityEntry Entry<T>(T entity) where T : DomainObject;

        void Dispose();
    }
}