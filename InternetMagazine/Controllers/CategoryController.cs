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
      
        IMapper productMap;
        IMapper categoryMap;
        IMapper productMapRev;

        IEnumerable<CategoryDTO> categories;

        public CategoryController(ICategoryService _csvc)
        {
            CSvc = _csvc;
           
            productMap = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            productMapRev = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, ProductDTO > ()).CreateMapper();
            categoryMap = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();

            categories = CSvc.Categories();
        }

        public ActionResult Index()
        {
            var ct = categoryMap.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
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
            IEnumerable<ProductDTO> prod = CSvc.Products();
            IEnumerable<ProductViewModel>  productsvm = productMap.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(prod);

            return View(productsvm);
        }

        public ActionResult CreateProduct()
        {
            var ct = categoryMap.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
            ViewBag.categories = ct;
            ViewBag.Edit = false;
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel view)
        {
            var ct = categoryMap.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
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
            ProductDTO ToSend = productMapRev.Map<ProductViewModel, ProductDTO>(view);
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
            var ct = categoryMap.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
            ViewBag.categories = ct;
            ViewBag.Edit = true;

            try
            {
                ProductDTO product = CSvc.GetOneProduct(id);
                ProductViewModel vProduct = productMap.Map<ProductDTO, ProductViewModel>(product);
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
            var ct = categoryMap.Map<IEnumerable<CategoryDTO>, List<CategoryViewModel>>(categories);
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

            ProductDTO ToSend = productMapRev.Map<ProductViewModel, ProductDTO>(model);
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