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
    public class DAsistencia
    {
        public bool buscarAsistencaId(ref DataTable dt, int IdPersonal)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarAsistenciaId", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IdPersonal", IdPersonal);
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
        public bool insertarAsistencia(LAsistencia parametros)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlCommand cmd = new SqlCommand("insertarAsistencia", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPersonal", parametros.IdPersonal);
                cmd.Parameters.AddWithValue("@FechaEntrada", parametros.FechaEntrada);
                cmd.Parameters.AddWithValue("@FechaSalida", parametros.FechaSalida);
                cmd.Parameters.AddWithValue("@Estado", parametros.Estado);
                cmd.Parameters.AddWithValue("@Horas", parametros.Horas);
                cmd.Parameters.AddWithValue("@Observacion", parametros.Observacion);
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
        public bool confirmarSalida(LAsistencia parametros)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlCommand cmd = new SqlCommand("confirmarSalida", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPersonal", parametros.IdPersonal);
                cmd.Parameters.AddWithValue("@FechaSalida", parametros.FechaSalida);            
                cmd.Parameters.AddWithValue("@Horas", parametros.Horas);
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
    }
}
