#region credits
// ***********************************************************************
// Assembly	: Deten
// Author	: Marko Ilievski
// Created	: 04-12-2013
// 
// Last Modified By : Marko Ilievski
// Last Modified On : 04-12-2013
// ***********************************************************************
#endregion

using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace TaskForceManager.Extensions.ErrorHandlingHelpers
{
    #region

    

    #endregion

    public class NotFoundController : IController
    {
        public void Execute(RequestContext requestContext)
        {
            (new ErrorResult { StatusCode = HttpStatusCode.NotFound }).ExecuteResult(
                new ControllerContext(requestContext, new FakeController())
            );
        }

        // ControllerContext requires an object that derives from ControllerBase.
        // NotFoundController does not do this.
        // So the easiest workaround is this FakeController.
        class FakeController : Controller { }
    }
}