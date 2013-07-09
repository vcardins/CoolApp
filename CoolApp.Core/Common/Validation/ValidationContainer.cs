using System.Collections.Generic;
using CoolApp.Core.Interfaces.Validation;

namespace CoolApp.Core.Common.Validation
{
    public partial class ValidationContainer<T> : IValidationContainer<T>
    {
        public IDictionary<string, IList<string>> ValidationErrors { get; private set; }
        public T Entity { get; private set; }

        public bool IsValid
        {
            get { return ValidationErrors.Count == 0; }
        }

        public ValidationContainer(IDictionary<string, IList<string>> validationErrors, T entity)
        {
            ValidationErrors = validationErrors;
            Entity = entity;
        }
    }
}
