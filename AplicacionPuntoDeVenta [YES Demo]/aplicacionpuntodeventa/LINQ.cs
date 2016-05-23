using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionPuntoDeVenta
{
    class LINQ
    {
        PescaditoDBEntities entity = new PescaditoDBEntities();
        En_De_Crypt crypt = new En_De_Crypt();

        public List<OrderDetail> SelectOrderDetails(int IdOrder)
        {
            List<OrderDetail> list = new List<OrderDetail>();

            var collection = from a in entity.OrderDetails
                             where a.IdOrder == IdOrder
                             select a;

            foreach (var item in collection)
            {
                list.Add((OrderDetail)item);
            }

            return list;
        }

        public OrderDetail SelectMenuOrderDetailbyId(int IdOrderDetail)
        {
            OrderDetail result = (from a in entity.OrderDetails
                                  where a.IdOrderDetail == IdOrderDetail
                                  select a).FirstOrDefault();

            return result;
        }

        public OrderDetail SelectMenuOrderDetails(int IdOrder, int IdMenu)
        {
            OrderDetail result = (from a in entity.OrderDetails
                                  where a.IdOrder == IdOrder
                                  && a.IdMenu == IdMenu
                                  select a).FirstOrDefault();

            return result;
        }

        public void UpdateOrderDetail(OrderDetail order, bool update)
        {
            OrderDetail result = (from a in entity.OrderDetails
                                  where a.IdOrderDetail == order.IdOrderDetail
                                  select a).FirstOrDefault();

            if (result != null)
            {

                if (update == true)
                {
                    result.Amount = (result.Amount / result.Qty) * (result.Qty + 1);
                    result.Qty = result.Qty + 1;
                    entity.SaveChanges();
                }
                else
                {
                    if (result.Qty - 1 <= 0)
                    {
                        entity.OrderDetails.Remove(result);
                        entity.SaveChanges();
                    }
                    else
                    {
                        result.Amount = (result.Amount / result.Qty) * (result.Qty - 1);
                        result.Qty = result.Qty - 1;
                        entity.SaveChanges();
                    }
                }
                
            }
        }

        public Order SelectOrder(int order)
        {
            Order result = (from a in entity.Orders
                           where a.IdOrder == order
                           select a).FirstOrDefault();

            return result;
        }

        public Order SelectUnsaved()
        {
            Order result = (from a in entity.Orders
                            where a.bActive == null
                            select a).FirstOrDefault();

            return result;
        }

        public List<Order> SelectActiveOrders(int orden)
        {
            List<Order> list = new List<Order>();

            if (orden == 0)
            {
                var collection = from a in entity.Orders
                                 //where a.bActive == true
                                 select a;

                foreach (var item in collection)
                {
                    list.Add((Order)item);
                }
            }
            else
            {
                var collection = from a in entity.Orders
                                 where a.bActive == true && a.OrderNum == orden
                                 select a;

                foreach (var item in collection)
                {
                    list.Add((Order)item);
                }
            }
            
            return list;
        }

        public List<Menu> SelectMenu()
        {
            List<Menu> list = new List<Menu>();

            var collection = from a in entity.Menus
                             select a;

            foreach (var item in collection)
            {
                list.Add((Menu)item);
            }

            return list;
        }

        public List<Menu> SelectMenu(int Tipo)
        {
            List<Menu> list = new List<Menu>();

            var collection = from a in entity.Menus
                             where a.IdTipo == Tipo
                             select a;

            foreach (var item in collection)
            {
                list.Add((Menu)item);
            }

            return list;
        }

        public User SelectUser(string User, string Password)
        {
            User Result = (from a in entity.Users
                           where a.Username == User
                           && a.Password == Password
                           select a).FirstOrDefault();

            //Result = new User
            //{
            //    IdUser = 1,
            //    IdUserType = 1,
            //    LastName = "Valenzuela",
            //    Name = "Josue"
            //};


            return Result;
        }

        public void ValidateUser(string User, string Password, Form frm)
        {
            FormOrders formOrd = new FormOrders();
            FormLogin formLog = new FormLogin();

            User Result = SelectUser(User, crypt.EncryptString(Password));
            if (Result != null)
            {
                frm.Hide();
                formOrd.ShowDialog();
            }
        }

        public int CreateOrder()
        {
            Order order = 
                new Order
                {
                    IdUser = 1,
                    IdPromo = 0,
                    ClientName = "josue",
                    Qty = 0, 
                    FinalAmount = 0,
                    dDateTime = DateTime.Now,
                    OrderNum = GetNextOrderNum()
                };

            entity.Orders.Add(order);
            entity.SaveChanges();

            return order.IdOrder;            
        }

        public int CreatePartialOrder(int IdParent)
        {
            Order order =
                new Order
                {
                    IdUser = 1,
                    IdPromo = 0,
                    ClientName = "josue",
                    Qty = 0,
                    FinalAmount = 0,
                    dDateTime = DateTime.Now,
                    OrderNum = GetNextOrderNum(),
                    IdParent = IdParent,
                    bActive = false
                };

            entity.Orders.Add(order);
            entity.SaveChanges();

            return order.IdOrder;
        }

        public void DeleteItemsFromOrder(int IdOrder)
        {
            List<OrderDetail> list = new List<OrderDetail>();
            var collection = from a in entity.OrderDetails
                             where a.IdOrder == IdOrder
                             select a;

            foreach (var item in collection)
            {
                list.Add((OrderDetail)item);
            }

            for (int i = 0; i < list.Count; i++)
            {
                entity.OrderDetails.Remove(list[i]);
                entity.SaveChanges();
            }
        }

        public int InsertOrderDetail(OrderDetail order)
        {
            entity.OrderDetails.Add(order);
            entity.SaveChanges();

            return order.IdOrderDetail;
        }

        public string GetMenuName(int IdMenu)
        {
            Menu result = (from a in entity.Menus
                           where a.IdMenu == IdMenu
                           select a).FirstOrDefault();

            return result.Description;
        }

        public int GetNextOrderNum()
        {
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = startDate.AddDays(1);

            int result = (from a in entity.Orders
                          where a.dDateTime>= startDate && a.dDateTime<=endDate
                          select a).Count();

            return result + 1;
        }

        public int GetOrderNum(int IdOrden)
        {
            Order result = (from a in entity.Orders
                            where a.IdOrder == IdOrden
                            select a).FirstOrDefault();

            if (result != null)
            {
                return (int)result.OrderNum;
            }
            else return 0;
        }

        public void UpdateOrderStatus(int IdOrder, bool Status)
        {
            Order result = (from a in entity.Orders
                            where a.IdOrder == IdOrder
                            select a).FirstOrDefault();

            if (result != null)
            {
                result.bActive = Status;
                entity.SaveChanges();

            }

        }
    }
}
