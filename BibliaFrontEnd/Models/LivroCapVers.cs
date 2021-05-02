using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BibliaFrontEnd.Models
{
    public class LivroCapVers
    {
        public int      Livro_id        { get; set; }
        public int      Capitulo_id     { get; set; }
        public int      Vers_total      { get; set; }
        public int      Caps_total      { get; set; }
        public string   Livro_nome      { get; set; }
        public string   Livro_sigla     { get; set; }
        public string   Testamento      { get; set; }
        public dynamic    Error         { get; set; }

        
        public List<VersiculoSlim> VersiculosList { get; set; }

        public LivroCapVers() { }

    }
}