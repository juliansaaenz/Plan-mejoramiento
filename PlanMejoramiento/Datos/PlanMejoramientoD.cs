using PlanMejoramiento.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PlanMejoramiento.Datos
{
    public class PlanMejoramientoD
    {
        public int MtCrearPlanMejoramiento(PlanesMejoramiento oPlanesMejoramiento)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                string consulta = "INSERT INTO PlanMejoramiento (TipoPlan, FechaAsignacion, FechaLimite, Observaciones, EstadoPlan, IdAprendiz, IdInstructor, IdPlanOrigen) " +
                                   "VALUES (@TipoPlan, GETDATE(), @FechaLimite, @Observaciones, @EstadoPlan, @IdAprendiz, @IdInstructor, @IdPlanOrigen); " +
                                   "SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@TipoPlan", oPlanesMejoramiento.TipoPlan);
                    cmd.Parameters.AddWithValue("@FechaLimite", oPlanesMejoramiento.FechaLimite);
                    cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrWhiteSpace(oPlanesMejoramiento.Observaciones) ? (object)DBNull.Value : oPlanesMejoramiento.Observaciones);
                    cmd.Parameters.AddWithValue("@EstadoPlan", oPlanesMejoramiento.EstadoPlan ?? "En Proceso");
                    cmd.Parameters.AddWithValue("@IdAprendiz", oPlanesMejoramiento.IdAprendiz);
                    cmd.Parameters.AddWithValue("@IdInstructor", oPlanesMejoramiento.IdInstructor);
                    cmd.Parameters.AddWithValue("@IdPlanOrigen", oPlanesMejoramiento.IdPlanOrigen.HasValue ? (object)oPlanesMejoramiento.IdPlanOrigen.Value : DBNull.Value);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public DataTable MtConsultarAprendicesPorInstructor(int idInstructor)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                string query = "SELECT a.Id AS IdAprendiz, u.NumeroDocumento, u.Nombres, u.Apellidos, u.Correo, f.Id AS IdFicha, a.Estado " +
                               "FROM Aprendiz a " +
                               "INNER JOIN Usuarios u ON a.IdUsuario = u.Id " +
                               "INNER JOIN Ficha f ON a.IdFicha = f.Id " +
                               "INNER JOIN FichaInstructor fi ON f.Id = fi.IdFicha " +
                               "WHERE fi.IdInstructor = @IdInstructor";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdInstructor", idInstructor);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        tabla.Load(reader);
                    }
                }
            }
            return tabla;
        }
        public DataTable MtConsultarResultadosPendientes(int idAprendiz)
        {
            DataTable tabla = new DataTable();
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string query = "SELECT Id AS IdResultado, Codigo, Descripcion, Estado FROM ResultadoAprendizaje WHERE IdAprendiz = @IdAprendiz AND Estado = 'Pendiente'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdAprendiz", idAprendiz);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        tabla.Load(reader);
                    }
                }
            }
            return tabla;
        }

        public bool MtAsignarDetallesInstructor(int idPlan, DateTime fechaLimite, string observacionesEstructuradas)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string query = "UPDATE PlanMejoramiento SET FechaLimite = @FechaLimite, Observaciones = @Observaciones WHERE Id = @IdPlan";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdPlan", idPlan);
                    cmd.Parameters.AddWithValue("@FechaLimite", fechaLimite);
                    cmd.Parameters.AddWithValue("@Observaciones", observacionesEstructuradas);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
        }
    }
}