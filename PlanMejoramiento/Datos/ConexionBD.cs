using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Datos
{
    public class ConexionBD
    {
        public static readonly string CadenaConexion = "Data Source=localhost;Initial Catalog=PlanMejoramiento;Integrated Security=True";

        public static SqlConnection MtAbrirConexion()
        {
            return new SqlConnection(CadenaConexion);
        }
    }
}