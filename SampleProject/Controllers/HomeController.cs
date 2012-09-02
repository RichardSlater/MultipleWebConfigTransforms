using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Configuration;

namespace SampleProject.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            var dictionary = new Dictionary<string, string>();
            dictionary.Add("Per Build Config Setting", ConfigurationManager.AppSettings["PerBuildConfig"]);
            dictionary.Add("Per Environment Setting", ConfigurationManager.AppSettings["PerEnvironment"]);
            return View(dictionary);
        }
    }
}
