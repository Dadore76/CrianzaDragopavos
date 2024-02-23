﻿using CrianzaMonturas.Dal.Modelo;
using System.Data;
using CrianzaMonturas.Dal.Controlador;
using System.Data.SqlClient;

namespace CrianzaMonturas.Dal.Dao
{
    public static class TipoDao
    {
        public static List<Tipo> CargarTipos(int tipoMontura)
        {
            List<Tipo> tipos = new();

            try
            {
                MasterConnection.Open();

                var query = "SELECT [Id], [Alias], [Nombre], [Imagen], [Sigla], [Generacion] " +
                    "FROM [dbo].[Tipo] WHERE [TipoMonturaId] = @TipoMontura;";

                using (SqlCommand cmd = new(query, MasterConnection.connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@TipoMontura", tipoMontura);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tipo tipo = new()
                            {
                                Id = reader.GetInt32("Id"),
                                Alias = reader.GetValue("Alias") != DBNull.Value ? reader.GetString("Alias") : string.Empty,
                                Nombre = reader.GetValue("Nombre") != DBNull.Value ? reader.GetString("Nombre") : string.Empty,
                                Imagen = reader.GetValue("Imagen") != DBNull.Value ? (Byte[])reader.GetValue("Imagen") : Array.Empty<byte>(),
                                Sigla = reader.GetValue("Sigla") != DBNull.Value ? reader.GetString("Sigla") : string.Empty,
                                Generacion = reader.GetValue("Generacion") != DBNull.Value ? reader.GetInt32("Generacion") : 0
                            };

                            tipos.Add(tipo);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                MasterConnection.Close();
            }

            return tipos;
        }
    }
}
