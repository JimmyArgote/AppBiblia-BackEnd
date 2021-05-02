using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Routing;
using BibliaFrontEnd.Models;
using ActionNameAttribute = System.Web.Http.ActionNameAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using System.Web;

namespace BibliaFrontEnd.Controllers
{
    public class SearchController : Controller
    {
        [Route("")]
        [Route("Search")]
        [Route("Search/Index/{palavra:string}")]
        [System.Web.Http.HttpPost, ActionName("Index")]
        public ActionResult Index(string palavra)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "http://localhost:8080");

            if (palavra == null || palavra == "")
            {
                var list = "insira uma palavra chave para fazer sua busca!";
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            List<Versiculo> VersiculoList = new List<Versiculo>();

            Versiculo vers = new Versiculo();

            var lista = vers.Pesquisar(palavra, out string _erro);

            var json = Json(new { lista }, JsonRequestBehavior.AllowGet);

            json.MaxJsonLength = Int32.MaxValue;


            return json;
        }
    }
}
