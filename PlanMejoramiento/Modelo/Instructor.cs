using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Especialidad { get; set; }
        public int IdUsuario { get; set; }
    }
}