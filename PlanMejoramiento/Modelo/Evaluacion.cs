using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class Evaluacion
    {
        public int Id { get; set; }
        public string EvalProducto { get; set; }
        public string EvalConocimiento { get; set; }
        public string EvalDesempeño { get; set; }
        public DateTime FechaEvaluacion { get; set; }
        public int IdPlan { get; set; }
    }
}