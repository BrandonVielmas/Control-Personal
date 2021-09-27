using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HaciendoReportes.Logica;
using HaciendoReportes.Datos;
using HaciendoReportes.Presentacion;

namespace HaciendoReportes.Presentacion
{
    public partial class ctrPersonal : UserControl
    {
        public ctrPersonal()
        {
            InitializeComponent();
        }

        int IdCargo = 0;
        int desde = 1;
        int hasta = 10;
        int contador;
        int IdPersonal;
        private int itemPorPagina = 10;
        string estado;
        int totalPaginas;

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            dwvCargo.Visible = false;
            panelCargos.Visible = false;
            panelPaginado.Visible = false;
            panelRegistros.Visible = true;
            panelRegistros.Dock = DockStyle.Fill;
            btnGuardarPersonal.Visible = true;
            btnGuardarCambiosPersonal.Visible = false;
            Limpiar();
        }

        private void LocalizarDwvCargos()
        {
            dwvCargo.Location = new Point(panelSueldo.Location.X, panelCargo.Location.Y);
            dwvCargo.Size = new Size(455, 145);
            dwvCargo.Visible = true;
            lblSueldo.Visible = false;
            flpGuardarPersonal.Visible = false;
        }

        private void Limpiar()
        {
            txtNombres.Clear();
            txtIdentificacion.Clear();
            txtCargo.Clear();
            txtSueldoPorHora.Clear();
            buscarCargo();
        }

        private void btnGuardarPersonal_Click(object sender, EventArgs e)
         {
            if (!string.IsNullOrEmpty(txtNombres.Text))
            {
                if (!string.IsNullOrEmpty(txtIdentificacion.Text)){
                    
                    if (!string.IsNullOrEmpty(cboPais.Text)){
                        
                        if(IdCargo > 0){

                            if (!string.IsNullOrEmpty(txtSueldoPorHora.Text)){

                                insertarPersonal();
                            }
                        }
                    }
                }
            }
        }

        private void insertarPersonal()
        {
            LPersonal parametros = new LPersonal();
            DPersonal funcion = new DPersonal();

            parametros.Nombres = txtNombres.Text;
            parametros.Identificacion = txtIdentificacion.Text;
            parametros.Pais = cboPais.Text;
            parametros.IdCargo = IdCargo;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoPorHora.Text);

            if(funcion.insertarPersonal(parametros) == true)
            {
                reiniciarPaginado();
                panelRegistros.Visible = false;
                MessageBox.Show("Personal Agregado Correctamente", "Agg Personal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mostrarPersonal();
                
            }

        }

        private void mostrarPersonal()
        {
            DataTable dt = new DataTable();
            DPersonal funcion = new DPersonal();
            funcion.mostrarPersonal(ref dt, desde, hasta);
            dwvPersonal.DataSource = dt;
            diseñarDwvPersonal();
        }

        private void diseñarDwvPersonal()
        {
            Bases.DisenoDwv(ref dwvPersonal);
            Bases.DisenoDvwEliminados(ref dwvPersonal);
            panelPaginado.Visible = true;
            dwvPersonal.Columns[2].Visible = false;
            dwvPersonal.Columns[7].Visible = false;
        }

        private void insetarCargo()
        {
            if (!string.IsNullOrEmpty(txtCargoNew.Text))
            {
                if (!string.IsNullOrEmpty(txtSueldoPorHoraNew.Text))
                {
                    LCargo parametros = new LCargo();
                    DCargos funcion = new DCargos();

                    parametros.Cargo = txtCargoNew.Text;
                    parametros.SueldoPorHora = Convert.ToDouble(txtSueldoPorHoraNew.Text);
                    if (funcion.insertarCargo(parametros) == true)
                    {
                        txtCargo.Clear();
                        buscarCargo();
                        panelCargos.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Agregue el sueldo", "Falta el sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Agregue el cargo", "Falta el cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void buscarCargo()
        {
            DataTable dt = new DataTable();
            DCargos funcion = new DCargos();
            funcion.buscarCargo(ref dt, txtCargo.Text);
            dwvCargo.DataSource = dt;
            Bases.DisenoDwv(ref dwvCargo);
            dwvCargo.Columns[1].Visible = false;
            dwvCargo.Columns[3].Visible = false;
            //dwvCargo.Visible = true;
        }

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            buscarCargo();
        }

        private void btnAgregarCargo_Click(object sender, EventArgs e)
        {
            panelCargos.Visible = true;
            panelCargos.Dock = DockStyle.Fill;
            panelCargos.BringToFront();
            btnGuardarCargo.Visible = true;
            btnGuardarCambiosCargo.Visible = false;
            txtCargoNew.Clear();
            txtSueldoPorHoraNew.Clear();
        }

        private void btnGuardarCargo_Click(object sender, EventArgs e)
        {
            insetarCargo();
        }

        private void txtSueldoPorHoraNew_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoPorHoraNew, e);
        }

        private void txtSueldoPorHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoPorHora, e);
        }

        private void txtCargo_Click(object sender, EventArgs e)
        {
            LocalizarDwvCargos();
        }

        private void dwvCargo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dwvCargo.Columns["EditarCargo"].Index)
            {
                ObtenerDatosParaEditarCargos();
            }
            if (e.ColumnIndex == dwvCargo.Columns["Cargo"].Index)
            {
                ObtenerDatosCargos();
            }
        }

        private void ObtenerDatosCargos()
        {
            IdCargo = Convert.ToInt32(dwvCargo.SelectedCells[1].Value);
            txtCargo.Text = Convert.ToString(dwvCargo.SelectedCells[2].Value);
            txtSueldoPorHora.Text = Convert.ToString(dwvCargo.SelectedCells[3].Value);
            dwvCargo.Visible = false;
            flpGuardarPersonal.Visible = true;
            lblSueldo.Visible = true;
        }

        private void ObtenerDatosParaEditarCargos()
        {
            IdCargo = Convert.ToInt32(dwvCargo.SelectedCells[1].Value);
            txtCargoNew.Text = Convert.ToString(dwvCargo.SelectedCells[2].Value);
            txtSueldoPorHoraNew.Text = Convert.ToString(dwvCargo.SelectedCells[3].Value);
            btnGuardarCargo.Visible = false;
            btnGuardarCambiosCargo.Visible = true;
            txtCargoNew.Focus();
            txtCargoNew.SelectAll();
            panelCargos.Visible = true;
            panelCargos.Dock = DockStyle.Fill;
            panelCargos.BringToFront();
        }

        private void btnVolverCargo_Click(object sender, EventArgs e)
        {
            panelCargos.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelRegistros.Visible = false;
            panelPaginado.Visible = true;
        }

        private void btnGuardarCambiosCargo_Click(object sender, EventArgs e)
        {
            editarCargos();
        }

        private void editarCargos()
        {
            LCargo parametros = new LCargo();
            DCargos funcion = new DCargos();

            parametros.IdCargo = this.IdCargo;
            parametros.Cargo = txtCargoNew.Text;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoPorHoraNew.Text);

            if (funcion.editarCargo(parametros) == true)
            {
                txtCargo.Clear();
                buscarCargo();
                panelCargos.Visible = false;
            }
        }

        private void ctrPersonal_Load(object sender, EventArgs e)
        {
            reiniciarPaginado();
            mostrarPersonal();
            dwvCargo.ReadOnly = true;
            dwvPersonal.ReadOnly = true;    
        }

        private void reiniciarPaginado()
        {
            desde = 1;
            hasta = 10;

            Contar();
            if(contador > hasta)
            {
                btnSiguiente.Visible = true;
                btnAnterior.Visible = false;
                btnUltimaPagina.Visible = true;
                btnPrimeraPagina.Visible = true;
            }
            else{
                btnSiguiente.Visible = false;
                btnAnterior.Visible = false;
                btnUltimaPagina.Visible = false;
                btnPrimeraPagina.Visible = false;
            }

            Paginar();

        }

        private void dwvPersonal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dwvPersonal.Columns["Eliminar"].Index)
            {
                DialogResult result = MessageBox.Show("Solo se cambiara el estado para que no se pueda acceder, Continuar¿?","Eliminando Registros",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if(result == DialogResult.OK)
                {
                  eliminarPersonal();
                }  
            }

            if (e.ColumnIndex == dwvPersonal.Columns["Editar"].Index)
            {
                obtenerDatos();
            }
        }

        private void obtenerDatos()
        {
            IdPersonal = Convert.ToInt32(dwvPersonal.SelectedCells[2].Value);
            estado = Convert.ToString(dwvPersonal.SelectedCells[9].Value);
            if(estado == "Baja")
            {
                restaurarPersonal();
            }
            else
            {
                txtNombres.Text = Convert.ToString(dwvPersonal.SelectedCells[3].Value);
                txtIdentificacion.Text = Convert.ToString(dwvPersonal.SelectedCells[4].Value);
                cboPais.Text = Convert.ToString(dwvPersonal.SelectedCells[8].Value);
                txtCargo.Text = Convert.ToString(dwvPersonal.SelectedCells[6].Value);
                IdCargo = Convert.ToInt32(dwvPersonal.SelectedCells[7].Value);
                txtSueldoPorHora.Text = Convert.ToString(dwvPersonal.SelectedCells[5].Value);

                panelPaginado.Visible = false;
                panelRegistros.Visible = true;
                panelRegistros.Dock = DockStyle.Fill;
                dwvCargo.Visible = false;
                lblSueldo.Visible = true;
                flpGuardarPersonal.Visible = true;
                btnGuardarCambiosPersonal.Visible = true;
                btnGuardarPersonal.Visible = false;
                panelCargos.Visible = false;
            }
        }

        private void restaurarPersonal()
        {
            DialogResult result = MessageBox.Show("Este personal se elimino, desea habilitarlo de nuevo, Continuar¿?", "Restauracion Registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(result == DialogResult.OK)
            {
                habilitarPersonal();
            }
        }

        private void habilitarPersonal()
        {
            LPersonal parametros = new LPersonal();
            DPersonal funcion = new DPersonal();

            parametros.IdPersonal = IdPersonal;

            if(funcion.restaurarrPersonal(parametros) == true)
            {
                mostrarPersonal();
            }
        }
           
        private void eliminarPersonal()
        {
            IdPersonal = Convert.ToInt32(dwvPersonal.SelectedCells[2].Value);
            LPersonal parametros = new LPersonal();
            DPersonal funcion = new DPersonal();
            parametros.IdPersonal = IdPersonal;

            if(funcion.eliminarPersonal(parametros) == true)
            {
                mostrarPersonal();
            }
        }

        private void btnGuardarCambiosPersonal_Click(object sender, EventArgs e)
        {
            editarPersonal();
        }

        private void editarPersonal()
        {
            LPersonal parametros = new LPersonal();
            DPersonal funcion = new DPersonal();

            parametros.IdPersonal = IdPersonal;
            parametros.Nombres = txtNombres.Text;
            parametros.Identificacion = txtIdentificacion.Text;
            parametros.Pais = cboPais.Text;
            parametros.IdCargo = IdCargo;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoPorHora.Text);

            if(funcion.editarPersonal(parametros) == true)
            {
                mostrarPersonal();
                panelRegistros.Visible = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            diseñarDwvPersonal();
            timer1.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            desde += 10;
            hasta += 10;
            mostrarPersonal();
            Contar();
            if(contador > hasta)
            {
                btnSiguiente.Visible = true;
                btnAnterior.Visible = false;
            }
            else
            {
                btnSiguiente.Visible = false;
                btnAnterior.Visible = true;
            }

            Paginar();
        }

        private void Paginar()
        {
            try
            {
                lblPagina.Text = Convert.ToString(hasta / itemPorPagina);
                lblTPagina.Text = Math.Ceiling(Convert.ToSingle(contador) / itemPorPagina).ToString();
                totalPaginas = Convert.ToInt32(lblTPagina.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Contar()
        {
            DPersonal funcion = new DPersonal();
            funcion.contarPersonal(ref contador);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            desde -= 10;
            hasta -= 10;
            mostrarPersonal();
            Contar();
            if(contador > hasta)
            {
                btnSiguiente.Visible = true;
                btnAnterior.Visible =true;
            }
            else
            {
                btnSiguiente.Visible = false;
                btnAnterior.Visible = true;
            }
            if(desde == 1)
            {
                reiniciarPaginado();
            }
            Paginar();
        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            hasta = totalPaginas * itemPorPagina;
            desde = hasta - 9;
            mostrarPersonal();
            Contar();

            if (contador > hasta)
            {
                btnSiguiente.Visible = true;
                btnAnterior.Visible = true;
            }
            else
            {
                btnSiguiente.Visible = false;
                btnAnterior.Visible = true;
            }
            Paginar();
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            reiniciarPaginado();
            mostrarPersonal();
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            buscarPersonal();
        }

        private void buscarPersonal()
        {
            DPersonal funcion = new DPersonal();
            DataTable dt = new DataTable();

            funcion.buscarPersonal(ref dt, desde, hasta,txtBuscador.Text);
            dwvPersonal.DataSource = dt;
            diseñarDwvPersonal();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            reiniciarPaginado();
            mostrarPersonal();
        }
    } 
}