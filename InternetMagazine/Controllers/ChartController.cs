using AutoMapper;
using InternetMagazine.Models;
using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Infrastructure;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.Services;
using InternetMagazine.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetMagazine.Controllers
{
    public class ChartController : Controller
    {

        ICategoryService cats;
        IOrderService ord;
        IUserService us;
        MapperConfiguration config = new ViewAutoMapperConfiguration().Configure();
        IMapper map;

        public ChartController(ICategoryService _cats,IOrderService _ord,IUserService _us)
        {
            cats = _cats;
            ord = _ord;
            us = _us;

            map = config.CreateMapper();

        }

        public ActionResult Index()
        {
            List<OrderItemDTO> ord = GetOrder().Lines.ToList();

            ViewBag.Price = GetOrder().ComputeTotalValue();

            return View(ord);
        }

        public ActionResult AddToCart(int id)
        {
            ProductDTO p = cats.GetOneProduct(id);

            GetOrder().AddItem(p, 1);
            return Redirect("/Home");
        }

        public ActionResult Remove(int id)
        {

            GetOrder().RemoveLine(id);
            return Redirect("/Chart");
        }

        public ActionResult Plus(int id)
        {

            GetOrder().PlusItem(id);
            return Redirect("/Chart");
        }

        public ActionResult MakeOrder()
        {
            UserDTO u = (UserDTO)Session["user"];

            if(u == null && User.Identity.IsAuthenticated)
            {
                u = us.getUserByName(User.Identity.Name);
            }

            if(u != null && User.Identity.IsAuthenticated)
            {
                
                List<OrderItemDTO> its = GetOrder().Lines.ToList();

                ord.AddOrder(its,u);

                GetOrder().Clear();
                return View();
            }

            return Redirect("/Home/Error");
           
        }

        public ActionResult Minus(int id)
        {

            GetOrder().MinusItem(id);
            return Redirect("/Chart");
        }

        public ActionResult Orders()
        {
            IEnumerable<OrderItemDTO> orders = null;
            try
            {
               orders = ord.Orders();
            }catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
           
            return View(map.Map< IEnumerable<OrderItemDTO> ,List<OrderItemVIewModel>>(orders));
        }


        public ActionResult RemoveOrder(int id)
        {
            try
            {
                ord.Delete(id);
            }catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return Content(ex.Message);
            }

            return Content("done");
        }

        public OrderLogic GetOrder()
        {
            OrderLogic cart = (OrderLogic)Session["Cart"];
            if (cart == null)
            {
                cart = new OrderLogic();
                Session["Cart"] = cart;
            }
            return cart;
        }

       
    }
}