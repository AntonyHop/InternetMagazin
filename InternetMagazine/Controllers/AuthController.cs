using System;
using System.Collections.Generic;
using System.Web.Mvc;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.DTO;
using InternetMagazine.Models;
using InternetMagazine.PL.Infrastructure;
using System.Web.Security;
using AutoMapper;

namespace InternetMagazine.Controllers
{
    public class AuthController : Controller
    {
        IUserService USvc;
        IMapper ViewToDto;
        IMapper DtoToView;
        public AuthController(IUserService _svc)
        {
            USvc = _svc;

            ViewToDto = new MapperConfiguration(cfg => cfg.CreateMap<RegistViewModel, UserDTO>()).CreateMapper();
            DtoToView = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, RegistViewModel > ()).CreateMapper();
        }
        // GET: Auth
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/Account");
            return View();
        }

        public ActionResult Regist()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/Account");
            return View();
        }

        [HttpPost]
        public ActionResult Regist(RegistViewModel model)
        {
            
            if(model.Password == model.ConfirmPassword)
            {
                UserDTO newUser = ViewToDto.Map<RegistViewModel, UserDTO>(model);
                try
                {
                    USvc.RegistUser(newUser);
                    FormsAuthentication.SetAuthCookie(model.NickName, true);
                    return Redirect("/");
                }
                catch (UserNotFoundExaption ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("AuthController", "Подтвердите пароль");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AuthViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool state = false;
                try { state = USvc.LoginVerify(model.NickName, model.Password); }
                catch(UserNotFoundExaption ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
                if (state)
                {
                    FormsAuthentication.SetAuthCookie(model.NickName, true);
                    return Redirect("/Account");
                }
                else
                {
                    ModelState.AddModelError("AuthController", "Вы не верно ввели пароль");
                }
            }
            return View(model);

        }

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();

            return Redirect("/");
        }
    }
}