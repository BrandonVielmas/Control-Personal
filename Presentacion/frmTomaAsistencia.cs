using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HaciendoReportes.Datos;
using HaciendoReportes.Logica;

namespace HaciendoReportes.Presentacion
{
    public partial class frmTomaAsistencia : Form
    {
        string Identificacion;
        int IdPersonal;
        int Contador;
        DateTime FechaReg;

        public frmTomaAsistencia()
        {
            InitializeComponent();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmTomaAsistencia_Load(object sender, EventArgs e)
        {
            pnlObservacion.Visible = true;
            
        }

        private void timerHora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {
            buscarPersonalIdentidad();
            if(Identificacion == txtIdentificacion.Text)
            {
                buscarAsistenciaId();
                if(Contador == 0)
                {
                    DialogResult resultado = MessageBox.Show("¿Agregar una observacion?", "Observaciones", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if(resultado == DialogResult.OK)
                    {
                        pnlObservacion.Visible = true;
                        pnlObservacion.Location = new Point((pnlRegistro.Location.X), (pnlRegistro.Location.Y));
                        pnlObservacion.Size = new Size(pnlRegistro.Width, pnlRegistro.Height);
                        pnlObservacion.BringToFront();
                        rtxtObservacion.Clear();
                        rtxtObservacion.Focus();
                        //insertarAsistencia();
                    }
                    else
                    {
                        insertarAsistencia();
                    }

                }
                else
                {
                    confirmarSalida();
                }
            }
        }

        private void confirmarSalida()
        {
            DAsistencia funcion = new DAsistencia();
            LAsistencia parametros = new LAsistencia();

            parametros.IdPersonal = IdPersonal;
            parametros.FechaSalida = DateTime.Now;
            parametros.Horas = Bases.DateDiff(Bases.DateInterval.Hour, FechaReg, DateTime.Now);
            if(funcion.confirmarSalida(parametros) == true)
            {
                lblAviso.Text = "Saliga registrada";
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        }

        private void insertarAsistencia()
        {
            if (string.IsNullOrEmpty(rtxtObservacion.Text))
            {
                rtxtObservacion.Text = "-";
            }

            LAsistencia parametros = new LAsistencia();
            DAsistencia funcion = new DAsistencia();

            parametros.IdPersonal = IdPersonal;
            parametros.FechaEntrada = DateTime.Now;
            parametros.FechaSalida = DateTime.Now;
            parametros.Estado = "Entrada";
            parametros.Horas = 0;
            parametros.Observacion = rtxtObservacion.Text;
            //funcion.insertarAsistencia(parametros);

            if(funcion.insertarAsistencia(parametros) == true)
            {
                lblAviso.Text = "Entrada Registrada";
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
                pnlObservacion.Visible = false;
            }
            
        }

        private void buscarAsistenciaId()
        {
            DataTable dt = new DataTable();
            DAsistencia funcion = new DAsistencia();

            funcion.buscarAsistencaId(ref dt,IdPersonal);
            Contador = dt.Rows.Count;

            if(Contador > 0)
            {
                FechaReg = Convert.ToDateTime(dt.Rows[0][2]);
            }

        }

        private void buscarPersonalIdentidad()
        {
            DataTable dt = new DataTable();
            DPersonal funcion = new DPersonal();
            funcion.buscarPersonalIdentidad(ref dt, txtIdentificacion.Text);

            if(dt.Rows.Count > 0)
            {
                Identificacion = dt.Rows[0]["Identificacion"].ToString();
                IdPersonal = Convert.ToInt32(dt.Rows[0]["IdPersonal"]);
                lblNombre.Text = Convert.ToString(dt.Rows[0]["Nombres"]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertarAsistencia();
        }
    }
}
