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
    public partial class FormChefView : Form
    {
        SPSQL SQL = new SPSQL();
        public FormChefView()
        {
            InitializeComponent();
        }

        private void FormChefView_Load(object sender, EventArgs e)
        {
            CheckControls();
        }

        private void btnOrdF_Click(object sender, EventArgs e)
        {
            SQL.FinishChefOrd(lblOrderF.Tag.ToString());
            SQL.GetChefView(lblOrderF, lblOrderS, lblOrderT, lbOrdF, lbOrdS, lbOrdT);
            CheckControls();
        }

        private void btnOrdS_Click(object sender, EventArgs e)
        {
            SQL.FinishChefOrd(lblOrderS.Tag.ToString());
            SQL.GetChefView(lblOrderF, lblOrderS, lblOrderT, lbOrdF, lbOrdS, lbOrdT);
            CheckControls();
        }

        private void btnOrdT_Click(object sender, EventArgs e)
        {
            SQL.FinishChefOrd(lblOrderT.Tag.ToString());
            SQL.GetChefView(lblOrderF, lblOrderS, lblOrderT, lbOrdF, lbOrdS, lbOrdT);
            CheckControls();
        }

        private void CheckControls()
        {
            if (lbOrdF.Items.Count == 0 || lbOrdS.Items.Count == 0 || lbOrdT.Items.Count == 0)
                timerCheck.Enabled = true;
            else
                timerCheck.Enabled = false;

            if (lbOrdF.Items.Count == 0)
            {
                btnOrdF.Enabled = false;
                lblOrderF.Text = "";
                lblOrdActual.Text = "Sin Actividad";
            }
            else
            {
                btnOrdF.Enabled = true;
                lblOrdActual.Text = "Orden Actual";
            }

            if (lbOrdS.Items.Count == 0)
            {
                btnOrdS.Enabled = false;
                lblOrderS.Text = "";
            }
            else
            {
                btnOrdS.Enabled = true;
            }

            if (lbOrdT.Items.Count == 0)
            {
                btnOrdT.Enabled = false;
                lblOrderT.Text = "";
            }
            else
            {
                btnOrdT.Enabled = true;
            }
        }

        private void timerCheck_Tick(object sender, EventArgs e)
        {
            SQL.GetChefView(lblOrderF, lblOrderS, lblOrderT, lbOrdF, lbOrdS, lbOrdT);
            CheckControls();
        }
    }
}
