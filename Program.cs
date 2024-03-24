using System;
using System.Runtime.CompilerServices;
using inventory_management.src;

namespace Inventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Item waterBottle = new("Water Bottle", 10, new DateTime(2023, 1, 1));

                Item notebook = new("Notebook", 5, new DateTime(2023, 3, 1));
                Item pen = new("Pen", 20, new DateTime(2023, 4, 1));
                Item tissuePack = new("ATissue Pack", 30, new DateTime(2023, 5, 1));

                Store store1 = new();

                store1.AddItem(waterBottle);
                store1.AddItem(notebook);
                store1.AddItem(pen);
                store1.AddItem(tissuePack);

                Console.WriteLine(store1.GetItems());
                Console.WriteLine($"========");
                store1.DeleteItem(waterBottle);
                Console.WriteLine($"========");
                Console.WriteLine(store1.GetItems());

                Console.WriteLine($"========");
                Console.WriteLine(store1.GetCurrentVolume());
                Console.WriteLine($"========");

                Console.WriteLine($"========");
                Console.WriteLine($"{store1.FindItemByName("Notebook").GetQuantity()}");
                Console.WriteLine($"========");

                Console.WriteLine($"========");
                store1.SortByNameAsc().ForEach(item =>
                {
                    Console.WriteLine($"{item.GetName()}");

                });
                Console.WriteLine($"========");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // throw;
            }
        }
    }
}
