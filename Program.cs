using System;

namespace Inventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Item waterBottle = new("Water Bottle", 10, new DateTime(2023, 1, 1));

                Console.WriteLine(waterBottle.GetName());
                Console.WriteLine(waterBottle.GetQuantity());
                Console.WriteLine(waterBottle.GetDate());
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (System.Exception)
            {
                throw;
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
            throw new IndexOutOfRangeException("Quantity can only be positive");
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

