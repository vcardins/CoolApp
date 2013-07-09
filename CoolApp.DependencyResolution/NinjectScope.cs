#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.DependencyResolution
// Author	: Rod Johnson
// Created	: 03-19-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-21-2013
// ***********************************************************************
#endregion

using System.Web.Http.Dependencies;
using Ninject.Activation;
using Ninject.Parameters;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoolApp.DependencyResolution
{
    #region

    #endregion

    /// <summary>
    /// Class NinjectScope
    /// </summary>
    public class NinjectScope : IDependencyScope
    {
        protected IResolutionRoot ResolutionRoot;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectScope"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectScope(IResolutionRoot kernel)
        {
            this.ResolutionRoot = kernel;
        }

        /// <summary>
        /// Retrieves a service from the scope.
        /// </summary>
        /// <param name="serviceType">The service to be retrieved.</param>
        /// <returns>The retrieved service.</returns>
        public object GetService(Type serviceType)
        {
            IRequest request = this.ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return this.ResolutionRoot.Resolve(request).SingleOrDefault();
        }

        /// <summary>
        /// Retrieves a collection of services from the scope.
        /// </summary>
        /// <param name="serviceType">The collection of services to be retrieved.</param>
        /// <returns>The retrieved collection of services.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            IRequest request = this.ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return this.ResolutionRoot.Resolve(request).ToList();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            var disposable = (IDisposable)this.ResolutionRoot;
            if (disposable != null) disposable.Dispose();
            this.ResolutionRoot = null;
        }
    }
}