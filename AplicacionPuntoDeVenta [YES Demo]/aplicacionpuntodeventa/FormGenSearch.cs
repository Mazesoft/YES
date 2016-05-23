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
    public partial class FormGenSearch : Form
    {
        public FormGenSearch()
        {
            InitializeComponent();
        }

        public static string table;
        SPSQL SQL = new SPSQL();

        private string _value = null;
        public string SelectedValue
        {
            get { return _value; }
        }

        private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                switch(table)
                {
                    case "Users":
                        SQL.UsersSearch(dgv, txtFiltro.Text);
                        dgv.ClearSelection();
                        break;
                }
            }
        }

        private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this._value = dgv[0, e.RowIndex].Value.ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
