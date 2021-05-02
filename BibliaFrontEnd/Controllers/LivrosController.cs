using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BibliaFrontEnd.Models;
using Microsoft.AspNetCore.Cors;

namespace BibliaFrontEnd.Controllers
{
    //[EnableCors("MyCORSPolicy")]
    //[DisableCors]
    public class LivrosController : Controller
    {
        [Route("")]
        [Route("Livros")]
        [Route("Livros/Index")]
        [HttpGet, ActionName("Index")]
        public ActionResult Index(int Livro = 0, int Capitulo = 0, int Versiculo = 0)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "http://localhost:8080");

            //GET livros
            if (Livro == 0 && Capitulo == 0 && Versiculo == 0)
            {
                List<Livro> CapituloList = new List<Livro>();

                Livro livros = new Livro();

                var lista = livros.ListarLivros(out string _erro);

                //return View(lista);
                return Json(lista, JsonRequestBehavior.AllowGet);
            }

            //GET capitulos
            else if (Capitulo == 0 && Versiculo == 0)
            {
                List<Capitulo> CapituloList = new List<Capitulo>();

                Capitulo capitulos = new Capitulo();

                var lista = capitulos.ListarCapitulos(Livro, out string _erro);

                return Json(lista, JsonRequestBehavior.AllowGet);
            }

            //GET versiculos
            else if (Versiculo == 0)
            {
                List<Versiculo> VersiculoList = new List<Versiculo>();

                Versiculo versiculos = new Versiculo();

                var lista = versiculos.ListarVersiculos(Livro, Capitulo, out string _erro);

                return Json(lista, JsonRequestBehavior.AllowGet);
            }

            //GET versiculo
            else
            {
                List<Versiculo> VersiculoList = new List<Versiculo>();

                Versiculo versiculo = new Versiculo();

                var lista = versiculo.ListarVersiculo(Livro, Capitulo, Versiculo, out string _erro);

                return Json(lista, JsonRequestBehavior.AllowGet);
            }

            //return View(lista);
        }

        [HttpGet, ActionName("ListarCapitulos")]
        public ActionResult Index(int livro)
        {
            List<Capitulo> CapituloList = new List<Capitulo>();

            Capitulo cap = new Capitulo();

            var lista = cap.ListarCapitulos(livro, out string _erro);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ListarVersiculos(int livro, int capitulo)
        {
            List<Versiculo> VersiculoList = new List<Versiculo>();

            Versiculo vers = new Versiculo();

            var lista = vers.ListarVersiculos(livro, capitulo, out string _erro);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ListarVersiculo(int livro, int capitulo, int versiculo)
        {
            List<Versiculo> VersiculoList = new List<Versiculo>();

            Versiculo vers = new Versiculo();

            var lista = vers.ListarVersiculo(livro, capitulo, versiculo, out string _erro);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [Route("")]
        [Route("Livros")]
        [Route("Livros/ListarVers")]
        [HttpGet, ActionName("ListarVers")]
        public ActionResult ListarVers(int livro, int capitulo)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "http://localhost:8080");

            List<Versiculo> VersiculoList = new List<Versiculo>();

            Versiculo vers = new Versiculo();

            LivroCapVers lista = vers.ListarVers(livro, capitulo, out string _erro);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}