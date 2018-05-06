using System;
using System.Collections.Generic;
using System.Web.Mvc;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.DTO;
using InternetMagazine.Models;
using InternetMagazine.PL.Infrastructure;
using AutoMapper;

namespace InternetMagazine.Controllers
{
    public class AccountController : Controller
    {
        ICategoryService CSvc;
        IUserService USvc;

        IMapper ViewToDto;
        IMapper DtoToView;
        IMapper productMap;
        IMapper categoryMap;
        IMapper productMapRev;

        IEnumerable<CategoryDTO> categories;

        public AccountController(ICategoryService _csvc, IUserService _usvc)
        {
            CSvc = _csvc;
            USvc = _usvc;

            ViewToDto = new MapperConfiguration(cfg => cfg.CreateMap<RegistViewModel, UserDTO>()).CreateMapper();
            DtoToView = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, RegistViewModel>()).CreateMapper();
            productMap = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            productMapRev = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, ProductDTO > ()).CreateMapper();
            categoryMap = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();

            categories = CSvc.Categories();
        }

        public ActionResult Index()
        {
            ViewBag.CurrentPage = "Account";

            if (!User.Identity.IsAuthenticated)
                return Redirect("/");

            UserDTO curr = USvc.getUserByName(User.Identity.Name);

            return View(DtoToView.Map<UserDTO, RegistViewModel>(curr));
        }

        [HttpGet]
        public ActionResult Categories()
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
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel view)
        {
            
            if (view.File != null)
            {
                if(view.File.ContentType == "image/jpeg")
                {
                    // получаем имя файла
                    DateTime dt = new DateTime();
                    
                    string filename = DateTime.Now.ToString("ddMMMMyyyyHHmmss")+".jpg";
                    string path = "/Images/tmp/" + filename;
                    // сохраняем файл в папку Files в проекте
                    view.File.SaveAs(Server.MapPath(path));
                    view.ImgUrl = path;
                }
               
            }

            ProductDTO ToSend = productMapRev.Map<ProductViewModel,ProductDTO>(view);

            CSvc.AddProduct(ToSend);
            return Redirect("/Account/Products");
        }

    }
}