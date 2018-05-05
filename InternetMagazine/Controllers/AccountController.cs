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

        public AccountController(ICategoryService _csvc, IUserService _usvc)
        {
            CSvc = _csvc;
            USvc = _usvc;

            ViewToDto = new MapperConfiguration(cfg => cfg.CreateMap<RegistViewModel, UserDTO>()).CreateMapper();
            DtoToView = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, RegistViewModel>()).CreateMapper();
            productMap = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            categoryMap = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryViewModel>()).CreateMapper();
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
            IEnumerable<CategoryDTO> categories = CSvc.Categories();
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
                    ModelState.AddModelError(ex.Message, ex.Property);
                }
            }else if (mode == "edit"|| name != ""){
                try {
                    CSvc.EditCategory(id,name);
                }catch (ValidationException ex){
                    ModelState.AddModelError(ex.Message, ex.Property);
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
    }
}