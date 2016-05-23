using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace AplicacionPuntoDeVenta
{
    class SPSQL
    {
        En_De_Crypt crypt = new En_De_Crypt();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PescaditoConnection"].ConnectionString);
        public bool SaveInventoryEntry(string Producto, string Cant, string Unidad, string Precio, string CantTaco)
        {
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction("InvTrans");
            try
            {
                SqlCommand cmd = new SqlCommand("SP_SaveInventoryEntry '" + Producto + "', " + Cant + ", " + Unidad + ", " + Precio + ", " + (CantTaco == "" ? "0" : CantTaco), conn, trans);
                cmd.ExecuteNonQuery();
                trans.Commit();
                conn.Close();
                return true;
            }
            catch
            {
                trans.Rollback();
                conn.Close();
                return false;
            }
        }

        public bool UpdateInventoryEntry(string InvID, string Producto, string Cant, string Unidad, string Precio, string CantTaco)
        {
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction("InvTrans");
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateInventoryEntry " + InvID + ", '" + Producto + "', " + Cant + ", " + Unidad + ", " + Precio + ", " + (CantTaco == "" ? "0" : CantTaco), conn, trans);
                cmd.ExecuteNonQuery();
                trans.Commit();
                conn.Close();
                return true;
            }
            catch
            {
                trans.Rollback();
                conn.Close();
                return false;
            }
        }

        public bool UpdateMenuItem(string MenuID, string Desc, string TipoID, string Precio)
        {
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction("InvTrans");
            try
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateMenuItem " + MenuID + ", '" + Desc + "', " + TipoID + ", " + Precio, conn, trans);
                cmd.ExecuteNonQuery();
                trans.Commit();
                conn.Close();
                return true;
            }
            catch
            {
                trans.Rollback();
                conn.Close();
                return false;
            }
        }

        public void BindComboUnidad(ComboBox cb)
        {
            conn.Open();
            Dictionary<int, string> DicCbUnidad = new Dictionary<int, string>();
            SqlCommand cmd = new SqlCommand("select IdUnidad, UnidadDesc from InvUnidadMed", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                DicCbUnidad.Add(Convert.ToInt16(rdr[0].ToString()), rdr[1].ToString());
            }
            cb.DataSource = new BindingSource(DicCbUnidad, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
            rdr.Close();
            conn.Close();
        }

        public void BindComboProducto(ComboBox cb)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Producto from InvProductos", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                cb.Items.Add(rdr[0].ToString());
            }
            rdr.Close();
            conn.Close();
        }

        public void BindComboTipoUsuario(ComboBox cb, string TypeUserID)
        {
            string SqlText;
            if (TypeUserID == "1")
                SqlText = "select * from UsersType";
            else
                SqlText = "select * from UsersType where IdUserType <> 1";

            conn.Open();
            Dictionary<int, string> DicCbUnidad = new Dictionary<int, string>();
            SqlCommand cmd = new SqlCommand(SqlText, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                DicCbUnidad.Add(Convert.ToInt16(rdr[0].ToString()), rdr[1].ToString());
            }
            cb.DataSource = new BindingSource(DicCbUnidad, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
            rdr.Close();
            conn.Close();
        }
        public void BindGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            int r = 0;
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_BindGridInventory", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                dgv.Rows.Add();
                for (int c = 2; c < dgv.ColumnCount; c++)
                {
                    dgv[c, r].Value = rdr[c - 2].ToString();
                }
                r++;
            }
            rdr.Close();
            conn.Close();
        }

        public void BorrarEntradaInventario(string ID)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from InvInventario where IdInventario = " + ID, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void BorrarEntradaMenu(string ID)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from Menu where IdMenu = " + ID, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public string GetPrecioXTaco(string InvID)
        {
            string PrecioXTaco;
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GetPrecioXTaco " + InvID, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
                PrecioXTaco = rdr[0].ToString();
            else
                PrecioXTaco = "";
            rdr.Close();
            conn.Close();
            return PrecioXTaco;
        }

        public bool GuardarUsuario(string username, string pass, string name, string lastname, string UserTypeID)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_SaveNewUser " + UserTypeID + ", '" + name + "', '" + lastname + "', '" + username + "', '" + crypt.EncryptString(pass) + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch
            {
                conn.Close();
                return false;
            }
        }

        public bool UserExists(string username)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from users where username = '" + username + "'", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            { rdr.Close(); conn.Close(); return true; }
            else
            { rdr.Close(); conn.Close(); return false; }
        }

        public bool ActualizaUsuario(string username, string pass, string name, string lastname, string UserTypeID)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_UpdateUser " + UserTypeID + ", '" + name + "', '" + lastname + "', '" + username + "', '" + crypt.EncryptString(pass) + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch
            {
                conn.Close();
                return false;
            }
        }

        public void SelectUser(string username, TextBox name, TextBox lastname, ComboBox type)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Users u inner join UsersType ut on ut.IdUserType = u.IdUserType where u.Username = '" + username + "'", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                name.Text = rdr[2].ToString();
                lastname.Text = rdr[3].ToString();
                type.SelectedIndex = type.FindStringExact(rdr[7].ToString());
            }
            rdr.Close();
            conn.Close();
        }

        public void DeleteUser(string username)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete users where username = '" + username + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UsersSearch(DataGridView dgv, string filtro)
        {
            int cont = 0;
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.ColumnCount = 3;
            dgv.Columns[0].Name = "Usuario";
            dgv.Columns[1].Name = "Nombre";
            dgv.Columns[2].Name = "Apellido";

            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_UsersSearch '" + filtro + "'", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                dgv.Rows.Add();
                dgv[0, cont].Value = rdr[0].ToString();
                dgv[1, cont].Value = rdr[1].ToString();
                dgv[2, cont].Value = rdr[2].ToString();

                cont++;
            }
            rdr.Close();
            conn.Close();
        }

        public void BindComboTipoMenu(ComboBox cb)
        {
            conn.Open();
            Dictionary<int, string> DicCb= new Dictionary<int, string>();
            SqlCommand cmd = new SqlCommand("select * from MenuTipo", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                DicCb.Add(Convert.ToInt16(rdr[0].ToString()), rdr[1].ToString());
            }
            cb.DataSource = new BindingSource(DicCb, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
            rdr.Close();
            conn.Close();
        }

        public void InsertNewMenuItem(string idTipo, string Desc, string Price)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_InsertMenu " + idTipo + ", '" + Desc + "', " + Price, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void BindGridMenu(DataGridView dgv)
        {
            dgv.Rows.Clear();
            int r = 0;
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_BindGridMenu", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                dgv.Rows.Add();
                for (int c = 2; c < dgv.ColumnCount; c++)
                {
                    dgv[c, r].Value = rdr[c - 2].ToString();
                }
                r++;
            }
            rdr.Close();
            conn.Close();
        }

        public void GetChefView(Label lblOrdF, Label lblOrdS, Label lblOrdT, ListBox lbF, ListBox lbS, ListBox lbT)
        {
            lbF.Items.Clear(); lbS.Items.Clear(); lbT.Items.Clear();
            List<int> ListOrderID = new List<int>();
            conn.Open();
            SqlCommand cmdT3 = new SqlCommand("GetChefTop3", conn);
            SqlDataReader rdrT3 = cmdT3.ExecuteReader();
            while (rdrT3.Read())
            {
                ListOrderID.Add(Convert.ToInt16(rdrT3[0].ToString()));
            }
            rdrT3.Close();

            for (int x = 0; x < ListOrderID.Count; x++)
            {
                SqlCommand cmd = new SqlCommand("GetChefView " + ListOrderID[x].ToString(), conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    switch (x)
                    {
                        case 0:
                            lblOrdF.Text = "Orden No " + rdr[1].ToString();
                            lblOrdF.Tag = rdr[0].ToString();
                            lbF.Items.Add(rdr[2].ToString() + "x  " + rdr[3].ToString());
                            break;
                        case 1:
                            lblOrdS.Text = "Orden No " + rdr[1].ToString();
                            lblOrdS.Tag = rdr[0].ToString();
                            lbS.Items.Add(rdr[2].ToString() + "x  " + rdr[3].ToString());
                            break;
                        case 2:
                            lblOrdT.Text = "Orden No " + rdr[1].ToString();
                            lblOrdT.Tag = rdr[0].ToString();
                            lbT.Items.Add(rdr[2].ToString() + "x  " + rdr[3].ToString());
                            break;
                    }
                }
                rdr.Close();
            }
            conn.Close();
        }

        public void FinishChefOrd(string OrderID)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("update OrdChef set bActive = 0 where IdOrder = " + OrderID, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
