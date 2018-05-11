using AutoMapper;
using InternetMagazine.Models;
using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}