using System;
using Restaurant;
using Restaurant.Interfaces;
using Restaurant.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Restaurant
    {
        public Menu menu { get; set; }
        public Orders order { get; set; }
        public Restaurant(Menu menu, Orders order)
        {
            this.menu = menu;
            this.order = order;

            LoadInfo(0); // menu 
            LoadInfo(1); // clients 
            LoadInfo(2); // orders 
        }

        public void SaveInfo(int idx)
        {
            switch (idx)
            {
                case 0:
                    {
                        Files.WriteToJsonFile<List<MenuItem>>(Program.filePath + "menu.txt", menu.Items, false);
                        break;
                    }
                case 1:
                    { 
                        Files.WriteToJsonFile<Dictionary<int, string>>(Program.filePath + "clients.txt", Client.clients, false);
                        break;
                    }
                case 2:
                    { 
                        Files.WriteToJsonFile<List<OrderItem>>(Program.filePath + "orders.txt", order.Items, false);
                        break;
                    }
            }
        }

        void LoadInfo(int idx)
        {
            switch (idx)
            {
                case 0:
                    { 
                        menu.Items.Clear();
                        List<MenuItem> menuList = Files.ReadFromJsonFile<List<MenuItem>>(Program.filePath + "menu.txt");
                        if (menuList == null) break;

                        foreach (MenuItem c in menuList) 
                        { 
                            menu.Add(new MenuItem(c.ItemID, c.ItemName, c.Price, c.IsNew, c.Description)); 
                        }
                        break;
                    }
                case 1:
                    {
                        Client.clients.Clear();
                        Dictionary<int, string> clientList = Files.ReadFromJsonFile<Dictionary<int, string>>
                            (Program.filePath + "clients.txt");
                        if (clientList == null) break;

                        foreach (KeyValuePair<int, string> c in clientList)
                        {
                            AddClient(c.Key, c.Value);
                        }
                        break;
                    }
                case 2:
                    { 
                        order.Items.Clear();
                        List<OrderItem> ordersList = Files.ReadFromJsonFile<List<OrderItem>>(Program.filePath + "orders.txt");
                        if (ordersList == null) break;

                        foreach (OrderItem c in ordersList) 
                        { 
                            order.Add(new OrderItem(c.OrderID, c.ClientID, c.OrderName, c.TableID, c.WaiterID, c.Status)); 
                        }
                        break;
                    }
            }
        }

        public void GetStatWaiter(Waiter waiter,out int id,out string Name,out int amountOrders,out double tips,out string Time)
        {
            TimeSpan allTime = new TimeSpan(0, 0, 0, 0);
            TimeSpan averageTime = new TimeSpan(0, 0, 0, 0);
            int amountOrder = 0;
            double tip = 0;

            waiter.Items.ForEach(delegate (OrderItem oi)
            {
                if (oi.Ended >= DateTime.Now.AddDays(-1) && oi.WaiterID == waiter.WaiterID)
                {
                    amountOrder++;
                    tip += oi.Tips;
                    allTime += oi.Ended.Subtract(oi.Created);
                }
            });
            if (amountOrder != 0) 
            { 
                averageTime = new TimeSpan(allTime.Ticks / amountOrder); 
            }

            id = waiter.WaiterID;
            Name = waiter.Name;
            amountOrders = amountOrder;
            tips = tip;
            Time = averageTime.ToString(@"hh\:mm");
        }

        public void AddClient(int ID, string Name)
        {
            Client.clients.Add(ID, Name);
            Files.WriteToJsonFile<Dictionary<int, string>>(Program.filePath + "clients.txt", Client.clients, false);
        }

        public string SearchClients()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var client in Client.clients) 
            { 
                sb.Append($"ID: {client.Key} Name: {client.Value} \n"); 
            }
            return sb.ToString();
        }
    }
}
