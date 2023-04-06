using System;
using Restaurant;
using Restaurant.Interfaces;
using Restaurant.Models;
using System.Collections.Generic;

namespace Restaurant
{
    public class Program
    {
        public static string filePath = 
            @"C:/Users/maxim/source/repos/Restaurant/Restaurant/Restaurant/Files/";

        static public void Main(string[] args)
        {
            List<MenuItem> menuItems = new List<MenuItem>() { };
            Menu menu = new Menu(menuItems);

            List<OrderItem> itemOrder = new List<OrderItem>() { };
            Orders order = new Orders(itemOrder);


            Waiter waiterFirst = new Waiter(1, "Tom", itemOrder);
            Waiter waiterSecond = new Waiter(2, "Jhon", itemOrder);

            Restaurant sushiRestaurant = new Restaurant(menu, order);

            Console.WriteLine(@"
            1. Add a client
            2. Add a order
            3. Show/update the menu
            4. Update the status of the orders
            5. Close a bill
            6. Show the statistic
            7. Show the waiter's work history for a specific check
            8. Show the statistic of the tables
            9. Show the economic statistics
            10. Save the data
            11. Exit
            ");

            bool switchOrNot = true;

            while (switchOrNot == true)
            {
                Console.WriteLine("\nChoose the number: ");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("---New Client (Opening a bill)---");
                        Console.WriteLine(@"
                        1. Add a client
                        2. Show a list of the clients
                        ");
                        switch (Convert.ToInt32(Console.ReadLine()))
                        {
                            case 1:
                                /* AddClient(2, "Jhon"); */
                                Console.WriteLine("Enter the ID and the name: ");
                                string[] clientInfo = Console.ReadLine().Split(", ");
                                sushiRestaurant.AddClient(Convert.ToInt32(clientInfo[0]), Convert.ToString(clientInfo[1]));
                                Console.WriteLine($"The client is added. ID : {Convert.ToInt32(clientInfo[0])}, " +
                                    $"Name: {Convert.ToString(clientInfo[1])}");

                                break;
                            case 2:
                                Console.WriteLine(sushiRestaurant.SearchClients());
                                Console.WriteLine($"Last update: {menu.ShowLastUpdate()}");
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("---New order---");
                        Console.WriteLine(@"
                        1. Add a order
                        2. Show a list of the open orders
                        ");
                        switch (Convert.ToInt32(Console.ReadLine()))
                        {
                            case 1:
                                Console.WriteLine($"Ender order`s ID, client`s ID, dish`s name, table`s number (number of " +
                                    $"tables: {Orders.AmountTables}), waiter`s ID(number of waiters: {Menu.AmountWaiters})");
                                int orderID, clientID, tableID, waiterID;
                                //order.Add(new OrderItem(3, 2, "Steak", 3, 1, 1));
                                string orderName = "";
                                    string[] newOrder = Console.ReadLine().Split(", ");
                                orderID = Convert.ToInt32(newOrder[0]);
                                clientID = Convert.ToInt32(newOrder[1]);
                                orderName = Convert.ToString(newOrder[2]);
                                tableID = Convert.ToInt32(newOrder[3]);
                                waiterID = Convert.ToInt32(newOrder[4]);
                                if (orderID < 1 || clientID < 1 || tableID < 1 || tableID > Orders.AmountTables || 
                                    waiterID < 1 || waiterID > Menu.AmountWaiters) 
                                { 
                                    Console.WriteLine("Error: the data is incorrect"); 
                                    break; 
                                }
                                order.Add(new OrderItem(orderID, clientID, orderName, tableID, waiterID, 1));// order`s status by default is 1(ordered)
                                Console.WriteLine(order);
                                Console.WriteLine($"Last update: {order.ShowLastUpdate()}");
                                break;
                            case 2:
                                Console.WriteLine(order);
                                Console.WriteLine($"Last update: {order.ShowLastUpdate()}");
                                break;
                        }
                        break;
                    case 3:
                        Console.WriteLine("---Update the menu---");
                        Console.WriteLine(@"
                        1. Show the menu
                        2. Add a new dish
                        ");
                        switch (Convert.ToInt32(Console.ReadLine()))
                        {
                            case 1:
                                Console.WriteLine(menu);
                                Console.WriteLine($"LastUpdate: {menu.ShowLastUpdate()}");
                                break;
                            case 2:
                                string name, description;
                                double price;
                                /*menu.Add(new MenuItem(3, "Greek Salad", 13.80, true,
                                "Big cubes of feta, juicy tomatoes, olives, herbs, bell peppers, " +
                                "and cucumber tumble big cubes of feta, juicy tomatoes, olives, herbs, " +
                                "bell peppers, and cucumber tumble"));*/
                                Console.WriteLine("Add the information: dish`s name, price, describe");
                                    string[] newMenuItem = Console.ReadLine().Split(", ");
                                name = Convert.ToString(newMenuItem[0]);
                                price = Convert.ToDouble(newMenuItem[1]);
                                description = Convert.ToString(newMenuItem[2]);
                                menu.Add(new MenuItem(3, name, price, true, description));
                                Console.WriteLine($"\nMenu: \n{menu}");
                                Console.WriteLine($"Last update: {menu.ShowLastUpdate()}");
                                break;
                        }
                        break;
                    case 4:
                        int id, status;
                        Console.WriteLine("---Update order`s status---");
                        Console.WriteLine($"Open orders: \n{order}");
                        Console.WriteLine("Enter order`s ID and status (2 - cooking, 3 - issued):");
                        string[] statusChange = Console.ReadLine().Split(", ");
                        id = Convert.ToInt32(statusChange[0]);
                        status = Convert.ToInt32(statusChange[1]);
                        order.ChangeStatus(0, id, status);
                        break;
                    case 5:
                        Console.WriteLine("---Close the bill---");
                        Console.WriteLine($"Open orders: \n{order}");
                        Console.WriteLine("Enter order`s ID and tips:");
                        string[] input = Console.ReadLine().Split(", ");
                        int idOrder = Convert.ToInt32(input[0]);
                        int tips = Convert.ToInt32(input[1]);
                        if (tips < 0) { Console.WriteLine("Eroor: the data is incorrect"); break; }
                        order.ChangeStatus(1, idOrder, 4);
                        order.ChangeStatus(2, idOrder, tips);
                        break;
                    case 6:
                        Console.WriteLine("---Statistic for a day/week/month/quarter---");
                        order.CountOrder(out int inDay, out int inWeek, out int inMonth, out int inQuart, out int inYear);
                        Console.WriteLine($"day: {inDay} \nweek: {inWeek} \nmonth: {inMonth} \nquarter: {inQuart} \nyear: {inYear} \n");
                        break;
                    case 7:
                        Console.WriteLine("---Waiter`s work history---");
                        sushiRestaurant.GetStatWaiter(waiterFirst, out int WaiterID, out string Name, out int amountOrders, out double tip, out string averageTime);
                        Console.WriteLine($"#{WaiterID} Name: {Name}: processed orders per day: {amountOrders}, tips: {tip}, average waiter visit time: {averageTime}\n");
                        sushiRestaurant.GetStatWaiter(waiterSecond, out WaiterID, out Name, out amountOrders, out tip, out averageTime);
                        Console.WriteLine($"#{WaiterID} Name: {Name}: processed orders per day: {amountOrders}, tips: {tip}, average waiter visit time: {averageTime}\n");
                        break;
                    case 8:
                        Console.WriteLine("---Statistic of the tables---");
                        //Статистика: кілкість сидінь за період та середній час
                        for (int i = 1; i < Orders.AmountTables + 1; i++)
                        {
                            order.CountTables(i,out int orders, out string avarageTime);
                            Console.WriteLine($"Table #{i}: orders per day: {orders}, average table visit time: {avarageTime}\n");

                        }   
                        break;
                    case 9:
                        Console.WriteLine("---Economic statistics---");
                        Console.WriteLine(@"Enter statistic`s number:
                        1. Dishes of menu
                        2. Tables
                        3. Waiters
                        ");
                        int num = Convert.ToInt32(Console.ReadLine());
                        if (num > 0 && num < 4) {
                            switch (num)
                            {
                                case 1:
                                    {
                                        menuItems.ForEach(delegate (MenuItem oi)
                                        {
                                            menu.GetInfoMenu(oi.ItemID, itemOrder, out int itemID, out string itemName, out double price, out double salaryInDay);
                                            Console.WriteLine($"ID: {itemID}, Name: {itemName}, Price: {price}, Revenue per day: {salaryInDay}\n");
                                        });
                                        break;
                                    }
                                case 2:
                                    {
                                        for (int i = 1; i < Orders.AmountTables + 1; i++)
                                        {
                                            menu.GetInfoTables(i, itemOrder, out int amount_dishes, out double perc, out double summ, out averageTime);
                                            Console.WriteLine($"Table #{i}: orders per day {amount_dishes}, traffic: {Math.Round(perc, 2)}%, revenue: {Math.Round(summ, 2)}, average table visit time: {averageTime}\n");
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        for (int i = 1; i < Menu.AmountWaiters + 1; i++)
                                        {

                                            menu.GerInfoWaiters(i, itemOrder,out string name, out tip,out double averageTips,out  double summ);
                                            Console.WriteLine($"#{i} Name: {name}, tips: {tip}, average tips: {averageTips}, revenue: {Math.Round(summ, 2)}\n");
                                        }
                                        break;

                                    }
                                }

                        }
                        else { Console.WriteLine("Error: the data is incorrect"); break; }
                        break;
                    case 10:
                        sushiRestaurant.SaveInfo(0);
                        sushiRestaurant.SaveInfo(1);
                        sushiRestaurant.SaveInfo(2);
                        Console.WriteLine("The data save is successful");
                        break;
                    case 11:
                        switchOrNot = false;
                        break;
                    default:
                        Console.WriteLine("Error: enter number from 1 to 11");
                        break;
                    }   
                }
            }
        }
    }
