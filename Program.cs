using System;
using System.Runtime.CompilerServices;

namespace Inventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Item waterBottle = new("Water Bottle", 10, new DateTime(2023, 1, 1));
                Item waterBottle2 = new("Water Bottle 2", 100, new DateTime(2023, 1, 1));

                Store store1 = new();

                store1.AddItem(waterBottle);
                store1.AddItem(waterBottle2);

                Console.WriteLine(store1.GetItems());
                Console.WriteLine($"========");
                store1.DeleteItem(waterBottle);
                Console.WriteLine($"========");
                Console.WriteLine(store1.GetItems());

                Console.WriteLine($"========");
                Console.WriteLine(store1.GetCurrentVolume());
                Console.WriteLine($"========");

                Console.WriteLine($"{store1.FindItemByName("Water Bottle 2").GetQuantity()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // throw;
            }
        }
    }
}


class Item
{
    private string _name;
    private int _quantity;
    private DateTime _date;


    public Item(string name, int quantity, DateTime? date = null)
    {
        _name = name;
        if (quantity > 0)
        {
            _quantity = quantity;
        }
        else
        {
            throw new Exception("Quantity can only be positive");
        }
        if (date is null)
        {
            _date = DateTime.Now;
        }
        else
        {
            _date = (DateTime)date;
        }

    }

    public string GetName() { return _name; }
    public int GetQuantity() { return _quantity; }
    public DateTime GetDate() { return _date; }
}

class Store
{
    private List<Item> _items = [];

    public void AddItem(Item newItem)
    {
        _items.ForEach(item =>
        {
            if (item.GetName() == newItem.GetName())
            {
                throw new Exception("Item already exists");
            }
        });
        _items.Add(newItem);
        Console.WriteLine($"New item added successfully");
    }

    public void DeleteItem(Item itemToDelete)
    {
        _items.Remove(itemToDelete);
        Console.WriteLine($"Item deleted successfully");

    }

    public string GetItems()
    {
        List<string> itemsString = [];
        _items.ForEach(item => { itemsString.Add(item.GetName()); });
        return String.Join(" - ", itemsString);
    }

    public int GetCurrentVolume()
    {
        int totalAmount = 0;

        _items.ForEach(item =>
        {
            totalAmount += item.GetQuantity();
        });

        return totalAmount;
    }

    public Item FindItemByName(string name)
    {
        Item? itemToFind = _items.Find(item => item.GetName().ToLower() == name.ToLower());

        if (itemToFind is null)
        {
            throw new Exception($"Item {name} does not exists");
        }
        else
        {
            return itemToFind;
        }
    }
}