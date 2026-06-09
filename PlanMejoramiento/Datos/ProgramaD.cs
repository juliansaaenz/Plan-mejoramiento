using PlanMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PlanMejoramiento.Datos
{
    public class ProgramaD
    {
        public bool MtCrearPrograma(Programa oprograma)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "INSERT INTO Programa (CodigoPrograma, NombrePrograma, Version, NivelFormacion, Duracion, Estado) " +
                               "VALUES (@CodigoPrograma, @NombrePrograma, @Version, @NivelFormacion, @Duracion, @Estado)";

                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@CodigoPrograma", oprograma.CodigoPrograma);
                    cmd.Parameters.AddWithValue("@NombrePrograma", oprograma.NombrePrograma);
                    cmd.Parameters.AddWithValue("@Version", oprograma.Version);
                    cmd.Parameters.AddWithValue("@NivelFormacion", oprograma.NivelFormacion);
                    cmd.Parameters.AddWithValue("@Duracion", oprograma.Duracion);
                    cmd.Parameters.AddWithValue("@Estado", oprograma.Estado);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public bool MtModificarPrograma(Programa oprograma)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "UPDATE Programa SET CodigoPrograma = @CodigoPrograma, NombrePrograma = @NombrePrograma, Version = @Version, " +
                               "NivelFormacion = @NivelFormacion, Duracion = @Duracion, Estado = @Estado " +
                               "WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@Id", oprograma.Id);
                    cmd.Parameters.AddWithValue("@CodigoPrograma", oprograma.CodigoPrograma);
                    cmd.Parameters.AddWithValue("@NombrePrograma", oprograma.NombrePrograma);
                    cmd.Parameters.AddWithValue("@Version", oprograma.Version);
                    cmd.Parameters.AddWithValue("@NivelFormacion", oprograma.NivelFormacion);
                    cmd.Parameters.AddWithValue("@Duracion", oprograma.Duracion);
                    cmd.Parameters.AddWithValue("@Estado", oprograma.Estado);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public List<Programa> MtListarProgramas()
        {
            List<Programa> programas = new List<Programa>();
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "SELECT Id, CodigoPrograma, NombrePrograma, Version, NivelFormacion, Duracion, Estado FROM Programa";

                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Programa programa = new Programa
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                CodigoPrograma = reader["CodigoPrograma"].ToString(),
                                NombrePrograma = reader["NombrePrograma"].ToString(),
                                Version = reader["Version"].ToString(),
                                NivelFormacion = reader["NivelFormacion"].ToString(),
                                Duracion = Convert.ToInt32(reader["Duracion"]),
                                Estado = reader["Estado"].ToString()
                            };
                            programas.Add(programa);
                        }
                    }
                }
            }
            return programas;
        }

        public bool MtEliminarPrograma(int idPrograma)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "DELETE FROM Programa WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@Id", idPrograma);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
        public bool MtAsociarProgramaCentro(int idCentro, int idPrograma)
        {
            using (SqlConnection con = ConexionBD.MtAbrirConexion())
            {
                con.Open();
                string consulta = "INSERT INTO CentroPrograma (IdCentroFormacion, IdPrograma) VALUES (@IdCentro, @IdPrograma)";

                using (SqlCommand cmd = new SqlCommand(consulta, con))
                {
                    cmd.Parameters.AddWithValue("@IdCentro", idCentro);
                    cmd.Parameters.AddWithValue("@IdPrograma", idPrograma);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;

                }
            }
        }
    }
}