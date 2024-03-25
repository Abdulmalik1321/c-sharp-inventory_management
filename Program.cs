using System;
using System.Runtime.CompilerServices;
using inventory_management.src;






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
                    Console.WriteLine($"\n{msg} or (q) to quit");
                    string? input = Console.ReadLine();
                    Console.WriteLine();

                    if (input is null || input == "")
                    {
                        Console.WriteLine($"please provide a value");
                        continue;
                    }
                    else if (input == "q")
                    {
                        Console.Clear();
                        return null;
                    }
                    return input;
                }
            }


            Console.Clear();
            Console.WriteLine(@"
 ___                                       
  |  ._      _  ._ _|_  _  ._              
 _|_ | | \/ (/_ | | |_ (_) | \/            
 |\/|  _. ._   _.  _   _  ._ /   _  ._ _|_ 
 |  | (_| | | (_| (_| (/_ | | | (/_ | | |_ 
                   _|
");

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
            };

            Store store = new();
            while (true)
            {
                Console.WriteLine($"Welcome to [Inventory Management] enter the name of your store");
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

                    Console.WriteLine($"\nType number of the command or (ls) to list all the commands or (q) to quit");
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
                            Console.WriteLine($"\n");
                            Console.WriteLine("|{0,-20}|{1,-35}|", String.Concat(Enumerable.Repeat("-", 20)), String.Concat(Enumerable.Repeat("-", 35)));
                            Console.WriteLine("|{0,-20}|{1,-35}|", "Command", "Description");
                            Console.WriteLine("|{0,-20}|{1,-35}|\n", String.Concat(Enumerable.Repeat("-", 20)), String.Concat(Enumerable.Repeat("-", 35)));
                            Console.WriteLine("|{0,-20}|{1,-35}|", String.Concat(Enumerable.Repeat("-", 20)), String.Concat(Enumerable.Repeat("-", 35)));
                            foreach (var kvp in commands)
                            {
                                Console.WriteLine("|{0,-20}|{1,-35}|", kvp.Key, kvp.Value);
                                // Console.WriteLine("|{0,-20}|{1,-35}|", "-", "-");
                            }
                            Console.WriteLine("|{0,-20}|{1,-35}|", String.Concat(Enumerable.Repeat("-", 20)), String.Concat(Enumerable.Repeat("-", 35)));
                            break;

                        case "1":
                            Console.Clear();
                            Console.WriteLine($"{store.GetName()}");
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
                            string? itemName = GetInput("Type the name of the item");
                            string? itemQty = GetInput("Type the number of the item quantity");
                            string? itemDate = GetInput("Optional (type the date yyyy-mm-dd)");

                            if (itemName is not null && itemQty is not null)
                            {
                                Item newItem = new(itemName, int.Parse(itemQty), itemDate is not null ? DateTime.Parse(itemDate) : null);
                                store.AddItem(newItem);
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
                            store.GetItems().ForEach(item =>
                            {
                                Console.WriteLine($"Name: {item.GetName()}, Quantity: {item.GetQuantity()}, Date: {item.GetDate()}");
                            });
                            break;
                        case "6":
                            Console.Clear();
                            Console.WriteLine(store.GetCurrentVolume());
                            break;
                        case "7":
                            Console.Clear();
                            Console.WriteLine(store.GetCapacity());
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
                                Console.WriteLine($"Name: {item.GetName()}, Quantity: {item.GetQuantity()}, Date: {item.GetDate()}");

                            }
                            break;
                        case "10":
                            Console.Clear();
                            store.SortByNameAsc().ForEach(item =>
                            {
                                Console.WriteLine($"Name: {item.GetName()}, Quantity: {item.GetQuantity()}, Date: {item.GetDate()}");
                            });
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
                Item waterBottle = new("Water Bottle", 10, new DateTime(2023, 1, 1));

                Item notebook = new("Notebook", 500, new DateTime(2023, 3, 1));
                Item pen = new("Pen", 200, new DateTime(2023, 4, 1));
                Item tissuePack = new("ATissue Pack", 300, new DateTime(2023, 5, 1));

                Store store1 = new();

                store1.AddItem(waterBottle);
                store1.AddItem(notebook);
                store1.AddItem(pen);
                Console.WriteLine($"{store1.GetCapacity()}");

                store1.ChangeCapacity(1200);
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // throw;

            }
        }
    }
}
