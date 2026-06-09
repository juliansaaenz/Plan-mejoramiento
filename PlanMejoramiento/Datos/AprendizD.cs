using PlanMejoramiento.Logica;
using PlanMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace PlanMejoramiento.Datos
{
    public class AprendizD
    {
        public bool MtRegistrarAprendiz(Usuario oUsuario, Aprendiz oAprendiz)
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

                    string queryAprendiz = "INSERT INTO Aprendiz (IdFicha, Estado, IdUsuario) VALUES (@IdFicha, @Estado, @IdUsuario)";
                    int filasAprendiz = 0;

                    using (SqlCommand cmdAprendiz = new SqlCommand(queryAprendiz, con, transaccion))
                    {
                        cmdAprendiz.Parameters.AddWithValue("@IdFicha", oAprendiz.IdFicha);
                        cmdAprendiz.Parameters.AddWithValue("@Estado", oAprendiz.Estado);
                        cmdAprendiz.Parameters.AddWithValue("@IdUsuario", idUsuarioGenerado);

                        filasAprendiz = cmdAprendiz.ExecuteNonQuery();
                    }

                    if (idUsuarioGenerado > 0 && filasAprendiz > 0)
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

        public bool MtModificarAprendiz(Usuario oUsuario, Aprendiz oAprendiz)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();

                using (SqlTransaction transaccion = con.BeginTransaction())
                {
                    // Actualizar Datos Personales en Tabla Usuarios
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

                    string queryAprendiz = "UPDATE Aprendiz SET IdFicha = @IdFicha WHERE Id = @IdAprendiz";

                    int filasResultante = 0;
                    using (SqlCommand cmdAprendiz = new SqlCommand(queryAprendiz, con, transaccion))
                    {
                        cmdAprendiz.Parameters.AddWithValue("@IdAprendiz", oAprendiz.Id);
                        cmdAprendiz.Parameters.AddWithValue("@IdFicha", oAprendiz.IdFicha);

                        filasResultante = cmdAprendiz.ExecuteNonQuery();
                    }

                    if (filasUsuario > 0 && filasResultante > 0)
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

        public bool MtEliminarAprendiz(int idAprendiz)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string query = "UPDATE Aprendiz SET Estado = 'Inactivo' WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", idAprendiz);
                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
        }

        public bool MtCambiarEstadoAcademico(int idAprendiz, string nuevoEstado)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string query = "UPDATE Aprendiz SET Estado = @Estado WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", idAprendiz);
                    cmd.Parameters.AddWithValue("@Estado", nuevoEstado);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
        }

        public bool MtAsociarFicha(int idAprendiz, int idNuevaFicha)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string query = "UPDATE Aprendiz SET IdFicha = @IdFicha WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", idAprendiz);
                    cmd.Parameters.AddWithValue("@IdFicha", idNuevaFicha);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
        }
    }
}
