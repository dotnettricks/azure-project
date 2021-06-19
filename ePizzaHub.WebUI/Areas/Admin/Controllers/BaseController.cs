using ePizzaHub.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePizzaHub.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CustomAuthorize(Roles ="Admin")]
    public class BaseController : Controller
    {
       
    }
}
