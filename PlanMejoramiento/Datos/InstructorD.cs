using PlanMejoramiento.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Datos
{
    public class InstructorD
    {
        public bool MtRegistrarInstructor(Usuario oUsuario, Instructor oInstructor)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                using (SqlTransaction transaccion = con.BeginTransaction())
                {
                    string consultaUsuario = "INSERT INTO Usuarios (TipoDocumento, NumeroDocumento, Nombres, Apellidos, Correo, Telefono, Contrasena, IdRol, IdCentroFormacion) " +
                                          "VALUES (@TipoDocumento, @NumeroDocumento, @Nombres, @Apellidos, @Correo, @Telefono, @Contrasena, @IdRol, @IdCentroFormacion); " +
                                          "SELECT SCOPE_IDENTITY();";

                    int idUsuarioGenerado = 0;

                    using (SqlCommand cmdUsuario = new SqlCommand(consultaUsuario, con, transaccion))
                    {
                        cmdUsuario.Parameters.AddWithValue("@TipoDocumento", oUsuario.TipoDocumento);
                        cmdUsuario.Parameters.AddWithValue("@NumeroDocumento", oUsuario.NumeroDocumento);
                        cmdUsuario.Parameters.AddWithValue("@Nombres", oUsuario.Nombres);
                        cmdUsuario.Parameters.AddWithValue("@Apellidos", oUsuario.Apellidos);
                        cmdUsuario.Parameters.AddWithValue("@Correo", oUsuario.Correo);
                        cmdUsuario.Parameters.AddWithValue("@Telefono", oUsuario.Telefono ?? (object)DBNull.Value);
                        cmdUsuario.Parameters.AddWithValue("@Contrasena", oUsuario.Contrasena);
                        cmdUsuario.Parameters.AddWithValue("@IdRol", oUsuario.IdRol);
                        cmdUsuario.Parameters.AddWithValue("@IdCentroFormacion", oUsuario.IdCentroFormacion ?? (object)DBNull.Value);

                        idUsuarioGenerado = Convert.ToInt32(cmdUsuario.ExecuteScalar());
                    }

                    string consultaInstructor = "INSERT INTO Instructor (Especialidad, IdUsuario) VALUES (@Especialidad, @IdUsuario)";
                    int filasInstructor = 0;

                    using (SqlCommand cmdInstructor = new SqlCommand(consultaInstructor, con, transaccion))
                    {
                        cmdInstructor.Parameters.AddWithValue("@Especialidad", oInstructor.Especialidad);
                        cmdInstructor.Parameters.AddWithValue("@IdUsuario", idUsuarioGenerado);

                        filasInstructor = cmdInstructor.ExecuteNonQuery();
                    }

                    if (idUsuarioGenerado > 0 && filasInstructor > 0)
                    {
                        transaccion.Commit();
                        return true;
                    }
                    else
                    {
                        transaccion.Rollback();
                        return false;
                    }
                }
            }
        }
        public DataTable MtListarInstructores()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                string query = "SELECT i.Id AS IdInstructor, i.Especialidad, u.Id AS IdUsuario, " +
                               "u.TipoDocumento, u.NumeroDocumento, u.Nombres, u.Apellidos, u.Correo, u.Telefono " +
                               "FROM Instructor i " +
                               "INNER JOIN Usuarios u ON i.IdUsuario = u.Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        tabla.Load(reader);
                    }
                }
            }
            return tabla;
        }
        public bool MtModificarInstructor(Usuario oUsuario, Instructor oInstructor)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                using (SqlTransaction transaccion = con.BeginTransaction())
                {
                    string queryUsuario = "UPDATE Usuarios SET TipoDocumento = @TipoDocumento, NumeroDocumento = @NumeroDocumento, " +
                                          "Nombres = @Nombres, Apellidos = @Apellidos, Correo = @Correo, Telefono = @Telefono " +
                                          "WHERE Id = @IdUsuario";

                    int filasUsuario = 0;
                    using (SqlCommand cmdUsuario = new SqlCommand(queryUsuario, con, transaccion))
                    {
                        cmdUsuario.Parameters.AddWithValue("@IdUsuario", oUsuario.Id);
                        cmdUsuario.Parameters.AddWithValue("@TipoDocumento", oUsuario.TipoDocumento);
                        cmdUsuario.Parameters.AddWithValue("@NumeroDocumento", oUsuario.NumeroDocumento);
                        cmdUsuario.Parameters.AddWithValue("@Nombres", oUsuario.Nombres);
                        cmdUsuario.Parameters.AddWithValue("@Apellidos", oUsuario.Apellidos);
                        cmdUsuario.Parameters.AddWithValue("@Correo", oUsuario.Correo);
                        cmdUsuario.Parameters.AddWithValue("@Telefono", oUsuario.Telefono ?? (object)DBNull.Value);

                        filasUsuario = cmdUsuario.ExecuteNonQuery();
                    }

                    string queryInstructor = "UPDATE Instructor SET Especialidad = @Especialidad WHERE Id = @IdInstructor";

                    int filasInstructor = 0;
                    using (SqlCommand cmdInstructor = new SqlCommand(queryInstructor, con, transaccion))
                    {
                        cmdInstructor.Parameters.AddWithValue("@IdInstructor", oInstructor.Id);
                        cmdInstructor.Parameters.AddWithValue("@Especialidad", oInstructor.Especialidad);

                        filasInstructor = cmdInstructor.ExecuteNonQuery();
                    }

                    if (filasUsuario > 0 && filasInstructor > 0)
                    {
                        transaccion.Commit();
                        return true;
                    }
                    else
                    {
                        transaccion.Rollback();
                        return false;
                    }
                }
            }
        }
        public bool MtEliminarInstructor(int idUsuario)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "DELETE FROM Usuario WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@Id", idUsuario);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
        public bool MtAsociarInstructorFicha(int idInstructor, int idFicha)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "INSERT INTO FichaInstructor (IdFicha, IdInstructor) VALUES (@IdFicha, @IdInstructor)";

                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@IdFicha", idFicha);
                    cmd.Parameters.AddWithValue("@IdInstructor", idInstructor);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}