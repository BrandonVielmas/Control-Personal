using HaciendoReportes.Datos;
using HaciendoReportes.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HaciendoReportes.Presentacion
{
    public partial class ctrUsuarios : UserControl
    {
        public ctrUsuarios()
        {
            InitializeComponent();
        }

        private void limpiar()
        {
            txtNombre.Clear();
            txtContraseña.Clear();
            txtUsuario.Clear();
        }

        private void habiliarPaneles()
        {
            pnlRegistro.Visible = true;
            lblAnuncio.Visible = true;
            pnlIco.Visible = false;
            pnlRegistro.Dock = DockStyle.Fill;
            pnlRegistro.BringToFront();
            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            limpiar();
            habiliarPaneles();
            mostrarModulos();
        }

        private void mostrarModulos()
        {
            DModulos funcion = new DModulos();
            DataTable dt = new DataTable();
            funcion.mostrarModulos(ref dt);
            dwvModulos.DataSource = dt;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                if (!string.IsNullOrEmpty(txtUsuario.Text))
                {
                    if (!string.IsNullOrEmpty(txtContraseña.Text))
                    {
                        if (lblAnuncio.Visible == false)
                        {
                            insertarUsuario();
                        }
                        else
                        {
                            MessageBox.Show("Seleccione un icono");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingresa la contraseña");
                    }
                }
                else
                {
                    MessageBox.Show("Ingresa el usuario");
                }
            }
            else
            {
                MessageBox.Show("Ingresa el nombre");
            }
        }

        private void insertarUsuario()
        {
            LUsuarios parametros = new LUsuarios();
            DUsuarios funcion = new DUsuarios();
            parametros.Nombre = txtNombre.Text;
            parametros.Password = txtContraseña.Text;
            parametros.Login = txtUsuario.Text;
            MemoryStream ms = new MemoryStream();
            Icono.Image.Save(ms, Icono.Image.RawFormat);
            parametros.Icono = ms.GetBuffer();
            if (funcion.insertarUsuarios(parametros) == true)
            {

            }
        }
    }
}
