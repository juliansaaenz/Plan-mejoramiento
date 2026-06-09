using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class ActividadesPlan
    {
        public int Id { get; set; }
        public string DescripcionActividad { get; set; }
        public string EstadoActividad { get; set; } 
        public int IdPlan { get; set; }
    }
}