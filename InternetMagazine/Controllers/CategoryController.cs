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

        IEnumerable<CategoryDTO> categories;

        public CategoryController(ICategoryService _csvc)
        {
            CSvc = _csvc;

            map = config.CreateMapper();

            categories = CSvc.Categories();
        }

        public ActionResult Index()
        {
            var ct = map.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
            return View(ct);
        }

        [HttpPost]
        public ActionResult Categories(int id, string name, string mode)
        {
            UserDTO u = (UserDTO)Session["user"];
            if ((u.Role != "Admin" && u.Role != "Menager") || u == null)
                return Content("no");

            if (mode == "add"){
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

        public ActionResult Products(string sortOrder="id")
        {
            IEnumerable<ProductDTO> prod = CSvc.Products(sortOrder);
            IEnumerable<ProductViewModel>  productsvm = map.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(prod);

            return View(productsvm);
        }

        public ActionResult CreateProduct()
        {
            UserDTO u = (UserDTO)Session["user"];
            if ((u.Role != "Admin" && u.Role != "Menager") || u == null)
                return Redirect("/Home/Error");

            var ct = map.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
            ViewBag.categories = ct;
            ViewBag.Edit = false;
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel view)
        {
            UserDTO u = (UserDTO)Session["user"];
            if ((u.Role != "Admin" && u.Role != "Menager") || u == null)
                return Redirect("/Home/Error");

            var ct = map.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
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
            ProductDTO ToSend = map.Map<ProductViewModel, ProductDTO>(view);
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
            UserDTO u = (UserDTO)Session["user"];
            if ((u.Role != "Admin" && u.Role != "Menager") || u == null)
                return Content("Товар не удален нет прав");

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
            UserDTO u = (UserDTO)Session["user"];
            if ((u.Role != "Admin" && u.Role != "Menager") || u == null)
                return Redirect("/Home/Error");

            var ct = map.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
            ViewBag.categories = ct;
            ViewBag.Edit = true;

            try
            {
                ProductDTO product = CSvc.GetOneProduct(id);
                ProductViewModel vProduct = map.Map<ProductDTO, ProductViewModel>(product);
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
        public ActionResult EditProduct(ProductViewModel model)
        {

            UserDTO u = (UserDTO)Session["user"];
            if ((u.Role != "Admin" && u.Role != "Menager") || u == null)
                return Redirect("/Home/Error");

            var ct = map.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
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

            ProductDTO ToSend = map.Map<ProductViewModel, ProductDTO>(model);
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

    }
}