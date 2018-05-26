using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetMagazine.Controllers
{
    public class StatController : Controller
    {
        IStatService stat;

        public StatController(IStatService _stat)
        {
            stat = _stat;
        }
        // GET: Stat
        public ActionResult Index()
        {

            UserDTO u = (UserDTO)Session["user"];
            if (u.Role != "Admin" || u == null)
                return Redirect("/Home/Error");

            ViewBag.CountProducts = stat.CountProducts();
            ViewBag.CountOrders = stat.CountOrders();
            ViewBag.CountCategories = stat.CountCategories();
            ViewBag.CountUsers = stat.CountUsers();

            ViewBag.StatPeriod = stat.BestProducts(5);
            return View();
        }
    }
}