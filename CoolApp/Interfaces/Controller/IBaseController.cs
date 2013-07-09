using System.Web.Mvc;

namespace CoolApp.Interfaces.Controller
{
    public interface IBaseController<in TViewModel> : IController
    {
        ActionResult Index();

        /// <summary>
        /// Lists the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet] 
        ActionResult List(int page, int pageSize);

        /// <summary>
        /// Shows the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpGet]
        ActionResult Show(int id);

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        ActionResult Create();

        //Retrieve 
        [HttpGet]
        ActionResult Edit(int id);

        //Create/Edit
        [HttpPost]
        ActionResult Edit(TViewModel viewModel, int id); //, int id

        // Delete existing
        [HttpPost]
        ActionResult Delete(int id);

        [HttpPost]
        ActionResult DeleteSome(int[] id);

    }
}