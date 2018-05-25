using System;
using System.Collections.Generic;
using System.Web.Mvc;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.DTO;
using InternetMagazine.Models;
using InternetMagazine.PL.Infrastructure;
using System.Web.Security;
using AutoMapper;
using InternetMagazine.Util;

namespace InternetMagazine.Controllers
{
    public class AuthController : Controller
    {
        IUserService USvc;
        MapperConfiguration config = new ViewAutoMapperConfiguration().Configure();
        IMapper map;
        public AuthController(IUserService _svc)
        {
            USvc = _svc;

            map = config.CreateMapper();
        }
        // GET: Auth
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                UserDTO curr = USvc.getUserByName(User.Identity.Name);
                Session["user"] = curr;

                return Redirect("/User");
            }
            
            return View();
        }

        public ActionResult Regist()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/User");
            return View();
        }

        [HttpPost]
        public ActionResult Regist(RegistViewModel model)
        {
            
            if(model.Password == model.ConfirmPassword)
            {
                UserDTO newUser = map.Map<RegistViewModel, UserDTO>(model);
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
                try {
                    state = USvc.LoginVerify(model.NickName, model.Password);
                    UserDTO user = USvc.getUserByName(model.NickName);
                    Session["user"] = user;
                }
                catch(UserNotFoundExaption ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
                if (state)
                {
                    FormsAuthentication.SetAuthCookie(model.NickName, true);
                    return Redirect("/User");
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