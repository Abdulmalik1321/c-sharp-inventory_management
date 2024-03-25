using System;
using System.Runtime.CompilerServices;
using InventoryManagement.src;

namespace Inventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Item waterBottle = new("Water Bottle", 10, new DateTime(2023, 1, 1));

                Item notebook = new("Notebook", 500, new DateTime(2023, 3, 1));
                Item pen = new("Pen", 200, new DateTime(2023, 4, 1));
                Item tissuePack = new("ATissue Pack", 300, new DateTime(2024, 5, 1));

                Item chocolateBar = new Item("Chocolate Bar", 15, new DateTime(2023, 2, 1));
                Item chipsBag = new Item("Chips Bag", 25, new DateTime(2023, 6, 1));
                Item sodaCan = new Item("Soda Can", 8, new DateTime(2023, 7, 1));
                Item soap = new Item("Soap", 12, new DateTime(2023, 8, 1));
                Item shampoo = new Item("Shampoo", 40, new DateTime(2023, 9, 1));
                Item toothbrush = new Item("Toothbrush", 50, new DateTime(2023, 10, 1));
                Item coffee = new Item("Coffee", 20);
                Item sandwich = new Item("Sandwich", 15);
                Item batteries = new Item("Batteries", 10);
                Item umbrella = new Item("Umbrella", 5);

                Store store1 = new();


                store1.AddItem(chocolateBar);
                store1.AddItem(chipsBag);
                store1.AddItem(sodaCan);
                store1.AddItem(soap);
                store1.AddItem(shampoo);
                store1.AddItem(toothbrush);
                store1.AddItem(coffee);
                store1.AddItem(sandwich);
                store1.AddItem(batteries);
                store1.AddItem(umbrella);



                store1.AddItem(waterBottle);
                store1.AddItem(notebook);
                store1.AddItem(pen);
                Console.WriteLine($"{store1.GetCapacity()}");

                store1.ChangeCapacity(5000);
                Console.WriteLine($"{store1.GetCapacity()}");
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
                Console.WriteLine($"========");

                store1.collectionSortedByDate("asc").ForEach(item =>
                {
                    Console.WriteLine(item.GetDate());

                });
                Console.WriteLine($"========");

                Console.WriteLine($"========");

                store1.collectionSortedByDate("desc").ForEach(item =>
                {
                    Console.WriteLine(item.GetDate());

                });
                Console.WriteLine($"========");

                Dictionary<string, List<Item>> groupByDate = store1.GroupByDate();
                foreach (var group in groupByDate)
                {
                    Console.WriteLine($"{group.Key} Items:");
                    foreach (var item in group.Value)
                    {
                        Console.WriteLine($" - {item.GetName()}, Created: {item.GetDate().ToShortDateString()}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // throw;
            }
        }
    }
}
