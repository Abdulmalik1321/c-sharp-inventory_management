using System;
using System.Runtime.CompilerServices;
using InventoryManagement.src;
using ConsoleTables;





namespace Inventory
{
    internal class Program
    {

        static void Main(string[] args)
        {





            string? GetInput(string msg)
            {
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{msg} or (b) to go back");
                    Console.ResetColor();
                    string? input = Console.ReadLine();
                    Console.WriteLine();

                    if (input is null || input == "")
                    {
                        Console.WriteLine($"please provide a value");
                        continue;
                    }
                    else if (input == "b")
                    {
                        Console.Clear();
                        return null;
                    }
                    return input;
                }
            }


            Store store = new();

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

            store.AddItem(chocolateBar);
            store.AddItem(chipsBag);
            store.AddItem(sodaCan);
            store.AddItem(soap);
            store.AddItem(shampoo);
            store.AddItem(toothbrush);
            store.AddItem(coffee);
            store.AddItem(sandwich);
            store.AddItem(batteries);
            store.AddItem(umbrella);


            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkGray;

            Console.WriteLine(@"
 _____                     _                                  
|_   _|                   | |                                 
  | | _ ____   _____ _ __ | |_ ___  _ __ _   _                
  | || '_ \ \ / / _ \ '_ \| __/ _ \| '__| | | |               
 _| || | | \ V /  __/ | | | || (_) | |  | |_| |               
 \___/_| |_|\_/ \___|_| |_|\__\___/|_|   \__, |               
                                          __/ |               
                                         |___/                
___  ___                                                  _   
|  \/  |                                                 | |  
| .  . | __ _ _ __   __ _  __ _  ___ _ __ ___   ___ _ __ | |_ 
| |\/| |/ _` | '_ \ / _` |/ _` |/ _ \ '_ ` _ \ / _ \ '_ \| __|
| |  | | (_| | | | | (_| | (_| |  __/ | | | | |  __/ | | | |_ 
\_|  |_/\__,_|_| |_|\__,_|\__, |\___|_| |_| |_|\___|_| |_|\__|
                           __/ |                              
                          |___/                               
");
            Console.ResetColor();


            Dictionary<string, string> commands = new Dictionary<string, string>(){
                {"1. GetName", "Get the name of the store"},
                {"2. ChangeName", "change the name of the store"},
                {"3. AddItem", "add a new item"},
                {"4. DeleteItem", "delete an item"},
                {"5. GetItems", "list all the items"},
                {"6. GetCurrentVolume", "Get total quantity of all items"},
                {"7. GetCapacity", "Get the maximum capacity"},
                {"8. ChangeCapacity", "Change the maximum capacity"},
                {"9. FindItemByName", "Find Item By Name"},
                {"10. SortByNameAsc", "Get all the items sorted by name"},
                {"11. collectionSortedByDate", "sort items by date (asc or desc)"},
                {"12. GroupByDate", "Return 2 groups [New Arrival] and [Old] items"},
            };




            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Welcome to [Inventory Management] enter the name of your store");
                Console.ResetColor();
                string? nameInput = Console.ReadLine();
                if (nameInput is null || nameInput == "")
                {
                    Console.WriteLine($"please provide a value");
                    continue;
                }
                else
                {
                    store.ChangeName(nameInput);
                    break;
                }
            }
            while (true)
            {
                try
                {

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nType number of the command or (ls) to list all the commands or (q) to quit");
                    Console.ResetColor();
                    string? input = Console.ReadLine();
                    Console.WriteLine();

                    if (input is null || input == "")
                    {
                        Console.WriteLine($"please provide a value");
                        continue;
                    }
                    else if (input == "q")
                    {
                        break;
                    }

                    switch (input)
                    {
                        case "ls":
                            Console.Clear();
                            ConsoleTable commandsTable = new("Command", "Description");
                            foreach (var kvp in commands)
                            {
                                commandsTable.AddRow(kvp.Key, kvp.Value);
                            }
                            Console.WriteLine(commandsTable);
                            break;

                        case "1":
                            Console.Clear();
                            Console.WriteLine($"Store name is: {store.GetName()}");
                            break;

                        case "2":
                            Console.Clear();
                            string? newName = GetInput("Type the new name of the store");
                            if (newName is not null)
                            {
                                store.ChangeName(newName);
                                Console.WriteLine($"Name Changed successfully");
                            }

                            break;
                        case "3":
                            Console.Clear();
                            string? newItemString = GetInput("Type the name & quantity date of the item {name 10 optional=>(yyyy-mm-dd)}");
                            if (newItemString is not null)
                            {
                                List<string?> newItemSplit = new List<string?>(newItemString.Split(" "));
                                if (newItemSplit.Count < 3)
                                {
                                    newItemSplit.Add(null);
                                }
                                string? itemName = newItemSplit[0];
                                string? itemQty = newItemSplit[1];
                                string? itemDate = newItemSplit[2];

                                if (itemName is not null && itemQty is not null)
                                {
                                    Item newItem = new(itemName, int.Parse(itemQty), itemDate is not null ? DateTime.Parse(itemDate) : null);
                                    store.AddItem(newItem);
                                }
                            }
                            break;
                        case "4":
                            Console.Clear();
                            string? itemToDelete = GetInput("Type the name of the item you want to delete");

                            if (itemToDelete is not null)
                            {
                                store.DeleteItem(store.FindItemByName(itemToDelete));
                            }
                            break;
                        case "5":
                            Console.Clear();
                            ConsoleTable itemsTable = new("Name", "Qty", "Date");
                            store.GetItems().ForEach(item =>
                            {
                                itemsTable.AddRow(item.GetName(), item.GetQuantity(), item.GetDate().ToString("dd/MM/yyyy"));
                            });
                            Console.WriteLine(itemsTable);
                            break;
                        case "6":
                            Console.Clear();
                            Console.WriteLine($"Current volume is: {store.GetCurrentVolume()}");
                            break;
                        case "7":
                            Console.Clear();
                            Console.WriteLine($"Current capacity is: {store.GetCapacity()}");
                            ;
                            break;
                        case "8":
                            Console.Clear();
                            string? newCapacity = GetInput("Type the number of the new capacity");
                            if (newCapacity is not null)
                            {
                                store.ChangeCapacity(int.Parse(newCapacity));
                            }
                            break;
                        case "9":
                            Console.Clear();
                            string? itemToFind = GetInput("Type the name of the item you want to find");
                            if (itemToFind is not null)
                            {
                                Item item = store.FindItemByName(itemToFind);
                                ConsoleTable ItemTable = new("Name", "Qty", "Date");
                                ItemTable.AddRow(item.GetName(), item.GetQuantity(), item.GetDate().ToString("dd/MM/yyyy"));
                                Console.WriteLine(ItemTable);
                            }
                            break;
                        case "10":
                            Console.Clear();
                            ConsoleTable sortedItemsTable = new("Name", "Qty", "Date");
                            store.SortByNameAsc().ForEach(item =>
                            {
                                sortedItemsTable.AddRow(item.GetName(), item.GetQuantity(), item.GetDate().ToString("dd/MM/yyyy"));
                            });
                            Console.WriteLine(sortedItemsTable);

                            break;

                        case "11":
                            Console.Clear();
                            string? sortedItemsOrder = GetInput("please specify sorting order (asc or desc)");
                            if (sortedItemsOrder is not null)
                            {
                                ConsoleTable sortedItemsByDateTable = new("Name", "Qty", "Date");
                                store.collectionSortedByDate(sortedItemsOrder).ForEach(item =>
                                {
                                    sortedItemsByDateTable.AddRow(item.GetName(), item.GetQuantity(), item.GetDate().ToString("dd/MM/yyyy"));
                                });
                                Console.WriteLine(sortedItemsByDateTable);
                            }
                            break;

                        case "12":
                            Console.Clear();

                            Dictionary<string, List<Item>> groupByDate = store.GroupByDate();
                            foreach (var group in groupByDate)
                            {
                                ConsoleTable groupByDateTable = new("Name", "Qty", "Date");
                                Console.WriteLine($"\n{group.Key} Items:");
                                foreach (var item in group.Value)
                                {
                                    groupByDateTable.AddRow(item.GetName(), item.GetQuantity(), item.GetDate().ToString("dd/MM/yyyy"));

                                }
                                Console.WriteLine(groupByDateTable);

                            }

                            break;


                        default:
                            Console.WriteLine($"Invalid input");

                            break;


                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); // throw;
                    continue;
                }
            }

            try
            {
                Item waterBottle1 = new("Water Bottle", 10, new DateTime(2023, 1, 1));
                Item notebook1 = new("Notebook", 500, new DateTime(2023, 3, 1));
                Item pen1 = new("Pen", 200, new DateTime(2023, 4, 1));
                Item tissuePack1 = new("ATissue Pack", 300, new DateTime(2024, 5, 1));
                Item chocolateBar1 = new Item("Chocolate Bar", 15, new DateTime(2023, 2, 1));
                Item chipsBag1 = new Item("Chips Bag", 25, new DateTime(2023, 6, 1));
                Item sodaCan1 = new Item("Soda Can", 8, new DateTime(2023, 7, 1));
                Item soap1 = new Item("Soap", 12, new DateTime(2023, 8, 1));
                Item shampoo1 = new Item("Shampoo", 40, new DateTime(2023, 9, 1));
                Item toothbrush1 = new Item("Toothbrush", 50, new DateTime(2023, 10, 1));
                Item coffee1 = new Item("Coffee", 20);
                Item sandwich1 = new Item("Sandwich", 15);
                Item batteries1 = new Item("Batteries", 10);
                Item umbrella1 = new Item("Umbrella", 5);

                Store store1 = new();


                store1.AddItem(chocolateBar1);
                store1.AddItem(chipsBag1);
                store1.AddItem(sodaCan1);
                store1.AddItem(soap1);
                store1.AddItem(shampoo1);
                store1.AddItem(toothbrush1);
                store1.AddItem(coffee1);
                store1.AddItem(sandwich1);
                store1.AddItem(batteries1);
                store1.AddItem(umbrella1);



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
