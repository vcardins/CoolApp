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
using Ninject;

namespace CoolApp.DependencyResolution
{
    #region

    

    #endregion

    /// <summary>
    /// Class NinjectResolver
    /// </summary>
    public class NinjectResolver : NinjectScope, System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel _kernel;
        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectResolver"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectResolver(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Starts a resolution scope.
        /// </summary>
        /// <returns>The dependency scope.</returns>
        public IDependencyScope BeginScope()
        {
            return new NinjectScope(_kernel.BeginBlock());
        }
    }
}