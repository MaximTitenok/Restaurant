using System;
using Restaurant.Models;
using System.Collections.Generic;

namespace Restaurant.Interfaces {
    public interface IClient {
        public int ClientID { get; set; }
        public string Name { get; set; }
    }
}
