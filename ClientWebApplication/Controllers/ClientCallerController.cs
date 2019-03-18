using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClientWebApplication.HTTPClient;
using ClientWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientWebApplication.Controllers
{
    public class ClientCallerController : Controller
    {
        private IHttpClientAccessor _httpClientAccessor;

        /// <summary>
        /// Constructor
        /// </summary>
        public ClientCallerController(IHttpClientAccessor httpClientAccessor)
        {
            _httpClientAccessor = httpClientAccessor;
        }

        // GET: ClientCaller
        public ActionResult Index()
        {
            string response = ClientHTTPCaller.SendPost(_httpClientAccessor, "/api/customers", "");

            return Content(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}