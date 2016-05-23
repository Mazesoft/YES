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
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
        }
        SPSQL SQL = new SPSQL();
        string cbTypeUserID;
        static string UserNameSearch;
        private void FormUsers_Load(object sender, EventArgs e)
        {
            SQL.BindComboTipoUsuario(cbTypeUser, "1");//enviar el tipo de usuario logueado
        }

        private void pbGuardar_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == txtpassConf.Text && txtPass.Text != "")
            {
                if (!SQL.UserExists(txtUsername.Text))
                {
                    if (SQL.GuardarUsuario(txtUsername.Text, txtPass.Text, txtName.Text, txtLastName.Text, cbTypeUserID))
                    {
                        MessageBox.Show("El Usuario se guardó correctamente.");
                        ClearControls();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un problema al intentar guardar el usuario");
                    }
                }
                else
                {
                    if (MessageBox.Show("Ya existe un usuario con este nombre de usuario desea actualizarlo?", "Usuario Existente", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        if (SQL.ActualizaUsuario(txtUsername.Text, txtPass.Text, txtName.Text, txtLastName.Text, cbTypeUserID))
                        {
                            MessageBox.Show("El Usuario se actualizó correctamente.");
                            ClearControls();
                        }
                        else
                        {
                            MessageBox.Show("Ocurrió un problema al intentar actualizar el usuario");
                        }
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                txtPass.ResetText();
                txtpassConf.ResetText();
            }
        }

        private void pbBuscar_Click(object sender, EventArgs e)
        {
            FormGenSearch.table = "Users";

            FormGenSearch frm = new FormGenSearch();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtUsername.Text = frm.SelectedValue;
                SQL.SelectUser(frm.SelectedValue, txtName, txtLastName, cbTypeUser);
            }
        }  

        private void pbBorrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta a punto de eliminar este usuario. Desea Continuar?", "Eliminar Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                SQL.DeleteUser(txtUsername.Text);
                ClearControls();
                MessageBox.Show("El Usuario se a eliminado.");
            }
        }

        private void cbTypeUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbTypeUserID = ((KeyValuePair<int, string>)cbTypeUser.SelectedItem).Key.ToString();
        }

        private void ClearControls()
        {
            txtpassConf.ResetText();
            txtLastName.ResetText();
            txtName.ResetText();
            txtUsername.ResetText();
            txtPass.ResetText();
            cbTypeUser.SelectedIndex = 0;
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                SQL.SelectUser(txtUsername.Text, txtName, txtLastName, cbTypeUser);
            }
        }
    }
}
