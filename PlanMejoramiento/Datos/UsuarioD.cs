using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Datos
{
    public class UsuarioD
    {
        public DataTable MtAutenticarUsuario(string correo, string contrasena)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "SELECT u.Id, u.NumeroDocumento, u.Nombres, u.Apellidos, u.Correo, u.IdRol, r.NombreRol, u.IdCentroFormacion " +
                               "FROM Usuarios u " +
                               "INNER JOIN Rol r ON u.IdRol = r.Id " +
                               "WHERE u.Correo = @Correo AND u.Contrasena = @Contrasena";

                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        tabla.Load(reader);
                    }
                }
            }
            return tabla;
        }
    }
}