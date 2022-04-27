using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RedisCache.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            //Gravando na Sessão utilizando Redis
            String messageSession = "Agora Estou utilizando Sessão";

            Session["RedisMessage"] = messageSession;

            //Obtendo o objeto da sessão utilizando Redis
            var messageFromSession = Session["RedisMessage"];
            
            String message = "Gravando a mensagem no cache redis";

            //Grava no cache
            CacheManager.Set<String>("message", message);

            //Obtem do cache
            String messageFromCache = CacheManager.Get<String>("message");

            ViewBag.Message = messageFromCache;

            var nObj = new List<string>() { "Item 1", "Item 2", "Item 3" };

            // grava na session
            Session["RedisObject"] = nObj;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your About page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}