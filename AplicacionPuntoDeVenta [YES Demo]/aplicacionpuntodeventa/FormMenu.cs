using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionPuntoDeVenta
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }
        SPSQL SQL = new SPSQL();
        int cbUnidadID; 
        string MenuID;
        private void FormMenu_Load(object sender, EventArgs e)
        {
            SQL.BindComboTipoMenu(cbTipo);
            SQL.BindGridMenu(dgvMenu);
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbUnidadID = ((KeyValuePair<int, string>)cbTipo.SelectedItem).Key;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Guardar")
            {
                SQL.InsertNewMenuItem(cbUnidadID.ToString(), txtDesc.Text, txtPrecio.Text);
                SQL.BindGridMenu(dgvMenu);
                txtDesc.ResetText();
                txtPrecio.ResetText();
                cbTipo.SelectedIndex = 0;
            }

            if (btnGuardar.Text == "Actualizar")
            {
                if (SQL.UpdateMenuItem(MenuID, txtDesc.Text, cbUnidadID.ToString(), txtPrecio.Text))
                {
                    SQL.BindGridMenu(dgvMenu);
                    txtDesc.ResetText();
                    txtPrecio.ResetText();
                    cbTipo.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Se ha generado un error no identificado al tratar de guardar la nueva entrada en el Menú.");
                }
            }
        }

        private void dgvMenu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                switch (e.ColumnIndex)
                {
                    case 0: //Boton actualizar
                        MenuID = dgvMenu[2, e.RowIndex].Value.ToString();

                        txtDesc.Text = dgvMenu[3, e.RowIndex].Value.ToString();
                        cbTipo.Text = dgvMenu[4, e.RowIndex].Value.ToString();
                        txtPrecio.Text = dgvMenu[5, e.RowIndex].Value.ToString();

                        btnGuardar.Text = "Actualizar";
                        break;
                    case 1: //Boton Borrar
                        if (MessageBox.Show("Esta a punto de eliminar la entrada seleccionada del Menú, desea continuar?", "Borrar Entrada de Menú", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            SQL.BorrarEntradaMenu(dgvMenu[2, e.RowIndex].Value.ToString());
                            SQL.BindGridMenu(dgvMenu);
                        }
                        break;
                }
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtDesc_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
