using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.DTO;
using InternetMagazine.Models;
using InternetMagazine.PL.Infrastructure;
using AutoMapper;
using InternetMagazine.Util;

namespace InternetMagazine.Controllers
{
    public class HomeController : Controller
    {
        ICategoryService svc;
  
        MapperConfiguration config = new ViewAutoMapperConfiguration().Configure();
        IMapper map;


        public HomeController(ICategoryService _svc)
        {
            svc = _svc;
         
            map = config.CreateMapper();


            List<RoomDTO> categories = (List <RoomDTO>)svc.Categories();

            categories.Insert(0, new RoomDTO() { Id = 0, Name = "Все комнаты" });

            var ct = map.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(categories);
            ViewBag.books = ct;

        } 
        public ActionResult Index(int? id)
        {

            IEnumerable<EventViewModel> productsvm;

            
            if (id != null && id > 0)
            {
               IEnumerable<EventDTO> prod = svc.LoadProductsCategory(id);
               productsvm = map.Map<IEnumerable<EventDTO>, List<EventViewModel>>(prod);
                ViewBag.PageId = id;
            }else{
                IEnumerable<EventDTO> prod = svc.Products();
                productsvm = map.Map<IEnumerable<EventDTO>, List<EventViewModel>>(prod);
                ViewBag.PageId = 0;
            }

            
            ViewBag.CurrentPage = "Index";
            return View(productsvm);
        }

        public ActionResult About()
        {
            ViewBag.CurrentPage = "About";

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.CurrentPage = "Contact";

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProductItem(int? id)
        {
            if (id == null){
                return Redirect("/Home/Error");
            }

            EventDTO toShowItem;

            try{
                toShowItem = svc.GetOneProduct(id.GetValueOrDefault());

                return View(map.Map<EventDTO, EventViewModel>(toShowItem));
            }
            catch(ValidationException ex){
                return Redirect("/Home/Error");
            }
        }

        public ActionResult OrderRoom()
        {

            List<RoomDTO> categories = (List<RoomDTO>)svc.Categories();
            var ct = map.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(categories);
            ViewBag.categories = ct;

            ViewBag.CurrentPage = "MakeOrder";
            return View();
        }

        [HttpPost]
        public ActionResult OrderRoom(OrderItemVIewModel or)
        {
            List<RoomDTO> categories = (List<RoomDTO>)svc.Categories();
            var ct = map.Map<IEnumerable<RoomDTO>, List<RoomViewModel>>(categories);
            ViewBag.categories = ct;

            try
            {
                svc.CreateOrder(map.Map<OrderItemVIewModel, OrderItemDTO>(or));
                return Redirect("/Home");
            }catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return View(or);
            }

            
            
        }

        public ActionResult Search(string q)
        {
            IEnumerable<EventDTO> sevents = null;
            ViewBag.NotFound = false;
            try
            {
                 sevents = svc.Search(q);
            }catch(ValidationException ex)
            {
                ViewBag.NotFound = true;
            }
          


            
            ViewBag.PageId = 0;
            ViewBag.CurrentPage = "Index";
            return View("index", map.Map<IEnumerable<EventDTO>, List<EventViewModel>>(sevents));
        }

        public ActionResult Error()
        {

            return View("~/Views/Shared/Error.cshtml");
        }
    }
}