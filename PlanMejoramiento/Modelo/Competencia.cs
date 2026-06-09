using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class Competencia
    {
        public int Id { get; set; }
        public string CodigoCompetencia { get; set; }
        public string Denominacion { get; set; }
        public int IdPrograma { get; set; }
    }
}