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

        public AccountController(ICategoryService _csvc, IUserService _usvc)
        {
            CSvc = _csvc;
            USvc = _usvc;

            ViewToDto = new MapperConfiguration(cfg => cfg.CreateMap<RegistViewModel, UserDTO>()).CreateMapper();
            DtoToView = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, RegistViewModel>()).CreateMapper();
        }

        public ActionResult Index()
        {
            ViewBag.CurrentPage = "Account";

            if (!User.Identity.IsAuthenticated)
                return Redirect("/");

            UserDTO curr = USvc.getUserByName(User.Identity.Name);

            return View(DtoToView.Map<UserDTO, RegistViewModel>(curr));
        }
    }
}