using System;
using Restaurant;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Restaurant.Models {
    public class Menu {
        public static int AmountWaiters = 2;
        public List<MenuItem> Items { get; set; }
        public DateTime LastUpdated { get; set; }

        public Menu(List<MenuItem> items) {
            Items = items;
            LastUpdated = DateTime.Now;
        }

        public void Add(MenuItem item) {
            if (Items.FindIndex(m => m.ItemID == item.ItemID) < 0) {
                Items.Add(item);
                LastUpdated = DateTime.Now;
                Files.WriteToJsonFile<List<MenuItem>>(Program.filePath + "menu.txt", Items, false) ;
            }
        }
        public void GetInfoMenu(int idx, List<OrderItem> OrderItems,out int itemID,
            out string itemName,out double price, out double salaryInDay)
        {
            double summ = 0.0;
            int id=0;
            string name="";
            double priceItem = 0;

            Items.ForEach(delegate (MenuItem bk) {
            if (bk.ItemID == idx)
            {        
                OrderItems.ForEach(delegate (OrderItem bl)
                {
                    if (bl.Ended >= DateTime.Now.AddDays(-1) || bk.ItemName == bl.OrderName) 
                    { summ += bk.Price; }
                });

                summ = Math.Round(summ, 2);
                id = bk.ItemID;
                name = bk.ItemName;
                priceItem = bk.Price;
                    
            }
            });

            itemID = id;
            itemName = name;
            price = priceItem;
            salaryInDay = summ;
        }

        public void GetInfoTables(int i, List<OrderItem> OrderItems, out int amountDishes, 
            out double perc, out double summPrice, out string averageTime)
        {
            int[,] tables = new int[5, 2];
            int amount = 0;
            double summ = 0.0;
            TimeSpan all_time = new TimeSpan(0, 0, 0, 0);
            TimeSpan average_time = new TimeSpan(0, 0, 0, 0);

            OrderItems.ForEach(delegate (OrderItem oi)
            {
                if (oi.Ended >= DateTime.Now.AddDays(-1) && oi.TableID == i)
                {
                    amount++;
                    all_time += oi.Ended.Subtract(oi.Created);
                    Items.ForEach(delegate (MenuItem mi)
                    {
                        if (mi.ItemName == oi.OrderName) { summ += mi.Price; }
                    });
                }
            });

            perc = (all_time.TotalMilliseconds / 36000000) * 100;
            if (amount != 0) average_time = new TimeSpan(all_time.Ticks / amount);
            amountDishes = amount;
            summPrice = summ;
            averageTime = average_time.ToString(@"hh\:mm");
        }
        public void GerInfoWaiters(int i, List<OrderItem> OrderItems,out string Name, out double Tip,out double AverageTips,out double Summ)
        {
                int amountOrders = 0;
                double tips = 0, 
                summ=0;
                string name = "";
                double averageTip=0;

                OrderItems.ForEach(delegate (OrderItem oi) {
                    if (oi.Ended >= DateTime.Now.AddDays(-1) && oi.WaiterID == i) {
                        amountOrders++;
                        tips += oi.Tips;
                        Items.ForEach(delegate (MenuItem mi) {
                            if (mi.ItemName == oi.OrderName) { summ += mi.Price; }
                        });
                    }
                    foreach (Waiter item in Waiter.Objects) { if (item.WaiterID == i) { name = item.Name; }}
                    averageTip = amountOrders != 0 ? averageTip = tips / amountOrders : averageTip = 0;
                });

                AverageTips = averageTip;
                Name = name;
                Tip = tips;
                Summ = summ;
            
        }


        public bool Remove(string itemName) {
            int index = Items.FindIndex(m => m.ItemName.ToLower() == itemName.ToLower());
            if (index >= 0) {
                Items.RemoveAt(index);
                return true;
            }
            return false;
        }

        public string ShowLastUpdate() => $"{LastUpdated.ToString("d")}"; 

        public override string ToString() {
            string text = "";
            foreach (MenuItem item in Items) 
            { 
                text += item + "\n"; 
            }
            return text;
        }
    }
}
