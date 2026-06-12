using PlanMejoramiento.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Logica
{
    public class UsuarioL
    {
        private UsuarioD oUsuarioD = new UsuarioD();
        
        public DataTable MtIniciarSesion(string correo, string contrasena)
        {
            if (string.IsNullOrEmpty(correo))
                throw new ArgumentException("El correo electrónico es obligatorio para iniciar sesión.");

            if (string.IsNullOrEmpty(contrasena))
                throw new ArgumentException("La contraseña es obligatoria.");

            DataTable dtUsuario = oUsuarioD.MtAutenticarUsuario(correo.Trim(), contrasena);

            if (dtUsuario == null || dtUsuario.Rows.Count == 0)
                throw new ArgumentException("Correo electrónico o contraseña incorrectos. Intente nuevamente.");

            return dtUsuario;
        }
    }
}