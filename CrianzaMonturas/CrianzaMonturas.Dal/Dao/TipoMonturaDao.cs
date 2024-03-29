﻿using CrianzaMonturas.Dal.Controlador;
using CrianzaMonturas.Dal.Modelo;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using CrianzaMonturas.Dal.Utilidades;

namespace CrianzaMonturas.Dal.Dao
{
    public class TipoMonturaDao
    {
        public List<TipoMontura> CargarTipoMonturas()
        {
            List<TipoMontura> tipoMonturas = new();

            try
            {
                MasterConnection.Open();

                var query = "SELECT [Id], [Nombre] FROM [dbo].[TipoMontura];";

                using (var cmd = new SqlCommand(query, MasterConnection.connection))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TipoMontura tipoMontura = new()
                            {
                                Id = reader.GetInt32("Id"),
                                Nombre = reader.GetString("Nombre")
                            };

                            tipoMonturas.Add(tipoMontura);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog($"|{ex.Message} - {ex.StackTrace}|");
            }
            finally
            {
                MasterConnection.Close();
            }

            return tipoMonturas;
        }
    }
}
