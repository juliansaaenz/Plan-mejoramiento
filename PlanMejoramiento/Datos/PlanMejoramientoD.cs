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

                    string consultaActividad = "INSERT INTO ActividadesPlan (DescripcionActividad, EstadoActividad, IdPlan) " +
                                            "VALUES (@Descripcion, 'Pendiente', @IdPlan);";

                    foreach (string actividad in listaActividades)
                    {
                        using (SqlCommand cmdActividad = new SqlCommand(consultaActividad, con, transaccion))
                        {
                            cmdActividad.Parameters.AddWithValue("@Descripcion", actividad);
                            cmdActividad.Parameters.AddWithValue("@IdPlan", idPlanGenerado);

                            cmdActividad.ExecuteNonQuery();
                        }
                    }

                    string consultaResultado = "INSERT INTO DetallePlanResultados (IdPlan, IdResultado) " +
                                            "VALUES (@IdPlan, @IdResultado);";

                    foreach (int idResultado in listaIdResultados)
                    {
                        using (SqlCommand cmdResultado = new SqlCommand(consultaResultado, con, transaccion))
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

                string consulta = "SELECT a.Id AS IdAprendiz, u.NumeroDocumento, u.Nombres, u.Apellidos, u.Correo, f.Id AS IdFicha, f.NumeroFicha, a.Estado " +
                               "FROM Aprendiz a " +
                               "INNER JOIN Usuarios u ON a.IdUsuario = u.Id " +
                               "INNER JOIN Ficha f ON a.IdFicha = f.Id " +
                               "INNER JOIN AsignacionInstructorFicha fi ON f.Id = fi.IdFicha " +
                               "WHERE fi.IdInstructor = @IdInstructor";

                using (SqlCommand cmd = new SqlCommand(consulta, con))
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
                string consulta = "UPDATE PlanesMejoramiento SET FechaLimite = @FechaLimite, Observaciones = @Observaciones WHERE Id = @IdPlan";

                using (SqlCommand cmd = new SqlCommand(consulta, con))
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
            DataTable tabla = new DataTable();
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string query = "SELECT ra.Id AS IdResultado, ra.CodigoRAP, ra.Descripcion, c.Denominacion AS Competencia " +
                               "FROM ResultadoAprendizaje ra " +
                               "INNER JOIN Competencia c ON ra.IdCompetencia = c.Id " +
                               "INNER JOIN Programa p ON c.IdPrograma = p.Id " +
                               "INNER JOIN Ficha f ON p.Id = f.IdPrograma " +
                               "INNER JOIN Aprendiz a ON f.Id = a.IdFicha " +
                               "WHERE a.Id = @IdAprendiz";

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
        public DataTable MtConsultarEvidenciasPorPlan(int idPlan)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                string query = "SELECT e.Id AS IdEvidencia, e.RutaArchivo, e.TipoArchivo, e.FechaSubida, " +
                               "e.ObservacionesInstructor, ap.DescripcionActividad, ap.Id AS IdActividad " +
                               "FROM Evidencias e " +
                               "INNER JOIN ActividadesPlan ap ON e.IdActividad = ap.Id " +
                               "WHERE ap.IdPlan = @IdPlan";

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
        public bool MtRegistrarObservacionInstructorEvidencia(int idEvidencia, string observaciones)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                string query = "UPDATE Evidencias SET ObservacionesInstructor = @Observaciones WHERE Id = @IdEvidencia";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@IdEvidencia", idEvidencia);
                    cmd.Parameters.AddWithValue("@Observaciones", observaciones);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
        public bool MtInsertarAprendicesMasivo(DataTable tablaAprendices)
        {
            bool exito = false;

            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                using (SqlTransaction transaccion = con.BeginTransaction())
                {
                    foreach (DataRow fila in tablaAprendices.Rows)
                    {
                        string consultaUsuario = "INSERT INTO Usuarios (TipoDocumento, NumeroDocumento, Nombres, Apellidos, Correo, Telefono, Contrasena, IdRol, IdCentroFormacion) " +
                                             "VALUES (@TipoDoc, @NumDoc, @Nombres, @Apellidos, @Correo, @Telefono, @Contrasena, @IdRol, @IdCentro); " +
                                             "SELECT SCOPE_IDENTITY();";

                        int idUsuarioGenerado = 0;

                        using (SqlCommand cmdUser = new SqlCommand(consultaUsuario, con, transaccion))
                        {
                            cmdUser.Parameters.AddWithValue("@TipoDoc", fila["TipoDocumento"].ToString());
                            cmdUser.Parameters.AddWithValue("@NumDoc", fila["NumeroDocumento"].ToString());
                            cmdUser.Parameters.AddWithValue("@Nombres", fila["Nombres"].ToString());
                            cmdUser.Parameters.AddWithValue("@Apellidos", fila["Apellidos"].ToString());
                            cmdUser.Parameters.AddWithValue("@Correo", fila["Correo"].ToString());
                            cmdUser.Parameters.AddWithValue("@Telefono", fila["Telefono"].ToString() ?? (object)DBNull.Value);
                            cmdUser.Parameters.AddWithValue("@Contrasena", fila["NumeroDocumento"].ToString());
                            cmdUser.Parameters.AddWithValue("@IdRol", 3); 
                            cmdUser.Parameters.AddWithValue("@IdCentro", Convert.ToInt32(fila["IdCentroFormacion"]));

                            idUsuarioGenerado = Convert.ToInt32(cmdUser.ExecuteScalar());
                        }

                        string consultaAprendiz = "INSERT INTO Aprendiz (Estado, ImagenUrl, IdFicha, IdUsuario, IdCiudadResidencia) " +
                                               "VALUES ('En Formacion', NULL, @IdFicha, @IdUsuario, @IdCiudad)";

                        using (SqlCommand cmdAprendiz = new SqlCommand(consultaAprendiz, con, transaccion))
                        {
                            cmdAprendiz.Parameters.AddWithValue("@IdFicha", Convert.ToInt32(fila["IdFicha"]));
                            cmdAprendiz.Parameters.AddWithValue("@IdUsuario", idUsuarioGenerado);
                            cmdAprendiz.Parameters.AddWithValue("@IdCiudad", Convert.ToInt32(fila["IdCiudadResidencia"]));

                            cmdAprendiz.ExecuteNonQuery();
                        }
                    }
                    transaccion.Commit();
                    exito = true;
                }
            }
            return exito;
        }
        public bool MtExisteDocumento(string documento)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string query = "SELECT COUNT(1) FROM Usuarios WHERE NumeroDocumento = @Doc";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Doc", documento);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public bool MtExisteCorreo(string correo)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string query = "SELECT COUNT(1) FROM Usuarios WHERE Correo = @Correo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }
    }
}