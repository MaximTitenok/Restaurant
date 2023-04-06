using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models {
    public class OrderItem {
        public int OrderID { get; }
        public int ClientID { get; }
        public string OrderName { get; }
        public int TableID { get; }
        public int WaiterID { get; }
        public double Tips { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Ended { get; set; }

        public OrderItem(int OrderID, int ClientID, string OrderName, int TableID, int WaiterID, 
            int Status) {
            this.OrderID = OrderID;
            this.ClientID = ClientID;
            this.OrderName = OrderName;
            this.TableID = TableID;
            this.WaiterID = WaiterID;
            this.Status = Status;
            Created = DateTime.Now;
        }

        public override string ToString() {
            string statusText="";
            switch(Status) {
                case 1:
                    statusText = "ordered";
                    break;
                case 2:
                    statusText = "cooking";
                    break;
                case 3:
                    statusText = "issued";
                    break;
                default:
                    statusText = "not valid";
                    break;
            }
            return $"{statusText}\torder`s ID: {OrderID}\tclient`s ID: {ClientID}\tdish`s name: " +
                $"{OrderName}\ttable`s ID: {TableID}\twaiter`s ID: {WaiterID}\ttipe: {Tips}";
        }
    }
}
