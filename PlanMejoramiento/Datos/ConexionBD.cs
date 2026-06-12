using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Datos
{
    public class ConexionBD
    {
        private static readonly string CadenaConexion = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public static SqlConnection MtAbrirConexion()
        {
            return new SqlConnection(CadenaConexion);
        }
    }
}