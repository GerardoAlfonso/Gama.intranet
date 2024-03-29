﻿using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.Request;
using Gama.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Gama.Intranet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Documentos()
        {
            return View();
        }
        public IActionResult PortalDocumental()
        {
            return View();
        }
        public IActionResult MantenimientoUsuarios()
        {
            return View();
        }
        public IActionResult Bitacora()
        {
            return View();
        }
        public IActionResult Proyectos()
        {
            return View();
        }
        public IActionResult MantenimientoGeneral()
        {
            return View();
        }
        public IActionResult RRHH()
        {
            return View();
        }
        public IActionResult Recursos()
        {
            return View();
        }

    }
}
