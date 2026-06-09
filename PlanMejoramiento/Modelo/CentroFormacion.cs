using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class CentroFormacion
    {
        public int Id { get; set; }
        public string NombreCentro { get; set; }
        public int IdCiudad { get; set; }
    }
}