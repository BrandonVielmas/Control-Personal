using HaciendoReportes.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaciendoReportes.Datos
{
    public class DUsuarios
    {
        public bool insertarUsuarios(LUsuarios parametros)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlCommand cmd = new SqlCommand("insertarUsuario",ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombre);
                cmd.Parameters.AddWithValue("@Login", parametros.Login);
                cmd.Parameters.AddWithValue("Password", parametros.Password);
                cmd.Parameters.AddWithValue("Icono", parametros.Icono);
                cmd.Parameters.AddWithValue("Estado","Activo");
                cmd.ExecuteNonQuery();
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
        public void mostrarUsuarios(ref DataTable dt)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlDataAdapter da = new SqlDataAdapter("select * from Usuarios", ConexionMaestra.conectar);
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
    }
}
