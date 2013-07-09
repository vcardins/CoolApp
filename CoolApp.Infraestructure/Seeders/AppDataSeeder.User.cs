using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using CoolApp.Common.Crypto;
using CoolApp.Core.Enums;
using CoolApp.Core.Models;
using CoolApp.Infraestructure.Data;

namespace CoolApp.Infraestructure.Seeders
{

    public static partial class AppDataSeeder
    {
        public static void SeedMembership (DataContext context)
        {
            try
            {
                //var settings = DependencyResolver.Current.GetService<IMembershipSettings>();
                new List<User>
                {
                    new User
                        {
                            UserId = 1,
                            UserGuid = Guid.NewGuid(),
                            Username = "admin",
                            Password = CryptoHelper.HashPassword("admin", 10),
                            Email = "admin@CoolApp.com",
                            PasswordChanged = null,
                            IsAccountVerified = true,
                            IsLoginAllowed = true,
                            IsAccountClosed = false,
                            AccountClosed = null,
                            LastLogin = null,
                            LastFailedLogin = null,
                            FailedLoginCount = 0,
                            VerificationKey = null,
                            VerificationKeySent = null,
                            HashedPassword = CryptoHelper.HashPassword("admin", 10),
                            IsLockedOut = false,
                            PhotoFile = null,
                            Address = "Admin Address",
                            PhoneNumber = "555-555-555",
                            FirstName = "System",
                            LastName = "Administrator",
                            Gender = Gender.Male,
                            Created = DateTime.UtcNow,
                            Updated = null
                        },
                         new User
                         {
                            UserId = 2,
                            UserGuid = Guid.NewGuid(),
                            Username = "obama",
                            Password = CryptoHelper.HashPassword("obama", 10),
                            Email = "obama@CoolApp.com",
                            PasswordChanged = null,
                            IsAccountVerified = true,
                            IsLoginAllowed = true,
                            IsAccountClosed = false,
                            AccountClosed = null,
                            LastLogin = null,
                            LastFailedLogin = null,
                            FailedLoginCount = 0,
                            VerificationKey = null,
                            VerificationKeySent = null,
                            HashedPassword = CryptoHelper.HashPassword("obama", 10),
                            IsLockedOut = false,
                            PhotoFile = null,
                            Address = "Obama Address",
                            PhoneNumber = "555-555-555",
                            FirstName = "Barak",
                            LastName = "Obama",
                            Gender = Gender.Male,
                            Created = DateTime.UtcNow,
                            Updated = null
                        },
                         new User
                         {
                            UserId = 3,
                            UserGuid = Guid.NewGuid(),
                            Username = "bush",
                            Password = CryptoHelper.HashPassword("bush", 10),
                            Email = "bush@CoolApp.com",
                            PasswordChanged = null,
                            IsAccountVerified = true,
                            IsLoginAllowed = true,
                            IsAccountClosed = false,
                            AccountClosed = null,
                            LastLogin = null,
                            LastFailedLogin = null,
                            FailedLoginCount = 0,
                            VerificationKey = null,
                            VerificationKeySent = null,
                            HashedPassword = CryptoHelper.HashPassword("bush", 10),
                            IsLockedOut = false,
                            PhotoFile = null,
                            Address = "Bush Address",
                            PhoneNumber = "555-555-555",
                            FirstName = "George",
                            LastName = "Bush",
                            Gender = Gender.Male,
                            Created = DateTime.UtcNow,
                            Updated = null
                        }
                }.ForEach(u => context.Users.AddOrUpdate(u));

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        Console.Write("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
           
        }
    }
}
