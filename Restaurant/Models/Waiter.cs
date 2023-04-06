using System;
using Restaurant.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models {
    public class Waiter : IWaiter {
        public int WaiterID { get; set; }
        public string Name { get; set; }
        public List<OrderItem> Items { get; set; }
        public static List<Waiter> Objects = new List<Waiter>();

        public Waiter(int WaiterID, string Name, List<OrderItem> Items) {
            this.WaiterID = WaiterID;
            this.Name = Name;
            this.Items = Items;
            Objects.Add(this);
        }
    }
}
