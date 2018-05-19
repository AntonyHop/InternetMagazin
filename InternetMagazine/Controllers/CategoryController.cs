using System;
using System.Collections.Generic;
using System.Web.Mvc;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.DTO;
using InternetMagazine.Models;
using InternetMagazine.PL.Infrastructure;
using InternetMagazine.Util;
using AutoMapper;

namespace InternetMagazine.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryService CSvc;

        MapperConfiguration config = new ViewAutoMapperConfiguration().Configure();
        IMapper map;

        IEnumerable<RoomDTO> categories;

        public CategoryController(ICategoryService _csvc)
        {
            CSvc = _csvc;

            map = config.CreateMapper();

            categories = CSvc.Categories();
        }

        public ActionResult Index()
        {
            var ct = map.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(categories);
            return View(ct);
        }

        [HttpPost]
        public ActionResult Categories(int id, string name, string mode)
        {
            if(mode == "add"){
                try{
                    CSvc.AddCategory(name);
                }catch(ValidationException ex){
                    return Json(ex.Message);
                }
            }else if (mode == "edit"|| name != ""){
                try {
                    CSvc.EditCategory(id,name);
                }catch (ValidationException ex){
                    return Json(ex.Message);
                }
            }else if (mode == "delete"){
                try{
                    if(id != 1)
                    {
                        CSvc.DeleteCategory(id);//Нельзя удалить без категории
                    }else{
                        return Json("no");
                    }
                }catch (ValidationException ex){
                    return Json("no");
                }
            }
            return Json("ok");
        }

        public ActionResult Products()
        {
            IEnumerable<EventDTO> prod = CSvc.Products();
            IEnumerable<EventViewModel>  productsvm = map.Map<IEnumerable<EventDTO>, List<EventViewModel>>(prod);

            return View(productsvm);
        }

        public ActionResult CreateProduct()
        {
            var ct = map.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(categories);
            ViewBag.categories = ct;
            ViewBag.Edit = false;
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(EventViewModel view)
        {
            var ct = map.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(categories);
            ViewBag.categories = ct;
            ViewBag.CurrId = 1;
            ViewBag.Edit = false;

            try
            {
                view.ImgUrl = LoadLogic.UploadLogic(view.File, Server);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, ex.Message);
            }
            EventDTO ToSend = map.Map<EventViewModel, EventDTO>(view);
            try
            {
                CSvc.AddProduct(ToSend);
                return Redirect("/Category/Products");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(view);

        }

        [HttpGet]
        public ActionResult RemoveProduct(int? id)
        {
            if (id != null)
            {
                try {
                    CSvc.DeleteProduct(id);
                }catch(ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                    return Content(ex.Message);
                }
            }
            return Content("done");
           
        }

        public ActionResult EditProduct(int? id)
        {
            var ct = map.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(categories);
            ViewBag.categories = ct;
            ViewBag.Edit = true;

            try
            {
                EventDTO product = CSvc.GetOneProduct(id);
                EventViewModel vProduct = map.Map<EventDTO, EventViewModel>(product);
                ViewBag.CurrId = product.CategoryId;

                return View("CreateProduct", vProduct);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
               
            }

            return Redirect("/Category/Products");

        }

        [HttpPost]
        public ActionResult EditProduct(EventViewModel model)
        {
            var ct = map.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(categories);
            ViewBag.categories = ct;
            ViewBag.CurrId = model.Id;
            ViewBag.Edit = true;

            try
            {
                model.ImgUrl = LoadLogic.UploadLogic(model.File, Server);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, ex.Message);
            }

            EventDTO ToSend = map.Map<EventViewModel, EventDTO>(model);
            try
            {
                CSvc.UpdateOneProduct(ToSend);
                return Redirect("/Category/Products");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Message, ex.Message);
            }

            return View("CreateProduct",model);

        }

        public ActionResult Orders()
        {
            IEnumerable<OrderItemDTO> prod = CSvc.GetOrders();
            IEnumerable<OrderItemVIewModel> productsvm = map.Map<IEnumerable<OrderItemDTO>, List<OrderItemVIewModel>>(prod);

            return View(productsvm);
        }

        public ActionResult DeleteOrder(int? id)
        {
            try
            {
                CSvc.DellOrder(id);
            }
            catch (ValidationException ex)
            {
                return Redirect("/Home/Error");
            }
            
            return Redirect("/Category/Orders");
        }

    }
}