using System.Collections.Generic;

namespace CoolApp.Core.Interfaces.Validation
{
    public interface IValidationContainer<out T> : IValidationContainer
    {
        T Entity { get;  }
    }
    
    public interface IValidationContainer
    {
        IDictionary<string, IList<string>> ValidationErrors { get; }
        bool IsValid { get; }
    }
}