using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace HaciendoReportes.Datos
{
    public class ConexionMaestra
    {
        public static string conexion = "Data Source = DESKTOP-1LA75V6; Initial Catalog = BAVTCORP; Integrated Security = true";
        public static SqlConnection conectar = new SqlConnection(conexion);

        public static void Abrir()
        {
            if(conectar.State == ConnectionState.Closed)
            {
                conectar.Open();
            }
        }

        public static void Cerrar()
        {
            if (conectar.State == ConnectionState.Open)
            {
                conectar.Close();
            }
        }
    }
}
