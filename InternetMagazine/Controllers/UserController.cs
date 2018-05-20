using AutoMapper;
using InternetMagazine.Models;
using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.Infrastructure;
using System.Collections.Generic;
using System.Web.Mvc;
using InternetMagazine.Util;
using System.Web.Security;

namespace InternetMagazine.Controllers
{
    public class UserController : Controller
    {
        IUserService USvc;
        MapperConfiguration config = new ViewAutoMapperConfiguration().Configure();
        IMapper map;

        public UserController(IUserService _USvc)
        {
            map = config.CreateMapper();
            USvc = _USvc;
        }

        public ActionResult Index()
        {
            ViewBag.CurrentPage = "Account";
            UserDTO curr;
            if (!User.Identity.IsAuthenticated)
                return Redirect("/");
            try
            {
                curr = USvc.getUserByName(User.Identity.Name);
            }catch(UserNotFoundExaption ex)
            {
                FormsAuthentication.SignOut();
                return Redirect("/Auth");
            }
            return View(map.Map<UserDTO, RegistViewModel>(curr));
        }

        public ActionResult Users()
        {
            try
            {
                IEnumerable<UserDTO> users = USvc.GetUsers();
                List<RegistViewModel> UserToView = map.Map<IEnumerable<UserDTO>, List<RegistViewModel>>(users);
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
                RegistViewModel UserToView = map.Map<UserDTO, RegistViewModel>(ud);

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
                USvc.UpdateUser(map.Map<RegistViewModel, UserDTO>(model));
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
            UserDTO newUser = map.Map<RegistViewModel, UserDTO>(model);
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
                try
                {
                    USvc.RemoveUser(id);
                }catch(UserNotFoundExaption ex)
                {
                    FormsAuthentication.SignOut();
                }
              

              
                return  Content("done");
                
            }
            else
            {
                return Content("Пользователь не добавлен");
            }
           
        }
    }
}