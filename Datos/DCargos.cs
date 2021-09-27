using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaciendoReportes.Logica;
using HaciendoReportes.Datos;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace HaciendoReportes.Datos
{
    public class DCargos
    {
        public bool insertarCargo(LCargo parametros)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlCommand cmd = new SqlCommand("insertarCargo", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cargo", parametros.Cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.Cerrar();
            }

        }
        public bool editarCargo(LCargo parametros)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlCommand cmd = new SqlCommand("editarCargo", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCargo", parametros.IdCargo);
                cmd.Parameters.AddWithValue("@Cargo", parametros.Cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                ConexionMaestra.Cerrar();
            }

        }
        public bool buscarCargo(ref DataTable dt, string Buscador)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarCargo", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Buscador", Buscador);
                da.Fill(dt);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                ConexionMaestra.Cerrar();
            }

        }
    }
}
