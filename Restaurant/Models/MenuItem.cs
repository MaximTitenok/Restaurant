using System;
using Restaurant;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models {
   public class MenuItem: IComparable<MenuItem> {
      public int ItemID { get; set; }
      public string ItemName { get; set; }
      public double Price { get; set; }
      public string Description { get; set; }
      public bool IsNew { get; set; }

      public MenuItem(int ItemID, string ItemName, double Price, bool IsNew, string Description) {
         this.ItemID = ItemID;
         this.ItemName = ItemName;
         this.Price = Price;
         this.IsNew = IsNew;
         this.Description = Description;
       }

        private string checkNew() { return IsNew ? "NEW! " : ""; }

      public int CompareTo(MenuItem compareItem) {
         if (compareItem == null) 
            { 
                return 1; 
            }
         else 
            { 
                return ItemName.CompareTo(compareItem.ItemName); 
            }
      }

      public override string ToString() => $"{checkNew()}\tID: {ItemID}\tItem: {ItemName}\t\t" +
            $"Price: {Price}\t\tDescription: {Description}"; 
   }
}
