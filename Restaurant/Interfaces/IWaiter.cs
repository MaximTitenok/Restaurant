using System;
using Restaurant.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Interfaces {
    public interface IWaiter {
        public int WaiterID { get; set; }
        public string Name { get; set; }
    }
}
