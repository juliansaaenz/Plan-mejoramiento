using PlanMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlanMejoramiento.Datos
{
    public class FichaD
    {
        public bool MtCrearFicha(Ficha oFicha)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "INSERT INTO Ficha (NumeroFicha, Jornada, FechaInicio, FechaFinalizacion, Descripcion, Estado, IdPrograma, IdCentroFormacion) " +
                               "VALUES (@NumeroFicha, @Jornada, @FechaInicio, @FechaFinalizacion, @Descripcion, @Estado, @IdPrograma, @IdCentroFormacion)";
                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@NumeroFicha", oFicha.NumeroFicha);
                    cmd.Parameters.AddWithValue("@Jornada", oFicha.Estado);
                    cmd.Parameters.AddWithValue("@FechaInicio", oFicha.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", oFicha.FechaFinalizacion);
                    cmd.Parameters.AddWithValue("@Descripcion", oFicha.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", oFicha.Estado);
                    cmd.Parameters.AddWithValue("@IdPrograma", oFicha.IdPrograma);
                    cmd.Parameters.AddWithValue("@IdCentroFormacion", oFicha.IdCentroFormacion);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
        public List<Ficha> MtListarFicha()
        {
            List<Ficha> lstFicha = new List<Ficha>();
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "SELECT * FROM Ficha";
                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Ficha ficha = new Ficha()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                NumeroFicha = dr["NumeroFicha"].ToString(),
                                FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                FechaFinalizacion = Convert.ToDateTime(dr["FechaFinalizacion"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = dr["Estado"].ToString(),
                                IdPrograma = Convert.ToInt32(dr["IdPrograma"]),
                                IdCentroFormacion = Convert.ToInt32(dr["IdCentroFormacion"])
                            };
                            lstFicha.Add(ficha);
                        }
                    }
                }
            }
            return lstFicha;
        }
        public bool MtModificarFicha(Ficha oFicha)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "UPDATE Ficha SET NumeroFicha=@NumeroFicha, FechaInicio=@FechaInicio, FechaFinalizacion=@FechaFinalizacion, " +
                                  "Descripcion=@Descripcion, Estado=@Estado, IdPrograma=@IdPrograma, IdCentroFormacion=IdCentroFormacion" +
                                  "WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@Id", oFicha.Id);
                    cmd.Parameters.AddWithValue("@NumeroFicha", oFicha.NumeroFicha);
                    cmd.Parameters.AddWithValue("@Jornada", oFicha.Estado);
                    cmd.Parameters.AddWithValue("@FechaInicio", oFicha.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", oFicha.FechaFinalizacion);
                    cmd.Parameters.AddWithValue("@Descripcion", oFicha.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", oFicha.Estado);
                    cmd.Parameters.AddWithValue("@IdPrograma", oFicha.IdPrograma);
                    cmd.Parameters.AddWithValue("@IdCentroFormacion", oFicha.IdCentroFormacion);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
        public bool MtEliminarFicha(int idFicha)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "DELETE FROM Ficha WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@Id", idFicha);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}