﻿using AutoMapper;
using InternetMagazine.Models;
using InternetMagazine.PL.DTO;
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
        MapperConfiguration config = new ViewAutoMapperConfiguration().Configure();
        IMapper map;

        public ChartController(ICategoryService _cats)
        {
            cats = _cats;
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

        public ActionResult Minus(int id)
        {

            GetOrder().MinusItem(id);
            return Redirect("/Chart");
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