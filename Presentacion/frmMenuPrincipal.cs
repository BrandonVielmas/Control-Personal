using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaciendoReportes.Presentacion
{
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
            pnlBienvenida.Dock = DockStyle.Fill;
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            panelPadre.Controls.Clear();
            ctrPersonal control = new ctrPersonal();
            control.Dock = DockStyle.Fill;
            panelPadre.Controls.Add(control);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            panelPadre.Controls.Clear();
            ctrUsuarios control = new ctrUsuarios();
            control.Dock = DockStyle.Fill;
            panelPadre.Controls.Add(control);
        }
    }
}
