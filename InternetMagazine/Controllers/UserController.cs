using AutoMapper;
using InternetMagazine.Models;
using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.Infrastructure;
using System.Collections.Generic;
using System.Web.Mvc;

namespace InternetMagazine.Controllers
{
    public class UserController : Controller
    {
        IUserService USvc;
        IMapper ViewToDto;
        IMapper DtoToView;

        public UserController(IUserService _USvc)
        {
            ViewToDto = new MapperConfiguration(cfg => cfg.CreateMap<RegistViewModel, PL.DTO.UserDTO>()).CreateMapper();
            DtoToView = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, RegistViewModel>()).CreateMapper();

            USvc = _USvc;
        }

        public ActionResult Index()
        {
            ViewBag.CurrentPage = "Account";

            if (!User.Identity.IsAuthenticated)
                return Redirect("/");

            UserDTO curr = USvc.getUserByName(User.Identity.Name);

            return View(DtoToView.Map<UserDTO, RegistViewModel>(curr));
        }

        public ActionResult Users()
        {
            try
            {
                IEnumerable<UserDTO> users = USvc.GetUsers();
                List<RegistViewModel> UserToView = DtoToView.Map<IEnumerable<UserDTO>, List<RegistViewModel>>(users);
                return View(UserToView);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Message, ex.Source);
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                UserDTO ud = USvc.getUserById(id);
                RegistViewModel UserToView = DtoToView.Map<UserDTO, RegistViewModel>(ud);

                return View(UserToView);
            }
            catch (UserNotFoundExaption ex)
            {
                return Redirect("/Home/Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(RegistViewModel model)
        {
            try
            {
                USvc.UpdateUser(ViewToDto.Map<RegistViewModel, UserDTO>(model));
                return  Redirect("/User/Users");
            }
            catch (UserNotFoundExaption ex)
            {
                ModelState.AddModelError(ex.Message, ex.Source);
            }
            return View(model);
        }

        public ActionResult Add()
        {
            return View("Edit");  
        }

        [HttpPost]
        public ActionResult Add(RegistViewModel model)
        {
            UserDTO newUser = ViewToDto.Map<RegistViewModel, UserDTO>(model);
            try
            {
                USvc.RegistUser(newUser);
                return Redirect("/User/Users");
            }
            catch (UserNotFoundExaption ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View("Edit",model);
        }

        public ActionResult Delete(int? id)
        {
            if(id > 1 || id != null)
            {
                USvc.RemoveUser(id);
                return  Content("done");
            }
            else
            {
                return Content("Пользователь не добавлен");
            }
           
        }
    }
}