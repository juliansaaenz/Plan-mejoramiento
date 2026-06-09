using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class Ficha
    {
        public int Id { get; set; }
        public string NumeroFicha { get; set; }
        public string Jornada { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int IdPrograma { get; set; }
        public int IdCentroFormacion { get; set; }
    }
}