using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using CoolApp.Core.Interfaces.Data;
using CoolApp.Core.Models;

namespace CoolApp.Infraestructure.Data
{
    public partial class UnitOfWork : IUnitOfWork
    {        
        private readonly IDatabaseFactory _databaseFactory;
        private IDataContext _datacontext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
            DataContext.ObjectContext().SavingChanges += (sender, e) => BeforeSave(GetChangedOrNewEntities());
        }

        public IDataContext DataContext
        {
            get { return _datacontext ?? (_datacontext = _databaseFactory.Get()); }
        }

        private IEnumerable<DomainObject> GetChangedOrNewEntities()
        {
            const EntityState NewOrModified = EntityState.Added | EntityState.Modified;

            return DataContext.ObjectContext().ObjectStateManager.GetObjectStateEntries(NewOrModified)
                .Where(x => x.Entity != null).Select(x => x.Entity as DomainObject);
        }

        public void BeforeSave(IEnumerable<DomainObject> entities)
        {
            foreach (var entity in entities)
            {
                entity.Updated = DateTime.UtcNow;
                entity.Created = !IsPersistent(entity) ? DateTime.UtcNow : entity.Created;
            }
        }

        public int Commit()
        {
            return DataContext.ObjectContext().SaveChanges();
        }


        public static bool IsPersistent(DomainObject entity)
        {
            var isPersistent = true;
            var keys = GetPrimaryKeys(entity);
            foreach (var key in keys)
            {
                isPersistent = isPersistent && !(key.Value.ToString().Equals(string.Empty) || key.Value.ToString().Equals("0"));
            }

            return isPersistent;
        }

        public static Dictionary<string, object> GetPrimaryKeys(DomainObject entity)
        {
            var properties = entity.GetType().GetProperties();
            var keys = new Dictionary<string, object>();
            foreach (var property in properties)
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute)) as KeyAttribute;
                if (attribute != null)
                {
                    keys.Add(property.Name, property.GetValue(entity));
                }
            }
            return keys;
        }
    }
}
