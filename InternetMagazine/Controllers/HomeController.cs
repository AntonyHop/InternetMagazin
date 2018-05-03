using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.DTO;
using InternetMagazine.Models;
using InternetMagazine.PL.Infrastructure;
using AutoMapper;

namespace InternetMagazine.Controllers
{
    public class HomeController : Controller
    {
        ICategoryService svc;
        IMapper productMap;
        IMapper categoryMap;


        public HomeController(ICategoryService _svc)
        {
            svc = _svc;

            productMap = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            categoryMap = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();


            List<CategoryDTO> categories = (List <CategoryDTO>)svc.Categories();

            categories.Insert(0, new CategoryDTO() { Id = 0, Name = "Все категории" });

            var ct = categoryMap.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
            ViewBag.books = categories;

        } 
        public ActionResult Index(int? id)
        {

            IEnumerable<ProductViewModel> productsvm;

            
            if (id != null && id > 0)
            {
               IEnumerable<ProductDTO> prod = svc.LoadProductsCategory(id);
                productsvm = productMap.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(prod);
                ViewBag.PageId = id;
            }else{
                IEnumerable<ProductDTO> prod = svc.Products();
                productsvm = productMap.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(prod);
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

            ProductDTO toShowItem;

            try{
                toShowItem = svc.GetOneProduct(id.GetValueOrDefault());

                return View(productMap.Map<ProductDTO, ProductViewModel>(toShowItem));
            }
            catch(ValidationException ex){
                return Redirect("/Home/Error");
            }
        }

        public ActionResult Error()
        {

            return View("~/Views/Shared/Error.cshtml");
        }
    }
}