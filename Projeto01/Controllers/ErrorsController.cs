﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto01.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult General()
        {
            return View();
        }

        public ActionResult Http400()
        {
            return View();
        }

        public ActionResult Http403()
        {
            return View();
        }

        public ActionResult Http404()
        {
            return View();
        }
    }
}