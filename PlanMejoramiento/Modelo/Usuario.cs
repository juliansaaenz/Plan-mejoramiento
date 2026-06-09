using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Modelo
{
    public class Usuario
    {
        public int Id { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public int IdRol { get; set; }
        public int? IdCentroFormacion { get; set; }
    }
}