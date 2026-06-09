using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class Aprendiz
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public string ImagenUrl { get; set; }
        public int IdFicha { get; set; }
        public int IdUsuario { get; set; }
        public int IdCiudadResidencia { get; set; }
    }
}