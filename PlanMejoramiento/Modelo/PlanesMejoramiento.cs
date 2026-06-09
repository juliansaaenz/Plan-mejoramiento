}using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class PlanesMejoramiento
    {
        public int Id { get; set; }
        public string TipoPlan { get; set; } 
        public DateTime FechaAsignacion { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Observaciones { get; set; }
        public string EstadoPlan { get; set; }
        public int IdAprendiz { get; set; }
        public int IdInstructor { get; set; }
        public int? IdPlanOrigen { get; set; }
    }
}