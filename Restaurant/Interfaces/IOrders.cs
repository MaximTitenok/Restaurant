using System;
using Restaurant;
using Restaurant.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Interfaces {
    public interface IOrders {
        public List<OrderItem> Items { get; set; }
        public DateTime Created { get; set; }
    }
}
