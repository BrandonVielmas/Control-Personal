using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaciendoReportes.Logica
{
    public class Bases
    {
        public static void DisenoDwv(ref DataGridView dwv)
        {
            dwv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dwv.BackgroundColor = Color.FromArgb(29, 29, 29);
            dwv.EnableHeadersVisualStyles = false;
            dwv.BorderStyle = BorderStyle.None;
            dwv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dwv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dwv.RowHeadersVisible = false;


            DataGridViewCellStyle cabecera = new DataGridViewCellStyle();
            cabecera.BackColor = Color.FromArgb(29, 29, 29);
            cabecera.ForeColor = Color.White;
            cabecera.Font = new Font("Microsoft YaHei UI", 12,FontStyle.Bold);
            dwv.ColumnHeadersDefaultCellStyle = cabecera;
        }

        public static object Decimales(TextBox txt, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '.') || (e.KeyChar == ','))
            {
                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }

            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if(char.IsControl(e.KeyChar))
            {
                 e.Handled = false;
            }else if (e.KeyChar == '.' && (~txt.Text.IndexOf(".")) != 0 )
            {
                e.Handled = true;
            }else if (e.KeyChar == '.'){
                e.Handled = false;
            }
            else if (e.KeyChar == ','){
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            return null;
        }

        public static void DisenoDvwEliminados(ref DataGridView dwv)
        {
            foreach (DataGridViewRow row in dwv.Rows)
            {
                string estado;
                estado = Convert.ToString(row.Cells["Estado"].Value);
                if(estado == "Baja")
                {
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 128, 128);

                }
            }
             
        }
        
        public  enum DateInterval
        {
            Day,
            DayOfYear,
            Hour,
            Minute,
            Month,
            Quareter,
            Second,
            WeekDay,
            WeekOfYear,
            Year
        }

        public static long DateDiff(DateInterval intervalType,DateTime dateOne,DateTime dateTwo)
        {
            switch (intervalType)
            {
                case DateInterval.Day:
                    TimeSpan spanForDays = dateTwo - dateOne;
                    return (long)spanForDays.TotalDays;
                case DateInterval.Hour:
                    TimeSpan spanForHours = dateTwo - dateOne;
                    return (long)spanForHours.TotalHours;
                case DateInterval.Minute:
                    TimeSpan spanForMinutues = dateTwo - dateOne;
                    return (long)spanForMinutues.TotalMinutes;
                case DateInterval.Month:
                    return ((dateTwo.Year - dateOne.Year) * 12) + (dateTwo.Month - dateOne.Month);
                case DateInterval.Quareter:
                    long dateOneQuarter = (long)Math.Ceiling(dateOne.Month / 3.0);
                    long dateTwoQuarter = (long)Math.Ceiling(dateTwo.Month / 3.0);
                    return (4 * (dateTwo.Year - dateOne.Year)) + dateTwoQuarter - dateOneQuarter;
                case DateInterval.Second:
                    TimeSpan spanForSeconds = dateTwo - dateOne;
                    return (long)spanForSeconds.TotalSeconds;
                case DateInterval.WeekDay:
                    DateTime dateOneModified = dateOne;
                    DateTime dateTwoModified = dateOne;
                    while(dateTwoModified.DayOfWeek != DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                    {
                        dateTwoModified = dateTwoModified.AddDays(-1);
                    }
                    while (dateOneModified.DayOfWeek != DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                    {
                        dateOneModified = dateOneModified.AddDays(-1);
                    }
                    TimeSpan spanForWeekOfYear = dateTwoModified - dateOneModified;
                    return (long)(spanForWeekOfYear.TotalDays / 7.0);
                case DateInterval.Year:
                    return dateTwo.Year - dateOne.Year;
                default:
                    return 0;
            }
        }
    }
}
