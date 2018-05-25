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


            List<CategoryDTO> categories = (List <CategoryDTO>)svc.Categories();

            categories.Insert(0, new CategoryDTO() { Id = 0, Name = "Все категории" });

            var ct = map.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
            ViewBag.books = ct;

           
           

        } 
        public ActionResult Index(int? id,string sortOrder = "id")
        {
            ViewBag.IdSortParam = sortOrder == "id" ? "idASC" : "id";
            ViewBag.CatSortParam = sortOrder == "cat" ? "catASC" : "cat";
            ViewBag.PriceSortParam = sortOrder == "price" ? "priceASC" : "price";
            ViewBag.AuthorSortParam = sortOrder == "author" ? "authorASC" : "author";

            IEnumerable<ProductViewModel> productsvm;

            
            if (id != null && id > 0)
            {
               IEnumerable<ProductDTO> prod = svc.LoadProductsCategory(id, sortOrder);
               productsvm = map.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(prod);
                ViewBag.PageId = id;
            }else{
                IEnumerable<ProductDTO> prod = svc.Products(sortOrder);
                productsvm = map.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(prod);
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

                return View(map.Map<ProductDTO, ProductViewModel>(toShowItem));
            }
            catch(ValidationException ex){
                return Redirect("/Home/Error");
            }
        }

        public ActionResult Error()
        {

            return View("~/Views/Shared/Error.cshtml");
        }

        public ActionResult Search(string q)
        {
            IEnumerable<ProductDTO> products = null;
            ViewBag.NotFound = false;
            try
            {
                products = svc.Search(q);
            }
            catch (ValidationException ex)
            {
                ViewBag.NotFound = true;
            }

            ViewBag.PageId = 0;
            ViewBag.CurrentPage = "Index";
            return View("index", map.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(products));
        }
    }
}