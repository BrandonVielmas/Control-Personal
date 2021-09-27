using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using HaciendoReportes.Logica;
using System.Windows.Forms;

namespace HaciendoReportes.Datos
{
    public class DPermisos
    {
        public bool insertarPermisos(LPermisos parametros)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlCommand cmd = new SqlCommand("insertarPermisos", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdModulo", parametros.IdModulo);
                cmd.Parameters.AddWithValue("@IdUsuarios", parametros.IdUsuarios);
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
        public void mostrarPermisos(ref DataTable dt, LPermisos parametos)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarPermisos", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IdUsuarios", parametos.IdUsuarios);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                ConexionMaestra.Cerrar();
            }

        }
        public bool eliminarPersonal(LPermisos parametros)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlCommand cmd = new SqlCommand("eliminarPermisos", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", parametros.IdUsuarios);
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
    }
}
