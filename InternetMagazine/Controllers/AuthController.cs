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
        public AuthController(IUserService _svc)
        {
            USvc = _svc;

            ViewToDto = new MapperConfiguration(cfg => cfg.CreateMap<RegistViewModel, UserDTO>()).CreateMapper();
        }
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Regist()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Regist(RegistViewModel model)
        {
            UserDTO newUser = ViewToDto.Map<RegistViewModel, UserDTO>(model);
            try
            {
                USvc.RegistUser(newUser);
                FormsAuthentication.SetAuthCookie(model.NickName, true);
                return Redirect("/");
            }
            catch(UserNotFoundExaption ex)
            {
                ModelState.AddModelError("", ex.Message);
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
                    ModelState.AddModelError("", ex.Message);
                }
                if (state)
                {
                    FormsAuthentication.SetAuthCookie(model.NickName, true);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("Nikname", "Пользователь с таким логином уже существует");
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