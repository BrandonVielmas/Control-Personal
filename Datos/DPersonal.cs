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
    public class DPersonal
    { 
        public bool insertarPersonal(LPersonal parametros)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlCommand cmd = new SqlCommand("insertarPersonal",ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                cmd.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
                cmd.Parameters.AddWithValue("@Pais", parametros.Pais);
                cmd.Parameters.AddWithValue("@IdCargo", parametros.IdCargo);
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
        public bool editarPersonal(LPersonal parametros)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlCommand cmd = new SqlCommand("editarPersonal", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdPersonal", parametros.IdPersonal);
                cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                cmd.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
                cmd.Parameters.AddWithValue("@Pais", parametros.Pais);
                cmd.Parameters.AddWithValue("@IdCargo", parametros.IdCargo);
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
        public bool eliminarPersonal(LPersonal parametros)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlCommand cmd = new SqlCommand("eliminarPersonal", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPersonal", parametros.IdPersonal);
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
        public bool mostrarPersonal(ref DataTable dt,int Desde, int Hasta)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrarPersonal",ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Desde", Desde);
                da.SelectCommand.Parameters.AddWithValue("@Hasta", Hasta);
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
        public bool buscarPersonal(ref DataTable dt, int Desde, int Hasta,string Buscador)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarPersonal", ConexionMaestra.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Desde", Desde);
                da.SelectCommand.Parameters.AddWithValue("@Hasta", Hasta);
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
        public bool restaurarrPersonal(LPersonal parametros)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlCommand cmd = new SqlCommand("restaurarPersonal", ConexionMaestra.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPersonal", parametros.IdPersonal);
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
        public void contarPersonal(ref int contador)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlCommand cmd = new SqlCommand("select COUNT(IdPersonal) from Personal",ConexionMaestra.conectar);
                contador = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                contador = 0;
            }
            finally{
                ConexionMaestra.Cerrar();
            }
        }
        public bool buscarPersonalIdentidad(ref DataTable dt, string Buscador)
        {
            try
            {
                ConexionMaestra.Abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarPersonalIdentidad", ConexionMaestra.conectar);
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
