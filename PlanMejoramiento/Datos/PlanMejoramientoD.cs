using PlanMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlanMejoramiento.Datos
{
    public class PlanMejoramientoD
    {
        public bool MtCrearPlanMejoramientoCompleto(PlanesMejoramiento oPlanesMejoramiento, List<string> listaActividades, List<int> listaIdResultados)
        {
            bool exito = false;

            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                using (SqlTransaction transaccion = con.BeginTransaction())
                {
                    string consulta = "INSERT INTO PlanesMejoramiento (TipoPlan, FechaAsignacion, FechaLimite, Observaciones, EstadoPlan, IdAprendiz, IdInstructor, IdPlanOrigen) " +
                                       "VALUES (@TipoPlan, GETDATE(), @FechaLimite, @Observaciones, @EstadoPlan, @IdAprendiz, @IdInstructor, @IdPlanOrigen); " +
                                       "SELECT SCOPE_IDENTITY();";

                    int idPlanGenerado = 0;

                    using (SqlCommand cmd = new SqlCommand(consulta, con, transaccion))
                    {
                        cmd.Parameters.AddWithValue("@TipoPlan", oPlanesMejoramiento.TipoPlan);
                        cmd.Parameters.AddWithValue("@FechaLimite", oPlanesMejoramiento.FechaLimite);
                        cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrWhiteSpace(oPlanesMejoramiento.Observaciones) ? (object)DBNull.Value : oPlanesMejoramiento.Observaciones);
                        cmd.Parameters.AddWithValue("@EstadoPlan", oPlanesMejoramiento.EstadoPlan ?? "Asignado");
                        cmd.Parameters.AddWithValue("@IdAprendiz", oPlanesMejoramiento.IdAprendiz);
                        cmd.Parameters.AddWithValue("@IdInstructor", oPlanesMejoramiento.IdInstructor);
                        cmd.Parameters.AddWithValue("@IdPlanOrigen", oPlanesMejoramiento.IdPlanOrigen.HasValue ? (object)oPlanesMejoramiento.IdPlanOrigen.Value : DBNull.Value);

                        idPlanGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    string queryActividad = "INSERT INTO ActividadesPlan (DescripcionActividad, EstadoActividad, IdPlan) " +
                                            "VALUES (@Descripcion, 'Pendiente', @IdPlan);";

                    foreach (string actividad in listaActividades)
                    {
                        using (SqlCommand cmdActividad = new SqlCommand(queryActividad, con, transaccion))
                        {
                            cmdActividad.Parameters.AddWithValue("@Descripcion", actividad);
                            cmdActividad.Parameters.AddWithValue("@IdPlan", idPlanGenerado);

                            cmdActividad.ExecuteNonQuery();
                        }
                    }

                    string queryResultado = "INSERT INTO DetallePlanResultados (IdPlan, IdResultado) " +
                                            "VALUES (@IdPlan, @IdResultado);";

                    foreach (int idResultado in listaIdResultados)
                    {
                        using (SqlCommand cmdResultado = new SqlCommand(queryResultado, con, transaccion))
                        {
                            cmdResultado.Parameters.AddWithValue("@IdPlan", idPlanGenerado);
                            cmdResultado.Parameters.AddWithValue("@IdResultado", idResultado);

                            cmdResultado.ExecuteNonQuery();
                        }
                    }

                    transaccion.Commit();
                    exito = true;
                }
            }

            return exito;
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
        public DataTable MtConsultarDatosAprendiz(int idAprendiz)
        {
            DataTable dtAprendiz = new DataTable();
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "SELECT u.Id AS IdUsuario, u.TipoDocumento, u.NumeroDocumento, " +
                                  "u.Nombres, u.Apellidos, u.Correo, u.Telefono, " +
                                  "a.Id AS IdAprendiz, a.IdFicha, a.Estado " +
                                  "FROM Aprendiz a " +
                                  "INNER JOIN Usuario u ON a.IdUsuario = u.Id " +
                                  "WHERE a.Id = @IdAprendiz";
                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@IdAprendiz", idAprendiz);
                    using (SqlDataReader drTabla = cmd.ExecuteReader())
                    {
                        dtAprendiz.Load(drTabla);
                    }
                }
            }
            return dtAprendiz;
        }
        public DataTable MtConsultarFichaAsignada(int idAprendiz)
        {
            DataTable dtFicha = new DataTable();
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "SELECT f.Id AS IdFIcha, f.Codigo AS CodigoFicha, f.NombrePrograma, f.Jornada " +
                                  "FROM Aprendiz a " +
                                  "INNER JOIN Ficha f ON a.IdFicha = f.Id" +
                                  "WHERE a.Id = @IdAprendiz";
                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@IdAprendiz", idAprendiz);
                    using (SqlDataReader drTabla = cmd.ExecuteReader())
                    {
                        dtFicha.Load(drTabla);
                    }
                }
            }
            return dtFicha;
        }

        public DataTable MtConsultarResultadosPendientes(int idAprendiz)
        {
            DataTable dtResultados = new DataTable();
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "SELECT Id AS IdResultado, Codigo AS CodigoResultado, Descripcion AS Resultado, Estado " +
                                  "FROM ResultadoAprendizaje " +
                                  "WHERE IdAprendiz = @IdAprendiz AND Estado ='Pendiente'";
                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@IdAprendiz", idAprendiz);
                    using (SqlDataReader drTabla = cmd.ExecuteReader())
                    {
                        dtResultados.Load(drTabla);
                    }
                }
            }
            return dtResultados;
        }
        public DataTable MtAprendizConsultarPlanesAsignados(int idAprendiz)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                string query = "SELECT p.Id AS IdPlan, p.TipoPlan, p.FechaAsignacion, p.FechaLimite, p.EstadoPlan, " +
                               "u.Nombres + ' ' + u.Apellidos AS NombreInstructor, p.IdPlanOrigen " +
                               "FROM PlanesMejoramiento p " +
                               "INNER JOIN Instructor i ON p.IdInstructor = i.Id " +
                               "INNER JOIN Usuarios u ON i.IdUsuario = u.Id " +
                               "WHERE p.IdAprendiz = @IdAprendiz";

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
        public DataTable MtAprendizConsultarObservacionesPlan(int idPlan)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                string query = "SELECT p.Id AS IdPlan, p.Observaciones AS ObservacionInicial, " +
                               "e.ObservacionesInstructor AS ObservacionEvidencia, " +
                               "ev.EvalProducto, ev.EvalConocimiento, ev.EvalDesempeño " +
                               "FROM PlanesMejoramiento p " +
                               "LEFT JOIN ActividadesPlan ap ON p.Id = ap.IdPlan " +
                               "LEFT JOIN Evidencias e ON ap.Id = e.IdActividad " +
                               "LEFT JOIN Evaluaciones ev ON p.Id = ev.IdPlan " +
                               "WHERE p.Id = @IdPlan";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdPlan", idPlan);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        tabla.Load(reader);
                    }
                }
            }
            return tabla;
        }
        public string MtAprendizConsultarEstadoAcademico(int idAprendiz)
        {
            string estadoAcademico = "Desconocido";

            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                string query = "SELECT Estado FROM Aprendiz WHERE Id = @IdAprendiz";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdAprendiz", idAprendiz);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        estadoAcademico = resultado.ToString();
                    }
                }
            }
            return estadoAcademico;
        }
        public bool MtAprendizInsertarEvidencia(string rutaArchivo, string tipoArchivo, int idActividad)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                string query = "INSERT INTO Evidencias (RutaArchivo, TipoArchivo, IdActividad, ObservacionesInstructor) " +
                               "VALUES (@RutaArchivo, @TipoArchivo, @IdActividad, null)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RutaArchivo", rutaArchivo);
                    cmd.Parameters.AddWithValue("@TipoArchivo", tipoArchivo);
                    cmd.Parameters.AddWithValue("@IdActividad", idActividad);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}