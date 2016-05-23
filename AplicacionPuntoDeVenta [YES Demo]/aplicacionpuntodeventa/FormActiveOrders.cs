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
    public partial class FormActiveOrders : Form
    {
        public FormActiveOrders()
        {
            InitializeComponent();
        }

        LINQ objLINQ = new LINQ();
        List<Order> lOrders = new List<Order>();
        int OrderSearch;
        private void FormActiveOrders_Load(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
                OrderSearch = Convert.ToInt16(txtBuscar.Text);
            else
                OrderSearch = 0;

            lOrders = objLINQ.SelectActiveOrders(OrderSearch);

            if (lOrders.Count != 0)
            {
                for (int row = 0; row < lOrders.Count; row++)
                {
                    dgvOrders.Rows.Add();

                    dgvOrders[2, row].Value = lOrders[row].OrderNum;
                    dgvOrders[3, row].Value = lOrders[row].ClientName;
                    dgvOrders[4, row].Value = lOrders[row].Qty;
                    dgvOrders[5, row].Value = lOrders[row].FinalAmount;
                    dgvOrders[6, row].Value = lOrders[row].IdOrder;
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvOrders.Rows.Clear();

            if (txtBuscar.Text != "")
                OrderSearch = Convert.ToInt16(txtBuscar.Text);
            else
                OrderSearch = 0;

            lOrders = objLINQ.SelectActiveOrders(OrderSearch);

            if (lOrders.Count != 0)
            {
                for (int row = 0; row < lOrders.Count; row++)
                {
                    dgvOrders.Rows.Add();

                    dgvOrders[2, row].Value = lOrders[row].OrderNum;
                    dgvOrders[3, row].Value = lOrders[row].ClientName;
                    dgvOrders[4, row].Value = lOrders[row].Qty;
                    dgvOrders[5, row].Value = lOrders[row].FinalAmount;
                    dgvOrders[6, row].Value = lOrders[row].IdOrder;
                }
                FormOrders.ordernum = Convert.ToInt16(dgvOrders[6, 0].Value.ToString());
                this.Close();
            }
        }

        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.ColumnIndex == 0)
            {
                FormOrders.ordernum = Convert.ToInt16(dgvOrders[6, e.RowIndex].Value.ToString());
                FormOrders.pay = false;
                FormOrders.partial = new List<OrderDetail>();
                this.Close();
            }
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.ColumnIndex == 1)
            {
                FormOrders.ordernum = Convert.ToInt16(dgvOrders[6, e.RowIndex].Value.ToString());
                FormOrders.pay = true;
                FormOrders.partial = new List<OrderDetail>();
                this.Close();
            }
        }

        private void FormActiveOrders_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormOrders.formorder.Form1_Load(null, null);
        }
    }
}
