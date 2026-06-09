using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class Evidencia
    {
        public int Id { get; set; }
        public string RutaArchivo { get; set; }
        public string TipoArchivo { get; set; } 
        public DateTime FechaSubida { get; set; }
        public string ObservacionesInstructor { get; set; }
        public int IdActividad { get; set; }
    }
}