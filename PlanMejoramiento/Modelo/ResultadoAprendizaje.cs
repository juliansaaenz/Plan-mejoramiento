using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class ResultadoAprendizaje
    {
        public int Id { get; set; }
        public string CodigoRAP { get; set; }
        public string Descripcion { get; set; }
        public int IdCompetencia { get; set; }
    }
}