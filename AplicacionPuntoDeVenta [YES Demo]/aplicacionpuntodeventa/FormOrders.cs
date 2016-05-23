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
    public partial class FormOrders : Form
    {
        public FormOrders()
        {
            InitializeComponent();
        }

        static int ActualIdOrder;
        static int OrderNum;
        static FormOrders FormOrder;
        static bool AuxPay;
        static List<OrderDetail> AuxPartial = new List<OrderDetail>();

        public static bool pay
        {
            get { return AuxPay; }
            set { AuxPay = value; }
        }

        public static List<OrderDetail> partial
        {
            get { return AuxPartial; }
            set { AuxPartial = value; }
        }

        public static int ordernum
        {
            get { return ActualIdOrder; }
            set { ActualIdOrder = value; }
        }

        public static FormOrders formorder
        {
            get { return FormOrder; }
            set { FormOrder = value; }
        }
        

        LINQ objLINQ = new LINQ();
        List<Menu> listComidas = new List<Menu>();
        List<Menu> listBebidas = new List<Menu>();

        Order NewOrder;
        List<OrderDetail> listOrderDetails = new List<OrderDetail>();
        List<Control> ctrls;

        PescaditoDBEntities entity = new PescaditoDBEntities();

        public void Form1_Load(object sender, EventArgs e)
        {
            menuStrip1.ForeColor = Color.White;
            //AuxPay = true;

            if (ctrls != null)
            {
                panelOrder.Controls.Clear();

                for (int j = 0; j < ctrls.Count; j++)
                {
                    panelOrder.Controls.Add(ctrls[j]);
                }
            }
            FormOrder = this;

            if (ActualIdOrder != 0)
            {
                //MessageBox.Show(OrderNum.ToString());

                //OrderNum = objLINQ.GetNextOrderNum();

                NewOrder = objLINQ.SelectOrder(ActualIdOrder);
            }
            else
            {
                NewOrder = objLINQ.SelectUnsaved();

                if (NewOrder == null)
                {
                    NewOrder = objLINQ.SelectOrder(objLINQ.CreateOrder());
                }
            }
            OrderNum = (int)NewOrder.OrderNum;
            ActualIdOrder = NewOrder.IdOrder;

            //lblOrderNum.Text = "Orden: " + objLINQ.GetOrderNum(OrderNum).ToString("0000");
            lblOrderNum.Text = "Orden: " + (OrderNum).ToString("0000");

            ctrls = new List<Control>();
            foreach (Control c in panelOrder.Controls)
            {
                ctrls.Add(c);
            }

            //NewOrder = objLINQ.SelectOrder(OrderNum);
            listOrderDetails = objLINQ.SelectOrderDetails(NewOrder.IdOrder);
            //objLINQ.CreateOrder();

            double amount = 0;
            int zeros = 0;
            for (int i = 0; i < listOrderDetails.Count; i++)
            {
                int contParcial = 0;
                for (int j = 0; j < AuxPartial.Count; j++)
                {
                    if (AuxPartial[j].IdOrderDetail == listOrderDetails[i].IdOrderDetail)
                        contParcial = AuxPartial[j].Qty;
                }

                if (listOrderDetails[i].Qty - contParcial > 0)
                {
                    Button button2 = new Button();
                    button2.Name = listOrderDetails[i].IdOrderDetail.ToString();
                    //button2.Text = "1x  |  Tacotote";
                    button2.Text = (listOrderDetails[i].Qty - contParcial) + "x  |  " + objLINQ.GetMenuName(listOrderDetails[i].IdMenu);
                    button2.Left = 20;
                    button2.Top = 60 + ((i - zeros) * 55);
                    button2.Width = 415;
                    button2.Height = 45;
                    button2.Font = new Font("Arial", 18, FontStyle.Regular);
                    button2.TextAlign = ContentAlignment.MiddleLeft;
                    button2.Click += new EventHandler(button_Click);
                    panelOrder.Controls.Add(button2);

                    Button button3 = new Button();
                    button3.Name = listOrderDetails[i].IdOrderDetail.ToString();
                    button3.Text = "X";
                    button3.Left = 445;
                    button3.Top = 60 + ((i - zeros) * 55);
                    button3.Width = 50;
                    button3.Height = 45;
                    button3.Font = new Font("Arial", 18, FontStyle.Bold);
                    button3.ForeColor = Color.White;
                    button3.BackColor = Color.FromArgb(217, 83, 79);
                    button3.Click += new EventHandler(button_Click);
                    panelOrder.Controls.Add(button3);

                    amount += Convert.ToDouble(listOrderDetails[i].Amount);
                }
                else
                {
                    zeros++;
                }
            }

            int ordersHeight = 55 * (listOrderDetails.Count - zeros) + 10;

            if (AuxPay != true)
            {

                lblDivision.Location = new Point(-3, 60 + ordersHeight);

                Label label = new Label();
                label.Text = "Total:";
                label.Left = 20;
                label.Top = 90 + ordersHeight;
                label.Width = 100;
                label.Height = 40;
                label.Font = new Font("Arial", 18, FontStyle.Regular);
                panelOrder.Controls.Add(label);

                Label label2 = new Label();
                label2.Text = "$ " + amount.ToString("N");
                label2.Width = 200;
                label2.Height = 40;
                label2.Left = 120;
                label2.Top = 85 + ordersHeight;
                label2.Font = new Font("Arial", 20, FontStyle.Regular);
                panelOrder.Controls.Add(label2);


                Button btnFinish = new Button();
                btnFinish.Name = "Save";
                btnFinish.Text = "Guardar Orden";
                btnFinish.Left = 20;
                btnFinish.Top = 145 + ordersHeight;
                btnFinish.Width = 475;
                btnFinish.Height = 60;
                btnFinish.Font = new Font("Arial", 18, FontStyle.Regular);
                btnFinish.FlatStyle = FlatStyle.System;
                btnFinish.Click += new EventHandler(button_Click);
                panelOrder.Controls.Add(btnFinish);

                panelOrder.Height = 1 * 85 + 135 + ordersHeight;

                lblParcial.Visible = false;
                lblDivision2.Visible = false;
                lblDivision.Height = 10;
            }
            else
            {
                lblDivision.Location = new Point(-3, 60 + ordersHeight);
                lblDivision.Height = 30;

                lblParcial.Location = new Point(3, 62 + ordersHeight);
                lblDivision2.Width = 600;
                lblParcial.Visible = true;

                double amountTotal = amount;
                amount = 0;
                for (int i = 0; i < AuxPartial.Count; i++)
                {
                    Button button2 = new Button();
                    button2.Name = AuxPartial[i].IdOrderDetail.ToString();
                    //button2.Text = "1x  |  Tacotote";
                    button2.Text = AuxPartial[i].Qty + "x  |  " + objLINQ.GetMenuName(AuxPartial[i].IdMenu);
                    button2.Left = 20;
                    button2.Top = 50 + (i * 35) + ordersHeight + 50;
                    button2.Width = 240;
                    button2.Height = 30;
                    button2.Font = new Font("Arial", 12, FontStyle.Regular);
                    button2.TextAlign = ContentAlignment.MiddleLeft;
                    button2.Click += new EventHandler(button_Click);
                    panelOrder.Controls.Add(button2);

                    Button button3 = new Button();
                    button3.Name = AuxPartial[i].IdOrderDetail.ToString();
                    button3.Text = "X";
                    button3.Left = 260;
                    button3.Top = 50 + (i * 35) + ordersHeight + 50;
                    button3.Width = 35;
                    button3.Height = 30;
                    button3.Font = new Font("Arial", 12, FontStyle.Bold);
                    button3.ForeColor = Color.White;
                    button3.BackColor = Color.FromArgb(217, 83, 79);
                    button3.Click += new EventHandler(button_Click);
                    panelOrder.Controls.Add(button3);

                    amount += Convert.ToDouble(AuxPartial[i].Amount);
                }

                lblDivision2.Location = new Point(-3, 109 + ordersHeight + (35 * AuxPartial.Count));
                lblDivision2.Visible = true;

                Label label = new Label();
                if (AuxPartial.Count >  0)
                    label.Text = "Pago Parcial:";
                else
                    label.Text = "Pago Total:";
                label.Left = 20;
                label.Height = 40;
                label.Top = 90 + ordersHeight + 49 + (35 * AuxPartial.Count);
                label.Width = 200;
                label.Font = new Font("Arial", 18, FontStyle.Regular);
                panelOrder.Controls.Add(label);

                Label label2 = new Label();
                if (AuxPartial.Count > 0)
                    label2.Text = "$ " + amount.ToString("N");
                else
                    label2.Text = "$ " + amountTotal.ToString("N");
                label2.Left = 220;
                label2.Height = 40;
                label2.Width = 400;
                label2.Top = 85 + ordersHeight + 49 + (35 * AuxPartial.Count);
                label2.Font = new Font("Arial", 20, FontStyle.Regular);
                panelOrder.Controls.Add(label2);


                Button btnFinish = new Button();
                btnFinish.Name = "Print";
                btnFinish.Text = "Imprimir Recibo";
                btnFinish.Left = 20;
                btnFinish.Top = 140 + ordersHeight + 49 + (35 * AuxPartial.Count);
                btnFinish.Width = 475;
                btnFinish.Height = 60;
                btnFinish.Font = new Font("Arial", 18, FontStyle.Regular);
                btnFinish.FlatStyle = FlatStyle.System;
                btnFinish.Click += new EventHandler(button_Click);
                panelOrder.Controls.Add(btnFinish);

                panelOrder.Height = 1 * 45 + 170 + ordersHeight + 49 + (35 * AuxPartial.Count);

            }


            ///////////////////////////////////////////////////////
            listComidas = objLINQ.SelectMenu(1);
            listBebidas = objLINQ.SelectMenu(2);

            //for (int i = 0; i < listComidas.Count; i++)
            //{
            //    ListViewItem lvItem = new ListViewItem();
            //    lvItem.SubItems[0].Text = listComidas[i].Description;
            //    lvItem.SubItems.Add(listComidas[i].Description);
            //    listViewMenu.Items.Add(lvItem);
            //}

            int cont = 0;

            int RowsMeals = 0;
            int ColsMeals = 0;

            RowsMeals = (int)Math.Round((decimal)listComidas.Count / 2, 0, MidpointRounding.AwayFromZero);
            ColsMeals = 2;

            menuStrip1.ForeColor = Color.White;

            int RowsDrinks = (int)Math.Round((decimal)listBebidas.Count / 2, 0, MidpointRounding.AwayFromZero);
            int ColsDrinks = 2;

            for (int row = 0; row < RowsMeals; row++)
            {
                for (int column = 0; column < ColsMeals; column++)
                {
                    Button button = new Button();
                    button.Name = listComidas[cont].IdMenu.ToString();
                    button.Text = listComidas[cont].Description.ToString();
                    button.Left = 20 + column * 255;
                    button.Top = 60 + row * 75;
                    button.Width = 220;
                    button.Height = 50;
                    button.Font = new Font("Arial", 18, FontStyle.Regular);
                    button.Click += new EventHandler(button_Click);
                    panelMeals.Controls.Add(button);

                    cont++;
                    if (cont == listComidas.Count) break;
                }
                if (cont == listComidas.Count) break;
            }

            panelMeals.Height = RowsMeals * 75 + 50;
            panelDrinks.Top = RowsMeals * 75 + 80 + 80;

            cont = 0;

            for (int row = 0; row < RowsDrinks; row++)
            {
                for (int column = 0; column < ColsDrinks; column++)
                {
                    Button button = new Button();
                    button.Name = listBebidas[cont].IdMenu.ToString();
                    button.Text = listBebidas[cont].Description.ToString();
                    button.Left = 20 + column * 255;
                    button.Top = 60 + row * 75;
                    button.Width = 220;
                    button.Height = 50;
                    button.Font = new Font("Arial", 18, FontStyle.Regular);
                    button.Click += new EventHandler(button_Click);
                    panelDrinks.Controls.Add(button);

                    cont++;
                    if (cont == listBebidas.Count) break;
                }
                if (cont == listBebidas.Count) break;
            }

            panelDrinks.Height = RowsDrinks * 75 + 50;
        }

        private void button_Click(object sender, EventArgs e)
        {
            //
            Button button = sender as Button;

            if (button.Text == "Guardar Orden")
            {
                objLINQ.UpdateOrderStatus(ActualIdOrder, true);
                objLINQ.CreateOrder();
                ActualIdOrder = 0;
                Form1_Load(null, null);
            }

            if (button.Text == "Imprimir Recibo")
            {
                if (AuxPartial.Count >  0)
                {
                    objLINQ.DeleteItemsFromOrder(ActualIdOrder);

                    for (int i = 0; i < listOrderDetails.Count; i++)
                    {
                        int contParcial = 0;
                        for (int j = 0; j < AuxPartial.Count; j++)
                        {
                            if (AuxPartial[j].IdOrderDetail == listOrderDetails[i].IdOrderDetail)
                                contParcial = AuxPartial[j].Qty;
                        }

                        if (listOrderDetails[i].Qty - contParcial > 0)
                        {
                            objLINQ.InsertOrderDetail(listOrderDetails[i]);
                        }
                    }


                    int partial = objLINQ.CreatePartialOrder(ActualIdOrder);

                    for (int i = 0; i < AuxPartial.Count; i++)
                    {
                        AuxPartial[i].IdOrder = partial;
                        objLINQ.InsertOrderDetail(AuxPartial[i]);
                    }

                    AuxPartial = new List<OrderDetail>();
                    ////////////////////////Print ticket
                    Form1_Load(null, null);
                }
                else
                {
                    objLINQ.UpdateOrderStatus(ActualIdOrder, false);
                    objLINQ.CreateOrder();
                    ActualIdOrder = 0;

                    ////////////////////////Print ticket
                    Form1_Load(null, null);
                }
            }

            if (button.Text.Contains("|"))
            {
                if (AuxPay == true)
                {
                    if (button.Top < lblParcial.Top)
                    {
                        bool found = false;
                        for (int i = 0; i < FormOrders.partial.Count; i++)
                        {
                            OrderDetail ord = objLINQ.SelectMenuOrderDetailbyId(Convert.ToInt32(button.Name));

                            if (FormOrders.partial[i].IdMenu == ord.IdMenu)
                            {
                                FormOrders.partial[i].Amount = (FormOrders.partial[i].Amount / FormOrders.partial[i].Qty) * (FormOrders.partial[i].Qty + 1);
                                FormOrders.partial[i].Qty++;
                                found = true;
                            }
                        }

                        if (found == false)
                        {
                            OrderDetail neworder = objLINQ.SelectMenuOrderDetailbyId(Convert.ToInt32(button.Name));
                            FormOrders.partial.Add(new OrderDetail
                            {
                                IdOrderDetail = neworder.IdOrderDetail,
                                IdMenu = neworder.IdMenu,
                                Qty = 1,
                                Amount = neworder.Amount / neworder.Qty
                            });
                        }
                        Form1_Load(null, null);
                    }
                }
            }
            else
            {
                if (button.Text == "X")
                {
                    if (button.Top < lblParcial.Top)
                    {
                        OrderDetail orderdet = objLINQ.SelectMenuOrderDetailbyId(Convert.ToInt32(button.Name));

                        OrderDetail result = (from a in entity.OrderDetails
                                              where a.IdOrderDetail == orderdet.IdOrderDetail
                                              select a).FirstOrDefault();

                        if (result != null)
                        {

                            if (result.Qty - 1 <= 0)
                            {
                                entity.OrderDetails.Remove(result);
                                entity.SaveChanges();

                                Form1_Load(null, null);
                            }
                            else
                            {
                                result.Amount = (result.Amount / result.Qty) * (result.Qty - 1);

                                result.Qty = result.Qty - 1;
                                entity.SaveChanges();

                                objLINQ = new LINQ();
                                listOrderDetails = objLINQ.SelectOrderDetails(NewOrder.IdOrder);

                                double amount = 0;
                                for (int i = 0; i < listOrderDetails.Count; i++)
                                {
                                    int contParcial = 0;
                                    for (int j = 0; j < AuxPartial.Count; j++)
                                    {
                                        if (AuxPartial[j].IdOrderDetail == listOrderDetails[i].IdOrderDetail)
                                            contParcial = AuxPartial[j].Qty;
                                    }

                                    if (listOrderDetails[i].Qty - contParcial > 0)
                                    {
                                        amount += Convert.ToDouble(listOrderDetails[i].Amount);
                                    }
                                }

                                string name = objLINQ.GetMenuName(result.IdMenu);
                                for (int j = 0; j < panelOrder.Controls.Count; j++)
                                {
                                    if (panelOrder.Controls[j].Text.Contains(name))
                                    {
                                        panelOrder.Controls[j].Text = (result.Qty) + "x  |  " + name;
                                    }

                                    if (panelOrder.Controls[j].Text.Contains("$"))
                                    {
                                        panelOrder.Controls[j].Text = "$ " + amount.ToString("N");
                                    }
                                }
                            }

                        }

                        //Form1_Load(null, null);
                    }
                    else
                    {
                        OrderDetail orderdet = objLINQ.SelectMenuOrderDetailbyId(Convert.ToInt32(button.Name));
                        for (int i = 0; i < FormOrders.partial.Count; i++)
                        {
                            if (orderdet.IdMenu == FormOrders.partial[i].IdMenu)
                            {
                                if (FormOrders.partial[i].Qty > 1)
                                {
                                    FormOrders.partial[i].Amount = (FormOrders.partial[i].Amount / FormOrders.partial[i].Qty) * (FormOrders.partial[i].Qty - 1);
                                    FormOrders.partial[i].Qty--;

                                    objLINQ = new LINQ();
                                    listOrderDetails = objLINQ.SelectOrderDetails(NewOrder.IdOrder);

                                    double amount = 0;
                                    for (int j = 0; j < AuxPartial.Count; j++)
                                    {
                                        amount += Convert.ToDouble(AuxPartial[j].Amount);
                                    }

                                    string name = objLINQ.GetMenuName(orderdet.IdMenu);
                                    for (int j = 0; j < panelOrder.Controls.Count; j++)
                                    {
                                        if (panelOrder.Controls[j].Text.Contains(name))
                                        {
                                            panelOrder.Controls[j].Text = FormOrders.partial[i].Qty + "x  |  " + name;
                                        }

                                        if (panelOrder.Controls[j].Text.Contains("$"))
                                        {
                                            panelOrder.Controls[j].Text = "$ " + amount.ToString("N");
                                        }
                                    }



                                    //Form1_Load(null, null);
                                }
                                else
                                {
                                    FormOrders.partial.RemoveAt(i);
                                    Form1_Load(null, null);
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < listComidas.Count; i++)
                    {
                        if (listComidas[i].IdMenu.ToString() == button.Name)
                        {
                            OrderDetail orderdet = objLINQ.SelectMenuOrderDetails(NewOrder.IdOrder, listComidas[i].IdMenu);

                            if (orderdet == null)
                            {
                                objLINQ.InsertOrderDetail(
                                    new OrderDetail
                                    {
                                        IdOrder = NewOrder.IdOrder,
                                        IdMenu = listComidas[i].IdMenu,
                                        Qty = 1,
                                        Amount = listComidas[i].UnitPrice
                                    });
                                Form1_Load(null, null);
                            }
                            else
                            {
                                objLINQ.UpdateOrderDetail(
                                    new OrderDetail
                                    {
                                        IdOrderDetail = orderdet.IdOrderDetail,
                                        IdOrder = NewOrder.IdOrder,
                                        IdMenu = orderdet.IdMenu,
                                        Qty = orderdet.Qty,
                                        Amount = orderdet.Amount
                                    }, true);

                                listOrderDetails = objLINQ.SelectOrderDetails(NewOrder.IdOrder);

                                double amount = 0;
                                for (int j = 0; j < listOrderDetails.Count; j++)
                                {
                                    int contParcial = 0;
                                    for (int k = 0; k < AuxPartial.Count; k++)
                                    {
                                        if (AuxPartial[k].IdOrderDetail == listOrderDetails[j].IdOrderDetail)
                                            contParcial = AuxPartial[k].Qty;
                                    }

                                    if (listOrderDetails[j].Qty - contParcial > 0)
                                    {
                                        amount += Convert.ToDouble(listOrderDetails[j].Amount);
                                    }
                                }

                                string name = objLINQ.GetMenuName(orderdet.IdMenu);
                                for (int j = 0; j < panelOrder.Controls.Count; j++)
                                {
                                    if (panelOrder.Controls[j].Text.Contains(name))
                                    {
                                        panelOrder.Controls[j].Text = (orderdet.Qty) + "x  |  " + name;
                                    }

                                    if (panelOrder.Controls[j].Text.Contains("$"))
                                    {
                                        panelOrder.Controls[j].Text = "$ " + amount.ToString("N");
                                    }
                                }
                            }



                            //Form1_Load(null, null);
                        }
                    }
                }
            }
        }

        private void menu1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormActiveOrders frm = new FormActiveOrders();
            frm.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FormOrders_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInventory frm = new FormInventory();
            frm.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUsers frm = new FormUsers();
            frm.Show();
        }

        private void editarMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMenu frm = new FormMenu();
            frm.Show();
        }

        private void chefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChefView frm = new FormChefView();
            frm.Show();
        }
    }
}
