using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CoolApp.Enums;
using CoolApp.Extensions.ModelState;
using CoolApp.Extensions.TempData;
using CoolApp.Filters;
using CoolApp.Interfaces.Controller;
using CoolApp.Models;
using CoolApp.Core.Interfaces.Service;
using CoolApp.Core.Models;
using Omu.ValueInjecter;

namespace CoolApp.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    [MultiResponseFormat]
    public abstract class BaseController<T, TViewModel> : Controller, IBaseController<TViewModel>
        where T : DomainObject, new() where TViewModel : new()
    {
        /// <summary>
        /// The service
        /// </summary>
        protected IService<T> Service;

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return List();
        }

        /// <summary>
        /// Lists the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult List(int page = 1, int pageSize = Int16.MaxValue)
        {
            var models = Service.GetAll().OrderBy(y => y.Created);

            IEnumerable<TViewModel> viewModels = models.
                                        ToList().
                                        Select(o => new TViewModel().InjectFrom(o)).Cast<TViewModel>();
            
            //IPage<T> model = Service.Page(page, pageSize);
            return View("Index", viewModels);
        }

        /// <summary>
        /// Shows the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Show(int id)
        {
            T entity = Service.GetById(id);

            var model = new TViewModel();
            model.InjectFrom<UnflatLoopValueInjection>(entity);

            return View(model);
        }


        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View("Edit", new TViewModel());
        }

        /// <summary>
        /// Edits the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            T entity = Service.GetById(id);
            if (entity == null)
            {
                TempData.AddErrorMessage("Record was not found");
                return View();
            }

            var model = new TViewModel();
            model.InjectFrom<UnflatLoopValueInjection>(entity);

            return View(model);
        }


        /// <summary>
        /// Edits the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TViewModel model, int id)
        {
            var info = new ViewInfoModel { Status = ModelState.IsValid ? ViewInfoStatus.Error : ViewInfoStatus.Success };
            if (ModelState.IsValid)
            {
                T entity = (id > 0) ? Service.GetById(id) : new T();
                entity.InjectFrom<UnflatLoopValueInjection>(model);

                Service.SaveOrUpdate(entity);
                if (ModelState.Process(Service.SaveOrUpdate(entity)))
                {
                    info.Status = ViewInfoStatus.Success;
                    TempData["Success"] =
                        info.Message = String.Format("Record was successfully {0}", (id > 0) ? "updated" : "created");
                }
            }
            else
            {
                info.Errors = ModelState.Errors();
                TempData["Error"] =
                        info.Message = "Error processing your request";

            }
            
            return View("_Info", info);
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var entity = Service.GetById(id);
            var info = new ViewInfoModel();
            try
            {
                Service.Delete(entity);
                info.Status = ViewInfoStatus.Success;
                TempData["Success"] = info.Message = "Record was successfully deleted";
            }
            catch(Exception ex)
            {
                info.Status = ViewInfoStatus.Error;
                TempData["Error"] = info.Errors = ex.Message;
            }
            return View("_Info", info);
        }


        /// <summary>
        /// Deletes some.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult DeleteSome(int[] id)
        {
            var info = new ViewInfoModel();
            try
            {
                foreach (var i in id)
                {
                    Service.Delete(Service.GetById(i));
                }
                TempData["Success"] = info.Message = "Records successfully deleted";
            }
            catch (Exception ex)
            {
                info.Status = ViewInfoStatus.Error;
                TempData["Error"] = info.Message = ex.Message;
            }
            return View("_Info", info);
        }

    }

}