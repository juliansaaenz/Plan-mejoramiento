using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string NombreCiudad { get; set; }
        public int IdRegional { get; set; }
        public Regional Regional { get; set; }
    }
}