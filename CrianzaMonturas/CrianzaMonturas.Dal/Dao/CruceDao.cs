﻿using CrianzaMonturas.Dal.Modelo;
using System.Data;
using CrianzaMonturas.Dal.Controlador;
using System.Data.SqlClient;
using CrianzaMonturas.Dal.Utilidades;

namespace CrianzaMonturas.Dal.Dao
{
    public class CruceDao
    {
        public List<Cruce> CargarCruces(int tipoMontura)
        {
            List<Cruce> cruces = new();

            try
            {
                MasterConnection.Open();

                var query = "SELECT [TipoId_Masculino], [TipoId_Femenino], [TipoIdResultado] " +
                    "FROM [dbo].[CruceMontura] WHERE [TipoMonturaId] = @TipoMontura;";

                using (var cmd = new SqlCommand(query, MasterConnection.connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@TipoMontura", tipoMontura);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cruce cruce = new()
                            {
                                Tipo1 = reader.GetInt32("TipoId_Masculino"),
                                Tipo2 = reader.GetInt32("TipoId_Femenino"),
                                TipoResultado = reader.GetInt32("TipoIdResultado")
                            };

                            cruces.Add(cruce);
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

            return cruces;
        }
    }
}
