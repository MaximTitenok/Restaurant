using System;
using Restaurant;
using Restaurant.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models {
    public class Orders : IOrders {
        public static int AmountTables = 5;
        public List<OrderItem> Items { get; set; }
        public DateTime Created { get; set; }

        public Orders(List<OrderItem> Items) {
            this.Items = Items;
            Created = DateTime.Now;
        }

        public bool Add(OrderItem item) {
            Items.Add(item);
            Created = DateTime.Now;
            Files.WriteToJsonFile<List<OrderItem>>(Program.filePath + "orders.txt", Items, false);
            return true;
        }

        public void ChangeStatus(int idx, int id, int value = -1) {
            var itemIndex = Items.FindIndex(f => f.OrderID == id);

            switch (idx) {
                case 0: Items[itemIndex].Status = value; break;
                case 1: Items[itemIndex].Ended = DateTime.Now.AddHours(2); break;
                case 2: Items[itemIndex].Tips = value; break;
            }
        }

        public void CountOrder(out int inDay, out int inWeek, out int inMonth, out int inQuart, out int inYear) {

            var amountOrders = Items.FindAll(delegate (OrderItem oi) {
                return oi.Ended >= DateTime.Now.AddDays(-1);
            });

            inDay = amountOrders.Count;
            amountOrders = Items.FindAll( delegate (OrderItem oi) {
                return oi.Ended > DateTime.Now.AddDays(-7);
            });

            inWeek = amountOrders.Count;
            amountOrders = Items.FindAll( delegate (OrderItem oi) {
                return oi.Ended > DateTime.Now.AddMonths(-1);
            });

            inMonth = amountOrders.Count;
            amountOrders = Items.FindAll(delegate (OrderItem oi) {
                return oi.Ended > DateTime.Now.AddMonths(-3);
            });

            inQuart = amountOrders.Count;
            amountOrders = Items.FindAll( delegate (OrderItem oi) {
                return oi.Ended > DateTime.Now.AddYears(-1);
            });

            inYear = amountOrders.Count;
        }

        public void CountTables(int i,out int orders, out string avarageTime) {
            int idx = 0;
            TimeSpan allTime = new TimeSpan(0, 0, 0, 0);
            TimeSpan averageTime = new TimeSpan(0, 0, 0, 0);

            Items.ForEach(delegate (OrderItem oi) {
                if (oi.Ended >= DateTime.Now.AddDays(-1) && oi.TableID == i) {
                    idx++;
                    allTime += oi.Ended.Subtract(oi.Created);
                }
            });

            var results = Items.FindAll(delegate (OrderItem bk) {
                return (bk.Ended >= DateTime.Now.AddDays(-1) && bk.TableID == i );
            });

            if (idx != 0) averageTime = new TimeSpan(allTime.Ticks / idx);
            orders = results.Count;
            avarageTime = averageTime.ToString(@"hh\:mm");
        }

        public string ShowLastUpdate() => $"{Created.ToString("d")}";

        public override string ToString() {
            string theString = "";
            foreach (OrderItem item in Items)
            { 
                theString += item + "\n"; 
            }
            return theString;
        }
    }
}
