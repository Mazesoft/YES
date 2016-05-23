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
    public partial class FormInventory : Form
    {
        public FormInventory()
        {
            InitializeComponent();
        }
        SPSQL SQL = new SPSQL();
        string cbUnidadID;
        static string InvID;

        private void FormInventory_Load(object sender, EventArgs e)
        {
            SQL.BindComboUnidad(cbUnidad);
            SQL.BindComboProducto(cbProducto);
            SQL.BindGrid(dgvInvent);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Guardar")
            {
                if (SQL.SaveInventoryEntry(cbProducto.Text, txtCantidad.Text, cbUnidad.SelectedValue.ToString(), txtPrecio.Text, txtGramosTaco.Text))
                {
                    SQL.BindGrid(dgvInvent);
                    MessageBox.Show("Nueva entrada en el Inventario guardada con éxito.");
                    cbProducto.Text = "";
                    txtCantidad.Text = "";
                    cbUnidad.SelectedIndex = 0;
                    txtPrecio.Text = "";
                    txtGramosTaco.Text = "";
                    pnlGramosTaco.Visible = false;
                }
                else
                {
                    MessageBox.Show("Se ha generado un error no identificado al tratar de guardar la nueva entrada en el Inventario.");
                }
            }

            if(btnGuardar.Text == "Actualizar")
            {
                if (SQL.UpdateInventoryEntry(InvID, cbProducto.Text, txtCantidad.Text, cbUnidad.SelectedValue.ToString(), txtPrecio.Text, txtGramosTaco.Text))
                {
                    SQL.BindGrid(dgvInvent);
                    MessageBox.Show("Actualización en el Inventario guardada con éxito.");
                    cbProducto.Text = "";
                    txtCantidad.Text = "";
                    cbUnidad.SelectedIndex = 0;
                    txtPrecio.Text = "";
                    txtGramosTaco.Text = "";
                    pnlGramosTaco.Visible = false;
                    btnGuardar.Text = "Guardar";
                }
                else
                {
                    MessageBox.Show("Se ha generado un error no identificado al tratar de Actualizar el Inventario.");
                }
            }
        }

        private void cbUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbUnidadID = ((KeyValuePair<int, string>)cbUnidad.SelectedItem).Value;
            if (cbUnidad.Text == "Kilogramos")
                pnlGramosTaco.Visible = true;
            else
                pnlGramosTaco.Visible = false;
        }

        private void dgvInvent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                switch (e.ColumnIndex)
                {
                    case 0: //Boton actualizar
                        InvID = dgvInvent[2, e.RowIndex].Value.ToString();

                        cbProducto.Text = dgvInvent[3, e.RowIndex].Value.ToString();
                        txtCantidad.Text = dgvInvent[4, e.RowIndex].Value.ToString();
                        cbUnidad.Text = dgvInvent[5, e.RowIndex].Value.ToString();
                        txtPrecio.Text = dgvInvent[6, e.RowIndex].Value.ToString();
                        txtGramosTaco.Text = SQL.GetPrecioXTaco(InvID);
                        if (SQL.GetPrecioXTaco(InvID) == "0.00")
                            pnlGramosTaco.Visible = false;
                        else
                            pnlGramosTaco.Visible = true;

                        btnGuardar.Text = "Actualizar";
                        break;
                    case 1: //Boton Borrar
                        if (MessageBox.Show("Esta a punto de eliminar la entrada seleccionada del inventario, desea continuar?", "Borrar Entrada de Inventario", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            SQL.BorrarEntradaInventario(dgvInvent[2, e.RowIndex].Value.ToString());
                            SQL.BindGrid(dgvInvent);
                        }
                        break;
                }
            }
        }
    }
}
