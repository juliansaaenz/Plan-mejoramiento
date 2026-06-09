using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class Programa
    {
        public int Id { get; set; }
        public string CodigoPrograma { get; set; }
        public string NombrePrograma { get; set; }
        public string Version { get; set; }
        public string NivelFormacion { get; set; }
        public int Duracion { get; set; }
        public string Estado { get; set; }
    }
}