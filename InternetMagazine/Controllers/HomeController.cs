using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.DTO;
using InternetMagazine.Models;
using AutoMapper;

namespace InternetMagazine.Controllers
{
    public class HomeController : Controller
    {
        ICategoryService svc;


        public HomeController(ICategoryService _svc)
        {
            svc = _svc;

            List<CategoryDTO> categories = (List <CategoryDTO>)svc.Categories();
            categories.Insert(0, new CategoryDTO() { Id = 0, Name = "Все категории" });

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
            var ct = mapper.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
            ViewBag.books = categories;

        } 
        public ActionResult Index(int? id)
        {

            IEnumerable<ProductViewModel> productsvm;

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();

            if (id != null && id > 0)
            {
               IEnumerable<ProductDTO> prod = svc.LoadProductsCategory(id);
                productsvm = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(prod);
                ViewBag.PageId = id;
            }else{
                IEnumerable<ProductDTO> prod = svc.Products();
                productsvm = mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(prod);
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

        public ActionResult ProductItem(int id)
        {
            
            return View();
        }
    }
}