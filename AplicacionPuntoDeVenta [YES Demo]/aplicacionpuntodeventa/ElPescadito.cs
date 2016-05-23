using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AplicacionPuntoDeVenta
{
    public partial class ElPescadito : Form
    {
        public ElPescadito()
        {
            InitializeComponent();
        }

        private void ElPescadito_Load(object sender, EventArgs e)
        {

        }

        int x = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (x == 1)
            {
                FormLogin frm = new FormLogin();
                frm.Show();

                this.Hide();
                x++;
            }
            else x++;
        }
    }
}
