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
    public class VersiculoSlim
    {
        public int numero { get; set; }
        public string formatado { get; set; }

        public VersiculoSlim() { }
    }
}