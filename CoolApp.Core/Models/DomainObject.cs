using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;

namespace CoolApp.Core.Models
{
    public abstract class DomainObject : IValidatableObject
    {
        [NotMapped]
        [ScriptIgnore, XmlIgnore]
        public int Id
        {
            get
            {
                var keyAttributedProps = GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length == 1);
                return (keyAttributedProps != null) ? (int)keyAttributedProps.GetValue(this, null) : 0;
            }
            set { }
        }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Override this method to implement custom validation in your entities
            
            // This is only for making it compile... and returning null will give an exception.
            if (false)
                yield return new ValidationResult("Well, this should not happend...");
        }

        public virtual bool IsTransient()
        {
            return Id == default(int);
        } 
    }
}