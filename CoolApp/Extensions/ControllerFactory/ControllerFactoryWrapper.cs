#region credits
// ***********************************************************************
// Assembly	: Deten
// Author	: Andrew Davey (https://github.com/andrewdavey/notfoundmvc)
// 
// Last Modified By : Marko Ilievski
// Last Modified On : 04-12-2013
// ***********************************************************************
#endregion

using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using TaskForceManager.Extensions.ErrorHandlingHelpers;

namespace TaskForceManager.Extensions.ControllerFactory
{
    #region

    

    #endregion

    class ControllerFactoryWrapper : IControllerFactory
    {
        readonly IControllerFactory factory;

        public ControllerFactoryWrapper(IControllerFactory factory)
        {
            this.factory = factory;
        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (string.IsNullOrEmpty(controllerName))
                throw new ArgumentNullException("controllerName");

            try
            {
                var controller = factory.CreateController(requestContext, controllerName);
                WrapControllerActionInvoker(controller);
                return controller;
            }
            catch (HttpException ex)
            {
                if (ex.GetHttpCode() == 404)
                {
                    // TODO: Log the exception

                    return new NotFoundController();
                }

                throw;
            }
        }

        void WrapControllerActionInvoker(IController controller)
        {
            var controllerWithInvoker = controller as Controller;
            if (controllerWithInvoker != null)
            {
                controllerWithInvoker.ActionInvoker = new ActionInvokerWrapper(controllerWithInvoker.ActionInvoker);
            }
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return factory.GetControllerSessionBehavior(requestContext, controllerName);
        }

        public void ReleaseController(IController controller)
        {
            factory.ReleaseController(controller);
        }
    }
}